using IQCare.CCC.UILogic;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web.Script.Services;
using System.Web.Services;
using static Entities.CCC.Encounter.PatientEncounter;

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

            int val = patientEncounter.savePatientEncounterPresentingComplaints(patientMasterVisitID, Session["PatientId"].ToString(), "211",VisitDate,VisitScheduled,VisitBy, Complaints,TBScreening,NutritionalStatus, lmp,PregStatus,edd,ANC, OnFP,fpMethod, CaCx,STIScreening,STIPartnerNotification, adverseEvent);
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

            patientEncounter.savePatientEncounterChronicIllness(patientMasterVisitID, Session["PatientId"].ToString(), chronicIllness,vaccines);
        }

        [WebMethod(EnableSession = true)]
        public void savePatientPhysicalExam(string physicalExam)
        {
            string patientMasterVisitID = "0";
            PatientEncounterLogic patientEncounter = new PatientEncounterLogic();
            if (Session["PatientMasterVisitID"].ToString() != null)
                patientMasterVisitID = Session["PatientMasterVisitID"].ToString();

            patientEncounter.savePatientEncounterPhysicalExam(patientMasterVisitID, Session["PatientId"].ToString(), physicalExam);
        }

        [WebMethod(EnableSession = true)]
        public void savePatientManagement(string phdp,string ARVAdherence,string CTXAdherence,string appointmentDate,string appointmentType,string diagnosis)
        {
            string patientMasterVisitID = "0";
            PatientEncounterLogic patientEncounter = new PatientEncounterLogic();
            if (Session["PatientMasterVisitID"].ToString() != null)
                patientMasterVisitID = Session["PatientMasterVisitID"].ToString();

            patientEncounter.savePatientManagement(patientMasterVisitID, Session["PatientId"].ToString(), ARVAdherence,CTXAdherence,appointmentDate,appointmentType,phdp,diagnosis);
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public ArrayList GetAdverseEvents()
        {
            string patientMasterVisitID = "0";
            PatientEncounterLogic patientEncounter = new PatientEncounterLogic();
            if (Session["PatientMasterVisitID"].ToString() != null)
                patientMasterVisitID = Session["PatientMasterVisitID"].ToString();

            DataTable theDT = patientEncounter.loadPatientEncounterAdverseEvents(patientMasterVisitID, Session["PatientId"].ToString());
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

            DataTable theDT = patientEncounter.loadPatientEncounterChronicIllness(patientMasterVisitID, Session["PatientId"].ToString());
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

            DataTable theDT = patientEncounter.loadPatientEncounterVaccines(patientMasterVisitID, Session["PatientId"].ToString());
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

            DataTable theDT = patientEncounter.loadPatientEncounterPhysicalExam(patientMasterVisitID, Session["PatientId"].ToString());
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

            DataTable theDT = patientEncounter.loadPatientEncounterDiagnosis(patientMasterVisitID, Session["PatientId"].ToString());
            ArrayList rows = new ArrayList();

            foreach (DataRow row in theDT.Rows)
            {
                string[] i = new string[3] { row["Diagnosis"].ToString(), row["ManagementPlan"].ToString(), "<button type='button' class='btnDelete btn btn-danger fa fa-minus-circle btn-fill' > Remove</button>" };
                rows.Add(i);
            }
            return rows;
        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public ArrayList GetDrugList(string regimenLine)
        {
            PatientEncounterLogic patientEncounter = new PatientEncounterLogic();

            DataTable theDT = patientEncounter.getPharmacyDrugList(regimenLine);
            ArrayList rows = new ArrayList();

            foreach (DataRow row in theDT.Rows)
            {
                string[] i = new string[2] { row["val"].ToString(), row["DrugName"].ToString()};
                rows.Add(i);
            }
            return rows;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public ArrayList GetDrugBatches(string DrugPk)
        {
            PatientEncounterLogic patientEncounter = new PatientEncounterLogic();

            List<DrugBatch> lst = patientEncounter.getPharmacyDrugBatch(DrugPk);
            ArrayList rows = new ArrayList();

            for(int i=0; i < lst.Count; i++)
            {
                string[] j = new string[2] { lst[i].id, lst[i].batch };
                rows.Add(j);
            }
            return rows;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public ArrayList GetDrugSwitchReasons(string TreatmentPlan)
        {
            PatientEncounterLogic patientEncounter = new PatientEncounterLogic();

            DataTable theDT = patientEncounter.getPharmacyDrugSwitchInterruptionReason(TreatmentPlan);
            ArrayList rows = new ArrayList();

            foreach (DataRow row in theDT.Rows)
            {
                string[] i = new string[2] { row["LookupItemId"].ToString(), row["DisplayName"].ToString() };
                rows.Add(i);
            }
            return rows;
        }

        [WebMethod(EnableSession = true)]
        public int savePatientPharmacy(string TreatmentPlan, string TreatmentPlanReason, string RegimenLine, string drugPrescription)
        {
            PatientEncounterLogic patientEncounter = new PatientEncounterLogic();

            int val = patientEncounter.saveUpdatePharmacy(Session["PatientMasterVisitID"].ToString(), Session["PatientId"].ToString(),
                Session["AppLocationId"].ToString(), Session["AppUserId"].ToString(), Session["AppUserId"].ToString(), 
                Session["AppUserId"].ToString(), RegimenLine, Session["ModuleId"].ToString(),drugPrescription);
            Session["PatientMasterVisitID"] = val;
            return val;
        }

    }
}
