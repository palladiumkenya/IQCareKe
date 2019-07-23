using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IQCare.CCC.UILogic.Reporting;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Office.Excel;
using System.IO;

namespace IQCare.Web.CCC.Patient
{
    public partial class PatientLinelist : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpContext.Current.Session["qtrace"] = Request.QueryString["q"].ToString();
            string question = HttpContext.Current.Session["qtrace"].ToString();
            HttpContext.Current.Session["qfrom"] = Request.QueryString["qfrom"].ToString();
            HttpContext.Current.Session["qto"] = Request.QueryString["qto"].ToString();
        }

        [System.Web.Services.WebMethod(EnableSession = true)]
        public static string SetSelectedPatient(int patientId, int personId)
        {
            HttpContext.Current.Session["PatientPK"] = patientId;
            HttpContext.Current.Session["PersonId"] = personId;
            HttpContext.Current.Session["PatientTrace"] = "yes";
            HttpContext.Current.Session["PatientInformation"] = null;
            return "success";
        }

        public static String GetTimestamp(DateTime value)
        {
            return value.ToString("yyyyMMddHHmmss");
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            string reportingdatefrom = Request.QueryString["qfrom"].ToString();
            string reportingdateto = Request.QueryString["qto"].ToString();
            string question = Request.QueryString["q"].ToString();
            if(question == "txcurr")
            {
                generateTxCurrExcell(reportingdatefrom, reportingdateto);
            }
            else if (question == "firstdefaulters")
            {
                generateFirstDefaultersExcell(reportingdatefrom, reportingdateto);
            }
            else if (question == "seconddefaulters")
            {
                generateSecondDefaultersExcell(reportingdatefrom, reportingdateto);
            }
            else if (question == "ltfu")
            {
                generateLTFUExcell(reportingdatefrom, reportingdateto);
            }
            else
            {

            }
        }

        protected void generateTxCurrExcell(string reportingdatefrom, string reportingdateto)
        {
            ReportingResultsManager reportingLogic = new ReportingResultsManager();
            //int txcurrcount = 0;
            DataTable dt = reportingLogic.gettxcurr(Convert.ToDateTime(reportingdateto));
            //IQWebUtils theUtils = new IQWebUtils();
            //theUtils.ExporttoExcel(dt, Response);
            using (XLWorkbook wb = new XLWorkbook())
            {
                String timeStamp = GetTimestamp(DateTime.Now);
                var ws = wb.Worksheets.Add(dt, "txcurr");
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.Charset = "";
                HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + "txcurr" + timeStamp + ".xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(HttpContext.Current.Response.OutputStream);
                    HttpContext.Current.Response.Flush();
                    HttpContext.Current.Response.End();
                }
            }
        }
        protected void generateFirstDefaultersExcell(string reportingdatefrom, string reportingdateto)
        {
            ReportingResultsManager reportingLogic = new ReportingResultsManager();
            DataTable dt = reportingLogic.getfirstdefaulters(Convert.ToDateTime(reportingdateto), 1, 30);
            //IQWebUtils theUtils = new IQWebUtils();
            //theUtils.ExporttoExcel(dt, Response);
            using (XLWorkbook wb = new XLWorkbook())
            {
                String timeStamp = GetTimestamp(DateTime.Now);
                var ws = wb.Worksheets.Add(dt, "1to30days");
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.Charset = "";
                HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + "defaulters" + timeStamp + ".xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(HttpContext.Current.Response.OutputStream);
                    HttpContext.Current.Response.Flush();
                    HttpContext.Current.Response.End();
                }
            }
        }

        protected void generateSecondDefaultersExcell(string reportingdatefrom, string reportingdateto)
        {
            ReportingResultsManager reportingLogic = new ReportingResultsManager();
            DataTable dt = reportingLogic.getseconddefaulters(Convert.ToDateTime(reportingdateto), 31, 90);
            //IQWebUtils theUtils = new IQWebUtils();
            //theUtils.ExporttoExcel(dt, Response);
            using (XLWorkbook wb = new XLWorkbook())
            {
                String timeStamp = GetTimestamp(DateTime.Now);
                var ws = wb.Worksheets.Add(dt, "31to90days");
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.Charset = "";
                HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + "Defaulters" + timeStamp + ".xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(HttpContext.Current.Response.OutputStream);
                    HttpContext.Current.Response.Flush();
                    HttpContext.Current.Response.End();
                }
            }
        }

        protected void generateLTFUExcell(string reportingdatefrom, string reportingdateto)
        {
            ReportingResultsManager reportingLogic = new ReportingResultsManager();
            DataTable dt = reportingLogic.getltfu(Convert.ToDateTime(reportingdatefrom), Convert.ToDateTime(reportingdateto));
            //IQWebUtils theUtils = new IQWebUtils();
            //theUtils.ExporttoExcel(dt, Response);
            using (XLWorkbook wb = new XLWorkbook())
            {
                String timeStamp = GetTimestamp(DateTime.Now);
                var ws = wb.Worksheets.Add(dt, "ltfu");
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.Charset = "";
                HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + "LTFU" + timeStamp + ".xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(HttpContext.Current.Response.OutputStream);
                    HttpContext.Current.Response.Flush();
                    HttpContext.Current.Response.End();
                }
            }
        }
    }
}