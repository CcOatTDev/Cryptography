using System;
using System.IO;
using System.Security.Cryptography;

namespace SymmetricKeyCryptography
{
    class Program
    {
        // Symmetric Key Cryptography 

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            string plainText = "Hello World!"; //Console.ReadLine().ToString();

            using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider())
            {
                byte[] encrypted = Encrypt(plainText, aes.Key, aes.IV);
                string decrypted = Decrypt(encrypted, aes.Key, aes.IV);

                Console.WriteLine("PlainText: {0}", plainText);
                Console.WriteLine("Decrypted: {0}", decrypted);
            }

            Console.ReadKey();
        }

        private static byte[] Encrypt(string plainText, byte[] key, byte[] iV)
        {
            using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider())
            {
                aes.Key = key;
                aes.IV = iV;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plainText);
                    }

                    return msEncrypt.ToArray();
                }
            }
        }

        private static string Decrypt(byte[] encrypted, byte[] key, byte[] iV)
        {
            using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
            {
                aesAlg.Key = key;
                aesAlg.IV = iV;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(encrypted))
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                {
                    return srDecrypt.ReadToEnd();
                }
            }
        }
    }
}
