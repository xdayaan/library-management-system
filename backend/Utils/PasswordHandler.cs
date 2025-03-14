using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace LMS.Utils
{
    public class PasswordHandler
    {
        private static readonly byte[] Key = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SECRET_KEY"));

        public static string Encrypt(string text)
        {
            if (string.IsNullOrEmpty(text))
                throw new ArgumentException("Text to encrypt cannot be null or empty.");

            using var aes = Aes.Create();
            aes.Key = Key;
            aes.GenerateIV();
            
            using var encryptor = aes.CreateEncryptor();
            byte[] encryptedBytes = encryptor.TransformFinalBlock(Encoding.UTF8.GetBytes(text), 0, text.Length);
            
            return Convert.ToBase64String(aes.IV) + ":" + Convert.ToBase64String(encryptedBytes);
        }

        public static string Decrypt(string encryptedText)
        {
            if (string.IsNullOrEmpty(encryptedText))
                throw new ArgumentException("Encrypted text cannot be null or empty.");

            var parts = encryptedText.Split(':');
            if (parts.Length != 2)
                throw new FormatException("Invalid encrypted text format. Expected IV:CipherText format.");

            try
            {
                byte[] iv = Convert.FromBase64String(parts[0]);
                byte[] encryptedBytes = Convert.FromBase64String(parts[1]);

                using var aes = Aes.Create();
                aes.Key = Key;
                aes.IV = iv;
                
                using var decryptor = aes.CreateDecryptor();
                byte[] decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
                
                return Encoding.UTF8.GetString(decryptedBytes);
            }
            catch (FormatException ex)
            {
                throw new FormatException("Invalid Base64 string in encrypted text. Ensure correct format and no corruption.", ex);
            }
            catch (CryptographicException ex)
            {
                throw new CryptographicException("Decryption failed. Ensure the key is correct and the data is not altered.", ex);
            }
        }
    }
}