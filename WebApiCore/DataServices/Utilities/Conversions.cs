using DataModels.ViewModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace DataServices
{
    public static class Conversions
    {
        #region DataMapping
        public static List<T> DataReaderMapToList<T>(IDataReader reader)
        {
            var results = new List<T>();

            var columnCount = reader.FieldCount;
            while (reader.Read())
            {
                var item = Activator.CreateInstance<T>();
                try
                {
                    var rdrProperties = Enumerable.Range(0, columnCount).Select(i => reader.GetName(i)).ToArray();
                    foreach (var property in typeof(T).GetProperties())
                    {
                        if ((typeof(T).GetProperty(property.Name).GetGetMethod().IsVirtual) || (!rdrProperties.Contains(property.Name)))
                        {
                            continue;
                        }
                        else
                        {
                            if (!reader.IsDBNull(reader.GetOrdinal(property.Name)))
                            {
                                Type convertTo = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                                property.SetValue(item, Convert.ChangeType(reader[property.Name], convertTo), null);
                            }
                        }
                    }
                    results.Add(item);
                }
                catch (Exception)
                {

                }
            }
            return results;
        }

        public static List<T> ToCollection<T>(this DataTable dt)
        {
            List<T> lst = new System.Collections.Generic.List<T>();
            Type tClass = typeof(T);
            PropertyInfo[] pClass = tClass.GetProperties();
            List<DataColumn> dc = dt.Columns.Cast<DataColumn>().ToList();
            T cn;
            foreach (DataRow item in dt.Rows)
            {
                cn = (T)Activator.CreateInstance(tClass);
                foreach (PropertyInfo pc in pClass)
                {
                    // Can comment try catch block. 
                    try
                    {
                        DataColumn d = dc.Find(c => c.ColumnName == pc.Name);
                        if (d != null)
                            pc.SetValue(cn, item[pc.Name], null);
                    }
                    catch
                    {
                    }
                }
                lst.Add(cn);
            }
            return lst;
        }

        #endregion

        #region Pagination
        public static List<T> SkipTake<T>(List<T> model, vmCmnParameters cmncls)
        {
            List<T> lst = new List<T>();
            int skipnumber = 0;
            if (cmncls.pageNumber > 0)
            {
                skipnumber = ((int)cmncls.pageNumber - 1) * (int)cmncls.pageSize;
            }
            lst = model.Skip(skipnumber).Take((int)cmncls.pageSize).ToList();
            return lst;
        }

        public static int Skip(vmCmnParameters cmncls)
        {
            int skipnumber = 0;
            if (cmncls.pageNumber > 0)
            {
                skipnumber = ((int)cmncls.pageNumber - 1) * (int)cmncls.pageSize;
            }
            return skipnumber;
        }

        #endregion

        #region APIAuth
        public static string GetApiAuthenticationKey(string username, string password)
        {
            username += ":" + password;
            return "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(username));
        }
        

        #endregion

        #region DateTime
        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddMilliseconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        public static DateTime UnixTimeStampToDateTimeMiliSec(double unixTimeStamp)
        {
            string strTime = unixTimeStamp.ToString();
            double dTime = Convert.ToDouble(strTime.Substring(0, strTime.Length - 3));
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(dTime).ToLocalTime();
            return dtDateTime;
        }

        public static double DateTimeToUnixTimestamp(DateTime dateTime)
        {
            return (TimeZoneInfo.ConvertTimeToUtc(dateTime) -
                   new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc)).TotalSeconds;
        }

        public static double DateTimeToUnixTimestampExact(DateTime dateTime)
        {
            return (dateTime -
                   new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc)).TotalSeconds;
        }

        public static double getMinutes(DateTime fromdate)
        {
            return DateTime.Now.Subtract(fromdate).TotalMinutes;
        }

        public static double getHour(DateTime fromdate)
        {
            return DateTime.Now.Subtract(fromdate).TotalHours;
        }

        public static double getDay(DateTime fromdate)
        {
            return DateTime.Now.Subtract(fromdate).TotalDays;
        }

        /// <summary>
        /// UniversalToLocal is a method to convert a universal time to local time. input a parameter as universal time
        /// </summary>
        /// <param name="universalTime"></param>
        /// <returns></returns>
        public static DateTime UniversalToLocal(DateTime universalTime)
        {
            DateTime convertedUtc = Convert.ToDateTime(universalTime).ToUniversalTime();
            return convertedUtc;
        }

        /// <summary>
        /// LocalToUniversal is a method to convert a local time to universal time. input a parameter as local time
        /// </summary>
        /// <param name="localTime"></param>
        /// <returns></returns>
        public static DateTime LocalToUniversal(DateTime localTime)
        {
            DateTime convertedLocal = Convert.ToDateTime(localTime).ToLocalTime();
            return convertedLocal;
        }
        #endregion

        #region Encrypt-Decrypt
        public static string Encryptdata(string inputString)
        {
            string strmsg = string.Empty;
            try
            {
                byte[] encode = new byte[inputString.Length];
                encode = Encoding.UTF8.GetBytes(inputString);
                strmsg = Convert.ToBase64String(encode);
            }
            catch (Exception) { }

            return strmsg;
        }

        public static string Decryptdata(string inputString)
        {
            string decryptpwd = string.Empty;
            try
            {
                UTF8Encoding encodepwd = new UTF8Encoding();
                Decoder Decode = encodepwd.GetDecoder();
                byte[] todecode_byte = Convert.FromBase64String(inputString);
                int charCount = Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
                char[] decoded_char = new char[charCount];
                Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
                decryptpwd = new String(decoded_char);
            }
            catch (Exception) { }
            return decryptpwd;
        }
        
        public static string GetJsonString(string bodys)
        {
            try
            {
                bodys = bodys.Remove(0, 0);
                bodys = bodys.Remove(bodys.Length - 1, 0);
            }
            catch (Exception) { }
            return bodys;
        }
        #endregion
    }
}
