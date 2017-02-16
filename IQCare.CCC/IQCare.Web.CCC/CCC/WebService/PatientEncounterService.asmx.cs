using IQCare.CCC.UILogic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

namespace IQCare.Web.CCC.WebService
{
    /// <summary>
    /// Summary description for PatientEncounterService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class PatientEncounterService : System.Web.Services.WebService
    {

        [WebMethod(EnableSession = true)]
        public int savePatientEncounterPresentingComplaints(string VisitDate,string VisitScheduled, string VisitBy, string Complaints, int TBScreening, int NutritionalStatus,string lmp, string PregStatus, string edd, string ANC, int OnFP, int fpMethod, string CaCx, string STIScreening, string STIPartnerNotification, string adverseEvent)
        {
            string patientMasterVisitID = "0";
            PatientEncounterLogic patientEncounter = new PatientEncounterLogic();
            if (Session["PatientMasterVisitID"].ToString() != null)
                patientMasterVisitID = Session["PatientMasterVisitID"].ToString();

            int val = patientEncounter.savePatientEncounterPresentingComplaints(patientMasterVisitID, "1","211",VisitDate,VisitScheduled,VisitBy, Complaints,TBScreening,NutritionalStatus, lmp,PregStatus,edd,ANC, OnFP,fpMethod, CaCx,STIScreening,STIPartnerNotification, adverseEvent);
            Session["PatientMasterVisitID"] = val;
            return val;
        }


        [WebMethod(EnableSession = true)]
        public void savePatientEncounterChronicIllness(string chronicIllness, string vaccines)
        {
            string patientMasterVisitID = "0";
            PatientEncounterLogic patientEncounter = new PatientEncounterLogic();
            if (Session["PatientMasterVisitID"].ToString() != null)
                patientMasterVisitID = Session["PatientMasterVisitID"].ToString();

            patientEncounter.savePatientEncounterChronicIllness(patientMasterVisitID, "1", chronicIllness,vaccines);
        }

        [WebMethod(EnableSession = true)]
        public void savePatientPhysicalExam(string physicalExam)
        {
            string patientMasterVisitID = "0";
            PatientEncounterLogic patientEncounter = new PatientEncounterLogic();
            if (Session["PatientMasterVisitID"].ToString() != null)
                patientMasterVisitID = Session["PatientMasterVisitID"].ToString();

            patientEncounter.savePatientEncounterPhysicalExam(patientMasterVisitID, "1", physicalExam);
        }

        [WebMethod(EnableSession = true)]
        public void savePatientManagement(string phdp,string ARVAdherence,string CTXAdherence,string appointmentDate,string appointmentType,string diagnosis)
        {
            string patientMasterVisitID = "0";
            PatientEncounterLogic patientEncounter = new PatientEncounterLogic();
            if (Session["PatientMasterVisitID"].ToString() != null)
                patientMasterVisitID = Session["PatientMasterVisitID"].ToString();

            patientEncounter.savePatientManagement(patientMasterVisitID, "1", ARVAdherence,CTXAdherence,appointmentDate,appointmentType,phdp,diagnosis);
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public ArrayList GetAdverseEvents()
        {
            string patientMasterVisitID = "0";
            PatientEncounterLogic patientEncounter = new PatientEncounterLogic();
            if (Session["PatientMasterVisitID"].ToString() != null)
                patientMasterVisitID = Session["PatientMasterVisitID"].ToString();

            DataTable theDT = patientEncounter.loadPatientEncounterAdverseEvents(patientMasterVisitID, "1");
            ArrayList rows = new ArrayList();

            foreach (DataRow row in theDT.Rows)
            {
                string[] i = new string[6] { row["SeverityID"].ToString(), row["EventName"].ToString(), row["EventCause"].ToString(), row["Severity"].ToString(), row["Action"].ToString(), "<button type='button' class='btnDelete btn btn-danger fa fa-minus-circle btn-fill' > Remove</button>" };
                rows.Add(i);
            }
            return rows;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public ArrayList GetChronicIllness()
        {
            string patientMasterVisitID = "0";
            PatientEncounterLogic patientEncounter = new PatientEncounterLogic();
            if (Session["PatientMasterVisitID"].ToString() != null)
                patientMasterVisitID = Session["PatientMasterVisitID"].ToString();

            DataTable theDT = patientEncounter.loadPatientEncounterChronicIllness(patientMasterVisitID, "1");
            ArrayList rows = new ArrayList();

            foreach (DataRow row in theDT.Rows)
            {
                string[] i = new string[6] { row["chronicIllnessID"].ToString(), row["chronicIllnessName"].ToString(), row["Treatment"].ToString(), row["dose"].ToString(), row["duration"].ToString(), "<button type='button' class='btnDelete btn btn-danger fa fa-minus-circle btn-fill' > Remove</button>" };
                rows.Add(i);
            }
            return rows;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public ArrayList GetVaccines()
        {
            string patientMasterVisitID = "0";
            PatientEncounterLogic patientEncounter = new PatientEncounterLogic();
            if (Session["PatientMasterVisitID"].ToString() != null)
                patientMasterVisitID = Session["PatientMasterVisitID"].ToString();

            DataTable theDT = patientEncounter.loadPatientEncounterVaccines(patientMasterVisitID, "1");
            ArrayList rows = new ArrayList();

            foreach (DataRow row in theDT.Rows)
            {
                string[] i = new string[6] { row["vaccineID"].ToString(), row["vaccineStageID"].ToString(), row["VaccineName"].ToString(), row["VaccineStageName"].ToString(), row["VaccineDate"].ToString(), "<button type='button' class='btnDelete btn btn-danger fa fa-minus-circle btn-fill' > Remove</button>" };
                rows.Add(i);
            }
            return rows;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public ArrayList GetPhysicalExam()
        {
            string patientMasterVisitID = "0";
            PatientEncounterLogic patientEncounter = new PatientEncounterLogic();
            if (Session["PatientMasterVisitID"].ToString() != null)
                patientMasterVisitID = Session["PatientMasterVisitID"].ToString();

            DataTable theDT = patientEncounter.loadPatientEncounterPhysicalExam(patientMasterVisitID, "1");
            ArrayList rows = new ArrayList();

            foreach (DataRow row in theDT.Rows)
            {
                string[] i = new string[6] { row["examTypeID"].ToString(), row["examID"].ToString(), row["examType"].ToString(), row["exam"].ToString(), row["findings"].ToString(), "<button type='button' class='btnDelete btn btn-danger fa fa-minus-circle btn-fill' > Remove</button>" };
                rows.Add(i);
            }
            return rows;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public ArrayList GetDiagnosis()
        {
            string patientMasterVisitID = "0";
            PatientEncounterLogic patientEncounter = new PatientEncounterLogic();
            if (Session["PatientMasterVisitID"].ToString() != null)
                patientMasterVisitID = Session["PatientMasterVisitID"].ToString();

            DataTable theDT = patientEncounter.loadPatientEncounterDiagnosis(patientMasterVisitID, "1");
            ArrayList rows = new ArrayList();

            foreach (DataRow row in theDT.Rows)
            {
                string[] i = new string[3] { row["Diagnosis"].ToString(), row["ManagementPlan"].ToString(), "<button type='button' class='btnDelete btn btn-danger fa fa-minus-circle btn-fill' > Remove</button>" };
                rows.Add(i);
            }
            return rows;
        }

    }
}
