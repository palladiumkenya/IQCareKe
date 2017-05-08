using System;
using IQCare.CCC.UILogic;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using Application.Presentation;
using Interface.CCC.Visit;
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
        private readonly IPatientMasterVisitManager _visitManager = (IPatientMasterVisitManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.visit.BPatientmasterVisit, BusinessProcess.CCC");
        [WebMethod(EnableSession = true)]
        public int savePatientEncounterPresentingComplaints(string VisitDate,string VisitScheduled, string VisitBy, string anyComplaints, string Complaints, int TBScreening, int NutritionalStatus, string adverseEvent, string presentingComplaints)
        {
            PatientEncounterLogic patientEncounter = new PatientEncounterLogic();

            int val = patientEncounter.savePatientEncounterPresentingComplaints(Session["PatientMasterVisitID"].ToString(), Session["PatientId"].ToString(), "203",VisitDate,VisitScheduled,VisitBy, anyComplaints, Complaints,TBScreening,NutritionalStatus, Convert.ToInt32(Session["AppUserId"].ToString()), adverseEvent, presentingComplaints);
            return val;
        }

        [WebMethod(EnableSession = true)]
        public int savePatientEncounterTS(string VisitDate, string VisitScheduled, string VisitBy)
        {
            PatientEncounterLogic patientEncounter = new PatientEncounterLogic();

            int val = patientEncounter.savePatientEncounterTS(Session["PatientMasterVisitID"].ToString(), Session["PatientId"].ToString(), "203", VisitDate, VisitScheduled, VisitBy, Convert.ToInt32(Session["AppUserId"].ToString()));
            return val;
        }


        [WebMethod(EnableSession = true)]
        public void savePatientEncounterChronicIllness(string chronicIllness, string vaccines, string allergies)
        {
            PatientEncounterLogic patientEncounter = new PatientEncounterLogic();

            patientEncounter.savePatientEncounterChronicIllness(Session["PatientMasterVisitID"].ToString(), Session["PatientId"].ToString(), Session["AppUserId"].ToString(), chronicIllness,vaccines,allergies);
        }

        [WebMethod(EnableSession = true)]
        public void savePatientPhysicalExam(string physicalExam, string generalExam)
        {
            PatientEncounterLogic patientEncounter = new PatientEncounterLogic();

            patientEncounter.savePatientEncounterPhysicalExam(Session["PatientMasterVisitID"].ToString(), Session["PatientId"].ToString(), Session["AppUserId"].ToString(), physicalExam, generalExam);
        }

        [WebMethod(EnableSession = true)]
        public void savePatientManagement(string workplan, string phdp,string ARVAdherence,string CTXAdherence,string diagnosis)
        {
            PatientEncounterLogic patientEncounter = new PatientEncounterLogic();

            patientEncounter.savePatientManagement(Session["PatientMasterVisitID"].ToString(), Session["PatientId"].ToString(), Session["AppUserId"].ToString(), workplan, ARVAdherence,CTXAdherence,phdp,diagnosis);
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
        public ArrayList LoadComplaints()
        {
            PatientEncounterLogic patientEncounter = new PatientEncounterLogic();

            DataTable theDT = patientEncounter.loadPatientEncounterComplaints(Session["PatientMasterVisitID"].ToString(), Session["PatientId"].ToString());
            ArrayList rows = new ArrayList();

            foreach (DataRow row in theDT.Rows)
            {
                string[] i = new string[4] { row["presentingComplaintsId"].ToString(), row["complaint"].ToString(), row["onsetDate"].ToString(), "<button type='button' class='btnDelete btn btn-danger fa fa-minus-circle btn-fill' > Remove</button>" };
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
                string active = "";
                if (row["active"].ToString() == "1")
                    active = "checked";
                else
                    active = "";

                string[] i = new string[7] { row["chronicIllnessID"].ToString(), row["chronicIllnessName"].ToString(), row["Treatment"].ToString(), row["dose"].ToString(), row["OnsetDate"].ToString(),
                    "<input type='checkbox' id='chkChronic" + row["chronicIllnessID"].ToString() + "' " + active + " >",
                    "<button type='button' class='btnDelete btn btn-danger fa fa-minus-circle btn-fill' > Remove</button>" };
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
                string[] i = new string[8] { row["allergyId"].ToString(), row["reactionId"].ToString(), row["severityId"].ToString(),
                    row["allergy"].ToString(), row["reaction"].ToString(), row["severity"].ToString(),
                    row["onsetDate"].ToString(), "<button type='button' class='btnDelete btn btn-danger fa fa-minus-circle btn-fill' > Remove</button>" };
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
                string[] i = new string[4] { row["Diagnosis"].ToString(), row["DisplayName"].ToString(), row["ManagementPlan"].ToString(), "<button type='button' class='btnDelete btn btn-danger fa fa-minus-circle btn-fill' > Remove</button>" };
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
                string[] i = new string[7] { row["PatientMasterVisitID"].ToString(), row["Ptn_pk"].ToString(),
                    row["identifiervalue"].ToString(),row["FirstName"].ToString(),row["MidName"].ToString(),
                    row["LastName"].ToString(),row["prescribedBy"].ToString()};
                rows.Add(i);
            }
            return rows;
        }


        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public ArrayList GetDrugList(string PMSCM)
        {
            PatientEncounterLogic patientEncounter = new PatientEncounterLogic();

            DataTable theDT = patientEncounter.getPharmacyDrugList(PMSCM);
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
        public ArrayList GetPresentingComplaints()
        {
            //PatientEncounterLogic patientEncounter = new PatientEncounterLogic();
            var result = LookupLogic.GetLookUpItemViewByMasterName("PresentingComplaints");

            JavaScriptSerializer parser = new JavaScriptSerializer();
            var presentingComplaints = parser.Deserialize<List<KeyValue>>(result);

            ArrayList rows = new ArrayList();

            for(int i = 0; i < presentingComplaints.Count; i++)
            {
                string[] j = new string[2] { presentingComplaints[i].ItemId + "~" + presentingComplaints[i].DisplayName, presentingComplaints[i].DisplayName };
                rows.Add(j);
            }
            return rows;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public ArrayList loadAllergies()
        {
            var result = LookupLogic.GetLookUpItemViewByMasterName("Allergies");

            JavaScriptSerializer parser = new JavaScriptSerializer();
            var allergies = parser.Deserialize<List<KeyValue>>(result);

            ArrayList rows = new ArrayList();

            for (int i = 0; i < allergies.Count; i++)
            {
                string[] j = new string[2] { allergies[i].ItemId + "~" + allergies[i].DisplayName, allergies[i].DisplayName };
                rows.Add(j);
            }
            return rows;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public ArrayList loadAllergyReactions()
        {
            var result = LookupLogic.GetLookUpItemViewByMasterName("AllergyReactions");

            JavaScriptSerializer parser = new JavaScriptSerializer();
            var allergyReactions = parser.Deserialize<List<KeyValue>>(result);

            ArrayList rows = new ArrayList();

            for (int i = 0; i < allergyReactions.Count; i++)
            {
                string[] j = new string[2] { allergyReactions[i].ItemId + "~" + allergyReactions[i].DisplayName, allergyReactions[i].DisplayName };
                rows.Add(j);
            }
            return rows;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public ArrayList loadDiagnosis()
        {
            var result = LookupLogic.GetLookUpItemViewByMasterName("ICD10");

            JavaScriptSerializer parser = new JavaScriptSerializer();
            var diagnosis = parser.Deserialize<List<KeyValue>>(result);

            ArrayList rows = new ArrayList();

            for (int i = 0; i < diagnosis.Count; i++)
            {
                string[] j = new string[2] { diagnosis[i].ItemId + "~" + diagnosis[i].DisplayName, diagnosis[i].DisplayName };
                rows.Add(j);
            }
            return rows;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public ArrayList GetCurrentRegimen()
        {
            PatientEncounterLogic patientEncounter = new PatientEncounterLogic();

            List<PharmacyFields> lst = new List<PharmacyFields>();
            lst = patientEncounter.getPharmacyCurrentRegimen(Session["PatientId"].ToString());

            ArrayList rows = new ArrayList();

            if(lst.Count > 0)
            {
                string[] i = new string[2] { lst[0].RegimenLine , lst[0].Regimen };
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
            //PatientEncounterLogic patientEncounter = new PatientEncounterLogic();

            //DataTable theDT = patientEncounter.getPharmacyRegimens(RegimenLine);
            //ArrayList rows = new ArrayList();

            //foreach (DataRow row in theDT.Rows)
            //{
            //    string[] i = new string[2] { row["LookupItemId"].ToString(), row["DisplayName"].ToString() };
            //    rows.Add(i);
            //}
            //return rows;

            /////////////////////
            var result = LookupLogic.GetLookUpItemViewByMasterName(RegimenLine);

            JavaScriptSerializer parser = new JavaScriptSerializer();
            var regimen = parser.Deserialize<List<KeyValue>>(result);

            ArrayList rows = new ArrayList();

            for (int i = 0; i < regimen.Count; i++)
            {
                string[] j = new string[2] { regimen[i].ItemId, regimen[i].DisplayName };
                rows.Add(j);
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

        [WebMethod(EnableSession = true)]
        public ArrayList getZScoreValues(string height, string weight)
        {
            ArrayList result = new ArrayList();
            string weightForAgeResult="", weightForHeight="", BMIz = "";
            if (height != "" && weight != "")
            {
                PatientEncounterLogic patientEncounter = new PatientEncounterLogic();
                ZScores zsValues = new ZScores();
                zsValues = patientEncounter.getZScores(Session["PatientId"].ToString(), Convert.ToDouble(Session["Age"].ToString()), Session["Gender"].ToString(), Convert.ToDouble(height), Convert.ToDouble(weight));

                if (zsValues != null)
                {

                    //weight for age
                    if (zsValues.weightForAge >= 4)
                    {
                        weightForAgeResult = "4 (Overweight)";
                    }
                    else if (zsValues.weightForAge >= 3 && zsValues.weightForAge < 4)
                    {
                        weightForAgeResult = "3 (Overweight)";
                    }
                    else if (zsValues.weightForAge >= 2 && zsValues.weightForAge < 3)
                    {
                        weightForAgeResult = "2 (Overweight)";
                    }
                    else if (zsValues.weightForAge >= 1 && zsValues.weightForAge < 2)
                    {
                        weightForAgeResult = "1 (Overweight)";
                    }
                    else if (zsValues.weightForAge > -1 && zsValues.weightForAge < 1)
                    {
                        weightForAgeResult = "0 (Normal)";
                    }
                    else if (zsValues.weightForAge <= -1 && zsValues.weightForAge > -2)
                    {
                        weightForAgeResult = "-1 (Mild)";
                    }
                    else if (zsValues.weightForAge <= -2 && zsValues.weightForAge > -3)
                    {
                        weightForAgeResult = "-2 (Moderate)";
                    }
                    else if (zsValues.weightForAge <= -3 && zsValues.weightForAge > -4)
                    {
                        weightForAgeResult = "-3 (Severe)";
                    }
                    else if (zsValues.weightForAge <= -4)
                    {
                        weightForAgeResult = "-4 (Severe)";
                    }
                    else
                    {
                        weightForAgeResult = "Out of Range";
                    }

                    //weight for height
                    if (zsValues.weightForHeight >= 4)
                    {
                        weightForHeight = "4 (Overweight)";
                        
                    }
                    else if (zsValues.weightForHeight >= 3 && zsValues.weightForHeight < 4)
                    {
                        weightForHeight = "3 (Overweight)";
                        
                    }
                    else if (zsValues.weightForHeight >= 2 && zsValues.weightForHeight < 3)
                    {
                        weightForHeight = "2 (Overweight)";
                        
                    }
                    else if (zsValues.weightForHeight >= 1 && zsValues.weightForHeight < 2)
                    {
                        weightForHeight = "1 (Overweight)";
                        
                    }
                    else if (zsValues.weightForHeight > -1 && zsValues.weightForHeight < 1)
                    {
                        weightForHeight = "0 (Normal)";
                        
                    }
                    else if (zsValues.weightForHeight <= -1 && zsValues.weightForHeight > -2)
                    {
                        weightForHeight = "-1 (Mild)";
                        
                    }
                    else if (zsValues.weightForHeight <= -2 && zsValues.weightForHeight > -3)
                    {
                        weightForHeight = "-2 (Moderate)";
                        
                    }
                    else if (zsValues.weightForHeight <= -3 && zsValues.weightForHeight > -4)
                    {
                        weightForHeight = "-3 (Severe)";
                        
                    }
                    else if (zsValues.weightForHeight <= -4)
                    {
                        weightForHeight = "-4 (Severe)";
                        
                    }
                    else
                    {
                        weightForHeight = "Out of Range";
                    }

                    //BMIz
                    if (zsValues.BMIz >= 4)
                    {
                        BMIz = "4 (Overweight)";
                        
                    }
                    else if (zsValues.BMIz >= 3 && zsValues.BMIz < 4)
                    {
                        BMIz = "3 (Overweight)";
                        
                    }
                    else if (zsValues.BMIz >= 2 && zsValues.BMIz < 3)
                    {
                        BMIz = "2 (Overweight)";
                        
                    }
                    else if (zsValues.BMIz >= 1 && zsValues.BMIz < 2)
                    {
                        BMIz = "1 (Overweight)";
                        
                    }
                    else if (zsValues.BMIz > -1 && zsValues.BMIz < 1)
                    {
                        BMIz = "0 (Normal)";
                        
                    }
                    else if (zsValues.BMIz <= -1 && zsValues.BMIz > -2)
                    {
                        BMIz = "-1 (Mild)";
                        
                    }
                    else if (zsValues.BMIz <= -2 && zsValues.BMIz > -3)
                    {
                        BMIz = "-2 (Moderate)";
                        
                    }
                    else if (zsValues.BMIz <= -3 && zsValues.BMIz > -4)
                    {
                        BMIz = "-3 (Severe)";
                        
                    }
                    else if (zsValues.BMIz <= -4)
                    {
                        BMIz = "-4 (Severe)";
                        
                    }
                    else
                    {
                        BMIz = "Out of Range";
                    }



                    string[] i = new string[3] { weightForAgeResult, weightForHeight, BMIz };
                    result.Add(i);
                }
            }
            return result;

        }

        [WebMethod(EnableSession = true)]
        public string SavePatientAdherenceAssessment(string feelBetter, string carelessAboutMedicine, string feelWorse, string forgetMedicine)
        {
            PatientAdherenceAssessmentManager patientAdherenceAssessment = new PatientAdherenceAssessmentManager();
            int adherenceScore = 0;
            string adherenceRating = null;

            int patientId = Convert.ToInt32(Session["PatientId"].ToString());
            int patientMasterVisitId = Convert.ToInt32(Session["PatientMasterVisitId"].ToString());
            int createdBy = Convert.ToInt32(Session["AppUserId"].ToString());
            bool feel_Better = Convert.ToBoolean(Convert.ToInt32(feelBetter));
            bool careless_Medicine = Convert.ToBoolean(Convert.ToInt32(carelessAboutMedicine));
            bool feel_Worse = Convert.ToBoolean(Convert.ToInt32(feelWorse));
            bool forget_Medicine = Convert.ToBoolean(Convert.ToInt32(forgetMedicine));

            adherenceScore = Convert.ToInt32(feelBetter) + Convert.ToInt32(carelessAboutMedicine) +
                             Convert.ToInt32(feelWorse) + Convert.ToInt32(forgetMedicine);

            if (adherenceScore == 0)
            {
                adherenceRating = "Good";
            }else if (adherenceScore >= 1 && adherenceScore <= 2)
            {
                adherenceRating = "Fair";
            }else if (adherenceScore >= 3 && adherenceScore <= 4)
            {
                adherenceRating = "Poor";
            }

            var history = patientAdherenceAssessment.GetActiveAdherenceAssessment(patientId);

            if (history.Count > 0)
            {
                history[0].DeleteFlag = true;
                patientAdherenceAssessment.UpdateAdherenceAssessment(history[0]);
            }

            int result = patientAdherenceAssessment.AddPatientAdherenceAssessment(patientId, patientMasterVisitId, createdBy, feel_Better, careless_Medicine, feel_Worse, forget_Medicine);

            if (result > 0)
            {
                var lookUpLogic =  new LookupLogic();
                var adherence = lookUpLogic.GetItemIdByGroupAndItemName("ARVAdherence", adherenceRating);
                var itemId = 0;
                var msg = "Successfully Saved Adherence Assessment";
                if (adherence.Count > 0)
                {
                    itemId = adherence[0].ItemId;
                }
                string[] arr1 = new string[] { msg, itemId.ToString()};
                return new JavaScriptSerializer().Serialize(arr1);
            }
            else
                return "An error occured";
        }

    }
}
