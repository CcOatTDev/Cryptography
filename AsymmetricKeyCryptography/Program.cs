using System;
using System.Security.Cryptography;
using System.Text;

namespace AsymmetricKeyCryptography
{
    class Program
    {
        // Asymmetric Key Cryptography 

        static void Main(string[] args)
        {
            string plainText = "Hello World!";
            UnicodeEncoding byteConverter = new UnicodeEncoding();
            byte[] data = byteConverter.GetBytes(plainText);

            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                RSAParameters publicKey = rsa.ExportParameters(false);
                RSAParameters privateKey = rsa.ExportParameters(true);

                byte[] encrypted = Encrypt(data, publicKey, false);
                byte[] decrypted = Decrypt(encrypted, privateKey, false);

                Console.WriteLine("PlainText: {0}", plainText);
                Console.WriteLine("Decrypted: {0}", byteConverter.GetString(decrypted));
            }

            Console.ReadKey();
        }

        private static byte[] Encrypt(byte[] data, RSAParameters publicKey, bool fOAEP)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.ImportParameters(publicKey);

                return rsa.Encrypt(data, fOAEP);
            }
        }

        private static byte[] Decrypt(byte[] encrypted, RSAParameters privateKey, bool fOAEP)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.ImportParameters(privateKey);

                return rsa.Decrypt(encrypted, fOAEP);
            }
        }
    }
}
