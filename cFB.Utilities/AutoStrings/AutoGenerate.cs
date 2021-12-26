using System;
using System.IO;
using System.Linq;
using System.Threading;

namespace cFB.Utilities.AutoStrings
{
    public static class AutoGenerate
    {
        private static Random random = new Random();
        public static string PostRandomID(string administrativeDivisionId)
        {
            Thread.Sleep(5);
            return DateTime.Now.ToString("ddMMyyyyHHmmss") + RandomString(6) + administrativeDivisionId;
        }

        public static string ReportRandomID(string administrativeDivisionId)
        {
            Thread.Sleep(5);
            return DateTime.Now.ToString("ddMMyyyyHHmmssss") + administrativeDivisionId;
        }
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static string RandomUser(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static string UserRandomID()
        {
            Thread.Sleep(5);
            return "86" + RandomUser(2) + DateTime.Now.ToString("dd");
        }
        public static string WatchListRandomId(string watchListURL, string administrativeDivision_Id)
        {
            Thread.Sleep(5);
            var checkWL = watchListURL.Replace(".", "");
            string watchlist = Path.GetFileNameWithoutExtension(checkWL.TrimEnd('/'));
            return watchlist + administrativeDivision_Id;
        }
    }
}
