﻿using System;
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
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Configuration;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Net;
using System.Xml.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using System.Xml.Serialization;
using System.Data.OracleClient;
using System.Collections;

namespace AutoCodeGenerator.Views
{
    /// <summary>
    /// Interaction logic for StatementMun.xaml
    /// </summary>
    public partial class GenerateCsharpDAL : UserControl
    {
        private BackgroundWorker _worker;
        public static string PackageOwner;
        public static string applicationPath;

        public GenerateCsharpDAL()
        {
            InitializeComponent();
            string path = System.AppDomain.CurrentDomain.BaseDirectory;//System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location); 
            applicationPath = path.Replace(@"\bin\Debug\", ""); //D:\working\CodeGenerator\AutoCodeGenerator\bin\Debug\

        }
        private void ValidateUSPSAddress_loaded(object sender, EventArgs e)
        {
            setenv.checkandsetenv();
            FillAddress();
        }




        private void createstmtemp_Click(object sender, RoutedEventArgs e)
        {
            _worker = new BackgroundWorker();
            _worker.WorkerReportsProgress = true;
            _worker.WorkerSupportsCancellation = true;

            _worker.DoWork += delegate (object s, DoWorkEventArgs args)
            {
                BackgroundWorker worker = s as BackgroundWorker;

                //for (int i = 0; i < 10; i++)
                //{
                if (worker.CancellationPending)
                {
                    args.Cancel = true;
                    return;
                }

                //Thread.Sleep(1000);
                CreateReportsEmp(args);
                //worker.ReportProgress(i + 1);
                //}
            };

            _worker.ProgressChanged += delegate (object s, ProgressChangedEventArgs args)
            {
                _progressBaremp.Value = args.ProgressPercentage;
                _progressBarEmplbl.Content = args.UserState.ToString();
            };

            _worker.RunWorkerCompleted += delegate (object s, RunWorkerCompletedEventArgs args)
            {
                createstmtemp.IsEnabled = true;
                _btnCancelemp.IsEnabled = false;
                _progressBaremp.Value = 0;
            };

            _worker.RunWorkerAsync();
            createstmtemp.IsEnabled = false;
            _btnCancelemp.IsEnabled = true;

        }

        private void _btnCancelemp_Click(object sender, RoutedEventArgs e)
        {
            _worker.CancelAsync();
        }

        private int GetMax()
        {
            int max = 0;
            if (listBox4.Items.Count == 0)
            {
                max = cbrptbymem.Items.Count;
            }
            else
            {
                max = cbrptbymem.Items.Count;
            }
            return max;
        }

        private ArrayList GetItems()
        {
            ArrayList items = default(ArrayList);
            if (listBox4.Items.Count == 0)
            {
                items = new ArrayList(listBox4.Items);
            }
            else
            {
                items = new ArrayList(listBox4.Items);
            }
            return items;
        }

        private void CreateReportsEmp(DoWorkEventArgs e)
        {

            string a = "";
            Dispatcher.Invoke(new Action(delegate
            {
                a = cbrptbymem.SelectedValue.ToString();
            }));



            int max = GetMax();
            ArrayList items = GetItems();
            BE dc = GetBE(a);
            // this holds all generated output 
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i <= GetMax() - 1; i++)
            {

                dc.CodeTypeToGenerate = Enums.CodeType.BusinessEntity;
                GenerateCSharpCode(dc, items[i].ToString(), max, sb);

                //dc.CodeTypeToGenerate = Enums.CodeType.DataAccess;
                //GenerateCSharpCode(dc, items[i].ToString(), max, sb);


                //if (ConfigurationManager.AppSettings.Get("cacheImplementation").Equals("off", StringComparison.InvariantCultureIgnoreCase))
                //{
                //    dc.CodeTypeToGenerate = Enums.CodeType.BusinessObject;
                //}
                //else
                //{
                //    dc.CodeTypeToGenerate = Enums.CodeType.BusinessObjectCached;
                //}

                //GenerateCSharpCode(dc, items[i].ToString(), max, sb);

                dc.CodeTypeToGenerate = Enums.CodeType.IServiceCallCode;
                GenerateCSharpCode(dc, items[i].ToString(), max, sb);

                dc.CodeTypeToGenerate = Enums.CodeType.ServiceCallCode;
                GenerateCSharpCode(dc, items[i].ToString(), max, sb);


                if (ConfigurationManager.AppSettings.Get("cacheImplementation").Equals("off", StringComparison.InvariantCultureIgnoreCase))
                {
                    max = 0;
                }
                //GenerateAppSettingDuration(dc, items, max, sb);
            }

        }


        private void GenerateAppSettingDuration(BE dc, ArrayList items, int max, StringBuilder allCode)
        {
            //StringBuilder sb = new StringBuilder();
            //// this holds code generated in this pass 
            //string className = BO.GetClassName(dc) + "BO.";

            //for (int i = 0; i <= max; i++)
            //{
            //    dc.Package = items.i items.IndexOf(i);
            //    string methodName = BO.GetClassName(dc);
            //    sb.AppendLine("<add key=" + Strings.Chr(34) + className + methodName + ".CacheDuration" + Strings.Chr(34) + " value=" + Strings.Chr(34) + ConfigurationManager.AppSettings("defaultCacheDuration") + Strings.Chr(34) + " />");
            //    ProgressBar1.Value = i + 1;
            //}

            //WriteTheFile(ConfigurationManager.AppSettings("codeOutputFolder") + className + "config", sb);

            //allCode.Append(sb.ToString);
        }
        private BE GetBE(string selectedval)
        {
            // Get the package owner and package name 
            string[] sep = { "." };
            string[] arr = selectedval.ToString().Split(sep, StringSplitOptions.RemoveEmptyEntries);
            // create new business entity  using owner and package
            BE dc = new BE(arr[0], arr[1]);
            dc.ConnectionString = System.Configuration.ConfigurationManager.AppSettings["connectionString"];
            dc.StandardAbbreviations = GetAbbreviations();
            dc.InterfaceName = System.Configuration.ConfigurationManager.AppSettings["interface"];
            return dc;
        }

        private DataSet GetAbbreviations()
        {
            DataSet abbreviations = new DataSet();
            //abbreviations.ReadXml(ConfigurationManager.AppSettings("standardAbbreviations"))
            abbreviations.ReadXml(applicationPath + "\\standardabbreviations.xml");  //D:\working\CodeGenerator\AutoCodeGenerator\bin\Debug
            return abbreviations;
        }

        private void GenerateCSharpCode(BE dc, string sp_name, int spno, StringBuilder allCode)
        {
            applicationPath = System.AppDomain.CurrentDomain.BaseDirectory.Replace(@"\bin\Debug\", "");
            applicationPath = applicationPath.Replace(@"Generator\AutoCodeGenerator", "");
            applicationPath = applicationPath.Replace(@"Generator\\AutoCodeGenerator", "");
            applicationPath = applicationPath + "Projects\\" ;

            string foldername = "";
            if (dc.CodeTypeToGenerate.ToString() == "BusinessEntity")
            {
                foldername = "BusinessEntities";
            }
            else if (dc.CodeTypeToGenerate.ToString() == "BusinessObjects")
            {
                foldername = "BusinessObjects";
            }
            else if (dc.CodeTypeToGenerate.ToString() == "ServiceCallCode")
            {
                foldername = "BusinessEntities";
            }
            else if (dc.CodeTypeToGenerate.ToString() == "BusinessObjectCached")
            {
                foldername = "BusinessObjects";
            }
            else if (dc.CodeTypeToGenerate.ToString() == "DataAccess")
            {

                foldername = "DataAccessLayer";
            }
            else if (dc.CodeTypeToGenerate.ToString() == "IServiceCallCode")
            {

                foldername = "BusinessEntities";
            }
            //applicationPath = applicationPath + foldername + "\\";


            string[] arr = sp_name.Split('_');
            dc.Package = sp_name;//"EzRide_RDR";//arr[1];
            //dc.CodeTypeToGenerate = Enums.CodeType.BusinessEntity;

            StringBuilder sb = new StringBuilder();  // this holds code generated in this pass 
            string classBaseName = BO.GetClassName(dc);
            string className = "";

            //sb.Append("//************************************************************");
            //sb.Append("//*                New Code Block                            *");
            //sb.Append("//************************************************************");

            switch (dc.CodeTypeToGenerate)
            {
                //case Enums.CodeType.BusinessObject:
                //    AddAWholeFile(applicationPath + @"\CsharpBOImports.txt", ref sb);
                //    className = classBaseName + "BO";
                //    sb.AppendLine("public class " + className);
                //    sb.AppendLine("{");
                //    break;
                //case Enums.CodeType.BusinessObjectCached:
                //    AddAWholeFile(applicationPath + @"\CsharpBOImports.txt", ref sb);
                //    className = classBaseName + "BO";
                //    sb.AppendLine("public class " + className);
                //    sb.AppendLine("{");
                //    break;
                //case Enums.CodeType.DataAccess:
                //    AddAWholeFile(applicationPath + @"\CsharpBOImports.txt", ref sb);
                //    className = classBaseName + "DO";
                //    sb.AppendLine("");
                //    sb.AppendLine("public class " + className);
                //    sb.AppendLine("{");
                //    break;
                case Enums.CodeType.IServiceCallCode:
                    className = "I" + classBaseName + "DataService";
                    sb.AppendLine("using System.ServiceModel;");
                    sb.AppendLine("[ServiceContract()]");
                    sb.AppendLine("public interface " + className + dc.InterfaceName);
                    sb.AppendLine("{");
                    sb.AppendLine("#region " + className + " Methods");
                    break;
                case Enums.CodeType.ServiceCallCode:
                    className = classBaseName + "DataService";
                    sb.AppendLine("using System;");
                    //sb.AppendLine("using PA.DPW.PACSES.Utilities;");
                    sb.AppendLine("public class " + className + "DataService");
                    sb.Append(": "+ " I" + className + dc.InterfaceName);
                    sb.AppendLine("{");
                    sb.AppendLine("#region " + className + " Methods");
                    break;
                case Enums.CodeType.BusinessEntity:
                    sb.AppendLine("using System.Runtime.Serialization;");
                    sb.AppendLine("using System;");
                    sb.AppendLine("using System.Data;");
                    className = classBaseName + "BE";
                    sb.AppendLine("namespace " + className);
                    sb.AppendLine("{");
                    break;
                default:
                    className = "UnknownCodeType";
                    break;

            }

            //for (int i = 0; i <= max; i++)
            //{
             dc.StoredProcedure = dc.Package;
            GenerateCSharpCode1(dc,ref sb);
            //    ProgressBar1.Value = i + 1;
            //    _with1.Append(dc.CodeOutput);
            //}

            switch (dc.CodeTypeToGenerate)
            {
                case Enums.CodeType.IServiceCallCode:
                    sb.AppendLine("#endregion");
                    sb.AppendLine("}");
                    break;
                //case Enums.CodeType.DataAccess:
                //    AddAWholeFile(applicationPath + "\\CsharpparameterConversionFunctions.txt", ref sb);
                //    sb.AppendLine("}");
                //    break;
                //case Enums.CodeType.BusinessObject:
                //    sb.AppendLine("}");
                //    break;
                //case Enums.CodeType.BusinessObjectCached:
                //    AddAWholeFile(applicationPath + "\\CsharpcacheKeyHelper.txt", ref sb);
                //    sb.AppendLine("}");
                //    break;
                case Enums.CodeType.ServiceCallCode:
                    sb.AppendLine("#endregion");
                    sb.AppendLine("}");
                    break;
                case Enums.CodeType.BusinessEntity:
                    sb.AppendLine("}");
                    break;
                default:
                    break;
            }




            //applicationPath = path.Replace(@"\bin\Debug\", "");
            //applicationPath = applicationPath.Replace(@"Generator\AutoCodeGenerator", "");
            //applicationPath = applicationPath.Replace(@"Generator\\AutoCodeGenerator", "");
            //applicationPath = applicationPath + "Projects\\" + foldername + "\\";

            applicationPath = applicationPath + foldername + "\\";
            FileInfo fileInfo = new FileInfo(applicationPath + className + ".cs");

            if (!fileInfo.Exists)
            {

                // Create the file.
                using (FileStream fs = File.Create(applicationPath + className + ".cs"))
                {

                }
            }

            WriteTheFile(applicationPath + "" + className + ".cs", sb);

            //allCode.Append(sb.ToString);

        }

        public static void GenerateCSharpCode1(BE dc,ref StringBuilder sb)
        {
            OracleConnection conn = new OracleConnection(UtilConstants.mssqldb);
            conn.Open();
            OracleCommand cmd = BO.GetOracleCommand(conn, dc);

            BO.methodName = BO.GetMethodName(dc.StoredProcedure.ToLower());
            BO.entityName = BO.GetMethodName(dc.Package) + "BE." + BO.methodName + "BE";
            switch (dc.CodeTypeToGenerate)
            {
                case Enums.CodeType.BusinessEntity:
                    BO.WriteCSharpBusinessEntity(cmd, sb, dc);
                    break;
                //case Enums.CodeType.BusinessObject:
                //    WriteCSharpBusinessObject(cmd, sb, dc);
                //    break;
                //case Enums.CodeType.BusinessObjectCached:
                //    WritecSharpBusinessObjectCached(cmd, sb, dc);
                //    break;
                case Enums.CodeType.IServiceCallCode:
                    BO.WriteCSharpIServiceInterface(cmd,ref sb, dc);
                    break;
                case Enums.CodeType.ServiceCallCode:
                    BO.WriteCSharpServiceMethods(cmd, sb, dc);
                    break;
                //case Enums.CodeType.DataAccess:
                //    WriteCSharpDataAccessCode(cmd, sb, dc);
                //    break;
                default:
                    break;
                    // forget it 
            }

            conn.Close();
            //dc.CodeOutput = sb.ToString;
        }

        private void AddAWholeFile(string fileName, ref StringBuilder sb)
        {
            StreamReader rdr = new StreamReader(fileName);
            sb.Append(rdr.ReadToEnd());
            rdr.Close();
        }

        private void WriteTheFile(string fileName, StringBuilder sb)
        {
            //Dim wrtr As StreamWriter = New StreamWriter(fileName)
            StreamWriter wrtr = null;
            try
            {
                wrtr = new StreamWriter(fileName);
                wrtr.Write(sb.ToString());
                wrtr.WriteLine(string.Empty);
                wrtr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                wrtr = null;
            }
            //wrtr.Write(sb.ToString)
            //wrtr.WriteLine(String.Empty)
            //wrtr.Close()
            //wrtr = Nothing
        }

        private void FillAddress()
        {

            DataSet ds = new DataSet();
            try
            {


                using (OracleConnection objConn = new OracleConnection(UtilConstants.mssqldb))
                {
                    OracleCommand objCmd = new OracleCommand();
                    objCmd.Connection = objConn;
                    objCmd.CommandText = "pkg_genr_data_access_layer.usp_get_pkge_list";
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("p_cur_ref", OracleType.Cursor).Direction = ParameterDirection.Output;


                    objConn.Open();
                    OracleDataAdapter da = new OracleDataAdapter(objCmd);
                    DataSet ds1 = new DataSet();
                    da.Fill(ds1);
                    if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
                    {
                        int i = 0;
                        for (i = 0; i < ds1.Tables[0].Rows.Count; i++)
                        {
                            cbrptbymem.Items.Add(ds1.Tables[0].Rows[i][0].ToString());
                        }
                    }


                    objConn.Close();
                }


            }
            catch (Exception ex)
            {
                string strReturn = "Generic Database Error: " + ex.Message;
                MessageBox.Show("Generic Database Error: " + ex.Message + ": " + ex.StackTrace);
                //return false;
            }
        }




        #region Report by Member - list mgmt
        private void addonemem_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < this.listBox3.SelectedItems.Count; i++)
            {
                this.listBox4.Items.Add(this.listBox3.SelectedItems[i]);//listBox1.Items[this.listBox1.SelectedItems[i].]);
            }
            for (int i = 0; i < this.listBox4.Items.Count; i++)
            {
                this.listBox3.Items.Remove(listBox4.Items[i]);
            }
        }
        private void removeonemem_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < this.listBox4.SelectedItems.Count; i++)
            {
                this.listBox3.Items.Add(this.listBox4.SelectedItems[i]);//listBox2.Items[this.listBox2.SelectedItems[i]]);
            }
            for (int i = 0; i < this.listBox3.Items.Count; i++)
            {
                this.listBox4.Items.Remove(listBox3.Items[i]);
            }
        }
        private void addallmem_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < this.listBox3.Items.Count; i++)
            {
                this.listBox4.Items.Add(listBox3.Items[i]);
            }
            for (int i = 0; i < this.listBox4.Items.Count; i++)
            {
                this.listBox3.Items.Remove(listBox4.Items[i]);
            }
        }
        private void removeallmem_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < this.listBox4.Items.Count; i++)
            {
                this.listBox3.Items.Add(listBox4.Items[i]);
            }
            for (int i = 0; i < this.listBox3.Items.Count; i++)
            {
                this.listBox4.Items.Remove(listBox3.Items[i]);
            }
        }
        #endregion


        private void btnsubmit_OnClick(object sender, RoutedEventArgs e)
        {

            try
            {


                using (OracleConnection objConn = new OracleConnection(UtilConstants.mssqldb))
                {
                    OracleCommand objCmd = new OracleCommand();
                    objCmd.Connection = objConn;
                    objCmd.CommandText = "pkg_genr_data_access_layer.usp_get_pkge_sp";
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("p_cur_ref", OracleType.Cursor).Direction = ParameterDirection.Output;
                    objCmd.Parameters.Add("p_nam_owner", OracleType.VarChar, 30).Value = DBNull.Value.ToString();
                    objCmd.Parameters.Add("p_nam_pkge", OracleType.VarChar, 30).Value = DBNull.Value.ToString();

                    objConn.Open();
                    OracleDataAdapter da = new OracleDataAdapter(objCmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        int i = 0;
                        for (i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            listBox3.Items.Add(ds.Tables[0].Rows[i][1].ToString());
                        }
                    }
                    objConn.Close();
                }


            }
            catch (Exception ex)
            {
                string strReturn = "Generic Database Error: " + ex.Message;
                MessageBox.Show("Generic Database Error: " + ex.Message + ": " + ex.StackTrace);
                //return false;
            }

        }

        //private void GenerateCSharpCode_old(BE dc, ArrayList items, int max, StringBuilder allCode)
        //{
        //    dc.CodeTypeToGenerate = Enums.CodeType.API;

        //    StringBuilder sb = new StringBuilder();  // this holds code generated in this pass 
        //    string classBaseName = BO.GetClassName(dc);
        //    string className = "";

        //    var _with1 = sb;
        //    _with1.AppendLine("//************************************************************");
        //    _with1.AppendLine("//*                New Code Block                            *");
        //    _with1.AppendLine("//************************************************************");
        //    switch (dc.CodeTypeToGenerate)
        //    {
        //        case Enums.CodeType.API:
        //            className = classBaseName + "DataService";
        //            _with1.AppendLine("using System;");
        //            _with1.AppendLine("using PA.DPW.PACSES.Utilities;");
        //            _with1.AppendLine("public class " + className + "Controller");
        //            _with1.Append(": " + dc.InterfaceName);
        //            _with1.AppendLine("{");
        //            _with1.AppendLine("#region " + className + " Methods" + "");
        //            break;

        //    }

        //    //for (int i = 0; i <= max; i++)
        //    //{
        //    //    dc.StoredProcedure = items(i).ToString;
        //    //    BO.GenerateCSharpCode(dc);
        //    //    ProgressBar1.Value = i + 1;
        //    //    _with1.Append(dc.CodeOutput);
        //    //}

        //    switch (dc.CodeTypeToGenerate)
        //    {
        //        case Enums.CodeType.API:
        //            _with1.AppendLine("#endregion");
        //            _with1.AppendLine("}");
        //            break;
        //        default:
        //            break;
        //    }

        //    WriteTheFile(applicationPath + "" + className + ".cs", sb);
        //    //allCode.Append(sb.ToString);

        //}
    }
}
