using IQCare.CCC.UILogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
    }
}
