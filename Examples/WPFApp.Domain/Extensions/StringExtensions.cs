using System;
using System.Security.Cryptography;
using System.Text;

namespace WPFApp.Domain.Extensions
{
    public static class StringExtensions
    {
        public static string Sha256(this string randomString)
        {
            var crypt = new SHA256Managed();
            var hash = new StringBuilder();
            byte[] crypto = crypt.ComputeHash(
                Encoding.UTF8.GetBytes(randomString)
            );

            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
        }

        private const string Senha = "MeG4D4V1rA#A";

        public static bool TryCrypt(this string decryptedMessage, out string encryptedMessage)
        {
            encryptedMessage = null;


            byte[] dataToEncrypt;
            byte[] results;
            byte[] tdesKey;
            UTF8Encoding utf8;

            if (string.IsNullOrEmpty(decryptedMessage))
                return false;

            utf8 = new UTF8Encoding();

            try
            {
                using var hashProvider = new MD5CryptoServiceProvider();

                tdesKey = hashProvider.ComputeHash(utf8.GetBytes(Senha));

                using (var tdesAlgorithm = new TripleDESCryptoServiceProvider() { Key = tdesKey, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    dataToEncrypt = utf8.GetBytes(decryptedMessage);
                    var encryptor = tdesAlgorithm.CreateEncryptor();
                    results = encryptor.TransformFinalBlock(dataToEncrypt, 0, dataToEncrypt.Length);
                }

                encryptedMessage = Convert.ToBase64String(results);

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public static bool TryDecrypt(this string encryptedMessage, out string decryptedMessage)
        {
            decryptedMessage = null;

            byte[] dataToDecrypt;
            byte[] results;
            byte[] tdesKey;
            UTF8Encoding objUtf8;

            if (string.IsNullOrEmpty(encryptedMessage))
                return false;

            objUtf8 = new UTF8Encoding();

            try
            {
                using var hashProvider = new MD5CryptoServiceProvider();

                tdesKey = hashProvider.ComputeHash(objUtf8.GetBytes(Senha));

                using var desAlgorithm = new TripleDESCryptoServiceProvider()
                {
                    Key = tdesKey,
                    Mode = CipherMode.ECB,
                    Padding = PaddingMode.PKCS7
                };

                dataToDecrypt = Convert.FromBase64String(encryptedMessage);
                var decryptor = desAlgorithm.CreateDecryptor();
                results = decryptor.TransformFinalBlock(dataToDecrypt, 0, dataToDecrypt.Length);

                decryptedMessage = objUtf8.GetString(results);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
