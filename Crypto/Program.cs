// https://docs.microsoft.com/en-us/dotnet/standard/security/cryptography-model#choosing-an-algorithm

// Notes on Encryption:
// Encryption - encrypt data so it can't be read by a third party but can be read by an agreed party        
// Scramble the data so it is meaningless, then at a later date, unscramble it

// Symmetric - both parties have the same key used to encrypt/decrypt
// How do you share that key securely initially, particulary over the web...

// Asymmetric - both parties have their own pair of keys
// Public Key - everyone knows this is your public key
// Private Key - this is never sent anywhere
// Two jobs - 1) encrypt/scramble the data 2) Prove who sent the message
// 1). Encrypt:
// Scramble the data using encryption algorithm
// Using the recievers public key so it can only be unencrypted with the recievers private key
// 2). Verify who the message came from: 
// To generate a signature, make a hash from the plaintext, encrypt it with your private key, include it alongside the plaintext.
// To verify a signature, make a hash from the plaintext, decrypt the signature with the sender's public key, check that both hashes are the same.

// HMAC Hash-based Message Authentication Code
// An HMAC can be used to determine whether a message sent over an insecure channel has been tampered with
// Message is sent along with a hash of(message + shared secret) Message:HashOfMessage
// reciever recaulates the hash with shared secret and checks against sent hash

// A certiticate is just a public key with some extra meta data and an authority saying who owns the public key

// Symmetric can be quicker and therefore used after sending key securely via Asymmetric
// this is how ssl works

// Hashing
// One way scramble of the data, cannot be mathematically undone
// Using the same algorithm with the same input will generate the same outcome
// Outcome will be a fixed length no matter the input
// Hashing algorithms are often very fast (md5, sha)
// Can be used to verify that 2 sets of data are exactly the same

// Passwords
// Not good by themselves for passwords! Being fast is bad
// Need to be slow so they can't be brute forced (iterations + size of hash) 
// Need a salt to stop rainbow tables - random string added to password before hashing and stored next to it in field
// Will often be in the form: 10000:salt:hashOfPasswordAndSalt, this can be split
// Can then change the iterations to make slower
// Slowness: needs to be fast for user, slow for attacker doing lots (compromise)
// Attacker will have a faster machine than user
// Current strong hash algorithms for passwords: PBKDF2, Bcrypt, Argon2 (PBKDF2 supported out of box in .net)

// https://www.youtube.com/playlist?list=PLO1Z97_hEI3CZRZZlJ-_vHvdbQdy-3jOP

using System;
using System.Security.Cryptography;

namespace Crypto
{
    public static class Program
    {

        static void Main(string[] args)
        {
            Hashing();
            Encryption();
        }

        private static void Hashing()
        {
            var basicHasher = new BasicHashing();
            var md5 = basicHasher.GetMD5Hash("Password123");
            Console.WriteLine("md5:   {0}", md5);
            var sha256 = basicHasher.GetSha256Hash("Password123");
            Console.WriteLine("sha256:   {0}", sha256);

            var pbkdf2 = basicHasher.BasicPbkdf2Hasher("Password123");
            var hashedPassword = basicHasher.ConvertBytesToHexadecimalString(pbkdf2.GetBytes(32));
            Console.WriteLine("pbkdf2 - simple:   {0}", hashedPassword);
            var hashedCheck = basicHasher.Basicpbkdf2Compare(hashedPassword, "Password123", pbkdf2.Salt, pbkdf2.IterationCount);
            Console.WriteLine("pbkdf2 - simple:   {0}", hashedCheck);

            // https://github.com/dotnet/aspnetcore/blob/master/src/Identity/Extensions.Core/src/PasswordHasher.cs
            var hasher = new NetCoreIdentityHashing();
            var hashed = hasher.HashPassword("Password123");
            Console.WriteLine("pbkdf2 - .net implementation:   {0}", hashed);
            var netResult = hasher.VerifyHashedPassword(hashed, "Password123");
            Console.WriteLine("pbkdf2 - .net implementation:   {0}", netResult);
        }

        private static void Encryption()
        {
            // AES
            string original = "Here is some data to encrypt!";

            // Create a new instance of the Aes class.
            // This generates a new key and initialization
            // vector (IV).
            var encryption = new BasicEncryption();

            byte[] key;
            byte[] IV;

            using (Aes myAes = Aes.Create())
            {
                key = myAes.Key;
                IV = myAes.IV; // initialization vector - random bytes dumped at start so things look different each time you do it
            }

            // Encrypt the string to an array of bytes.
            byte[] encrypted = encryption.EncryptStringToBytes_Aes(original, key, IV);
            // Decrypt the bytes to a string.
            string roundtrip = encryption.DecryptStringFromBytes_Aes(encrypted, key, IV);

            //Display the original data and the decrypted data.
            Console.WriteLine("Original:   {0}", original);
            Console.WriteLine("Round Trip: {0}", roundtrip);
        }
    }
}
