using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Interface.Reports
{
    /////////////////////////////////////////////////////////////////////
    // Code Written By   : Ashok Kr. Gupta
    // Written Date      : 06th Oct 2006
    // Modification Date : 
    // Description       : 
    //
    /// /////////////////////////////////////////////////////////////////
    public interface IReports
    {
        DataSet GetPatientDetails(int PatientID, DateTime StartDate, DateTime EndDate);
        //DataSet GetDrugARVPickup(int PatientID, DateTime StartDate, DateTime EndDate, int LocationID);
        DataSet GetDrugARVPickup(Int32 PatientID, DateTime StartDate, DateTime EndDate, string SatelliteID, string CountryID, string PosID, int LocationID);
        DataSet GetAllPatientDrugARVPickup(int LocationID);
        DataSet GetMissARVPickup(DateTime StartDate, string LocationID);
        DataSet GetOGACData(DateTime StartDate, DateTime EndDate, int LocationId);
        //DataSet GetAllPatientDrugARVPickup();
        DataSet GetNewPatients(DateTime StartDate, DateTime EndDate);
        DataSet GetPregnantPatients(int Pregnant, DateTime StartDate, DateTime EndDate);
        DataSet GetUpcomingARVPickPatients(DateTime StartDate, DateTime EndDate);
        DataSet GetMisARVPickPatients(DateTime StartDate, DateTime EndDate);
        DataSet EnrollmentNoCheck(string PatientId, string LocationID, string CountryID, string PosID, string SatelliteID);

        // New Requirement
        DataSet GetARVCollectionClients(int PatientID);
        DataSet GetPatientProfileAndHistory(int PatientId);
        DataSet GetMisARVAppointClients(String SType, DateTime SDate);
        DataSet GetCDSReportData(DateTime StartDate, DateTime EndDate, int LocationId);
        DataSet GetPMTCTTrack10ReportData(DateTime StartDate, DateTime EndDate, int LocationId);
        DataSet GetCDSReportQuarterDate(int QtrID, int QtrYear);
        DataSet GetPatientEnrollMonth(DateTime Startdate, DateTime Enddate, String LocationID);
        DataTable GetMonthlyNACAReportData(DateTime DateOrderedFrom, DateTime DateOrderedTo, int LocationID);
        DataSet GetNACPMonthlyReportData(int MonthId, int Year, int LocationID);
        DataSet GetNACPQuarterlyReportData(int QuarterId, int QuarterYear, int LocationID);
        DataSet GetNACPCohortMonthlyReport(int MonthId, int Year, string LocationID);
        DataSet GetNACPSixCohortMonthlyReport(int MonthId, int Year, string LocationID);
        DataSet GetLosttoFollowupPatientReport(int @LocationID);
        DataSet GetTBStatusbyAgeandSex(DateTime StartDate, DateTime EndDate, int LocationID);
        DataSet GetTotalNoTBPatientwithARVwithoutARV(String StartDate, String EndDate, String LocationID);
        DataSet GetARVRegimenforAdultandChild(DateTime StartDate, DateTime EndDate, int LocationID);
        DataSet GetARVRegimenReport(DateTime StartDate, DateTime EndDate, int LocationID);
        DataSet GetNonArtPatient(DateTime StartDate, DateTime EndDate, int LocationID);
        DataSet GetARVCohortReport(int StartDate, int EndDate, int StartDateYear, int EndDateYear, int LocationID);
        DataSet GetPtnotvisitedrecentlyUnknown(DateTime StartDate, DateTime EndDate, int LocationID);
        DataSet GetGeographicalPatientsDistribution(int @LocationID);
        DataSet GetUserDetail(DateTime StartDate, DateTime EndDate, String UserID, int LocationID,int ModuleID);
        DataSet GetBornToLive(int MonthId, int Year, int LocationID);
        DataSet GetPatientNascop(int MonthId, int Year, int LocationID);
        //==========================================================================
        // For Custom Reports
        DataSet GetAllFieldGroups(int SystemID);
        DataSet GetFields(int GroupId, int SystemID);
        DataSet GetAllCategory();
        DataSet GetReportQuarter();
        DataTable GetDropDownValueForField(int FieldId, string FieldName, string viewName, int SystemID);
        //DataTable ParseSQLStatement(string sqlstr);
        string ParseSQLStatement(string sqlstr);
        DataSet GetReportList(int CategoryId);
        DataSet GetCustomReportData(int ReportId);
        int SaveCustomReport(DataSet dsReportDetails, int intflag);
        int DeleteCustomReport(int ReportId);
        //String  GetReportTitle(int ReportId);
        //String  GetReportQuery(int ReportId);
        //int GetReportColumnCount(int ReportId);

        DataSet GetCustomReport(Int32 ReportId);
        DataTable GetUsers();
        DataTable GetCategory(string CategoryName);
        DataTable GetReport(string CategoryName, string ReportName);
        DataTable GetCustomReportJoin(string View1, string View2, Int16 Loc);
        DataTable CheckEnrollmentValidity(string enrollmentNumber);
        //This is function is no more in use - Jayant - 25-Aug-2008
        //DataSet GetDrugARVPickupTillDate(Int32 PatientID, int LocationID);
        //PMTCT REPORTS
        DataSet GetExposedInfantsData(DateTime StartDate, DateTime EndDate, int LocationId);
        DataSet GetKenyaMonthlyReport(int MonthId, int Year, int LocationID);
        DataSet GetUgandaMOHMonthlyReport(DateTime StartDate, DateTime EndDate, int LocationId);
        DataSet GetTanzaniaPMTCTMonthlyMoHReport(int MonthId, int Year, int LocationID);

        //----------Nigeria Reports------------
        DataSet GetNNRIMSFacilityMonthlyReport(int MonthId, int Year, int LocationID);
        DataSet GetMRReportData(DateTime StartDate, DateTime EndDate, int LocationId);
        ////-------Form Builder--------
        DataTable GetReportsCategory();
        DataTable GetCustomReports(Int32 CategoryId);
        DataSet ReturnQueryResult(string theQuery);

        DataSet GetbluecartIEFUinfo(Int32 patientid);

        DataSet GetFacilityPatientsCostPerMonth(DateTime TransactionstartDate, DateTime TransactionEndDate);
        DataSet GetFacilityAvgCD4CostPerPatient(DateTime TransactionstartDate, DateTime TransactionEndDate);
        DataSet GetFacilityAvgExcludingCD4CostPerPatient(DateTime TransactionstartDate, DateTime TransactionEndDate);
        DataSet GetFacilityTotalAvgCostofARVandOIPerPatientPerMonth(DateTime TransactionstartDate, DateTime TransactionEndDate);
        DataSet GetFacilityCumulAvgCostofARVandOIPerPatientPerMonth(DateTime TransactionstartDate, DateTime TransactionEndDate);
        DataSet GetFacilityTotalCostLostToFollowup(DateTime TransactionstartDate, DateTime TransactionEndDate);
        DataSet GetFacilityCumTotalCostLostToFollowup(DateTime TransactionstartDate, DateTime TransactionEndDate);
        DataSet GetFacilityAvgCostCovByProgramAndPatient(DateTime TransactionstartDate, DateTime TransactionEndDate);
        DataSet GetFacilityArvAvgCostCovByProgramAndPatient(DateTime TransactionstartDate, DateTime TransactionEndDate);
        DataSet GetFacilityCumCostCovByProgramAndPatient(DateTime TransactionstartDate, DateTime TransactionEndDate);

        DataSet GetPatientDebitNoteTotalCostByMonth(Int32 PatientId);

        //Added by Beatrice
        //Refreshes Temp tables for Reports
        int RefreshCustomTables(string FacilityName);
        //Added By Njung'e - 05/09/2013
        DataTable GetQueryBuilderReportQuery(string Report_ID);
        DataTable GetQueryBuilderReportParameters(string Report_ID);
        DataSet ReturnQueryResult(string theQuery, string paramTable);
    }
}
