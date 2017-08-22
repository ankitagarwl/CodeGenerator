using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Configuration;
using System.Collections;
using System.Data;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System.IO;
using System.Text.RegularExpressions;
using System.Security.Cryptography;

using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ezRide.DAL
{
    public static class HtmlRemoval
    {
        /// <summary>
        /// Remove HTML from string with Regex.
        /// </summary>
        public static string StripTagsRegex(string source)
        {
            return Regex.Replace(source, "<.*?>", string.Empty);
        }

        /// <summary>
        /// Compiled regular expression for performance.
        /// </summary>
        static Regex _htmlRegex = new Regex("&lt;.*?&gt;", RegexOptions.Compiled);
        static Regex _htmlRegex1 = new Regex("<.*?>", RegexOptions.Compiled);
        /// <summary>
        /// Remove HTML from string with compiled Regex.
        /// </summary>
        public static string StripTagsRegexCompiled(string source)
        {
            return _htmlRegex1.Replace(_htmlRegex.Replace(source, string.Empty), string.Empty);
        }

        /// <summary>
        /// Remove HTML tags from string using char array.
        /// </summary>
        public static string StripTagsCharArray(string source)
        {
            char[] array = new char[source.Length];
            int arrayIndex = 0;
            bool inside = false;

            for (int i = 0; i < source.Length; i++)
            {
                char let = source[i];
                if (let == '<')
                {
                    inside = true;
                    continue;
                }
                if (let == '>')
                {
                    inside = false;
                    continue;
                }
                if (!inside)
                {
                    array[arrayIndex] = let;
                    arrayIndex++;
                }
            }
            return new string(array, 0, arrayIndex);
        }
    }
    public class DAL
    {
        public string connstr = ConfigurationManager.AppSettings["ConnString"].ToString();
        private static string PASSWORD_CHARS_LCASE = "abcdefghijklmnopqrstuvwxyz";
        private static string PASSWORD_CHARS_UCASE = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private static string PASSWORD_CHARS_NUMERIC = "123456789";
        private static string PASSWORD_CHARS_SPECIAL = "#*$-+?_&=!%(){}";

        public DAL()
        {
        }

        #region web site related and may not be used

        public DataSet SelectEmailText()
        {
            SqlConnection conn = new SqlConnection(connstr);
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            StringBuilder sb = new StringBuilder();
            SqlDataAdapter oda = new SqlDataAdapter();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                //SqlCommand precmd = new SqlCommand();
                //precmd.Connection = conn;
                //precmd.CommandText = "alter session set current_schema = PMRS_web";
                //precmd.ExecuteNonQuery();

                #region ALLMuncipal
                cmd.CommandText = "DNSelectEmailText";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.Parameters.Add("cur_EmailText", OracleDbType.RefCursor, ParameterDirection.Output);
                da.SelectCommand = cmd;
                da.Fill(ds);
                #endregion
                return ds;

            }
            catch (Exception ex)
            {
                ExceptionLogHelper logindb = new ExceptionLogHelper();
                logindb.Logexception(ex);
                return null;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
                oda.Dispose();
            }

        }

        public DataSet checkpermissions(string CID)
        {
            SqlConnection conn = new SqlConnection(connstr);
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            StringBuilder sb = new StringBuilder();
            SqlDataAdapter oda = new SqlDataAdapter();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                //SqlCommand precmd = new SqlCommand();
                //precmd.Connection = conn;
                //precmd.CommandText = "alter session set current_schema = PMRS_web";
                //precmd.ExecuteNonQuery();

                #region ALLMuncipal
                cmd.CommandText = "DNcheckpermissions";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.Parameters.Add("vUserName", OracleDbType.Varchar2, CID, ParameterDirection.Input);
                cmd.Parameters.Add("cur_LoginDetails", OracleDbType.RefCursor, ParameterDirection.Output);
                da.SelectCommand = cmd;
                da.Fill(ds);
                #endregion
                return ds;

            }
            catch (Exception ex)
            {
                ExceptionLogHelper logindb = new ExceptionLogHelper();
                logindb.Logexception(ex);
                return null;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
                oda.Dispose();
            }

        }

        public DataTable getMunicipalDocumentList(string erkey)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable("MunicipalDocumentList");
            SqlConnection conn = new SqlConnection(connstr);  // C#
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            // StringBuilder sb = new StringBuilder();
            SqlDataAdapter da = new SqlDataAdapter();

            try
            {
                conn.Open();
                //SqlCommand precmd = new SqlCommand();
                //precmd.Connection = conn;
                //precmd.CommandText = "alter session set current_schema = PMRS_CLIENT_53";
                //precmd.ExecuteNonQuery();

                #region Employer status
                cmd.CommandText = "DNGETEMPDOCLIST";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.Parameters.Add("perkey", OracleDbType.Varchar2, erkey, ParameterDirection.Input);
                cmd.Parameters.Add("cur_empcontactinfo", OracleDbType.RefCursor, ParameterDirection.Output);
                da.SelectCommand = cmd;
                da.Fill(dt);
                #endregion

                return dt;
            }
            catch (Exception ex)
            {
                ExceptionLogHelper logindb = new ExceptionLogHelper();
                logindb.Logexception(ex);
                return null;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
                da.Dispose();
            }
        }

        public DataSet showfile()
        {
            SqlConnection conn = new SqlConnection(connstr);
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            StringBuilder sb = new StringBuilder();
            SqlDataAdapter oda = new SqlDataAdapter();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                //SqlCommand precmd = new SqlCommand();
                //precmd.Connection = conn;
                //precmd.CommandText = "alter session set current_schema = PMRS_web";
                //precmd.ExecuteNonQuery();

                #region ALLMuncipal
                cmd.CommandText = "DNShowFile";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.Parameters.Add("cur_LoginDetails", OracleDbType.RefCursor, ParameterDirection.Output);
                da.SelectCommand = cmd;
                da.Fill(ds);
                #endregion
                return ds;

            }
            catch (Exception ex)
            {
                ExceptionLogHelper logindb = new ExceptionLogHelper();
                logindb.Logexception(ex);
                return null;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
                oda.Dispose();
            }

        }

        public DataSet GetFile(string CID)
        {
            SqlConnection conn = new SqlConnection(connstr);
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            StringBuilder sb = new StringBuilder();
            SqlDataAdapter oda = new SqlDataAdapter();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                //SqlCommand precmd = new SqlCommand();
                //precmd.Connection = conn;
                //precmd.CommandText = "alter session set current_schema = PMRS_web";
                //precmd.ExecuteNonQuery();

                #region ALLMuncipal
                cmd.CommandText = "DNGetFile";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.Parameters.Add("vFILEID", OracleDbType.Int32, CID, ParameterDirection.Input);
                cmd.Parameters.Add("cur_LoginDetails", OracleDbType.RefCursor, ParameterDirection.Output);
                da.SelectCommand = cmd;
                da.Fill(ds);
                #endregion
                return ds;

            }
            catch (Exception ex)
            {
                ExceptionLogHelper logindb = new ExceptionLogHelper();
                logindb.Logexception(ex);
                return null;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
                oda.Dispose();
            }

        }

        public DataSet showmuncipal()
        {
            SqlConnection conn = new SqlConnection(connstr);
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            StringBuilder sb = new StringBuilder();
            SqlDataAdapter oda = new SqlDataAdapter();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                //SqlCommand precmd = new SqlCommand();
                //precmd.Connection = conn;
                //precmd.CommandText = "alter session set current_schema = PMRS_web";
                //precmd.ExecuteNonQuery();

                #region ALLMuncipal
                cmd.CommandText = "DNShowMunicipal";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.Parameters.Add("cur_LoginDetails", OracleDbType.RefCursor, ParameterDirection.Output);
                da.SelectCommand = cmd;
                da.Fill(ds, "Tbl1");
                #endregion
                return ds;

            }
            catch (Exception ex)
            {
                ExceptionLogHelper logindb = new ExceptionLogHelper();
                logindb.Logexception(ex);
                return null;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
                oda.Dispose();
            }

        }

        public DataSet activateselecteduser(string CID, string Pwd)
        {
            SqlConnection conn = new SqlConnection(connstr);
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            StringBuilder sb = new StringBuilder();
            SqlDataAdapter oda = new SqlDataAdapter();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                //SqlCommand precmd = new SqlCommand();
                //precmd.Connection = conn;
                //precmd.CommandText = "alter session set current_schema = PMRS_web";
                //precmd.ExecuteNonQuery();

                #region ALLMuncipal
                cmd.CommandText = "DNActivateselecteduser";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.Parameters.Add("vCONTACTID", OracleDbType.NVarchar2, CID, ParameterDirection.Input);
                cmd.Parameters.Add("vCONTACTPASSWORD", OracleDbType.NVarchar2, Pwd, ParameterDirection.Input);
                da.SelectCommand = cmd;
                da.Fill(ds, "Tbl1");
                #endregion
                return ds;

            }
            catch (Exception ex)
            {
                ExceptionLogHelper logindb = new ExceptionLogHelper();
                logindb.Logexception(ex);
                return null;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
                oda.Dispose();
            }

        }

        public DataSet Selectactivate(string CID)
        {
            SqlConnection conn = new SqlConnection(connstr);
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            StringBuilder sb = new StringBuilder();
            SqlDataAdapter oda = new SqlDataAdapter();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                //SqlCommand precmd = new SqlCommand();
                //precmd.Connection = conn;
                //precmd.CommandText = "alter session set current_schema = PMRS_web";
                //precmd.ExecuteNonQuery();

                #region ALLMuncipal
                cmd.CommandText = "DNSelectactivate";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.Parameters.Add("vCONTACTID", OracleDbType.NVarchar2, CID, ParameterDirection.Input);
                cmd.Parameters.Add("cur_LoginDetails", OracleDbType.RefCursor, ParameterDirection.Output);
                da.SelectCommand = cmd;
                da.Fill(ds, "Tbl1");
                #endregion
                return ds;

            }
            catch (Exception ex)
            {
                ExceptionLogHelper logindb = new ExceptionLogHelper();
                logindb.Logexception(ex);
                return null;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
                oda.Dispose();
            }

        }

        public DataSet fetchreportdata(int mID)
        {
            SqlConnection conn = new SqlConnection(connstr);
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            StringBuilder sb = new StringBuilder();
            SqlDataAdapter oda = new SqlDataAdapter();
            DataSet ds = new DataSet();
            try
            {
                //SqlCommand precmd = new SqlCommand();
                //precmd.Connection = conn;
                //precmd.CommandText = "alter session set current_schema = PMRS_CLIENT_53";
                //precmd.ExecuteNonQuery();

                //sb.Remove(0, sb.Length);
                //sb.Append(" SELECT distinct CALC_DATA.CALCID As CALCID,CALC_DATA.TITLE,CALC_DATA.FNAME,CALC_DATA.LNAME,CALC_DATA.BIRTH,CALC_DATA.SEX,CALC_BENEFICIARY.BIRTH AS SDOB , ");
                //sb.Append(" CALC_BENEFICIARY.SEX AS SsEX,CALC.CTYPE,CALC.CDATE,CALC_REPORT.FAE  ");
                //sb.Append(" FROM PMRS_WEB.MCONTACTINFO Inner join CALC on PMRS_WEB.MCONTACTINFO.MEMBERKEY=CALC.MKEY ");
                //sb.Append(" Inner join CALC_BENEFICIARY on CALC.CALCID =CALC_BENEFICIARY.CALCID ");
                //sb.Append(" Inner join CALC_DATA on CALC.CALCID=CALC_DATA.CALCID ");
                //sb.Append(" Inner join CALC_REPORT on CALC.CALCID = CALC_REPORT.CALCID ");
                //sb.Append(" WHERE CALC.MKEY = '" + mID + "' and CALC_DATA.CALCID ='654490' ");

                //cmd.CommandText = sb.ToString();
                //cmd.CommandType = CommandType.Text;
                cmd.CommandText = "DNfetchreportdata";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.Parameters.Add("vMID", OracleDbType.NVarchar2, mID, ParameterDirection.Input);
                cmd.Parameters.Add("cur_LoginDetails", OracleDbType.RefCursor, ParameterDirection.Output);
                oda.SelectCommand = cmd;
                oda.Fill(ds, "Table1");
                //sb.Remove(0, sb.Length);

                return ds;
            }
            catch (Exception ex)
            {
                ExceptionLogHelper logindb = new ExceptionLogHelper();
                logindb.Logexception(ex);
                return null;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
                oda.Dispose();
            }
        }

        public DataSet fillfederal(double txtval)
        {



            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);  // C#
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            SqlDataAdapter da = new SqlDataAdapter();
            //DataTable dt = new DataTable("aaa");              
            try
            {
                conn.Open();
                //SqlCommand precmd = new SqlCommand();
                //precmd.Connection = conn;
                //precmd.CommandText = "alter session set current_schema = PMRS_CLIENT_53";
                //precmd.ExecuteNonQuery();

                #region Employer status
                //cmd.CommandText = "CALFEDERALTAX";
                cmd.CommandText = "DNFillfederal";

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.Parameters.Add("nMonthAmt", OracleDbType.NVarchar2, txtval, ParameterDirection.Input);
                cmd.Parameters.Add("cur_empinfo", OracleDbType.RefCursor, ParameterDirection.Output);
                da.SelectCommand = cmd;
                //da.Fill(dt);
                //#endregion
                //ds.Tables.Add(dt);
                //return ds;
                da.Fill(ds, "Tbl1");
                #endregion
                return ds;

            }
            catch (Exception ex)
            {
                ExceptionLogHelper logindb = new ExceptionLogHelper();
                logindb.Logexception(ex);
                return null;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
                da.Dispose();
            }
        }

        public bool CheckUserExits(string erkey, string emailtxt)
        {
            SqlConnection conn = new SqlConnection(connstr);  // C#
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            StringBuilder sb = new StringBuilder();
            SqlDataAdapter da = new SqlDataAdapter();

            try
            {
                //SqlCommand precmd = new SqlCommand();
                //precmd.Connection = conn;
                //precmd.CommandText = "alter session set current_schema = PMRS_WEB";
                //precmd.ExecuteNonQuery();


                cmd.CommandText = "DNCheckUserExits";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.Parameters.Add("vMEMBERPERCID", OracleDbType.NVarchar2, erkey, ParameterDirection.Input);
                cmd.Parameters.Add("vMEMBEREMAIL", OracleDbType.NVarchar2, emailtxt, ParameterDirection.Input);
                cmd.Parameters.Add("cur_empinfo", OracleDbType.RefCursor, ParameterDirection.Output);
                //da.SelectCommand = cmd;


                //sb.Remove(0, sb.Length);
                //sb.Append("Select count(*) from PMRS_WEB.MCONTACTINFO c");
                //sb.Append(" where c.MEMBERPERCID = '" + erkey + "'");
                //sb.Append(" and LOWER(c.MEMBEREMAIL) = '" + emailtxt + "'");
                //cmd.CommandText = sb.ToString();
                //cmd.CommandType = CommandType.Text;
                int num = Convert.ToInt32(cmd.ExecuteScalar());
                if (num == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                ExceptionLogHelper logindb = new ExceptionLogHelper();
                logindb.Logexception(ex);
                return false;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
                da.Dispose();

            }
        }

        public bool RegisterNewUser(string erkey, string memid, string emailtxt, string fname, string minitial, string lname, string last4ssn, string phone, string ext)
        {
            SqlConnection conn = new SqlConnection(connstr);  // C#
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            StringBuilder sb = new StringBuilder();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                //SqlCommand precmd = new SqlCommand();
                //precmd.Connection = conn;
                //precmd.CommandText = "alter session set current_schema = PMRS_WEB";
                //precmd.ExecuteNonQuery();

                //                sb.Remove(0, sb.Length);
                //                sb.Append("BEGIN ");
                //                sb.Append("DECLARE ");
                //                sb.Append("vCONTACTID NUMBER; ");
                //                sb.Append("BEGIN ");
                //                sb.Append("SELECT CONTACTIDSEQ.NEXTVAL INTO vCONTACTID FROM DUAL; ");
                //                sb.Append("INSERT INTO MCONTACTINFO (MCONTACTID, MEMBERFNAME,MEMBERMINITIAL,MEMBERLNAME,MEMBEREMAIL,MEMBERPERCID,MEMBERKEY,LAST4DIGITOFSSN,MEMBERRESETPWD,MEMBERENABLE,MEMBERPHONE,MEMBEREXTENSION,SYSTEMDATE) ");
                //                sb.Append(" Values ( ");
                //                sb.Append(" vCONTACTID ");
                //                sb.Append(", '" + fname + "' ");
                //                sb.Append(", '" + minitial + "' ");
                //                sb.Append(", '" + lname + "' ");
                //                sb.Append(", '" + emailtxt + "' ");
                //                sb.Append(", '" + erkey + "' ");
                //                sb.Append(", '" + memid + "' ");
                //                sb.Append(", '" + last4ssn + "' ");
                //                sb.Append(", '1' ");
                //                sb.Append(", '0' ");
                //                sb.Append(", '" + phone + "' ");
                //                sb.Append(", '" + ext + "' ");
                //                sb.Append(", to_date(sysdate, 'DD-MON-YYYY HH:MI:SS *') ");
                //                sb.Append("); ");
                //                sb.Append("END;END; ");

                //                string strqry = sb.ToString();

                //                cmd.CommandText = sb.ToString();
                //                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "DNRegisterNewUser";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.Parameters.Add("vMEMBERFNAME", OracleDbType.NVarchar2, fname, ParameterDirection.Input);
                cmd.Parameters.Add("vMEMBERMINITIAL", OracleDbType.NVarchar2, minitial, ParameterDirection.Input);
                cmd.Parameters.Add("vMEMBERLNAME", OracleDbType.NVarchar2, lname, ParameterDirection.Input);
                cmd.Parameters.Add("vMEMBEREMAIL", OracleDbType.NVarchar2, emailtxt, ParameterDirection.Input);
                cmd.Parameters.Add("vMEMBERPERCID", OracleDbType.NVarchar2, erkey, ParameterDirection.Input);
                cmd.Parameters.Add("vMEMBERKEY", OracleDbType.NVarchar2, memid, ParameterDirection.Input);
                cmd.Parameters.Add("vLAST4DIGITOFSSN", OracleDbType.NVarchar2, last4ssn, ParameterDirection.Input);
                cmd.Parameters.Add("vMEMBERPHONE", OracleDbType.NVarchar2, phone, ParameterDirection.Input);
                cmd.Parameters.Add("vMEMBEREXTENSION", OracleDbType.NVarchar2, ext, ParameterDirection.Input);
                cmd.Parameters.Add("cur_LoginDetails", OracleDbType.RefCursor, ParameterDirection.Output);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                ExceptionLogHelper logindb = new ExceptionLogHelper();
                logindb.Logexception(ex);
                return false;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
                da.Dispose();

            }
        }

        public string GetEmployerName(string erkey)
        {
            SqlConnection conn = new SqlConnection(connstr);  // C#
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            StringBuilder sb = new StringBuilder();
            SqlDataAdapter da = new SqlDataAdapter();

            try
            {
                //SqlCommand precmd = new SqlCommand();
                //precmd.Connection = conn;
                //precmd.CommandText = "alter session set current_schema = PMRS_CLIENT_53";
                //precmd.ExecuteNonQuery();

                //sb.Append("select upper(emp.ENAME) from PMRS_CLIENT_53.EMPLOYER emp ");
                //sb.Append(" where emp.ERKEY = '" + erkey + "'");
                //cmd.CommandText = sb.ToString();
                //cmd.CommandType = CommandType.Text;

                cmd.CommandText = "DNGetEmployerName";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.Parameters.Add("vMCONTACTID", OracleDbType.NVarchar2, erkey, ParameterDirection.Input);
                cmd.Parameters.Add("cur_LoginDetails", OracleDbType.RefCursor, ParameterDirection.Output);
                da.SelectCommand = cmd;
                cmd.ExecuteScalar();
                string ename = null;
                ename = cmd.ExecuteScalar().ToString();
                return ename;



            }
            catch (Exception ex)
            {
                ExceptionLogHelper logindb = new ExceptionLogHelper();
                logindb.Logexception(ex);
                return "";
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
                da.Dispose();

            }
        }

        public DataSet GetMemberInfo(string mcid)
        {
            SqlConnection conn = new SqlConnection(connstr);
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            StringBuilder sb = new StringBuilder();
            SqlDataAdapter oda = new SqlDataAdapter();
            DataSet ds = new DataSet();
            try
            {
                //SqlCommand precmd = new SqlCommand();
                //precmd.Connection = conn;
                //precmd.CommandText = "alter session set current_schema = PMRS_WEB";
                //precmd.ExecuteNonQuery();

                cmd.CommandText = "DNGetMemberInfo";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.Parameters.Add("vMCONTACTID", OracleDbType.NVarchar2, mcid, ParameterDirection.Input);
                cmd.Parameters.Add("cur_LoginDetails", OracleDbType.RefCursor, ParameterDirection.Output);
                oda.SelectCommand = cmd;
                oda.Fill(ds);




            }
            catch (Exception ex)
            {
                ExceptionLogHelper logindb = new ExceptionLogHelper();
                logindb.Logexception(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
                ds.Dispose();

            }
            return ds;
        }

        public DataSet SelectEmployerInfo(string Sdate)
        {
            SqlConnection conn = new SqlConnection(connstr);
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            SqlDataAdapter oda = new SqlDataAdapter();
            DataSet ds = new DataSet();
            try
            {
                //SqlCommand precmd = new SqlCommand();
                //precmd.Connection = conn;
                //precmd.CommandText = "alter session set current_schema = PMRS_Client_53";
                //precmd.ExecuteNonQuery();

                cmd.CommandText = "DNSelectCalEmployerInfo";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.Parameters.Add("vSdate", OracleDbType.NVarchar2, Sdate, ParameterDirection.Input);
                cmd.Parameters.Add("cur_CALC", OracleDbType.RefCursor, ParameterDirection.Output);
                oda.SelectCommand = cmd;
                oda.Fill(ds);
            }
            catch (Exception ex)
            {
                ExceptionLogHelper logindb = new ExceptionLogHelper();
                logindb.Logexception(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
                ds.Dispose();

            }
            return ds;
        }

        public string PWGenerate(int minLength, int maxLength)
        {
            // Make sure that input parameters are valid.
            if (minLength <= 0 || maxLength <= 0 || minLength > maxLength)
                return null;

            // Create a local array containing supported password characters
            // grouped by types. You can remove character groups from this
            // array, but doing so will weaken the password strength.
            char[][] charGroups = new char[][] 
            {
                PASSWORD_CHARS_LCASE.ToCharArray(),
                PASSWORD_CHARS_UCASE.ToCharArray(),
                PASSWORD_CHARS_NUMERIC.ToCharArray(),
                PASSWORD_CHARS_SPECIAL.ToCharArray()
            };

            // Use this array to track the number of unused characters in each character group.
            int[] charsLeftInGroup = new int[charGroups.Length];

            // Initially, all characters in each group are not used.
            for (int i = 0; i < charsLeftInGroup.Length; i++)
                charsLeftInGroup[i] = charGroups[i].Length;

            // Use this array to track (iterate through) unused character groups.
            int[] leftGroupsOrder = new int[charGroups.Length];

            // Initially, all character groups are not used.
            for (int i = 0; i < leftGroupsOrder.Length; i++)
                leftGroupsOrder[i] = i;
            /*
            // Because we cannot use the default randomizer, which is based on the
            // current time (it will produce the same "random" number within a
            // second), we will use a random number generator to seed the
            // randomizer.

            // Use a 4-byte array to fill it with random bytes and convert it then
            // to an integer value.
            byte[] randomBytes = new byte[4];

            // Generate 4 random bytes.
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(randomBytes);

            // Convert 4 bytes into a 32-bit integer value.
            int seed = (randomBytes[0] & 0x7f) << 24 |
                        randomBytes[1] << 16 |
                        randomBytes[2] << 8 |
                        randomBytes[3];
            */
            // Now, this is real randomization.
            Random random = new Random();

            // This array will hold password characters.
            char[] password = null;

            // Allocate appropriate memory for the password.
            if (minLength < maxLength)
                password = new char[random.Next(minLength, maxLength + 1)];
            else
                password = new char[minLength];

            // Index of the next character to be added to password.
            int nextCharIdx;

            // Index of the next character group to be processed.
            int nextGroupIdx;

            // Index which will be used to track not processed character groups.
            int nextLeftGroupsOrderIdx;

            // Index of the last non-processed character in a group.
            int lastCharIdx;

            // Index of the last non-processed group.
            int lastLeftGroupsOrderIdx = leftGroupsOrder.Length - 1;

            // Generate password characters one at a time.
            for (int i = 0; i < password.Length; i++)
            {
                // If only one character group remained unprocessed, process it;
                // otherwise, pick a random character group from the unprocessed
                // group list. To allow a special character to appear in the
                // first position, increment the second parameter of the Next
                // function call by one, i.e. lastLeftGroupsOrderIdx + 1.
                if (lastLeftGroupsOrderIdx == 0)
                    nextLeftGroupsOrderIdx = 0;
                else
                    nextLeftGroupsOrderIdx = random.Next(0, lastLeftGroupsOrderIdx);

                // Get the actual index of the character group, from which we will
                // pick the next character.
                nextGroupIdx = leftGroupsOrder[nextLeftGroupsOrderIdx];

                // Get the index of the last unprocessed characters in this group.
                lastCharIdx = charsLeftInGroup[nextGroupIdx] - 1;

                // If only one unprocessed character is left, pick it; otherwise,
                // get a random character from the unused character list.
                if (lastCharIdx == 0)
                    nextCharIdx = 0;
                else
                    nextCharIdx = random.Next(0, lastCharIdx + 1);

                // Add this character to the password.
                password[i] = charGroups[nextGroupIdx][nextCharIdx];

                // If we processed the last character in this group, start over.
                if (lastCharIdx == 0)
                    charsLeftInGroup[nextGroupIdx] =
                                              charGroups[nextGroupIdx].Length;
                // There are more unprocessed characters left.
                else
                {
                    // Swap processed character with the last unprocessed character
                    // so that we don't pick it until we process all characters in
                    // this group.
                    if (lastCharIdx != nextCharIdx)
                    {
                        char temp = charGroups[nextGroupIdx][lastCharIdx];
                        charGroups[nextGroupIdx][lastCharIdx] =
                                    charGroups[nextGroupIdx][nextCharIdx];
                        charGroups[nextGroupIdx][nextCharIdx] = temp;
                    }
                    // Decrement the number of unprocessed characters in
                    // this group.
                    charsLeftInGroup[nextGroupIdx]--;
                }

                // If we processed the last group, start all over.
                if (lastLeftGroupsOrderIdx == 0)
                    lastLeftGroupsOrderIdx = leftGroupsOrder.Length - 1;
                // There are more unprocessed groups left.
                else
                {
                    // Swap processed group with the last unprocessed group
                    // so that we don't pick it until we process all groups.
                    if (lastLeftGroupsOrderIdx != nextLeftGroupsOrderIdx)
                    {
                        int temp = leftGroupsOrder[lastLeftGroupsOrderIdx];
                        leftGroupsOrder[lastLeftGroupsOrderIdx] =
                                    leftGroupsOrder[nextLeftGroupsOrderIdx];
                        leftGroupsOrder[nextLeftGroupsOrderIdx] = temp;
                    }
                    // Decrement the number of unprocessed groups.
                    lastLeftGroupsOrderIdx--;
                }
            }

            // Convert password characters into a string and return the result.


            return new string(password);
        }

        public DataSet SelectCalcData()
        {
            SqlConnection conn = new SqlConnection(connstr);
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            SqlDataAdapter oda = new SqlDataAdapter();
            DataSet ds = new DataSet();

            try
            {
                //SqlCommand precmd = new SqlCommand();
                //precmd.Connection = conn;
                //precmd.CommandText = "alter session set current_schema = PMRS_Client_53";
                //precmd.ExecuteNonQuery();

                cmd.CommandText = "DNSelectCalcData";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.Parameters.Add("cur_CALC", OracleDbType.RefCursor, ParameterDirection.Output);
                oda.SelectCommand = cmd;
                oda.Fill(ds);
            }
            catch (Exception ex)
            {
                ExceptionLogHelper logindb = new ExceptionLogHelper();
                logindb.Logexception(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
                ds.Dispose();

            }
            return ds;
        }

        public DataSet SelectCalcInfo()
        {
            SqlConnection conn = new SqlConnection(connstr);
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            SqlDataAdapter oda = new SqlDataAdapter();
            DataSet ds = new DataSet();

            try
            {
                //SqlCommand precmd = new SqlCommand();
                //precmd.Connection = conn;
                //precmd.CommandText = "alter session set current_schema = PMRS_Client_53";
                //precmd.ExecuteNonQuery();

                cmd.CommandText = "DNSelectCalcInfo";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;

                cmd.Parameters.Add("cur_CALC", OracleDbType.RefCursor, ParameterDirection.Output);
                oda.SelectCommand = cmd;
                oda.Fill(ds);
            }
            catch (Exception ex)
            {
                ExceptionLogHelper logindb = new ExceptionLogHelper();
                logindb.Logexception(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
                ds.Dispose();

            }
            return ds;
        }

        public DataSet SelectCalcInfo1(string mkey)
        {
            SqlConnection conn = new SqlConnection(connstr);
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            SqlDataAdapter oda = new SqlDataAdapter();
            DataSet ds = new DataSet();
            try
            {
                //SqlCommand precmd = new SqlCommand();
                //precmd.Connection = conn;
                //precmd.CommandText = "alter session set current_schema = PMRS_Client_53";
                //precmd.ExecuteNonQuery();

                cmd.CommandText = "DNSelectCalcInfo1";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.Parameters.Add("mkey", OracleDbType.NVarchar2, mkey, ParameterDirection.Input);
                cmd.Parameters.Add("cur_CALC", OracleDbType.RefCursor, ParameterDirection.Output);
                oda.SelectCommand = cmd;
                oda.Fill(ds);
            }
            catch (Exception ex)
            {
                ExceptionLogHelper logindb = new ExceptionLogHelper();
                logindb.Logexception(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
                ds.Dispose();

            }
            return ds;
        }

        public DataSet SelectCalcInfo2(string caps)
        {
            SqlConnection conn = new SqlConnection(connstr);
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            SqlDataAdapter oda = new SqlDataAdapter();
            DataSet ds = new DataSet();

            try
            {
                //SqlCommand precmd = new SqlCommand();
                //precmd.Connection = conn;
                //precmd.CommandText = "alter session set current_schema = PMRS_Client_53";
                //precmd.ExecuteNonQuery();

                cmd.CommandText = "DNSelectCalcInfo2";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.Parameters.Add("caps_ctype", OracleDbType.NVarchar2, caps, ParameterDirection.Input);
                cmd.Parameters.Add("cur_CALC", OracleDbType.RefCursor, ParameterDirection.Output);
                oda.SelectCommand = cmd;
                oda.Fill(ds);
            }
            catch (Exception ex)
            {
                ExceptionLogHelper logindb = new ExceptionLogHelper();
                logindb.Logexception(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
                ds.Dispose();

            }
            return ds;
        }

        public DataSet SelectCalcInfo3(string calcid)
        {
            SqlConnection conn = new SqlConnection(connstr);
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            SqlDataAdapter oda = new SqlDataAdapter();
            DataSet ds = new DataSet();

            try
            {
                //SqlCommand precmd = new SqlCommand();
                //precmd.Connection = conn;
                //precmd.CommandText = "alter session set current_schema = PMRS_Client_53";
                //precmd.ExecuteNonQuery();

                cmd.CommandText = "DNSelectCalcInfo3";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.Parameters.Add("calc", OracleDbType.NVarchar2, calcid, ParameterDirection.Input);
                cmd.Parameters.Add("cur_CALC", OracleDbType.RefCursor, ParameterDirection.Output);
                oda.SelectCommand = cmd;
                oda.Fill(ds);
            }
            catch (Exception ex)
            {
                ExceptionLogHelper logindb = new ExceptionLogHelper();
                logindb.Logexception(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
                ds.Dispose();

            }
            return ds;
        }

        public DataSet SelectCalEmployerInfo(string Sdate)
        {
            SqlConnection conn = new SqlConnection(connstr);
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            SqlDataAdapter oda = new SqlDataAdapter();
            DataSet ds = new DataSet();

            try
            {
                //SqlCommand precmd = new SqlCommand();
                //precmd.Connection = conn;
                //precmd.CommandText = "alter session set current_schema = PMRS_Client_53";
                //precmd.ExecuteNonQuery();

                cmd.CommandText = "DNSelectCalEmployerInfo";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.Parameters.Add("vSdate", OracleDbType.NVarchar2, Sdate, ParameterDirection.Input);
                cmd.Parameters.Add("cur_CALC", OracleDbType.RefCursor, ParameterDirection.Output);
                oda.SelectCommand = cmd;
                oda.Fill(ds);
            }
            catch (Exception ex)
            {
                ExceptionLogHelper logindb = new ExceptionLogHelper();
                logindb.Logexception(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
                ds.Dispose();

            }
            return ds;
        }

        public DataSet ActivateSelectedMember(string MCONTACTID, string MEMBERORIGINALPWD, string MEMBERPASSWORD)
        {
            SqlConnection conn = new SqlConnection(connstr);
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            SqlDataAdapter oda = new SqlDataAdapter();
            DataSet ds = new DataSet();

            try
            {
                //SqlCommand precmd = new SqlCommand();
                //precmd.Connection = conn;
                //precmd.CommandText = "alter session set current_schema = PMRS_Client_53";
                //precmd.ExecuteNonQuery();

                cmd.CommandText = "DNActivateSelectedMember";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.Parameters.Add("vMCONTACTID", OracleDbType.NVarchar2, MCONTACTID, ParameterDirection.Input);
                cmd.Parameters.Add("vMEMBERORIGINALPWD", OracleDbType.NVarchar2, MEMBERORIGINALPWD, ParameterDirection.Input);
                cmd.Parameters.Add("vMEMBERPASSWORD", OracleDbType.NVarchar2, MEMBERPASSWORD, ParameterDirection.Input);
                cmd.Parameters.Add("cur_CALC", OracleDbType.RefCursor, ParameterDirection.Output);
                oda.SelectCommand = cmd;
                oda.Fill(ds);
            }
            catch (Exception ex)
            {
                ExceptionLogHelper logindb = new ExceptionLogHelper();
                logindb.Logexception(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
                ds.Dispose();

            }
            return ds;
        }

        public DataSet ShowNewMbr()
        {
            SqlConnection conn = new SqlConnection(connstr);
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            SqlDataAdapter oda = new SqlDataAdapter();
            DataSet ds = new DataSet();

            try
            {
                //SqlCommand precmd = new SqlCommand();
                //precmd.Connection = conn;
                //precmd.CommandText = "alter session set current_schema = PMRS_Client_53";
                //precmd.ExecuteNonQuery();

                cmd.CommandText = "DNShowNewMbr";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.Parameters.Add("cur_CALC", OracleDbType.RefCursor, ParameterDirection.Output);
                oda.SelectCommand = cmd;
                oda.Fill(ds);
            }
            catch (Exception ex)
            {
                ExceptionLogHelper logindb = new ExceptionLogHelper();
                logindb.Logexception(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
                ds.Dispose();

            }
            return ds;
        }

        public DataSet ShwRprntUsr()
        {
            SqlConnection conn = new SqlConnection(connstr);
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            SqlDataAdapter oda = new SqlDataAdapter();
            DataSet ds = new DataSet();

            try
            {
                //SqlCommand precmd = new SqlCommand();
                //precmd.Connection = conn;
                //precmd.CommandText = "alter session set current_schema = PMRS_Client_53";
                //precmd.ExecuteNonQuery();

                cmd.CommandText = "DNShwRprntUsr";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.Parameters.Add("cur_CALC", OracleDbType.RefCursor, ParameterDirection.Output);
                oda.SelectCommand = cmd;
                oda.Fill(ds);
            }
            catch (Exception ex)
            {
                ExceptionLogHelper logindb = new ExceptionLogHelper();
                logindb.Logexception(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
                ds.Dispose();

            }
            return ds;
        }

        #region memeber service tab
        public DataSet CreateReportInfo(string MERKEY, string Sdate)
        {
            SqlConnection conn = new SqlConnection(connstr);
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            SqlDataAdapter oda = new SqlDataAdapter();
            DataSet ds = new DataSet();

            try
            {
                //SqlCommand precmd = new SqlCommand();
                //precmd.Connection = conn;
                //precmd.CommandText = "alter session set current_schema = PMRS_Client_53";
                //precmd.ExecuteNonQuery();

                //cmd.CommandText = "DNCreateReportInfo";
                //cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Connection = conn;

                //cmd.Parameters.Add("vSdate", OracleDbType.Varchar2, Sdate, ParameterDirection.Input);
                //cmd.Parameters.Add("vMERKEY", OracleDbType.NVarchar2, MERKEY, ParameterDirection.Input);
                //cmd.Parameters.Add("cur_CALC", OracleDbType.RefCursor, ParameterDirection.Output);

                string strSQL = "select calc.calcid as CALCID, calc.mkey as MKEY, calc.cdate as CDATE, member.erkey as ERKEY from PMRS_CLIENT_53.calc, PMRS_CLIENT_53.calc_data, PMRS_CLIENT_53.member "
                        + "where calc_data.calcid=calc.calcid and member.mkey = calc.mkey "
                        + "and calc.stage='ZZ' and calc.ctype='YC' and calc_data.pstatus in ('AC','DP') "
                        + "and calc.mkey in (select member.mkey from PMRS_CLIENT_53.member where member.erkey in (" + MERKEY + "))"
                        + " AND CALC.CDATE = '" + Sdate + "'";

                cmd.Connection = conn;
                cmd.CommandText = strSQL;
                cmd.CommandType = CommandType.Text;

                oda.SelectCommand = cmd;
                oda.Fill(ds);
            }
            catch (Exception ex)
            {
                ExceptionLogHelper logindb = new ExceptionLogHelper();
                logindb.Logexception(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
                ds.Dispose();

            }
            return ds;
        }
        public DataSet CreatReportsmem(string Sdate, string MKEY)
        {
            SqlConnection conn = new SqlConnection(connstr);
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            SqlDataAdapter oda = new SqlDataAdapter();
            DataSet ds = new DataSet();

            try
            {
                //cmd.CommandText = "DNCreatReportsmem";
                //cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Connection = conn;

                //cmd.Parameters.Add("vSdate", OracleDbType.Varchar2, Sdate, ParameterDirection.Input);
                //cmd.Parameters.Add("vMKEY", OracleDbType.Varchar2, MKEY, ParameterDirection.Input);
                //cmd.Parameters.Add("cur_CALC", OracleDbType.RefCursor, ParameterDirection.Output);

                //SqlCommand precmd = new SqlCommand();
                //precmd.Connection = conn;
                //precmd.CommandText = "alter session set current_schema = PMRS_Client_53";
                //precmd.ExecuteNonQuery();

                string strSQL = "select calc.calcid as CALCID, calc.mkey as MKEY, calc.cdate as CDATE, member.erkey as ERKEY from PMRS_CLIENT_53.calc, PMRS_CLIENT_53.calc_data, PMRS_CLIENT_53.member "
                        + "where calc_data.calcid=calc.calcid and member.mkey = calc.mkey "
                        + "and calc.stage='ZZ' and calc.ctype='YC' and calc_data.pstatus in ('AC','DP') "
                        + "and calc.mkey in (" + MKEY + ")"
                    //+ "and calc.mkey in (select member.mkey from member where member.erkey in (" + strERKEY + "))"
                        + " AND CALC.CDATE = '" + Sdate + "'";
                cmd.Connection = conn;
                cmd.CommandText = strSQL;
                cmd.CommandType = CommandType.Text;
                oda.SelectCommand = cmd;
                oda.Fill(ds);
            }
            catch (Exception ex)
            {
                ExceptionLogHelper logindb = new ExceptionLogHelper();
                logindb.Logexception(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
                ds.Dispose();

            }
            return ds;
        }
        #endregion
        public DataSet viewMemberInfo(int mcid)
        {
            SqlConnection conn = new SqlConnection(connstr);
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            StringBuilder sb = new StringBuilder();
            SqlDataAdapter oda = new SqlDataAdapter();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                //SqlCommand precmd = new SqlCommand();
                //precmd.Connection = conn;
                //precmd.CommandText = "alter session set current_schema = PMRS_Client_53";
                //precmd.ExecuteNonQuery();


                cmd.CommandText = "DNviewMemberInfo1";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.Parameters.Add("vMCONTACTID", OracleDbType.NVarchar2, mcid, ParameterDirection.Input);
                cmd.Parameters.Add("cur_LoginDetails", OracleDbType.RefCursor, ParameterDirection.Output);
                oda.SelectCommand = cmd;
                oda.Fill(dt);
                ds.Tables.Add(dt);
                dt = null;
                dt = new DataTable();
                cmd = new SqlCommand();
                cmd.CommandText = "DNviewMemberInfo2";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.Parameters.Add("vMCONTACTID", OracleDbType.NVarchar2, mcid, ParameterDirection.Input);
                cmd.Parameters.Add("cur_LoginDetails", OracleDbType.RefCursor, ParameterDirection.Output);
                oda.SelectCommand = cmd;
                oda.Fill(dt);
                ds.Tables.Add(dt);
            }
            catch (Exception ex)
            {
                ExceptionLogHelper logindb = new ExceptionLogHelper();
                logindb.Logexception(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
                ds.Dispose();

            }
            return ds;
        }

        #endregion

        #region Accounting realted

        public DataSet BtnFillEmpGASB()
        {
            SqlConnection conn = new SqlConnection(connstr);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter oda = new SqlDataAdapter();
            DataSet ds = new DataSet();

            try
            {
                conn.Open();
                cmd.Connection = conn;
                //SqlCommand precmd = new SqlCommand();
                //precmd.Connection = conn;
                //precmd.CommandText = "alter session set current_schema = PMRS_Client_53";
                //precmd.ExecuteNonQuery();

                cmd.CommandText = "DNBtnFillEmpGASB";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.Parameters.Add("cur_LoginDetails", OracleDbType.RefCursor, ParameterDirection.Output);
                oda.SelectCommand = cmd;
                oda.Fill(ds);
            }
            catch (Exception ex)
            {
                ExceptionLogHelper logindb = new ExceptionLogHelper();
                logindb.Logexception(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
                ds.Dispose();

            }
            return ds;
        }

        public DataSet PrintEmpReport(string MERKEY, string Sdate)
        {
            SqlConnection conn = new SqlConnection(connstr);
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            SqlDataAdapter oda = new SqlDataAdapter();
            DataSet ds = new DataSet();

            try
            {
                //SqlCommand precmd = new SqlCommand();
                //precmd.Connection = conn;
                //precmd.CommandText = "alter session set current_schema = PMRS_Client_53";
                //precmd.ExecuteNonQuery();

                cmd.CommandText = "DNPrintEmpReport";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.Parameters.Add("vSdate", OracleDbType.NVarchar2, Sdate, ParameterDirection.Input);
                cmd.Parameters.Add("vERKEY", OracleDbType.NVarchar2, MERKEY, ParameterDirection.Input);
                cmd.Parameters.Add("cur_CALC", OracleDbType.RefCursor, ParameterDirection.Output);
                oda.SelectCommand = cmd;
                oda.Fill(ds);
            }
            catch (Exception ex)
            {
                ExceptionLogHelper logindb = new ExceptionLogHelper();
                logindb.Logexception(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
                ds.Dispose();

            }
            return ds;
        }


        #endregion

        #region Member realted

        public DataSet RptByMem(string Sdate)
        {
            SqlConnection conn = new SqlConnection(connstr);
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            SqlDataAdapter oda = new SqlDataAdapter();
            DataSet ds = new DataSet();

            try
            {
                //SqlCommand precmd = new SqlCommand();
                //precmd.Connection = conn;
                //precmd.CommandText = "alter session set current_schema = PMRS_Client_53";
                //precmd.ExecuteNonQuery();

                cmd.CommandText = "DNRptByMem";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.Parameters.Add("vSdate", OracleDbType.NVarchar2, Sdate, ParameterDirection.Input);
                cmd.Parameters.Add("cur_CALC", OracleDbType.RefCursor, ParameterDirection.Output);
                oda.SelectCommand = cmd;
                oda.Fill(ds);
            }
            catch (Exception ex)
            {
                ExceptionLogHelper logindb = new ExceptionLogHelper();
                logindb.Logexception(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
                ds.Dispose();

            }
            return ds;
        }

        public DataSet RptByMemEmp(string Sdate, string ERKEY)
        {
            SqlConnection conn = new SqlConnection(connstr);
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            SqlDataAdapter oda = new SqlDataAdapter();
            DataSet ds = new DataSet();

            try
            {
                //SqlCommand precmd = new SqlCommand();
                //precmd.Connection = conn;
                //precmd.CommandText = "alter session set current_schema = PMRS_Client_53";
                //precmd.ExecuteNonQuery();

                cmd.CommandText = "DNRptByMemEmp";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.Parameters.Add("vSdate", OracleDbType.NVarchar2, Sdate, ParameterDirection.Input);
                cmd.Parameters.Add("vERKEY", OracleDbType.NVarchar2, ERKEY, ParameterDirection.Input);
                cmd.Parameters.Add("cur_CALC", OracleDbType.RefCursor, ParameterDirection.Output);
                oda.SelectCommand = cmd;
                oda.Fill(ds);
            }
            catch (Exception ex)
            {
                ExceptionLogHelper logindb = new ExceptionLogHelper();
                logindb.Logexception(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
                ds.Dispose();

            }
            return ds;
        }

        public DataSet RptByMemEmp1(string ERKEY)
        {
            SqlConnection conn = new SqlConnection(connstr);
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            SqlDataAdapter oda = new SqlDataAdapter();
            DataSet ds = new DataSet();

            try
            {
                //SqlCommand precmd = new SqlCommand();
                //precmd.Connection = conn;
                //precmd.CommandText = "alter session set current_schema = PMRS_Client_53";
                //precmd.ExecuteNonQuery();

                cmd.CommandText = "DNRptByMemEmp1";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                //cmd.Parameters.Add("vSdate", OracleDbType.NVarchar2, Sdate, ParameterDirection.Input);
                cmd.Parameters.Add("vERKEY", OracleDbType.NVarchar2, ERKEY, ParameterDirection.Input);
                cmd.Parameters.Add("cur_CALC", OracleDbType.RefCursor, ParameterDirection.Output);
                oda.SelectCommand = cmd;
                oda.Fill(ds);
            }
            catch (Exception ex)
            {
                ExceptionLogHelper logindb = new ExceptionLogHelper();
                logindb.Logexception(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
                ds.Dispose();

            }
            return ds;
        }

        public DataSet CreatReportsmem1(string Sdate, string MKEY)
        {
            SqlConnection conn = new SqlConnection(connstr);
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            SqlDataAdapter oda = new SqlDataAdapter();
            DataSet ds = new DataSet();

            try
            {
                //cmd.CommandText = "DNCreatReportsmem";
                //cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Connection = conn;

                //cmd.Parameters.Add("vSdate", OracleDbType.Varchar2, Sdate, ParameterDirection.Input);
                //cmd.Parameters.Add("vMKEY", OracleDbType.NVarchar2, MKEY, ParameterDirection.Input);
                //cmd.Parameters.Add("cur_CALC", OracleDbType.RefCursor, ParameterDirection.Output);

                //SqlCommand precmd = new SqlCommand();
                //precmd.Connection = conn;
                //precmd.CommandText = "alter session set current_schema = PMRS_Client_53";
                //precmd.ExecuteNonQuery();

                string strSQL = "select calc.calcid as CALCID, calc.mkey as MKEY, calc.cdate as CDATE, member.erkey as ERKEY from PMRS_CLIENT_53.calc, PMRS_CLIENT_53.calc_data, PMRS_CLIENT_53.member "
                        + "where calc_data.calcid=calc.calcid and member.mkey = calc.mkey "
                        + "and calc.stage='ZZ' and calc.ctype='YC' and calc_data.pstatus in ('AC','DP') "
                        + "and calc.mkey in (" + MKEY + ")"
                    //+ "and calc.mkey in (select member.mkey from member where member.erkey in (" + strERKEY + "))"
                        + " AND CALC.CDATE = '" + Sdate + "'";
                cmd.Connection = conn;
                cmd.CommandText = strSQL;
                cmd.CommandType = CommandType.Text;
                oda.SelectCommand = cmd;
                oda.Fill(ds);
            }
            catch (Exception ex)
            {
                ExceptionLogHelper logindb = new ExceptionLogHelper();
                logindb.Logexception(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
                ds.Dispose();

            }
            return ds;
        }

        public DataSet FillYear()
        {
            SqlConnection conn = new SqlConnection(connstr);
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            SqlDataAdapter oda = new SqlDataAdapter();
            DataSet ds = new DataSet();

            try
            {
                //SqlCommand precmd = new SqlCommand();
                //precmd.Connection = conn;
                //precmd.CommandText = "alter session set current_schema = PMRS_Client_53";
                //precmd.ExecuteNonQuery();

                cmd.CommandText = "DNFillYear";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.Parameters.Add("cur_CALC", SqlDbType.VarChar, ParameterDirection.Output);
                oda.SelectCommand = cmd;
                oda.Fill(ds);
            }
            catch (Exception ex)
            {
                ExceptionLogHelper logindb = new ExceptionLogHelper();
                logindb.Logexception(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
                ds.Dispose();

            }
            return ds;
        }

        #endregion

        #region Password Encrypt

        private static string strPassPhrase = "pa$$w0rd";// can be any string
        private static string strSaltValue = "s@1tValue";// can be any string
        private static string strHashAlgorithm = "SHA1";// can be "MD5"
        private static int intPasswordIterations = 2;  // can be any number
        private static string strInitVector = "@1B2c3D4e5F6g7H8";// must be 16 bytes
        private static int intKeySize = 128;    // can be 192 or 1


        public string Encrypt(string plainPassword)
        {
            return Encrypt(plainPassword, strPassPhrase, strSaltValue, strHashAlgorithm, intPasswordIterations, strInitVector, intKeySize);
        }
        private string Encrypt(string plainText, string passPhrase, string saltValue, string hashAlgorithm, int passwordIterations, string initVector, int keySize)
        {

            // Convert strings into byte arrays.
            // Let us assume that strings only contain ASCII codes.
            // If strings include Unicode characters, use Unicode, UTF7, or UTF8 
            // encoding.

            byte[] byInitVectorBytes = Encoding.ASCII.GetBytes(strInitVector);
            byte[] bySaltValueBytes = Encoding.ASCII.GetBytes(strSaltValue);

            // Convert our plaintext into a byte array.
            // Let us assume that plaintext contains UTF8-encoded characters.

            byte[] byPlainTextBytes = Encoding.UTF8.GetBytes(plainText);

            // First, we must create a password, from which the key will be derived.
            // This password will be generated from the specified passphrase and 
            // salt value. The password will be created using the specified hash 
            // algorithm. Password creation can be done in several iterations.

            PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, bySaltValueBytes, hashAlgorithm, passwordIterations);

            // Use the password to generate pseudo-random bytes for the encryption
            // key. Specify the size of the key in bytes (instead of bits).

            byte[] byKeyBytes = password.GetBytes(keySize / 8);

            // Create uninitialized Rijndael encryption object.

            RijndaelManaged symmetricKey = new RijndaelManaged();

            // It is reasonable to set encryption mode to Cipher Block Chaining
            // (CBC). Use default options for other symmetric key parameters.

            symmetricKey.Mode = CipherMode.CBC;

            // Generate encryptor from the existing key bytes and initialization 
            // vector. Key size will be defined based on the number of the key 
            // bytes.

            ICryptoTransform encryptor = symmetricKey.CreateEncryptor(byKeyBytes, byInitVectorBytes);

            // Define memory stream which will be used to hold encrypted data.

            MemoryStream memoryStream = new MemoryStream();

            // Define cryptographic stream (always use Write mode for encryption).

            CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);

            // Start encrypting.

            cryptoStream.Write(byPlainTextBytes, 0, byPlainTextBytes.Length);

            // Finish encrypting.

            cryptoStream.FlushFinalBlock();

            // Convert our encrypted data from a memory stream into a byte array.

            byte[] byCipherTextBytes = memoryStream.ToArray();

            // Close both streams.

            memoryStream.Close();
            cryptoStream.Close();

            // Convert encrypted data into a base64-encoded string.

            string strCipherText = Convert.ToBase64String(byCipherTextBytes);

            // Return encrypted string.

            return strCipherText;
        }


        public string Decrypt(string encryptedPassword)
        {
            if (encryptedPassword == "" || encryptedPassword == null)
                return string.Empty;
            return Decrypt(encryptedPassword, strPassPhrase, strSaltValue, strHashAlgorithm, intPasswordIterations, strInitVector, intKeySize);

        }
        private string Decrypt(string cipherText, string passPhrase, string saltValue, string hashAlgorithm, int passwordIterations, string initVector, int keySize)
        {
            // Convert strings defining encryption key characteristics into byte
            // arrays. Let us assume that strings only contain ASCII codes.
            // If strings include Unicode characters, use Unicode, UTF7, or UTF8
            // encoding.

            byte[] byInitVectorBytes = Encoding.ASCII.GetBytes(strInitVector);
            byte[] bySaltValueBytes = Encoding.ASCII.GetBytes(strSaltValue);

            // Convert our ciphertext into a byte array.

            byte[] byCipherTextBytes = Convert.FromBase64String(cipherText);

            // First, we must create a password, from which the key will be 
            // derived. This password will be generated from the specified 
            // passphrase and salt value. The password will be created using
            // the specified hash algorithm. Password creation can be done in
            // several iterations.

            PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, bySaltValueBytes, hashAlgorithm, passwordIterations);

            // Use the password to generate pseudo-random bytes for the encryption
            // key. Specify the size of the key in bytes (instead of bits).

            byte[] byKeyBytes = password.GetBytes(keySize / 8);

            // Create uninitialized Rijndael encryption object.
            RijndaelManaged symmetricKey = new RijndaelManaged();

            // It is reasonable to set encryption mode to Cipher Block Chaining
            // (CBC). Use default options for other symmetric key parameters.

            symmetricKey.Mode = CipherMode.CBC;

            // Generate decryptor from the existing key bytes and initialization 
            // vector. Key size will be defined based on the number of the key 
            // bytes.

            ICryptoTransform decryptor = symmetricKey.CreateDecryptor(byKeyBytes, byInitVectorBytes);

            // Define memory stream which will be used to hold encrypted data.

            MemoryStream memoryStream = new MemoryStream(byCipherTextBytes);

            // Define cryptographic stream (always use Read mode for encryption).

            CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);

            // Since at this point we don't know what the size of decrypted data
            // will be, allocate the buffer long enough to hold ciphertext;
            // plaintext is never longer than ciphertext.

            byte[] byPlainTextBytes = new byte[byCipherTextBytes.Length];

            // Start decrypting.

            int iDecryptedByteCount = cryptoStream.Read(byPlainTextBytes, 0, byPlainTextBytes.Length);

            // Close both streams.

            memoryStream.Close();
            cryptoStream.Close();

            // Convert decrypted data into a string. 
            // Let us assume that the original plaintext string was UTF8-encoded.

            string strPlainText = Encoding.UTF8.GetString(byPlainTextBytes, 0, iDecryptedByteCount);

            // Return decrypted string.

            return strPlainText;
        }

        #endregion password encryt

        #region SP Related to Calc_Dtails

        public DataSet GetMemInfo()
        {
            SqlConnection conn = new SqlConnection(connstr);
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            SqlDataAdapter oda = new SqlDataAdapter();
            DataSet ds = new DataSet();
            try
            {
                //SqlCommand precmd = new SqlCommand();
                //precmd.Connection = conn;
                //precmd.CommandText = "alter session set current_schema = PMRS_Client_53";
                //precmd.ExecuteNonQuery();

                cmd.CommandText = "DNRptByMemEmp2";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.Parameters.Add("cur_CALC", OracleDbType.RefCursor, ParameterDirection.Output);
                oda.SelectCommand = cmd;
                oda.Fill(ds);
            }
            catch (Exception ex)
            {
                ExceptionLogHelper logindb = new ExceptionLogHelper();
                logindb.Logexception(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
                ds.Dispose();

            }
            return ds;
        }

        public DataSet GetPlanInfo(string MKEY)
        {
            SqlConnection conn = new SqlConnection(connstr);
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            SqlDataAdapter oda = new SqlDataAdapter();
            DataSet ds = new DataSet();
            try
            {
                //SqlCommand precmd = new SqlCommand();
                //precmd.Connection = conn;
                //precmd.CommandText = "alter session set current_schema = PMRS_Client_53";
                //precmd.ExecuteNonQuery();

                cmd.CommandText = "DNSELECTPLANINFO";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.Parameters.Add("mkey", OracleDbType.NVarchar2, MKEY, ParameterDirection.Input);
                cmd.Parameters.Add("cur_CALC", OracleDbType.RefCursor, ParameterDirection.Output);
                oda.SelectCommand = cmd;
                oda.Fill(ds);
            }
            catch (Exception ex)
            {
                ExceptionLogHelper logindb = new ExceptionLogHelper();
                logindb.Logexception(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
                ds.Dispose();

            }
            return ds;
        }

        public DataSet GetCalcInfo(string mkey, string plan)
        {
            SqlConnection conn = new SqlConnection(connstr);
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            StringBuilder sb = new StringBuilder();
            cmd.Connection = conn;
            SqlDataAdapter oda = new SqlDataAdapter();
            DataSet ds = new DataSet();
            try
            {
                //SqlCommand precmd = new SqlCommand();
                //precmd.Connection = conn;
                //precmd.CommandText = "alter session set current_schema = PMRS_Client_53";
                //precmd.ExecuteNonQuery();

                //sb.Append("SELECT A.*,A.ROWID  FROM  PMRS_CLIENT_53.CALC$HISTORY  A  WHERE");
                //sb.Append(" A.CLNT = '0001'");
                //sb.Append(" AND  A.PLAN = '" + PLAN + "'");
                //sb.Append(" AND  A.MKEY='" + MKEY + "'");
                //sb.Append(" ORDER  BY  A.CDATE,A.CALCID");


                //sb.Append("SELECT B.NAME CALCTYPE,C.Name SUBTYPE,  A.*,A.ROWID  ");
                //sb.Append("FROM  ");
                //sb.Append("CPASARCH.CALC$HISTORYARCH  A, ");
                //sb.Append("PMRS_COMMON_53.CPAS_CALCTYPE B, ");
                //sb.Append("(SELECT VALU, NAME FROM PMRS_COMMON_53.CPAS_CODE_VALUE WHERE GRUP='GCS') C ");
                //sb.Append("WHERE ");
                //sb.Append("A.CTYPE = B.CTYPE ");
                //sb.Append("AND (A.STYPE = C.VALU(+)) ");
                //sb.Append("AND A.CLNT = '0001' ");
                //sb.Append("AND A.PLAN = '" + plan + "' ");
                //sb.Append("AND A.MKEY ='" + mkey + "' ");
                //sb.Append("ORDER  BY  A.CDATE,A.CALCID ");

                //cmd.CommandText = sb.ToString();
                //cmd.CommandType = CommandType.Text;
                //oda.SelectCommand = cmd;
                //oda.Fill(ds);

                cmd.CommandText = "DNSELECTCALC";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Connection = conn;

                cmd.Parameters.Add("planinfo", OracleDbType.NVarchar2, plan, ParameterDirection.Input);
                cmd.Parameters.Add("pmkey", OracleDbType.NVarchar2, mkey, ParameterDirection.Input);
                cmd.Parameters.Add("cur_CALC", OracleDbType.RefCursor, ParameterDirection.Output);
                oda.SelectCommand = cmd;
                oda.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                ExceptionLogHelper logindb = new ExceptionLogHelper();
                logindb.Logexception(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
                ds.Dispose();

            }
            return ds;
        }

        public DataSet GetCalcDetailsInfo(string calcid)
        {
            SqlConnection conn = new SqlConnection(connstr);
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            SqlDataAdapter oda = new SqlDataAdapter();
            DataSet ds = new DataSet();

            try
            {
                //SqlCommand precmd = new SqlCommand();
                //precmd.Connection = conn;
                //precmd.CommandText = "alter session set current_schema = PMRS_Client_53";
                //precmd.ExecuteNonQuery();

                cmd.CommandText = "DNSelectCalcInfo3";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.Parameters.Add("calc", OracleDbType.NVarchar2, calcid, ParameterDirection.Input);
                cmd.Parameters.Add("cur_CALC", OracleDbType.RefCursor, ParameterDirection.Output);
                oda.SelectCommand = cmd;
                oda.Fill(ds);
            }
            catch (Exception ex)
            {
                ExceptionLogHelper logindb = new ExceptionLogHelper();
                logindb.Logexception(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
                ds.Dispose();

            }
            return ds;
        }

        #endregion

        #region EmployerDocument

        public DataSet BtnFillEmpGASB1()
        {
            SqlConnection conn = new SqlConnection(connstr);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter oda = new SqlDataAdapter();
            DataSet ds = new DataSet();
            try
            {
                conn.Open();
                cmd.Connection = conn;
                //SqlCommand precmd = new SqlCommand();
                //precmd.Connection = conn;
                //precmd.CommandText = "alter session set current_schema = PMRS_Client_53";
                //precmd.ExecuteNonQuery();

                cmd.CommandText = "DNBtnFillEmpGASB";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("cur_LoginDetails", OracleDbType.RefCursor, ParameterDirection.Output);
                oda.SelectCommand = cmd;
                oda.Fill(ds);
            }
            catch (Exception ex)
            {
                ExceptionLogHelper logindb = new ExceptionLogHelper();
                logindb.Logexception(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
                ds.Dispose();

            }
            return ds;
        }

        public DataTable getMunicipalDocumentList1(string erkey)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable("MunicipalDocumentList");
            SqlConnection conn = new SqlConnection(connstr);
            SqlCommand cmd = new SqlCommand();
            // StringBuilder sb = new StringBuilder();
            SqlDataAdapter da = new SqlDataAdapter();

            try
            {
                conn.Open();
                cmd.Connection = conn;
                //SqlCommand precmd = new SqlCommand();
                //precmd.Connection = conn;
                //precmd.CommandText = "alter session set current_schema = PMRS_CLIENT_53";
                //precmd.ExecuteNonQuery();

                #region Employer status
                cmd.CommandText = "DNGETEMPDOCLIST";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("perkey", OracleDbType.Varchar2, erkey, ParameterDirection.Input);
                cmd.Parameters.Add("cur_empcontactinfo", OracleDbType.RefCursor, ParameterDirection.Output);
                da.SelectCommand = cmd;
                da.Fill(dt);
                #endregion

                return dt;
            }
            catch (Exception ex)
            {
                ExceptionLogHelper logindb = new ExceptionLogHelper();
                logindb.Logexception(ex);
                return null;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
                da.Dispose();
            }
        }

        public FilenameAndData getMunicipalDocumentbyFileID(string fileid)
        {
            Int32 fid = Convert.ToInt32(fileid);
            SqlConnection conn;
            FilenameAndData fad = new FilenameAndData();
            fad.fname = "";
            fad.fdata = "";
            if (fid <= 20364)
            {
                conn = new SqlConnection(connstr);
            }
            else
            {
                conn = new SqlConnection(connstr);
            }
            //SqlConnection conn = new SqlConnection(connstr);  // C#
            SqlCommand cmd = new SqlCommand();
            //string filedata = "";
            try
            {
                conn.Open();
                cmd.Connection = conn;
                //SqlCommand precmd = new SqlCommand();
                //precmd.Connection = conn;
                //precmd.CommandText = "alter session set current_schema = PMRS_CLIENT_53";
                //precmd.ExecuteNonQuery();

                #region Get file from BLOB
                //cmd.CommandText = "SELECT FILENAME, FILEITEM FROM BINFILE WHERE FILEID=" + fileid;
                //cmd.CommandType = CommandType.Text;
                cmd.CommandText = "DNGETMUNICIPALDOCBYFILEID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("pfileid", OracleDbType.Varchar2, fileid, ParameterDirection.Input);
                cmd.Parameters.Add("cur_getMunicipalDocbyFileID", OracleDbType.RefCursor, ParameterDirection.Output);
                OracleDataReader rdr = cmd.ExecuteReader();
                byte[] buf = null;
                if (rdr.Read())
                {
                    fad.fname = rdr.GetOracleString(rdr.GetOrdinal("FILENAME")).Value;
                    buf = rdr.GetOracleBlob(rdr.GetOrdinal("FILEITEM")).Value;
                    fad.fdata = Convert.ToBase64String(buf);
                }
                #endregion

                return fad;
            }
            catch (Exception ex)
            {
                ExceptionLogHelper logindb = new ExceptionLogHelper();
                logindb.Logexception(ex);
                return fad;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
            }
        }

        [Serializable]
        public struct FilenameAndData
        {
            public String fname;
            public String fdata;
        }

        #endregion

        #region SP Related to Calc_Dtails from service

        public DataSet GetMemInfo1()
        {
            SqlConnection conn = new SqlConnection(connstr);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter oda = new SqlDataAdapter();
            DataSet ds = new DataSet();
            try
            {
                conn.Open();
                cmd.Connection = conn;
                //SqlCommand precmd = new SqlCommand();
                //precmd.Connection = conn;
                //precmd.CommandText = "alter session set current_schema = PMRS_Client_53";
                //precmd.ExecuteNonQuery();

                cmd.CommandText = "DNRptByMemEmp2";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("cur_CALC", OracleDbType.RefCursor, ParameterDirection.Output);
                oda.SelectCommand = cmd;
                oda.Fill(ds);
            }
            catch (Exception ex)
            {

                ExceptionLogHelper logindb = new ExceptionLogHelper();
                logindb.Logexception(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
                ds.Dispose();

            }
            return ds;
        }

        public DataSet GetPlanInfo1(string MKEY)
        {
            SqlConnection conn = new SqlConnection(connstr);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter oda = new SqlDataAdapter();
            DataSet ds = new DataSet();
            try
            {
                conn.Open();
                cmd.Connection = conn;
                //SqlCommand precmd = new SqlCommand();
                //precmd.Connection = conn;
                //precmd.CommandText = "alter session set current_schema = PMRS_Client_53";
                //precmd.ExecuteNonQuery();

                cmd.CommandText = "DNSELECTPLANINFO";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("pmkey", OracleDbType.NVarchar2, MKEY, ParameterDirection.Input);
                cmd.Parameters.Add("cur_CALC", OracleDbType.RefCursor, ParameterDirection.Output);
                oda.SelectCommand = cmd;
                oda.Fill(ds);
            }
            catch (Exception ex)
            {
                ExceptionLogHelper logindb = new ExceptionLogHelper();
                logindb.Logexception(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
                ds.Dispose();

            }
            return ds;
        }

        public DataSet GetCalcInfo1(string mkey, string plan)
        {
            SqlConnection conn = new SqlConnection(connstr);
            SqlCommand cmd = new SqlCommand();
            StringBuilder sb = new StringBuilder();
            SqlDataAdapter oda = new SqlDataAdapter();
            DataSet ds = new DataSet();
            try
            {
                conn.Open();
                cmd.Connection = conn;

                //SqlCommand precmd = new SqlCommand();
                //precmd.Connection = conn;
                //precmd.CommandText = "alter session set current_schema = PMRS_Client_53";
                //precmd.ExecuteNonQuery();

                //sb.Append("SELECT A.*,A.ROWID  FROM  PMRS_CLIENT_53.CALC$HISTORY  A  WHERE");
                //sb.Append(" A.CLNT = '0001'");
                //sb.Append(" AND  A.PLAN = '" + PLAN + "'");
                //sb.Append(" AND  A.MKEY='" + MKEY + "'");
                //sb.Append(" ORDER  BY  A.CDATE,A.CALCID");


                sb.Append("SELECT B.NAME CALCTYPE,C.Name SUBTYPE,  A.*,A.ROWID  ");
                sb.Append("FROM  ");
                sb.Append("PMRS_CLIENT_53.CALC$HISTORY  A, ");
                sb.Append("PMRS_COMMON_53.CPAS_CALCTYPE B, ");
                sb.Append("(SELECT VALU, NAME FROM PMRS_COMMON_53.CPAS_CODE_VALUE WHERE GRUP='GCS') C ");
                sb.Append("WHERE ");
                sb.Append("A.CTYPE = B.CTYPE ");
                sb.Append("AND (A.STYPE = C.VALU(+)) ");
                sb.Append("AND A.CLNT = '0001' ");
                sb.Append("AND A.PLAN = '" + plan + "' ");
                sb.Append("AND A.MKEY ='" + mkey + "' ");
                sb.Append("ORDER  BY  A.CDATE,A.CALCID ");

                cmd.CommandText = sb.ToString();
                cmd.CommandType = CommandType.Text;
                oda.SelectCommand = cmd;
                oda.Fill(ds);

                //cmd.CommandText = "DNSELECTCALC";
                //cmd.CommandType = CommandType.StoredProcedure;
                ////cmd.Connection = conn;
                //cmd.Parameters.Add("vmkey", OracleDbType.NVarchar2, mkey, ParameterDirection.Input);
                //cmd.Parameters.Add("vplan", OracleDbType.NVarchar2, plan, ParameterDirection.Input);
                //cmd.Parameters.Add("cur_CALC", OracleDbType.RefCursor, ParameterDirection.Output);
                //oda.SelectCommand = cmd;
                //oda.Fill(ds);
                return ds;

            }
            catch (Exception ex)
            {
                ExceptionLogHelper logindb = new ExceptionLogHelper();
                logindb.Logexception(ex);
                return null;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
                oda.Dispose();
            }

        }

        public DataSet GetCalcDetailsInfo1(string calcid)
        {
            SqlConnection conn = new SqlConnection(connstr);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter oda = new SqlDataAdapter();
            DataSet ds = new DataSet();
            try
            {
                conn.Open();
                cmd.Connection = conn;
                //SqlCommand precmd = new SqlCommand();
                //precmd.Connection = conn;
                //precmd.CommandText = "alter session set current_schema = PMRS_Client_53";
                //precmd.ExecuteNonQuery();

                cmd.CommandText = "DNSelectCalcInfo3";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("pcalc", OracleDbType.NVarchar2, calcid, ParameterDirection.Input);
                cmd.Parameters.Add("cur_CALC", OracleDbType.RefCursor, ParameterDirection.Output);
                oda.SelectCommand = cmd;
                oda.Fill(ds);
            }
            catch (Exception ex)
            {
                ExceptionLogHelper logindb = new ExceptionLogHelper();
                logindb.Logexception(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
                ds.Dispose();

            }
            return ds;
        }

        #endregion

        #region Exception Management

        public class ExceptionLogHelper : IDisposable
        {
            const string ApplicationName = "PMRSWebServices";
            #region Properties
            private string _ModuleName;
            private string _ProcedureName;
            private string _UserID;
            private string _PercID;

            public string ModuleName
            {
                get { return _ModuleName; }
                set { _ModuleName = value; }
            }

            public string ProcedureName
            {
                get { return _ProcedureName; }
                set { _ProcedureName = value; }
            }

            public string UserID
            {
                get { return _UserID; }
                set { _UserID = value; }
            }

            public string PercID
            {
                get { return _PercID; }
                set { _PercID = value; }
            }

            #endregion

            public ExceptionLogHelper()
            {
                ModuleName = "";
                ProcedureName = "";
                UserID = "";
                PercID = "";
            }

            public ExceptionLogHelper(string valModuleName, string valProcedureName, string valUserID, string valPercID)
            {
                ModuleName = valModuleName;
                ProcedureName = valProcedureName;
                UserID = valUserID;
                PercID = valPercID;
            }

            public int Logexception(Exception ex)
            {
                ExceptionLogEntry oExceptionEntry = new ExceptionLogEntry();
                ExceptionLogWM oExceptionlog = new ExceptionLogWM();
                {
                    oExceptionEntry.machineName = System.Net.Dns.GetHostName();
                    oExceptionEntry.ExceptionMessage = (ex == null ? "" : ex.Message);
                    oExceptionEntry.ApplicationName = ApplicationName;
                    oExceptionEntry.ModuleName = ModuleName;
                    oExceptionEntry.ProcedureName = ProcedureName;
                    oExceptionEntry.MachineDate = System.DateTime.Now;
                    oExceptionEntry.StackTrace = ex.StackTrace;
                    oExceptionEntry.UserID = UserID;
                    oExceptionEntry.PercID = PercID;
                }
                return oExceptionlog.AddException(oExceptionEntry);
            }

            public int Logexception(string ErrorMsg)
            {
                ExceptionLogEntry oExceptionEntry = new ExceptionLogEntry();
                ExceptionLogWM oExceptionlog = new ExceptionLogWM();
                {
                    oExceptionEntry.machineName = System.Net.Dns.GetHostName();
                    oExceptionEntry.ExceptionMessage = ErrorMsg;
                    oExceptionEntry.ApplicationName = ApplicationName;
                    oExceptionEntry.ModuleName = ModuleName;
                    oExceptionEntry.ProcedureName = ProcedureName;
                    oExceptionEntry.MachineDate = System.DateTime.Now;
                    oExceptionEntry.StackTrace = "";
                    oExceptionEntry.UserID = UserID;
                    oExceptionEntry.PercID = PercID;
                }
                return oExceptionlog.AddException(oExceptionEntry);
            }

            public int Logexception(ExceptionLogEntry newLogEntry)
            {
                ExceptionLogWM oExceptionlog = new ExceptionLogWM();
                return oExceptionlog.AddException(newLogEntry);
            }
            private bool disposedValue = false;
            // To detect redundant calls

            // IDisposable
            protected virtual void Dispose(bool disposing)
            {
                if (!this.disposedValue)
                {
                    if (disposing)
                    {
                    }
                    // TODO: free managed resources when explicitly called
                }

                // TODO: free shared unmanaged resources
                this.disposedValue = true;
            }

            #region " IDisposable Support "
            // This code added by Visual Basic to correctly implement the disposable pattern.
            public void Dispose()
            {
                // Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
                Dispose(true);
                GC.SuppressFinalize(this);
            }
            #endregion
        }

        public class ExceptionLogEntry
        {
            //ExceptionID : int
            private int _ExceptionID;
            //MachineDate : datetime
            private System.DateTime _MachineDate;
            //ServerDate : datetime
            private System.DateTime _ServerDate;
            //machineName : varchar
            private string _machineName;
            //ExceptionMessage : varchar
            private string _ExceptionMessage;
            //ApplicationName : varchar
            private string _ApplicationName;
            //ModuleName : varchar
            private string _ModuleName;
            //ProcedureName : varchar
            private string _ProcedureName;
            //UserID : varchar
            private string _UserID = "";
            //PercID : Int
            private string _PercID = "";
            //StackTrace : varchar
            private string _StackTrace = "";

            public int ExceptionID
            {
                get { return _ExceptionID; }
                set { _ExceptionID = value; }
            }

            public System.DateTime MachineDate
            {
                get { return _MachineDate; }
                set { _MachineDate = value; }
            }

            public System.DateTime ServerDate
            {
                get { return _ServerDate; }
                set { _ServerDate = value; }
            }

            public string machineName
            {
                get { return _machineName; }
                set { _machineName = value; }
            }

            public string ExceptionMessage
            {
                get { return _ExceptionMessage; }
                set { _ExceptionMessage = value; }
            }

            public string ApplicationName
            {
                get { return _ApplicationName; }
                set { _ApplicationName = value; }
            }

            public string ModuleName
            {
                get { return _ModuleName; }
                set { _ModuleName = value; }
            }

            public string ProcedureName
            {
                get { return _ProcedureName; }
                set { _ProcedureName = value; }
            }

            public string UserID
            {
                get { return _UserID; }
                set { _UserID = value; }
            }

            public string PercID
            {
                get { return _PercID; }
                set { _PercID = value; }
            }

            public string StackTrace
            {
                get { return _StackTrace; }
                set { _StackTrace = value; }
            }
        }

        public class ExceptionLogWM
        {

            public ExceptionLogWM()
            {

                //Uncomment the following line if using designed components 
                //InitializeComponent(); 
            }

            public int AddException(ExceptionLogEntry newLogEntry)
            {
                SqlCommand cmd = null;
                SqlConnection Con = null;
                OracleTransaction tran = null;
                int ReturnValue = 0;

                try
                {
                    SqlCommand precmd = new SqlCommand();
                    precmd.CommandText = "alter session set current_schema = PMRS_WEB";
                    precmd.CommandType = CommandType.Text;

                    cmd = OraSQLHelper.GetCommand();
                    cmd.CommandText = "spInsertException";
                    cmd.CommandType = CommandType.StoredProcedure;
                    newLogEntry.MachineDate = DateTime.Now;
                    newLogEntry.ServerDate = DateTime.Now;
                    OraSQLHelper.AddOutParameter(cmd, "vExceptionID", OracleDbType.Int32, 5);
                    OraSQLHelper.AddInParameter(cmd, "vMachineName", OracleDbType.Varchar2, 50, newLogEntry.machineName);
                    OraSQLHelper.AddInParameter(cmd, "vMachineDate", OracleDbType.Varchar2, newLogEntry.MachineDate.ToString("dd-MMM-yyyy hh:mm:ss"));
                    OraSQLHelper.AddInParameter(cmd, "vServerDate", OracleDbType.Varchar2, newLogEntry.ServerDate.ToString("dd-MMM-yyyy hh:mm:ss"));//.ToString("yyyyMMdd")
                    OraSQLHelper.AddInParameter(cmd, "vApplicationName", OracleDbType.Varchar2, 50, newLogEntry.ApplicationName);
                    OraSQLHelper.AddInParameter(cmd, "vModuleName", OracleDbType.Varchar2, 50, newLogEntry.ModuleName);
                    OraSQLHelper.AddInParameter(cmd, "vProcedureName", OracleDbType.Varchar2, 50, newLogEntry.ProcedureName);
                    OraSQLHelper.AddInParameter(cmd, "vUserID", OracleDbType.Varchar2, 20, newLogEntry.UserID);
                    OraSQLHelper.AddInParameter(cmd, "vPercID", OracleDbType.Varchar2, 20, newLogEntry.PercID);
                    OraSQLHelper.AddInParameter(cmd, "vExceptionMessage", OracleDbType.Varchar2, 4000, newLogEntry.ExceptionMessage);
                    OraSQLHelper.AddInParameter(cmd, "vStackTrace", OracleDbType.Varchar2, 2000, newLogEntry.StackTrace);


                    Con = new SqlConnection(OraSQLHelper.GetConnection(OraSQLHelper.DataBaseNames.PMRSONLINE));
                    Con.Open();
                    tran = Con.BeginTransaction();
                    //cmd.Transaction= tran;
                    OraSQLHelper.ExecuteNonQuery(tran, precmd);
                    OraSQLHelper.ExecuteNonQuery(tran, cmd);

                    ReturnValue = Convert.ToInt32(cmd.Parameters["vExceptionID"].Value.ToString());
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    if (tran != null) tran.Rollback();
                    try
                    {
                        using (StreamWriter sw = new StreamWriter("C:\\PMRSException\\ExceptionsNotHandled.txt"))
                        {
                            // Add some text to the file.
                            sw.Write("Exception:");
                            sw.WriteLine(ex.Message);
                            sw.WriteLine("Trace:");
                            sw.WriteLine(ex.StackTrace);
                            sw.WriteLine("-------------------");
                            // Arbitrary objects can also be written to the file.
                            sw.Write("Orginal Exception:");
                            sw.WriteLine(newLogEntry.ExceptionMessage);
                            sw.WriteLine("Trace:");
                            sw.WriteLine(newLogEntry.StackTrace);
                            sw.Close();
                        }
                    }
                    catch
                    {
                        throw;
                    }

                    ReturnValue = -1;
                }
                finally
                {
                    if (Con != null)
                    {
                        if (Con.State != ConnectionState.Closed) Con.Close();
                    }
                    Con = null;
                    tran = null;
                    cmd = null;
                }
                return ReturnValue;
            }



        }

        #endregion
    }
}
