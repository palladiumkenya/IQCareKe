using System;
using IQCare.CCC.UILogic;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web.Script.Services;
using System.Web.Services;
using Application.Presentation;
using Interface.CCC.Visit;

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
        private readonly IPatientMasterVisitManager _visitManager = (IPatientMasterVisitManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.visit.BPatientmasterVisit, BusinessProcess.CCC");
        [WebMethod(EnableSession = true)]
        public int savePatientEncounterPresentingComplaints(string VisitDate,string VisitScheduled, string VisitBy, string Complaints, int TBScreening, int NutritionalStatus,string lmp, string PregStatus, string edd, string ANC, int OnFP, string fpMethod, string ReasonNotOnFP, string CaCx, string STIScreening, string STIPartnerNotification, string adverseEvent)
        {
            PatientEncounterLogic patientEncounter = new PatientEncounterLogic();

            int val = patientEncounter.savePatientEncounterPresentingComplaints(Session["PatientMasterVisitID"].ToString(), Session["PatientId"].ToString(), "211",VisitDate,VisitScheduled,VisitBy, Complaints,TBScreening,NutritionalStatus, lmp,PregStatus,edd,ANC, OnFP,fpMethod.TrimEnd(','), ReasonNotOnFP, CaCx,STIScreening,STIPartnerNotification, adverseEvent);
            return val;
        }


        [WebMethod(EnableSession = true)]
        public void savePatientEncounterChronicIllness(string chronicIllness, string vaccines, string allergies)
        {
            PatientEncounterLogic patientEncounter = new PatientEncounterLogic();

            patientEncounter.savePatientEncounterChronicIllness(Session["PatientMasterVisitID"].ToString(), Session["PatientId"].ToString(), chronicIllness,vaccines,allergies);
        }

        [WebMethod(EnableSession = true)]
        public void savePatientPhysicalExam(string physicalExam)
        {
            PatientEncounterLogic patientEncounter = new PatientEncounterLogic();

            patientEncounter.savePatientEncounterPhysicalExam(Session["PatientMasterVisitID"].ToString(), Session["PatientId"].ToString(), physicalExam);
        }

        [WebMethod(EnableSession = true)]
        public void savePatientManagement(string phdp,string ARVAdherence,string CTXAdherence,string appointmentDate,string appointmentType,string diagnosis)
        {
            PatientEncounterLogic patientEncounter = new PatientEncounterLogic();

            patientEncounter.savePatientManagement(Session["PatientMasterVisitID"].ToString(), Session["PatientId"].ToString(), ARVAdherence,CTXAdherence,appointmentDate,appointmentType,phdp,diagnosis);
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public ArrayList GetAdverseEvents()
        {
            PatientEncounterLogic patientEncounter = new PatientEncounterLogic();

            DataTable theDT = patientEncounter.loadPatientEncounterAdverseEvents(Session["PatientMasterVisitID"].ToString(), Session["PatientId"].ToString());
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
            PatientEncounterLogic patientEncounter = new PatientEncounterLogic();

            DataTable theDT = patientEncounter.loadPatientEncounterChronicIllness(Session["PatientMasterVisitID"].ToString(), Session["PatientId"].ToString());
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
        public ArrayList GetAllergies()
        {
            PatientEncounterLogic patientEncounter = new PatientEncounterLogic();

            DataTable theDT = patientEncounter.loadPatientEncounterAllergies(Session["PatientMasterVisitID"].ToString(), Session["PatientId"].ToString());
            ArrayList rows = new ArrayList();

            foreach (DataRow row in theDT.Rows)
            {
                string[] i = new string[4] { row["Allergen"].ToString(), row["AllergyResponse"].ToString(), "21-Mar-2017", "<button type='button' class='btnDelete btn btn-danger fa fa-minus-circle btn-fill' > Remove</button>" };
                rows.Add(i);
            }
            return rows;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public ArrayList GetVaccines()
        {
            PatientEncounterLogic patientEncounter = new PatientEncounterLogic();

            DataTable theDT = patientEncounter.loadPatientEncounterVaccines(Session["PatientMasterVisitID"].ToString(), Session["PatientId"].ToString());
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
            PatientEncounterLogic patientEncounter = new PatientEncounterLogic();

            DataTable theDT = patientEncounter.loadPatientEncounterPhysicalExam(Session["PatientMasterVisitID"].ToString(), Session["PatientId"].ToString());
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
            PatientEncounterLogic patientEncounter = new PatientEncounterLogic();

            DataTable theDT = patientEncounter.loadPatientEncounterDiagnosis(Session["PatientMasterVisitID"].ToString(), Session["PatientId"].ToString());
            ArrayList rows = new ArrayList();

            foreach (DataRow row in theDT.Rows)
            {
                string[] i = new string[3] { row["Diagnosis"].ToString(), row["ManagementPlan"].ToString(), "<button type='button' class='btnDelete btn btn-danger fa fa-minus-circle btn-fill' > Remove</button>" };
                rows.Add(i);
            }
            return rows;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public ArrayList GetPharmacyPrescriptionDetails()
        {
            PatientEncounterLogic patientEncounter = new PatientEncounterLogic();

            DataTable theDT = patientEncounter.loadPatientPharmacyPrescription(Session["PatientMasterVisitID"].ToString());
            ArrayList rows = new ArrayList();
            string remove = "";
            foreach (DataRow row in theDT.Rows)
            {
                if (row["DispensedQuantity"].ToString() == "")
                {
                    remove = "<button type='button' class='btnDelete btn btn-danger fa fa-minus-circle btn-fill' > Remove</button>";
                }
                else
                {
                    if (Convert.ToDecimal(row["DispensedQuantity"].ToString()) == 0)
                    {
                        remove = "<button type='button' class='btnDelete btn btn-danger fa fa-minus-circle btn-fill' > Remove</button>";
                    }
                    else
                    {
                        remove = "<button type='button' class='btnDelete btn btn-danger fa fa-minus-circle btn-fill' disabled> Remove</button>";
                    }
                }

                string[] i = new string[13] { row["Drug_Pk"].ToString(), row["batchId"].ToString(),
                    row["FrequencyID"].ToString(),row["abbr"].ToString(),row["DrugName"].ToString(),
                    row["batchName"].ToString(),row["dose"].ToString(),row["freq"].ToString(),
                    row["duration"].ToString(),row["OrderedQuantity"].ToString(),row["DispensedQuantity"].ToString(),
                    row["prophylaxis"].ToString(), remove
                     };
                rows.Add(i);
            }
            return rows;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public ArrayList GetPharmacyPendingPrescriptions()
        {
            PatientEncounterLogic patientEncounter = new PatientEncounterLogic();

            DataTable theDT = patientEncounter.getPharmacyPendingPrescriptions(Session["PatientMasterVisitID"].ToString(), Session["PatientId"].ToString());
            ArrayList rows = new ArrayList();

            foreach (DataRow row in theDT.Rows)
            {
                string[] i = new string[7] { row["masterVisitID"].ToString(), row["Ptn_pk"].ToString(),
                    row["identifiervalue"].ToString(),row["FirstName"].ToString(),row["MidName"].ToString(),
                    row["LastName"].ToString(),row["prescribedBy"].ToString()};
                rows.Add(i);
            }
            return rows;
        }


        [WebMethod(EnableSession = true)]
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

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public ArrayList GetDrugBatches(string DrugPk)
        {
            PatientEncounterLogic patientEncounter = new PatientEncounterLogic();

            List<Entities.CCC.Encounter.PatientEncounter.DrugBatch> lst = patientEncounter.getPharmacyDrugBatch(DrugPk);
            
            ArrayList rows = new ArrayList();

            for(int i=0; i < lst.Count; i++)
            {
                string[] j = new string[2] { lst[i].id, lst[i].batch };
                rows.Add(j);
            }
            return rows;
        }

        [WebMethod(EnableSession = true)]
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
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public ArrayList GetRegimensBasedOnRegimenLine(string RegimenLine)
        {
            PatientEncounterLogic patientEncounter = new PatientEncounterLogic();

            DataTable theDT = patientEncounter.getPharmacyRegimens(RegimenLine);
            ArrayList rows = new ArrayList();

            foreach (DataRow row in theDT.Rows)
            {
                string[] i = new string[2] { row["LookupItemId"].ToString(), row["DisplayName"].ToString() };
                rows.Add(i);
            }
            return rows;
        }

        [WebMethod(EnableSession = true)]
        public int savePatientPharmacy(string TreatmentProgram, string PeriodTaken, string TreatmentPlan, 
            string TreatmentPlanReason, string RegimenLine, string Regimen, string pmscm, string drugPrescription,
            string regimenText)
        {
            PatientEncounterLogic patientEncounter = new PatientEncounterLogic();

            int val = patientEncounter.saveUpdatePharmacy(Session["PatientMasterVisitID"].ToString(), Session["PatientId"].ToString(),
                Session["AppLocationId"].ToString(), Session["AppUserId"].ToString(), Session["AppUserId"].ToString(), 
                Session["AppUserId"].ToString(), RegimenLine, Session["ModuleId"].ToString(), pmscm, drugPrescription,
                TreatmentProgram,PeriodTaken,TreatmentPlan,TreatmentPlanReason,Regimen, regimenText);
            return val;
        }

        [WebMethod(EnableSession = true)]
        public string getDrugFrequencyMultiplier(string freqID)
        {
            PatientEncounterLogic patientEncounter = new PatientEncounterLogic();
            return patientEncounter.getPharmacyDrugMultiplier(freqID);
        }

    }
}
