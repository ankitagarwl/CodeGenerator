using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using NLog;
using NLog.Targets;
using NLog.Targets.Wrappers;

namespace AutoCodeGenerator
{
    class UtilConstants
    {
        public static string UserID = "";
        public static string gUname = "";
        public static string gPaswd = "";
        public static string mssqldb = "Data Source=";//"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=PMRSDG)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=CPASDOE)));" + "User Id=System;Password=greenol;";
        public static int ipbar;
        //public static string GASBRPT;
        //public static DateTime GASBRPT770DT;
        //public static string GASBRPTYEAR;
        public static string sEnvironment;
        public static string strsqlenv;
        //public or static variable for thread values
        public static FileTarget target = new FileTarget();
        public static Logger logger = LogManager.GetLogger("PMRSReports");
        public static string myPrinter = ConfigurationManager.AppSettings.Get("ReportPrinterMemberSvc");

    }
    class TabPermission
    {

        public static bool CreateRptMemTab = true;
        public static Int16 SelectedTabNum = -1;

    }
    public static class  setenv
    {
        public static string vUSERID;
        public static string vPASSWORD;
        //public static string vSCHEMA;
        //public static string vHOSTORIP;
        //public static string vSIDORSERVICE;
        public static string vODBCDNS;
        public static string vREPORTLOCATION;
        public static DateTime vSdate;
        public static string rptloc;
        public static string strTitle;
        #region set environment
        public static void checkandsetenv()
        {
            if (UtilConstants.sEnvironment == "PRODUCTION")
            {
                setprod();
            }
            else if (UtilConstants.sEnvironment == "TEST")
            {
                settestenv();
            }
        }
        private static void settestenv()
        {
            EnvironmentData configData = (EnvironmentData)ConfigurationManager.GetSection("TestEnvironmentSettings");
            rptloc = configData.REPORTLOCATION;
            if (rptloc.Substring(rptloc.Length - 1) == "\\")
            {
                rptloc = rptloc.Substring(0, rptloc.Length - 1);
            }
            UtilConstants.mssqldb = configData.strMSSqlDB + "User Id=" + UtilConstants.gUname + ";Password=" + UtilConstants.gPaswd + ";";
            vUSERID = UtilConstants.gUname;//txtUserID.Text;
            vPASSWORD = UtilConstants.gPaswd;//txtPW.Text;
            strTitle = "ezRide Statements Generator - Connected to TEST Environment";
        }
        private static void setprod()
        {
            EnvironmentData configData = (EnvironmentData)ConfigurationManager.GetSection("ProdEnvironmentSettings");
            rptloc = configData.REPORTLOCATION;
            if (rptloc.Substring(rptloc.Length - 1) == "\\")
            {
                rptloc = rptloc.Substring(0, rptloc.Length - 1);
            }
            UtilConstants.mssqldb = configData.strMSSqlDB + "User Id=" + configData.USERID + ";Password=" + configData.PASSWORD + ";";  
            //UtilConstants.mssqldb = configData.strMSSqlDB + "User Id=" + UtilConstants.gUname + ";Password=" + UtilConstants.gPaswd + ";";
            vUSERID = UtilConstants.gUname;//txtUserID.Text;
            vPASSWORD = UtilConstants.gPaswd;//txtPW.Text;
            vREPORTLOCATION = rptloc;//txtReportLocation.Text;
            strTitle = "ezRide Statements Generator - Connected to PRODUCTION Environment";
        }
        private static void setclear()
        {
            //txtUserID.Text = "";
            //txtPW.Text = "";
            //txtSchema.Text = "";
            //txtHostOrIP.Text = "";
            //txtSIDServiceName.Text = "";
            //txtODBCDNS.Text = "";
            //txtReportLocation.Text = "";
            //UtilConstants.oradb = "";
            //label1.Content = "CPAS Automation Tool for PMRS - Not Connected to Any Environment";
        }
        #endregion
    }
}
