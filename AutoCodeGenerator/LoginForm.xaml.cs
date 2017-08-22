using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Security.Principal;
//using Oracle.DataAccess.Client;
using System.Configuration;
using System.IO;

using System.Data;
using System.Data.OleDb;
using System.Reflection;
using System.Data.SqlClient;

namespace AutoCodeGenerator
{
    /// <summary>
    /// Interaction logic for LoginForm.xaml
    /// </summary>
    public partial class LoginForm : Window
    {

        public LoginForm()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            //WindowsPrincipal user = new WindowsPrincipal(WindowsIdentity.GetCurrent());
            //string uname = user.Identity.Name.Substring(Convert.ToInt16(user.Identity.Name.LastIndexOf("\\"))+1);
            //conn.ClientId = user.Identity.Name; // C#
            //http://www.tek-tips.com/viewthread.cfm?qid=728921
            //txtUserName.Text = System.Environment.UserName;// +" - " + user.Identity.Name; 
            //txtUserName.Text = "PMRS_WEB";// +" - " + user.Identity.Name; 
            //txtUserName.Text = "pmrs_web";
        }

        private void btnVerify_Click(object sender, RoutedEventArgs e)
        {

            verifypass();

        }

        private bool checkpermissions(string struname, string strpwd)
        {
            //DAL.DAL mr = new DAL.DAL();   
            UtilConstants.mssqldb = ConfigurationManager.AppSettings.Get("strMSSqlDBPD") + ConfigurationManager.AppSettings.Get("useridpasswordKey");
            string strSQLaccess = "SELECT * from [Users_Details] where email_id = '" + struname.ToLower() + "' and Password = '" + strpwd.ToLower() + "'";
            DataSet ds = new DataSet();
            try
            {

                using (SqlConnection conn = new SqlConnection(UtilConstants.mssqldb))
                {
                    conn.Open();
                    //OracleCommand cmd = new OracleCommand();
                    //ds = mr.checkpermissions(struname);
                    SqlCommand cmd = new SqlCommand(strSQLaccess, conn);
                    cmd.CommandType = CommandType.Text;
                    SqlDataAdapter oda = new SqlDataAdapter();
                    oda.SelectCommand = cmd;
                    oda.Fill(ds);

                    if (ds.Tables[0].Rows.Count == 1)
                    {
                        TabPermission.CreateRptMemTab = true;// Convert.ToBoolean(Convert.ToInt16(ds.Tables[0].Rows[0]["CreateRptMem"]));
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }


            }
            catch (Exception ex)
            {
                string strReturn = "Generic Database Error: " + ex.Message;
                MessageBox.Show("Generic Database Error: " + ex.Message + ": " + ex.StackTrace);
                return false;
            }
        }

        private void verifypass()
        {
            Assembly ass = Assembly.GetEntryAssembly();
            System.Windows.Controls.ComboBoxItem curItem = ((System.Windows.Controls.ComboBoxItem)cbEnvironment.SelectedItem);
            UtilConstants.sEnvironment = curItem.Content.ToString();
            UtilConstants.UserID = txtUserName.Text;
            bool yn = false;

            if (UtilConstants.sEnvironment == "PRODUCTION")
            {
                UtilConstants.mssqldb = ConfigurationManager.AppSettings.Get("strMSSqlDBPD") + ConfigurationManager.AppSettings.Get("useridpasswordKey");
                UtilConstants.myPrinter = ConfigurationManager.AppSettings.Get("ReportPrinter");
            }
            else if (UtilConstants.sEnvironment == "TEST")
            {
                UtilConstants.mssqldb = ConfigurationManager.AppSettings.Get("strMSSqlDB") + "User Id=" + txtUserName.Text + ";Password=" + txtPassword.Password.ToUpper() + ";";
                UtilConstants.myPrinter = ConfigurationManager.AppSettings.Get("ReportPrinter");
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(UtilConstants.mssqldb))
                {
                    conn.Open();
                    yn = true;
                    //gUname = txtUserName.Text;
                    //gPaswd = txtPassword.Password;

                    if (UtilConstants.sEnvironment == "PRODUCTION")
                    {
                        UtilConstants.strsqlenv = ConfigurationManager.AppSettings.Get("strMSSqlDBPD");
                    }
                    else if (UtilConstants.sEnvironment == "TEST")
                    {
                        UtilConstants.strsqlenv = ConfigurationManager.AppSettings.Get("strMSSqlDBPD");
                    }

                }
            }
            catch (Exception ex)
            {
                yn = false;
                //yn = true;
                //strReturn = "Generic Database Error: " + ex.Message;
                //System.Windows.MessageBox.Show("Generic Database Error: " + ex.Message);
                lblError.Content = "Generic Database Error: " + ex.Message;
            }
            if (UtilConstants.sEnvironment == "PRODUCTION")
            {
                TabPermission.CreateRptMemTab = true;
                //yn = checkpermissions(txtUserName.Text, txtPassword.Password);
            }
            else if (UtilConstants.sEnvironment == "TEST")
            {
                TabPermission.CreateRptMemTab = true;
             //   yn = checkpermissions(txtUserName.Text, txtPassword.Password);
            }




            ///===============================development===================================================================
            yn = true;// checkpermissions(txtUserName.Text, txtPassword.Password);
            if (yn)
            {

                //ReportForm rf = new ReportForm();
                //rf.Show();

                //------------------------------
                //setting environment
                //------------------------------
                setenv.checkandsetenv();
                //------------------------------
                ReportFormRibbon rfb = new ReportFormRibbon();
                rfb.Show();
                this.Close();
            }
            else
            {
                lblError.Content = lblError.Content + "UserName/Password combination is not correct. Please re-enter the correct information.";
                lblError.Visibility = Visibility.Visible;
            }
        }


        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && txtUserName.Text != null && txtPassword.Password != null)
            {
                //Whatever code you want if enter key is pressed goes here
                verifypass();
            }
        }
        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Uri.AbsoluteUri);
        }

  
    }
}
