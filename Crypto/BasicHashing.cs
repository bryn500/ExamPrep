using System;
using System.Security.Cryptography;
using System.Text;

namespace Crypto
{
    public class BasicHashing
    {
        public string GetSha256Hash(string data)
        {
            string result = "";
            using (SHA256 mySHA256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(data);
                // Compute the hash of the fileStream.
                byte[] hashValue = mySHA256.ComputeHash(bytes);
                // Convert the byte array to hexadecimal string
                result = ConvertBytesToHexadecimalString(hashValue);
            }

            return result;
        }

        public string ConvertBytesToHexadecimalString(byte[] hashValue)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashValue.Length; i++)
            {
                sb.Append(hashValue[i].ToString("X2"));
            }
            return sb.ToString();
        }

        public string GetMD5Hash(string data)
        {
            string result = "";
            using (MD5 mymd5 = MD5.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(data);
                // Compute the hash of the fileStream.
                byte[] hashValue = mymd5.ComputeHash(bytes);
                // Convert the byte array to hexadecimal string
                result = ConvertBytesToHexadecimalString(hashValue);
            }

            return result;
        }

        public Rfc2898DeriveBytes BasicPbkdf2Hasher(string pwd1)
        {
            // Create a byte array to hold the random value.
            byte[] salt1 = new byte[8];
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                // Fill the array with a random value.
                rngCsp.GetBytes(salt1);
            }

            //The default iteration count is 10000 so the two methods use the same iteration count.
            int myIterations = 10000;
            return new Rfc2898DeriveBytes(pwd1, salt1, myIterations, HashAlgorithmName.SHA512);
        }

        public bool Basicpbkdf2Compare(string storedPassword, string pwd1, byte[] salt, int iterations)
        {
            var result = new Rfc2898DeriveBytes(pwd1, salt, iterations, HashAlgorithmName.SHA512);

            return storedPassword == ConvertBytesToHexadecimalString(result.GetBytes(32));
        }
    }
}
