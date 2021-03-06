﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Reporting.WinForms;
using System.Data.SqlClient;

namespace AutoCodeGenerator.Utilities
{
    public class RdlcReportRunner
    {
        private const string FileExtension = "pdf";
        private string rdlcReportName;

        private IEnumerable<ReportDataSource> reportData;

        private IEnumerable<ReportParameter> reportParams;

        public Dictionary<string, string> ReportParameters { get; set; }

        public string OutputPath { get; set; }

        public string ReportDataFile { get; set; }

        public string RdlcReportName { get; set; }

        public string OutputFileName
        {
            get { return rdlcReportName + "." + FileExtension; }
            set { rdlcReportName = value; }
        }

        public byte[] GenerateReport(DataTable dt)
        {
            GetReportDataFromXml();
            GetReportParametersFromDictionary();

            return RunLocalReport(dt);
        }

        private void GetReportParametersFromDictionary()
        {
            reportParams = ReportParameters.Select(parameter => new ReportParameter(parameter.Key, parameter.Value));
        }

        public async Task WriteReportToFile(byte[] renderedReportBytes)
        {
            var fullFilePath = Path.Combine(OutputPath, OutputFileName);
            if (File.Exists(fullFilePath))
            {
                File.Delete(fullFilePath);
            }

            if (renderedReportBytes != null)
            {
                using (var fs = new FileStream(fullFilePath, FileMode.Create))
                {
                    await fs.WriteAsync(renderedReportBytes, 0, renderedReportBytes.Length);
                }
            }
        }

        private void GetReportDataFromXml()
        {
            var ds = new DataSet();

            if (!string.IsNullOrEmpty(ReportDataFile))
            {
                ds.ReadXml(ReportDataFile);
            }

            if (ds.Tables.Count == 0)
            {
                return;
            }

            reportData = (from DataTable dt in ds.Tables select new ReportDataSource(dt.TableName, dt)).ToList();
        }


        private static string GetReportDeviceInfo()
        {
            //// The DeviceInfo settings should be changed based on the reportType
            //// http://msdn2.microsoft.com/en-us/library/ms155397.aspx
            return "<DeviceInfo><OutputFormat>PDF</OutputFormat></DeviceInfo>";
        }

        private byte[] RunLocalReport(DataTable dt)
        {
            using (var localReport = new LocalReport { ReportPath = RdlcReportName })
            {
                byte[] renderedBytes;
                try
                {
                    using (var textReader = new StreamReader(RdlcReportName))
                    {
                        localReport.LoadReportDefinition(textReader);
                    }

                    if (reportData != null)
                    {
                        foreach (var ds in reportData)
                        {
                            localReport.DataSources.Add(ds);
                        }
                    }

                    if (reportParams != null && reportParams.Any())
                    {
                        localReport.SetParameters(reportParams);
                    }

                    //DataTable dt = GetData(fromdate, todate, accountno);
                    //DataTable dt = GetData(DateTime.Parse("2017-07-25"), DateTime.Parse("2017-07-25"), "01-226");
                    // DataTable dt = GetData(Convert.ToDateTime("05/01/2017"), Convert.ToDateTime("05/31/2017"), "07-008P");
                    ReportDataSource rds = new ReportDataSource("DataSet1", dt);

                    localReport.DataSources.Add(rds);

                    var deviceInfo = GetReportDeviceInfo();

                    localReport.Refresh();

                    string mimeType;
                    string encoding;
                    string fileNameExtension;
                    Warning[] warnings;
                    string[] streams;
                    renderedBytes = localReport.Render(
                        "PDF",
                        deviceInfo,
                        out mimeType,
                        out encoding,
                        out fileNameExtension,
                        out streams,
                        out warnings);
                }
                catch (Exception e)
                {
                    Console.WriteLine("{0} and {1}", e.Message, e.InnerException.InnerException.Message);
                    return null;
                }

                return renderedBytes;
            }
        }

        public DataTable GetData(DateTime FromDate, DateTime ToDate, string Account_Number)
        {
            DataTable dt = new DataTable();
            string connstr = UtilConstants.mssqldb; //System.Configuration.ConfigurationManager.ConnectionStrings["EZRIDEConnectionString"].ConnectionString;
            using (SqlConnection cn = new SqlConnection(connstr))
            {
                SqlCommand cmd = new SqlCommand("SP_Report_Tran_Details", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@From", SqlDbType.DateTime).Value = FromDate;
                cmd.Parameters.Add("@To", SqlDbType.DateTime).Value = ToDate;
                cmd.Parameters.Add("@Account_Number", SqlDbType.Char).Value = Account_Number;
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);


            }
            return dt;
        }
    }
}
