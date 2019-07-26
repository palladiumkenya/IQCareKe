using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using IQCare.CCC.UILogic.Reporting;
using AutoMapper;
using System.Collections;
using System.Diagnostics;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using Entities.CCC.Reports;
using IQCare.CCC.UILogic;
using IQCare.CCC.UILogic.Visit;
using Interface.CCC.Lookup;
using Entities.CCC.Lookup;
using Application.Presentation;


namespace IQCare.Web.CCC.WebService
{
    /// <summary>
    /// Summary description for ReportingService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class ReportingService : System.Web.Services.WebService
    {
        private int Result { get; set; }
        private string Msg { get; set; }
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod]
        public int getNumberOfTxcurr(string reportingdate)
        {
            ReportingResultsManager reportingLogic = new ReportingResultsManager();
            int txcurrcount = 0;
            DataTable theDT = reportingLogic.gettxcurr(Convert.ToDateTime(reportingdate));
            txcurrcount = theDT.Rows.Count;
            return txcurrcount;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public ArrayList gettxcurrlinelist(string reportingdate)
        {
            ReportingResultsManager reportingLogic = new ReportingResultsManager();
            //int txcurrcount = 0;
            DataTable theDT = reportingLogic.gettxcurr(Convert.ToDateTime(reportingdate));
            ArrayList rows = new ArrayList();
            int n = 0;
            foreach (DataRow items in theDT.Rows)
            {
                n = n + 1;
                string[] i = new string[13] { n.ToString(), items["CCCNumber"].ToString(), items["FirstName"].ToString(), items["MiddleName"].ToString(), items["LastName"].ToString(), items["MobileNumber"].ToString(), Convert.ToDateTime(items["DispenseDate"]).ToString("dd-MMM-yyyy"), items["Drug"].ToString(), Convert.ToDateTime(items["ExpectedReturn"]).ToString("dd-MMM-yyyy"), items["DaysOverDue"].ToString(), items["Traced"].ToString(), items["PID"].ToString(), items["personid"].ToString() };
                rows.Add(i);
            }
            return rows;
        }

        [WebMethod]
        public int getFirstStageDefaulters(string reportingdate, int mindays, int maxdays)
        {
            ReportingResultsManager reportingLogic = new ReportingResultsManager();
            int firstdefaulters = 0;
            DataTable theDT = reportingLogic.getfirstdefaulters(Convert.ToDateTime(reportingdate), mindays, maxdays);
            firstdefaulters = theDT.Rows.Count;
            return firstdefaulters;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public ArrayList getfirstdefaultersll(string reportingdate, int mindays, int maxdays)
        {
            ReportingResultsManager reportingLogic = new ReportingResultsManager();
            DataTable theDT = reportingLogic.getfirstdefaulters(Convert.ToDateTime(reportingdate), mindays, maxdays);
            ArrayList rows = new ArrayList();
            int n = 0;
            foreach (DataRow items in theDT.Rows)
            {
                n = n + 1;
                string[] i = new string[13] { n.ToString(), items["CCCNumber"].ToString(), items["FirstName"].ToString(), items["MiddleName"].ToString(), items["LastName"].ToString(), items["MobileNumber"].ToString(), Convert.ToDateTime(items["DispenseDate"]).ToString("dd-MMM-yyyy"), items["Drug"].ToString(), Convert.ToDateTime(items["ExpectedReturn"]).ToString("dd-MMM-yyyy"), items["DaysOverDue"].ToString(), items["Traced"].ToString(), items["PID"].ToString(), items["personid"].ToString() };
                rows.Add(i);
            }
            return rows;
        }

        [WebMethod]
        public int getSecondStageDefaulters(string reportingdate, int mindays, int maxdays)
        {
            ReportingResultsManager reportingLogic = new ReportingResultsManager();
            int seconddefaulters = 0;
            DataTable theDT = reportingLogic.getseconddefaulters(Convert.ToDateTime(reportingdate), mindays, maxdays);
            seconddefaulters = theDT.Rows.Count;
            return seconddefaulters;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public ArrayList getseconddefaultersll(string reportingdate, int mindays, int maxdays)
        {
            ReportingResultsManager reportingLogic = new ReportingResultsManager();
            DataTable theDT = reportingLogic.getseconddefaulters(Convert.ToDateTime(reportingdate), mindays, maxdays);
            ArrayList rows = new ArrayList();
            int n = 0;
            foreach (DataRow items in theDT.Rows)
            {
                n = n + 1;
                string[] i = new string[13] { n.ToString(), items["CCCNumber"].ToString(), items["FirstName"].ToString(), items["MiddleName"].ToString(), items["LastName"].ToString(), items["MobileNumber"].ToString(), Convert.ToDateTime(items["DispenseDate"]).ToString("dd-MMM-yyyy"), items["Drug"].ToString(), Convert.ToDateTime(items["ExpectedReturn"]).ToString("dd-MMM-yyyy"), items["DaysOverDue"].ToString(), items["Traced"].ToString(), items["PID"].ToString(), items["personid"].ToString() };
                rows.Add(i);
            }
            return rows;
        }


        [WebMethod]
        public int getltfu(string fromdate, string todate)
        {
            ReportingResultsManager reportingLogic = new ReportingResultsManager();
            int ltfu = 0;
            DataTable theDT = reportingLogic.getltfu(Convert.ToDateTime(fromdate), Convert.ToDateTime(todate));
            ltfu = theDT.Rows.Count;
            return ltfu;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public ArrayList getltfull(string fromdate, string todate)
        {
            ReportingResultsManager reportingLogic = new ReportingResultsManager();
            DataTable theDT = reportingLogic.getltfu(Convert.ToDateTime(fromdate), Convert.ToDateTime(todate));
            ArrayList rows = new ArrayList();
            int n = 0;
            foreach (DataRow items in theDT.Rows)
            {
                string[] i = new string[13] { n.ToString(), items["CCCNumber"].ToString(), items["FirstName"].ToString(), items["MiddleName"].ToString(), items["LastName"].ToString(), items["MobileNumber"].ToString(), Convert.ToDateTime(items["DispenseDate"]).ToString("dd-MMM-yyyy"), items["Drug"].ToString(), Convert.ToDateTime(items["ExpectedReturn"]).ToString("dd-MMM-yyyy"), items["DaysOverDue"].ToString(), items["Traced"].ToString(), items["PID"].ToString(), items["personid"].ToString() };
                rows.Add(i);
            }
            return rows;
        }
        [WebMethod(EnableSession = true)]
        public string saveTracingData(int PatientId,int PersonId, string tracingdate, int tracingmethod, int tracingoutcome, string othertracingoutcome, string tracingdateofdeath, string tracingdateoftransfer, string transferfacility, string tracingnotes, string tracingstatus)
        {
            int userId = Convert.ToInt32(Session["AppUserId"]);
            int patientmastervisitresult = 0;
            int EncounterResult = 0;
            string savestatus = "";
            DateTime? deathTracingDate = new DateTime();
            int CheckoutResult = 0;
            
            //save tracing data
            if (tracingdateofdeath == "")
            {
                deathTracingDate = null;
            }
            else
            {
                deathTracingDate = Convert.ToDateTime(tracingdateofdeath);
            }

            DateTime? transferTracingDate = new DateTime();
            if (tracingdateoftransfer == "")
            {
                transferTracingDate = null;
            }
            else
            {
                transferTracingDate = Convert.ToDateTime(tracingdateoftransfer);
            }

            try
            {
                //create patient master visit id
                PatientMasterVisitManager patientMasterVisit = new PatientMasterVisitManager();
                LookupLogic lookupLogic = new LookupLogic();
                var currentfacility = lookupLogic.GetFacility(Session["AppPosID"].ToString());
                if (currentfacility == null)
                {
                    currentfacility = lookupLogic.GetFacility();
                }
                patientmastervisitresult = patientMasterVisit.PatientMasterVisitCheckin(PatientId, userId, currentfacility.FacilityID);

                //create encounter
                PatientEncounterManager patientEncounterManager = new PatientEncounterManager();
                EncounterResult = patientEncounterManager.AddpatientEncounterTracing(Convert.ToInt32(PatientId), Convert.ToInt32(patientmastervisitresult),
                        patientEncounterManager.GetPatientEncounterId("EncounterType", "Patient-Tracing"), 203, userId, Convert.ToDateTime(tracingdate), Convert.ToDateTime(tracingdate));
                ILookupManager mgr = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager, BusinessProcess.CCC");
                //save tracing data
                Tracing patientTracing = new Tracing()
                {
                    PersonID = PersonId,
                    TracingType = Convert.ToInt32(mgr.GetLookupItemId("DefaulterTracing")),
                    PatientMasterVisitId = patientmastervisitresult,
                    DateTracingDone = Convert.ToDateTime(tracingdate),
                    Mode = tracingmethod,
                    Outcome = tracingoutcome,
                    TracingDateOfDeath = deathTracingDate,
                    TracingTransferFacility = transferfacility,
                    TracingTransferDate = Convert.ToDateTime(tracingdateoftransfer),
                    Remarks = tracingnotes,
                    CreateDate = DateTime.Now,
                    CreatedBy = Convert.ToInt32(Session["AppUserId"])
                };

                var tracing = new ReportingResultsManager();
                Result = tracing.AddPatientTracing(patientTracing);

                //checkout patient master visit
                PatientMasterVisitManager patientMasterVisitcheckout = new PatientMasterVisitManager();
                CheckoutResult = patientMasterVisit.PatientMasterVisitCheckout(patientmastervisitresult, PatientId, 0, 0, 0, Convert.ToDateTime(tracingdate));

                Session["EncounterStatusId"] = 0;
                Session["PatientEditId"] = 0;
                Session["PatientPK"] = 0;

                if (Result > 0 && CheckoutResult>0)
                {
                    Msg = "Patient appointment Added Successfully!";
                }
            }
            catch (Exception e)
            {
                Msg = e.Message;
            }
            return savestatus;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string gettracingdata(string visitid)
        {
            var RRM = new ReportingResultsManager();
            Tracing[] patientTracingData = RRM.getTracingData(Convert.ToInt32(visitid)).ToArray();
            string jsonScreeningObject = "[]";
            jsonScreeningObject = new JavaScriptSerializer().Serialize(patientTracingData);
            return jsonScreeningObject;
        }
    }
}
