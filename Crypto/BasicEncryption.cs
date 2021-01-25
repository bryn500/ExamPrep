using System;
using System.IO;
using System.Security.Cryptography;

namespace Crypto
{
    class BasicEncryption
    {
        public const int RSAKeyLength = 2048;

        public byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
        {
            byte[] encrypted;

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.GenerateIV();
                aesAlg.GenerateKey();

                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create an encryptor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            // Return the encrypted bytes from the memory stream.
            return encrypted;
        }

        public string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decryptor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return plaintext;
        }

        public byte[] RSAEncrypt(byte[] DataToEncrypt, RSAParameters RSAKeyInfo, bool DoOAEPPadding)
        {
            try
            {
                byte[] encryptedData;

                using (var rsa = RSA.Create(RSAKeyLength))
                {
                    rsa.ImportParameters(RSAKeyInfo);
                    encryptedData = rsa.Encrypt(DataToEncrypt, RSAEncryptionPadding.OaepSHA384);
                }
                
                return encryptedData;

                //using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider(RSAKeyLength))
                //{

                //    //Import the RSA Key information. This only needs to include the public key information.
                //    RSA.ImportParameters(RSAKeyInfo);

                //    //Encrypt the passed byte array and specify OAEP padding.  
                //    encryptedData = RSA.Encrypt(DataToEncrypt, DoOAEPPadding);
                //}
                //return encryptedData;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);

                return null;
            }
        }

        public byte[] RSADecrypt(byte[] DataToDecrypt, RSAParameters RSAKeyInfo, bool DoOAEPPadding)
        {
            try
            {
                byte[] decryptedData;

                using (var rsa = RSA.Create(RSAKeyLength))
                {
                    rsa.ImportParameters(RSAKeyInfo);
                    decryptedData = rsa.Decrypt(DataToDecrypt, RSAEncryptionPadding.OaepSHA384);
                }
                return decryptedData;

                //using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider(RSAKeyLength))
                //{
                //    //Import the RSA Key information. This needs to include the private key information.
                //    RSA.ImportParameters(RSAKeyInfo);

                //    //Decrypt the passed byte array and specify OAEP padding.  
                //    decryptedData = RSA.Decrypt(DataToDecrypt, DoOAEPPadding);
                //}
                //return decryptedData;
            }            
            catch (CryptographicException e)
            {
                Console.WriteLine(e.ToString());

                return null;
            }
        }
    }
}
