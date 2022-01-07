using System;
using System.Security.Cryptography;
using System.Text;

namespace cFB.Utilities.Constants
{
    public class ShareContants
    {
        public static int NumberPageVisits = 0;

        public static string RoleOfUser = null;

        public static int PageSizeErro = 0;

        public static string UserAdmin = "Admin";

        public class SentimentLabelName
        {
            public static string Positive = "POS";
            public static string Normal = "NEU";
            public static string Negative = "NEG";
        }
        public static string Checkday(DateTime dateTime, string name)
        {
            var day = Convert.ToInt32(DateTime.Now.Subtract(dateTime).Days);
            var hours = Convert.ToInt32(DateTime.Now.Subtract(dateTime).Hours);
            var minutes = Convert.ToInt32(DateTime.Now.Subtract(dateTime).Minutes);

            if (day != 0)
            {
                return $"{day} ngày trước";
            }
            else if (hours != 0)
            {
                return $"{hours} giờ {minutes} phút trước";
            }
            else if (minutes != 0)
            {
                return $"{minutes} giây trước";
            }
            else
            {
                return name == "history" ? $"Vừa lập" : $"Đang hoạt động";
            }

        }

        public static string MD5(string pPass)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] hashData = md5.ComputeHash(Encoding.UTF8.GetBytes(pPass));
            var sb = new StringBuilder();
            foreach (var hashByte in hashData)
            {
                sb.AppendFormat("{0:x2}", hashByte);
            }
            return sb.ToString();
        }


        public static string SHA1(string pPass)
        {
            SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();
            byte[] hashData = sha1.ComputeHash(Encoding.UTF8.GetBytes(pPass));
            var sb = new StringBuilder();
            foreach (var hashByte in hashData)
            {
                sb.AppendFormat("{0:x2}", hashByte);
            }
            return sb.ToString();
        }

        public static string SHA256(string src)
        {
            using (SHA512 hash = SHA512.Create())
            {
                byte[] hashData = hash.ComputeHash(Encoding.UTF8.GetBytes(src));
                var sb = new StringBuilder();
                foreach (var hashByte in hashData)
                {
                    sb.AppendFormat("{0:x2}", hashByte);
                }
                return sb.ToString();
            }
        }

    }
}
