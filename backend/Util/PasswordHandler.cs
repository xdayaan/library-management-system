using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public class PasswordHandler
{
    private static readonly byte[] Key = Encoding.UTF8.GetBytes("your-256-bit-key-here-should-be-32-chars");

    public static string Encrypt(string text)
    {
        using var aes = Aes.Create();
        aes.Key = Key;
        aes.GenerateIV();
        using var encryptor = aes.CreateEncryptor();
        var encrypted = encryptor.TransformFinalBlock(Encoding.UTF8.GetBytes(text), 0, text.Length);
        return Convert.ToBase64String(aes.IV) + ":" + Convert.ToBase64String(encrypted);
    }

    public static string Decrypt(string encryptedText)
    {
        var parts = encryptedText.Split(':');
        using var aes = Aes.Create();
        aes.Key = Key;
        aes.IV = Convert.FromBase64String(parts[0]);
        using var decryptor = aes.CreateDecryptor();
        var decrypted = decryptor.TransformFinalBlock(Convert.FromBase64String(parts[1]), 0, Convert.FromBase64String(parts[1]).Length);
        return Encoding.UTF8.GetString(decrypted);
    }
}

