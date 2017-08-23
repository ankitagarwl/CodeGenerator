using EZRide.StateClass;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Reflection;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EZRide.DAL
{
    public static class CommonFunctions
    {

        #region Fields

        private static byte[] key = { };
        private static byte[] IV = { 38, 55, 206, 48, 28, 64, 20, 16 };
        private static string stringKey = "@!#$^3470789";

        #endregion

        #region Public Methods

        public static string Encrypt(string text)
        {
            try
            {
                key = Encoding.UTF8.GetBytes(stringKey.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                Byte[] byteArray = Encoding.UTF8.GetBytes(text);
                MemoryStream memoryStream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream(memoryStream, des.CreateEncryptor(key, IV), CryptoStreamMode.Write);
                cryptoStream.Write(byteArray, 0, byteArray.Length);
                cryptoStream.FlushFinalBlock();
                return Convert.ToBase64String(memoryStream.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return string.Empty;
        }
        public static string Decrypt(string text)
        {
            try
            {
                key = Encoding.UTF8.GetBytes(stringKey.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                Byte[] byteArray = Convert.FromBase64String(text);
                MemoryStream memoryStream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream(memoryStream, des.CreateDecryptor(key, IV), CryptoStreamMode.Write);
                cryptoStream.Write(byteArray, 0, byteArray.Length);
                cryptoStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(memoryStream.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
                // System.Web.HttpContext.Current.Response.Redirect("");
            }
            return string.Empty;
        }
        public static string MD5Encryption(string strVal)
        {

            try
            {
                MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
                byte[] hashBytes;
                UTF8Encoding encoder = new UTF8Encoding();
                hashBytes = md5Hasher.ComputeHash(encoder.GetBytes(strVal));
                String retStr = "";
                foreach (byte b in hashBytes)
                {
                    retStr = retStr + b.ToString("X2");
                }
                //return retStr.ToLower();
                return retStr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string DynamicID()
        {

            try
            {
                String retStr = "";
                retStr = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                return retStr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion

        
        public static string ApplicationPath()
        {
            //string ExportFolder = System.Configuration.ConfigurationManager.AppSettings["Jsonfolder"];
            return string.Concat(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
                                .Split('\\').Reverse().Skip(3).Reverse().Aggregate((a, b) => a + "\\" + b)
                                , "\\", "");
            //get
            //{
            //    return HttpContext.Current.Server.MapPath("~/");
            //}
        }

        public static string killChars(string strWords)
        {
            string newChars;

            string[] badChars = { "select", "drop", "--", "insert", "script", "alert", "union", "alter", "update", "null", "having", "onmouseover", "onclick", "onsubmit", "onblur", "truncate", "=", "delete", "xp_", "'", "&nbsp;" };
            newChars = strWords;

            int i;
            for (i = 0; i < badChars.Length; i++)
            {
                newChars = Regex.Replace(newChars, badChars[i], "");
            }
            return newChars;

        }

        #region Token
        public static int expiryafteraddingseconds(int addseconds)
        {
            var unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var expiry = Math.Round((DateTime.Now.AddSeconds(addseconds) - unixEpoch).TotalSeconds);
            return Convert.ToInt32(expiry);
        }

   
        #endregion

        /// <summary>
        /// Used to get the hash value
        /// </summary>
        /// <param name="PlainText"></param>
        /// <param name="HashSalt"></param>
        /// <returns></returns>
        public static string GetHashedValue(string PlainText)
        {
            string HashSalt = "09ZmA0Y =";

            // Convert plain text into a byte array.
            byte[] plainTextBytes = null;
            plainTextBytes = Encoding.UTF8.GetBytes(PlainText);

            //Convert Hashsalt to byte array
            byte[] saltBytes1 = null;
            saltBytes1 = Encoding.UTF8.GetBytes(HashSalt);

            // Allocate array, which will hold plain text and salt.
            byte[] plainTextWithSaltBytes1 = new byte[plainTextBytes.Length + saltBytes1.Length];

            // Copy plain text bytes into resulting array.
            int I = 0;
            for (I = 0; I <= plainTextBytes.Length - 1; I++)
            {
                plainTextWithSaltBytes1[I] = plainTextBytes[I];
            }

            // Append salt bytes to the resulting array.
            for (I = 0; I <= saltBytes1.Length - 1; I++)
            {
                plainTextWithSaltBytes1[plainTextBytes.Length + I] = saltBytes1[I];
            }

            MD5CryptoServiceProvider Md5 = new MD5CryptoServiceProvider();
            //Compute the hash value from the source
            byte[] ByteHash = Md5.ComputeHash(plainTextWithSaltBytes1);
            //And convert it to String format for return
            return Convert.ToBase64String(ByteHash);

        }

        //public static void SendMailToUser(int clientId, int UserId)
        //{
        // //   UserDetails user = new UserDetails();
        //    string filename = "data";
        //    string mailbody = "ClientId";
        //    mailbody += user.firstname;
        //    mailbody += " " + clientId.ToString();
        //    mailbody = mailbody.Replace("##LastName##", UserId.ToString());

        //    //  mailbody = mailbody.Replace("##Mobile##", txtMobile.Text);
        //    string to = "@gmail.com";
        //    string from = "@gmail.com";
        //    MailMessage message = new MailMessage(from, to);
        //    message.Subject = "Auto Response Email";
        //    message.Body = mailbody;
        //    message.BodyEncoding = Encoding.UTF8;
        //    message.IsBodyHtml = true;
        //    SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
        //    System.Net.NetworkCredential basicCredential = new System.Net.NetworkCredential("@gmail.com", "");
        //    client.EnableSsl = true;
        //    client.UseDefaultCredentials = true;
        //    client.Credentials = basicCredential;
        //    try
        //    {
        //        client.Send(message);

        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

      

        public static string GeneratePassword()
        {
            string PasswordLength = "8";
            string NewPassword = "";

            string allowedChars = "";
            allowedChars = "A,B,X,Y,Z,M,L";
            allowedChars += "c,d,e,f,k,l,u";
            allowedChars += "+,#,$,@,*";


            char[] sep = {
            ','
        };
            string[] arr = allowedChars.Split(sep);


            string IDString = "";
            string temp = "";

            Random rand = new Random();

            for (int i = 0; i < Convert.ToInt32(PasswordLength); i++)
            {
                temp = arr[rand.Next(0, arr.Length)];
                IDString += temp;
                NewPassword = IDString;

            }
            return NewPassword;
        }

     
        #region Send Email
        //public static bool SendMail(string ToEmailId, string Subject, string Body, Attachment AttachFile)
        //{
        //    bool IsMailSend = false;
        //    try
        //    {
        //        System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
        //        //  mail.To.Add(new System.Net.Mail.MailAddress(ConfigurationManager.AppSettings["Email_FromEmail"], ConfigurationManager.AppSettings["Email_FromName"]));
        //        //mail.From = new System.Net.Mail.MailAddress(GetAppSetting("Email_FromEmail"), GetAppSetting("Email_FromName"));

        //        mail.To.Add(ToEmailId);
        //        mail.Subject = Subject;
        //        mail.Body = Body;
        //        mail.IsBodyHtml = true;
        //        mail.From = new MailAddress(ConfigurationSettings.AppSettings["Email_FromEmail"].ToString());
        //        if (AttachFile != null)
        //            mail.Attachments.Add(AttachFile);

        //        System.Net.Mail.SmtpClient SmtpServer = new SmtpClient();
        //        System.Net.Mail.SmtpClient mailclient = new System.Net.Mail.SmtpClient(ConfigurationSettings.AppSettings["Email_HostName"], Convert.ToInt32(ConfigurationSettings.AppSettings["Email_Port"]));
        //        SmtpServer.Host = ConfigurationSettings.AppSettings["Email_HostName"];
        //        SmtpServer.Credentials = new System.Net.NetworkCredential(ConfigurationSettings.AppSettings["Email_FromEmail"], ConfigurationSettings.AppSettings["EMail_Password"]);
        //        SmtpServer.EnableSsl = Convert.ToBoolean(ConfigurationSettings.AppSettings["EnableSSL"]);

        //        SmtpServer.Send(mail);
        //        IsMailSend = true;
        //    }
        //    catch (Exception ex)
        //    {

        //        HttpResponseMessage response = new HttpResponseMessage();
        //        HttpClient client = new HttpClient();
        //        #region exception_handling
        //        response = client.PostAsJsonAsync(ConfigurationSettings.AppSettings["LoggerURL"],
        //                    new LoggerDetails()
        //                    {
        //                        ContollerName = "commmonfunctions",
        //                        InOutType = "",
        //                        guid = "",
        //                        level = "Error",
        //                        message = "API finished",
        //                        ex = ex

        //                    }).Result;
        //        #endregion
        //    }
        //    return IsMailSend;
        //}
        #endregion

     

        public static string Encode64(string st)
        {
            string base64EncodedExternalAccount = Convert.ToBase64String(Encoding.UTF8.GetBytes(st));
            byte[] byteArray = Convert.FromBase64String(base64EncodedExternalAccount);

            return Encoding.UTF8.GetString(byteArray);
        }
        #region ComputerName
        /// <summary>
        /// ComputerName
        /// </summary>
        /// <returns></returns>
        public static string GetCompCode()
        {
            string strHostName = "";
            strHostName = Dns.GetHostName();
            return strHostName;
        }
        #endregion
        #region IPAddress
        /// <summary>
        /// Get IPAddress
        /// </summary>
        /// <returns></returns>
        public static string GetIpAddress()
        {
            string ip = "";
            IPHostEntry ipEntry = Dns.GetHostEntry(GetCompCode());
            IPAddress[] addr = ipEntry.AddressList;
            ip = addr[2].ToString();
            return ip;
        }

        #endregion

        public static DateTime FromUnixTime(long unixTime)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddSeconds(unixTime);
        }



    }
}
