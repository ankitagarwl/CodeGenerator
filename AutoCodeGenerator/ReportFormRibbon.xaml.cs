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
using System.Windows.Shapes;
using System.ComponentModel;
using Microsoft.Windows.Controls.Ribbon;
using AutoCodeGenerator.Views;

namespace AutoCodeGenerator
{
    /// <summary>
    /// Interaction logic for ReportFormRibbon.xaml
    /// </summary>
    public partial class ReportFormRibbon : Window
    {
        //public static SetUp setup = new SetUp();
        public static GenerateCsharpDAL generatecsharpdal = new GenerateCsharpDAL();
        
        //public static EmployerDocuments empdocform = new EmployerDocuments();
        //public static CalcDetails calcform = new CalcDetails();        
        //public static GASBRpt gasbrpt = new GASBRpt();
        //public static ReporttoCPAS rpttocpas = new ReporttoCPAS();
        //public static BatchAutomation batchautomation = new BatchAutomation();

        //public static DocstoCPAS docstoCPAS = new DocstoCPAS();
        //public static ActivateMun activatemum = new ActivateMun();
        //public static ActivateMem activatemem = new ActivateMem();
        //public static GetFile getfile = new GetFile();
        //public static TabDocument tabdocument = new TabDocument();
        //public static UpdateDocuments updatedocuments = new UpdateDocuments();
        //public static addlistofdocs addlistofdoc = new addlistofdocs();
        //public static QuarterValidateData qtrvalidatedata = new QuarterValidateData();
        //public static QuarterValidateWithNewMemberData qtrvalidatewithnewmemdata = new QuarterValidateWithNewMemberData();
        //public static QuarterTran qtrtran = new QuarterTran();
        //public static RankMsg rankmsg = new RankMsg();

        //public static ADMIN admin = new ADMIN();
        //public static AddNews addnews = new AddNews();
        //public static UpdateNews updatenews = new UpdateNews();
        //public static AddEvent addevent = new AddEvent();
        //public static UpdateEvent updateevent = new UpdateEvent();

        public ReportFormRibbon()
        {
            InitializeComponent();
            DependencyPropertyDescriptor.FromProperty(Ribbon.IsMinimizedProperty, typeof(Ribbon)).AddValueChanged(mribbon, (o, args) => mribbon.IsMinimized = false);
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            //setenv.checkandsetenv();

        }
        private void ReportFormRibbon_Load(object sender, EventArgs e)
        {
            Title = setenv.strTitle;
            label1.Content = setenv.strTitle;
            memberservice.Visibility = (TabPermission.CreateRptMemTab ? Visibility.Visible : Visibility.Hidden);
        }
        private void ribbonBtn_Click(object sender, RoutedEventArgs e)
        {

            RibbonButton rb = sender as RibbonButton;
            string str = rb.Name;
            ccReportFormRibbon.Content = null;
            //ccReportFormRibbon.Dispose();
        
            if (string.Equals(rb.Name, "ValidateUSPSAddress"))
            {
                ccReportFormRibbon.Content = generatecsharpdal;
            }
            
            //else if (string.Equals(rb.Name, "SetUp"))
            //{
            //    ccReportFormRibbon.Content = setup;ValidateUSPSAddress
            //}

        }
        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            //System.Diagnostics.Process.Start(e.Uri.AbsoluteUri);
        }

    }
}
