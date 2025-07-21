namespace Package.Utilities.Net
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Security.Cryptography;

    /// <summary>
    /// Clase con los metodos extendidos para centralizar funcionalidades puntuales de cada tipo de objeto
    /// </summary>
    public static partial class ExtensionMethods
    {
        /// <summary>
        /// 24 byte or 192 bit key and IV for TripleDES
        /// </summary>
        private static readonly byte[] KEY_Project = {
            143, 43, 52, 186, 129, 55, 60, 194, 153, 108, 25, 9, 252, 155, 66, 219, 142, 184, 26, 144, 254, 188, 162, 125, 18, 36, 48, 127, 178, 243, 52, 170
        };

        /// <summary>
        /// 
        /// </summary>
        private static readonly byte[] IV_Project = {
          40,254,182,20,91,159,92,241,171,86,225,253,129,178,63,3
        };

        /// <summary>
        /// Lets get an encrypted string of <paramref name="texto"/>
        /// to through code TripleDES
        /// </summary>
        /// <param name="texto">Text to be encrypted</param>
        /// <returns>Text encrypted</returns>
        public static string Encriptar(this string texto)
        {
            if (!string.IsNullOrEmpty(texto))
            {
                using var aes = new AesCryptoServiceProvider();

                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(KEY_Project, IV_Project), CryptoStreamMode.Write);
                StreamWriter sw = new StreamWriter(cs);

                sw.Write(texto.Trim());
                sw.Flush();
                cs.FlushFinalBlock();
                ms.Flush();

                return Convert.ToBase64String(ms.GetBuffer(), 0, int.Parse(ms.Length.ToString(CultureInfo.CurrentCulture)));
            }
            else { return null; }
        }

        /// <summary>
        /// Lets get the value Decrypted of <paramref name="texto"/>  to through code TripleDES
        /// </summary>
        /// <param name="texto">Text to be Decrypted</param>
        /// <returns>Text Decrypted</returns>
        public static string DesEncriptar(this string texto)
        {
            if (!string.IsNullOrEmpty(texto))
            {
                using var aes = new AesCryptoServiceProvider();

                byte[] buffer = Convert.FromBase64String(texto.Trim());
                MemoryStream ms = new MemoryStream(buffer);
                CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(KEY_Project, IV_Project), CryptoStreamMode.Read);
                StreamReader sr = new StreamReader(cs);

                return sr.ReadToEnd();
            }
            else { return null; }
        }
    }
}
