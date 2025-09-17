
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace DataUtilities
{
    public class Extension
    {
        private static IConfiguration _configuration = null;
        public Extension()
        {

        }

        public Extension(IConfiguration iConfig)
        {
            _configuration = iConfig;
        }

        public static DateTime Today
        {
            get
            {
                DateTime now = DateTime.Now;
                return now;
            }
        }

        public static DateTime UtcToday
        {
            get
            {
                DateTime now = DateTime.UtcNow;
                return now;
            }
        }

        public static DateTime LocalToUtc(DateTime local)
        {
            DateTime now = local.ToUniversalTime();
            return now;
        }

        public static DateTime UtcToLocal(string timeZoneId)
        {
            if (Environment.OSVersion.Platform.ToString() != "Win32NT")
                timeZoneId = StaticInfos.LinuxTimeZoneBD;
            DateTime timeUtc = DateTime.UtcNow;
            DateTime timeLocal = TimeZoneInfo.ConvertTime(timeUtc, TimeZoneInfo.FindSystemTimeZoneById(timeZoneId));
            return timeLocal;
        }

        public static DateTime UtcToLocal(DateTime UserDate,string timeZoneId)
        {
            if (Environment.OSVersion.Platform.ToString() != "Win32NT")
                timeZoneId = StaticInfos.LinuxTimeZoneBD;            
            DateTime timeLocal = TimeZoneInfo.ConvertTime(UserDate, TimeZoneInfo.FindSystemTimeZoneById(timeZoneId));
            return timeLocal;
        }

        public static string GetSubString(string str, int length)
        {
            if (!string.IsNullOrEmpty(str))
            {
                if (str.Length > length)
                {
                    str = str.Substring(0, length) + "..";
                }
            }
            return str;
        }
        
        public static string GetDefaultDateFormat(DateTime? date)
        {
            return date == null ? string.Empty : Convert.ToDateTime(date).ToString("MM/dd/yyyy hh:mm:ss tt");
        }

        public static DateTime UTCDefaultDateFormat(DateTime local)
        {
            DateTime date = local.ToUniversalTime();
            return date = Convert.ToDateTime(date.ToString("MM/dd/yyyy hh:mm:ss tt"));
        }

        public static string UTCDefaultDateFormatString(DateTime? local)
        {
            DateTime? date = local == null ? null : (DateTime?)Convert.ToDateTime(local).ToUniversalTime();
            return date == null ? string.Empty : Convert.ToDateTime(date).ToString("MM/dd/yyyy hh:mm:ss tt");
        }
    }
}
