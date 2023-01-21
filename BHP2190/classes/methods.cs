using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BHP2190.classes
{
    class methods
    {
        public static string date_now_shamsi()
        {
            DateTime dateTime = DateTime.Now;
            System.Globalization.PersianCalendar persianCalendar = new System.Globalization.PersianCalendar();
            string day_date;
            int i1 = persianCalendar.GetYear(dateTime);
            int i2 = persianCalendar.GetMonth(dateTime);
            int i3 = persianCalendar.GetDayOfMonth(dateTime);
            string year = i1.ToString();
            string month = i2.ToString();
            string day = i3.ToString();
            if (month.Length == 1) month = "0" + month;
            if (day.Length == 1) day = "0" + day;
            day_date = year + "/" + month + "/" + day;
            return day_date;
        }

        public static string date_now_milady()
        {
            DateTime dateTime = DateTime.Now;

            string year = dateTime.Year.ToString();
            string month = dateTime.Month.ToString();
            string day = dateTime.Day.ToString();

            if (month.Length == 1) month = "0" + month;
            if (day.Length == 1) day = "0" + day;
            string day_date = year + "/" + month + "/" + day;

            return day_date;
        }

        public static string convert_date_M_to_S(string date)
        {
            DateTime dateTime = DateTime.Parse(date);
            System.Globalization.PersianCalendar persianCalendar = new System.Globalization.PersianCalendar();
            string day_date;
            int i1 = persianCalendar.GetYear(dateTime);
            int i2 = persianCalendar.GetMonth(dateTime);
            int i3 = persianCalendar.GetDayOfMonth(dateTime);
            string year = i1.ToString();
            string month = i2.ToString();
            string day = i3.ToString();
            if (month.Length == 1) month = "0" + month;
            if (day.Length == 1) day = "0" + day;
            day_date = year + "/" + month + "/" + day;
            return day_date;
        }
        public static string convert_date_S_to_M(string date_shamsi)
        {
            int year = Convert.ToInt16(date_shamsi[0].ToString() + date_shamsi[1].ToString() + date_shamsi[2].ToString() + date_shamsi[3].ToString());
            int month = Convert.ToInt16(date_shamsi[5].ToString() + date_shamsi[6].ToString());
            int day = Convert.ToInt16(date_shamsi[8].ToString() + date_shamsi[9].ToString());

            PersianCalendar pc = new PersianCalendar();
            DateTime dt = new DateTime(year, month, day, pc);
            return dt.ToString("MM/dd/yyyy");
        }
        public static string time()
        {
            DateTime dateTime = DateTime.Now;//زمان 
            string time1 = dateTime.TimeOfDay.ToString();
            dateTime = Convert.ToDateTime(time1);
            string saat, dagheghe, saneye;
            saat = Convert.ToString(dateTime.Hour);
            dagheghe = Convert.ToString(dateTime.Minute);
            saneye = Convert.ToString(dateTime.Second);
            if (dateTime.Hour < 10) saat = "0" + Convert.ToString(dateTime.Hour);
            if (dateTime.Minute < 10) dagheghe = "0" + Convert.ToString(dateTime.Minute);
            if (dateTime.Second < 10) saneye = "0" + Convert.ToString(dateTime.Second);
            string p_time = saat + ":" + dagheghe + ":" + saneye;
            return p_time;
        }
        public static string hour_now()
        {
            DateTime dateTime = DateTime.Now;//زمان 
            string time1 = dateTime.TimeOfDay.ToString();
            dateTime = Convert.ToDateTime(time1);
            string saat = Convert.ToString(dateTime.Hour);
            if (dateTime.Hour < 10) saat = "0" + Convert.ToString(dateTime.Hour);
            string p_time = saat;
            return p_time;
        }

        public static DataTable LINQToDataTable<T>(IEnumerable<T> varlist)
        {
            DataTable dtReturn = new DataTable();
            // column names
            PropertyInfo[] oProps = null;
            if (varlist == null) return dtReturn;
            foreach (T rec in varlist)
            {
                // Use reflection to get property names, to create table, Only first time, others
                //will follow
                if (oProps == null)
                {
                    oProps = ((Type)rec.GetType()).GetProperties();
                    foreach (PropertyInfo pi in oProps)
                    {
                        Type colType = pi.PropertyType;
                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition()
                        == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }
                        dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                    }
                }
                DataRow dr = dtReturn.NewRow();
                foreach (PropertyInfo pi in oProps)
                {
                    dr[pi.Name] = pi.GetValue(rec, null) == null ? DBNull.Value : pi.GetValue
                    (rec, null);
                }

                dtReturn.Rows.Add(dr);
            }
            return dtReturn;
        }

        public static string Encrypt(string toEncrypt, bool useHashing)
        {
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            System.Configuration.AppSettingsReader settingsReader = new AppSettingsReader();
            // Get the key from config file
            string key = "behpajooh_naghdi"; //= (string)settingsReader.GetValue("SecurityKey", typeof(String));
            //System.Windows.Forms.MessageBox.Show(key);
            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                hashmd5.Clear();
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            tdes.Clear();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
        /// <summary>
        /// DeCrypt a string using dual encryption method. Return a DeCrypted clear string
        /// </summary>
        /// <param name="cipherString">encrypted string</param>
        /// <param name="useHashing">Did you use hashing to encrypt this data? pass true is yes</param>
        /// <returns></returns>
        public static string Decrypt(string cipherString, bool useHashing)
        {
            byte[] keyArray;
            byte[] toEncryptArray = Convert.FromBase64String(cipherString);

            System.Configuration.AppSettingsReader settingsReader = new AppSettingsReader();
            //Get your key from config file to open the lock!
            string key = "behpajooh_naghdi"; //(string)settingsReader.GetValue("SecurityKey", typeof(String));

            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                hashmd5.Clear();
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            tdes.Clear();
            return UTF8Encoding.UTF8.GetString(resultArray);
        }

    }
}
