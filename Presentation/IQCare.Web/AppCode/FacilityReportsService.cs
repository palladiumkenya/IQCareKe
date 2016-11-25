using Application.Presentation;
using Interface.Reports;
using System;
using System.Data;
using System.Web.Services;
namespace IQCare.Web
{

    /// <summary>
    /// Summary description for FacilityReportsService
    /// </summary>
    [WebService(Namespace = "http://www.fgi.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class FacilityReportsService : System.Web.Services.WebService
    {

        public FacilityReportsService()
        {

            //Uncomment the following line if using designed components 
            //InitializeComponent(); 
        }

        [WebMethod]
        public DataSet GetFacilityPatientsCostPerMonth(DateTime TransactionStartDate, DateTime TransactionEndDate)
        {
            IReports ReportDetails = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports,BusinessProcess.Reports");
            return ReportDetails.GetFacilityPatientsCostPerMonth(TransactionStartDate, TransactionEndDate);
        }

        [WebMethod]
        public DataSet GetFacilityAvgCD4CostPerPatient(DateTime TransactionStartDate, DateTime TransactionEndDate)
        {
            IReports ReportDetails = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports,BusinessProcess.Reports");
            return ReportDetails.GetFacilityAvgCD4CostPerPatient(TransactionStartDate, TransactionEndDate);
        }

        [WebMethod]
        public DataSet GetFacilityAvgExcludingCD4CostPerPatient(DateTime TransactionStartDate, DateTime TransactionEndDate)
        {
            IReports ReportDetails = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports,BusinessProcess.Reports");
            return ReportDetails.GetFacilityAvgExcludingCD4CostPerPatient(TransactionStartDate, TransactionEndDate);
        }

        [WebMethod]
        public DataSet GetFacilityTotalAvgCostofARVandOIPerPatientPerMonth(DateTime TransactionStartDate, DateTime TransactionEndDate)
        {
            IReports ReportDetails = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports,BusinessProcess.Reports");
            return ReportDetails.GetFacilityTotalAvgCostofARVandOIPerPatientPerMonth(TransactionStartDate, TransactionEndDate);
        }

        [WebMethod]
        public DataSet GetFacilityCumulAvgCostofARVandOIPerPatientPerMonth(DateTime TransactionStartDate, DateTime TransactionEndDate)
        {
            IReports ReportDetails = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports,BusinessProcess.Reports");
            return ReportDetails.GetFacilityCumulAvgCostofARVandOIPerPatientPerMonth(TransactionStartDate, TransactionEndDate);
        }

        [WebMethod]
        public DataSet GetFacilityTotalCostLostToFollowup(DateTime TransactionStartDate, DateTime TransactionEndDate)
        {
            IReports ReportDetails = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports,BusinessProcess.Reports");
            return ReportDetails.GetFacilityTotalCostLostToFollowup(TransactionStartDate, TransactionEndDate);
        }

        [WebMethod]
        public DataSet GetFacilityCumTotalCostLostToFollowup(DateTime TransactionStartDate, DateTime TransactionEndDate)
        {
            IReports ReportDetails = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports,BusinessProcess.Reports");
            return ReportDetails.GetFacilityCumTotalCostLostToFollowup(TransactionStartDate, TransactionEndDate);
        }

        [WebMethod]
        public DataSet GetFacilityAvgCostCovByProgramAndPatient(DateTime TransactionStartDate, DateTime TransactionEndDate)
        {
            IReports ReportDetails = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports,BusinessProcess.Reports");
            return ReportDetails.GetFacilityAvgCostCovByProgramAndPatient(TransactionStartDate, TransactionEndDate);
        }

        [WebMethod]
        public DataSet GetFacilityCumCostCovByProgramAndPatient(DateTime TransactionStartDate, DateTime TransactionEndDate)
        {
            IReports ReportDetails = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports,BusinessProcess.Reports");
            return ReportDetails.GetFacilityCumCostCovByProgramAndPatient(TransactionStartDate, TransactionEndDate);
        }

        [WebMethod]
        public DataSet GetPatientDebitNoteTotalCostByMonth(Int32 PatientId)
        {
            IReports ReportDetails = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports,BusinessProcess.Reports");
            return ReportDetails.GetPatientDebitNoteTotalCostByMonth(PatientId);
        }

    }

}