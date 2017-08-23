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

namespace AutoCodeGenerator.Views
{
    /// <summary>
    /// Interaction logic for StatementMun.xaml
    /// </summary>
    public partial class GenerateCsharpDAL : UserControl
    {
        private BackgroundWorker _worker;
     
        public GenerateCsharpDAL()
        {
            InitializeComponent();

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
            #region commentedcode
            //_worker = new BackgroundWorker();
            //_worker.WorkerReportsProgress = true;
            //_worker.WorkerSupportsCancellation = true;

            //_worker.DoWork += delegate(object s, DoWorkEventArgs args)
            //{
            //    BackgroundWorker worker = s as BackgroundWorker;

            //    //for (int i = 0; i < 10; i++)
            //    //{
            //    if (worker.CancellationPending)
            //    {
            //        args.Cancel = true;
            //        return;
            //    }

            //    //Thread.Sleep(1000);
            //    CreateReportsEmp(args);
            //    //worker.ReportProgress(i + 1);
            //    //}
            //};

            //_worker.ProgressChanged += delegate(object s, ProgressChangedEventArgs args)
            //{
            //    _progressBaremp.Value = args.ProgressPercentage;
            //    _progressBarEmplbl.Content = args.UserState.ToString();
            //};

            //_worker.RunWorkerCompleted += delegate(object s, RunWorkerCompletedEventArgs args)
            //{
            //    createstmtemp.IsEnabled = true;
            //    _btnCancelemp.IsEnabled = false;
            //    _progressBaremp.Value = 0;
            //};

            //_worker.RunWorkerAsync();
            //createstmtemp.IsEnabled = false;
            //_btnCancelemp.IsEnabled = true;
            #endregion
        }

        private void _btnCancelemp_Click(object sender, RoutedEventArgs e)
        {
             _worker.CancelAsync();
        }

    
        private void CreateReportsEmp(DoWorkEventArgs e)
        {

        }
 
    

        private void FillAddress()
        {
            
            DataSet ds = new DataSet();
            try
            {
              //  string oradb = "Data Source=(DESCRIPTION="
              //+ "(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.0.11)(PORT=1521)))"
              //+ "(CONNECT_DATA=(SERVER=Default)(SERVICE_NAME=ORCL)));"
              //+ "User Id=ezride;Password=ad#ujjwal;";
                string oradb = UtilConstants.mssqldb;

                string cmdtxt = "  select lower(owner || '.' || object_name) as owner_package, owner  , object_name as package from all_procedures " +
                               " where object_name like 'PKG%' and procedure_name like 'USP%'  group by owner, object_name  order by owner, object_name ";

                OracleConnection conn = new OracleConnection(oradb);
                using (OracleCommand cmd = new OracleCommand(cmdtxt, conn))
                {
                    conn.Open();

                    //reader is IDisposable and should be closed
                    using (OracleDataReader dr = cmd.ExecuteReader())
                    {
                        List<String> items = new List<String>();

                        while (dr.Read())
                        {
                            //items.Add(String.Format("{0}, {1}", dr.GetValue(0), dr.GetValue(1)));
                            cbrptbymem.Items.Add(dr.GetValue(2));
                        }
                        
                        //TB_PRODUCTS.Items.AddRange(items.ToArray());
                    }

                }

              
                conn.Close();
                conn.Dispose();

               


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
            DataSet ds = new DataSet();
            try
            {
                //  string oradb = "Data Source=(DESCRIPTION="
                //+ "(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.0.11)(PORT=1521)))"
                //+ "(CONNECT_DATA=(SERVER=Default)(SERVICE_NAME=ORCL)));"
                //+ "User Id=ezride;Password=ad#ujjwal;";
                string oradb = UtilConstants.mssqldb;

                string cmdtxt = @"select * from NAMES_PROCEDURE";

                OracleConnection conn = new OracleConnection(oradb);
                using (OracleCommand cmd = new OracleCommand(cmdtxt, conn))
                {
                    conn.Open();

                    //reader is IDisposable and should be closed
                    using (OracleDataReader dr = cmd.ExecuteReader())
                    {
                        List<String> items = new List<String>();

                        while (dr.Read())
                        {
                            //items.Add(String.Format("{0}, {1}", dr.GetValue(0), dr.GetValue(1)));
                            //cbrptbymem.Items.Add(dr.GetValue(1));
                            listBox3.Items.Add(dr.GetValue(1));
                        }

                        //TB_PRODUCTS.Items.AddRange(items.ToArray());
                    }

                }


                conn.Close();
                conn.Dispose();




            }
            catch (Exception ex)
            {
                string strReturn = "Generic Database Error: " + ex.Message;
                MessageBox.Show("Generic Database Error: " + ex.Message + ": " + ex.StackTrace);
                //return false;
            }
        }
        }
}
