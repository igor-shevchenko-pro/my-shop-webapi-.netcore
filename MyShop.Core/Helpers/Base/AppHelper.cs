using System;
using System.Security.Cryptography;
using System.Text;

namespace MyShop.Core.Helpers.Base
{
    public class AppHelper
    {
        private static readonly AppHelper _instance = new AppHelper();
        public static AppHelper Current => _instance;

        private AppHelper()
        {
        }

        public int GetUnixTimeStampJs(DateTime dt)
        {
            return (int)dt.Subtract(new DateTime(1970, 1, 1)).TotalSeconds * 1000;
        }

        public DateTime UnixTimeStampToDateTime(int unixTimeStamp)
        {
            var dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();

            return dateTime;
        }

        public string ToMd5(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] checkSum = md5.ComputeHash(Encoding.UTF8.GetBytes(text));

            return BitConverter.ToString(checkSum).Replace("-", String.Empty);
        }

        public string GetCryptoHash(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("Password is empty");
            }

            var bytes = Encoding.ASCII.GetBytes(value);

            SHA256Managed crypt = new SHA256Managed();
            string hash = string.Empty;
            byte[] crypto = crypt.ComputeHash(bytes, 0, bytes.Length);

            return Convert.ToBase64String(crypto);
        }
    }
}
