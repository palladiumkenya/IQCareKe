using System;
using IQCare.CCC.UILogic;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Diagnostics;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI.WebControls;
using Application.Presentation;
using Entities.CCC.Encounter;
using Entities.CCC.Enrollment;
using Interface.CCC.Visit;
using IQCare.CCC.UILogic.Enrollment;
using IQCare.CCC.UILogic.Triage;
using AutoMapper;
using IQCare.Events;
using Entities.CCC.Lookup;

//using static Entities.CCC.Encounter.PatientEncounter;

namespace IQCare.Web.CCC.WebService
{
    public class ArtDistributionDeTails : PatientArtDistribution
    {
        public string DateReferedToClinic { get; set; }
    }
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
        private string Msg { get; set; }
        private int Result { get; set; }
        private readonly IPatientMasterVisitManager _visitManager = (IPatientMasterVisitManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.visit.BPatientmasterVisit, BusinessProcess.CCC");
        [WebMethod(EnableSession = true)]
        public int savePatientEncounterPresentingComplaints(string VisitDate,string VisitScheduled, string VisitBy, string anyComplaints, string Complaints, int TBScreening, int NutritionalStatus, string adverseEvent, string presentingComplaints)
        {
            PatientEncounterLogic patientEncounter = new PatientEncounterLogic();

            int val = patientEncounter.savePatientEncounterPresentingComplaints(Session["ExistingRecordPatientMasterVisitID"].ToString() == "0" ? Session["PatientMasterVisitID"].ToString() : Session["ExistingRecordPatientMasterVisitID"].ToString(), Session["PatientPK"].ToString(), "203",VisitDate,VisitScheduled,VisitBy, anyComplaints, Complaints,TBScreening,NutritionalStatus, Convert.ToInt32(Session["AppUserId"].ToString()), adverseEvent, presentingComplaints);

            
            return val;

        }

        [WebMethod(EnableSession = true)]
        public int savePatientEncounterTS(string VisitDate, string VisitScheduled, string VisitBy)
        {
            PatientEncounterLogic patientEncounter = new PatientEncounterLogic();

            int val = patientEncounter.savePatientEncounterTS(Session["ExistingRecordPatientMasterVisitID"].ToString() == "0" ? Session["PatientMasterVisitID"].ToString() : Session["ExistingRecordPatientMasterVisitID"].ToString(), Session["PatientPK"].ToString(), "203", VisitDate, VisitScheduled, VisitBy, Convert.ToInt32(Session["AppUserId"].ToString()));
            return val;
        }


        [WebMethod(EnableSession = true)]
        public void savePatientEncounterChronicIllness(string chronicIllness, string vaccines, string allergies)
        {
            PatientEncounterLogic patientEncounter = new PatientEncounterLogic();

            patientEncounter.savePatientEncounterChronicIllness(Session["ExistingRecordPatientMasterVisitID"].ToString() == "0" ? Session["PatientMasterVisitID"].ToString() : Session["ExistingRecordPatientMasterVisitID"].ToString(), Session["PatientPK"].ToString(), Session["AppUserId"].ToString(), chronicIllness,vaccines,allergies);
        }

        [WebMethod(EnableSession = true)]
        public void savePatientPhysicalExam(string physicalExam, string generalExam)
        {
            PatientEncounterLogic patientEncounter = new PatientEncounterLogic();

            patientEncounter.savePatientEncounterPhysicalExam(Session["ExistingRecordPatientMasterVisitID"].ToString() == "0" ? Session["PatientMasterVisitID"].ToString() : Session["ExistingRecordPatientMasterVisitID"].ToString(), Session["PatientPK"].ToString(), Session["AppUserId"].ToString(), physicalExam, generalExam);
        }

        [WebMethod(EnableSession = true)]
        public void savePatientWhoStage(int whoStage)
        {
            PatientWhoStageManager whoStageManager = new PatientWhoStageManager();

            int patientId = Convert.ToInt32(Session["PatientPK"].ToString());
            int patientMasterVisitId = Convert.ToInt32(Session["ExistingRecordPatientMasterVisitID"].ToString() == "0" ? Session["PatientMasterVisitID"].ToString() : Session["ExistingRecordPatientMasterVisitID"].ToString());

            PatientWhoStage PatientWhoStage = whoStageManager.GetPatientWhoStage(patientId, patientMasterVisitId);
            if (PatientWhoStage != null)
            {
                PatientWhoStage.WHOStage = whoStage;
                whoStageManager.UpdatePatientWhoStage(PatientWhoStage);
            }
            else
            {
                int whoResult  = whoStageManager.addPatientWhoStage(patientId, patientMasterVisitId, whoStage);
                if (whoResult > 0)
                {
                    int facilityId = Convert.ToInt32(Session["AppPosID"]);

                    MessageEventArgs args = new MessageEventArgs()
                    {
                        PatientId = patientId,
                        EntityId = whoResult,
                        MessageType = MessageType.ObservationResult,
                        EventOccurred = "Patient Observation Result",
                        FacilityId = facilityId,
                        ObservationType = ObservationType.WhoStage
                    };

                    Publisher.RaiseEventAsync(this, args).ConfigureAwait(false);
                }
            }
        }

        [WebMethod(EnableSession = true)]
        public void savePatientManagement(string workplan, string phdp,string ARVAdherence,string CTXAdherence,string diagnosis)
        {
            PatientEncounterLogic patientEncounter = new PatientEncounterLogic();

            patientEncounter.savePatientManagement(Session["ExistingRecordPatientMasterVisitID"].ToString() == "0" ? Session["PatientMasterVisitID"].ToString() : Session["ExistingRecordPatientMasterVisitID"].ToString(), Session["PatientPK"].ToString(), Session["AppUserId"].ToString(), workplan, ARVAdherence,CTXAdherence,phdp,diagnosis);
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public ArrayList GetAdverseEvents()
        {
            int adverseEventId=0;
            int patientId = Convert.ToInt32(Session["PatientPK"].ToString());
            int patientMasterVisitId = Convert.ToInt32(Session["ExistingRecordPatientMasterVisitID"].ToString() == "0" ? Session["PatientMasterVisitID"].ToString() : Session["ExistingRecordPatientMasterVisitID"].ToString());
            var outcomeString = "";

            PatientEncounterLogic patientEncounter = new PatientEncounterLogic();
            PatientAdverseEventOutcomeManager patientAdverseEventOutcome = new PatientAdverseEventOutcomeManager();

            LookupLogic lookupLogic=new LookupLogic();


            DataTable theDT = patientEncounter.loadPatientEncounterAdverseEvents(Session["ExistingRecordPatientMasterVisitID"].ToString() == "0" ? Session["PatientMasterVisitID"].ToString() : Session["ExistingRecordPatientMasterVisitID"].ToString(), Session["PatientPK"].ToString());
            ArrayList rows = new ArrayList();

            foreach (DataRow row in theDT.Rows)
            {
                string eventoutcome = "";
                DateTime outcomeDate=DateTime.Today ;

                //get the adverse Event form the db
                var items = lookupLogic.GetItemIdByGroupAndItemName("AdverseEvents", row["EventName"].ToString());
                foreach (var item in items)
                {
                    adverseEventId = item.ItemId;
                }

                // get the outcome for the adverse event
               // var outcome =patientAdverseEventOutcome.GetAdverseEventOutcome(adverseEventId, patientMasterVisitId, patientId);

               var adverseEventOutcomes= patientAdverseEventOutcome.GetAdverseEventOutcome(adverseEventId,patientMasterVisitId,patientId);



                if (adverseEventOutcomes.Count > 0)
                {
                    foreach (var adverseEventOutcome in adverseEventOutcomes)
                    {
                        eventoutcome = lookupLogic.GetLookupItemNameById(adverseEventOutcome.OutcomeId);
                        outcomeDate = Convert.ToDateTime(adverseEventOutcome.OutcomeDate);
                    }
                    if (string.IsNullOrEmpty(eventoutcome))
                    {
                        string[] i = new string[7]
                        {
                            row["SeverityID"].ToString(),row["AdverseEventId"].ToString(), row["EventName"].ToString(), row["EventCause"].ToString(),
                            row["Severity"].ToString(), row["Action"].ToString(),
                            "<button type='button' class='btnAddAdverseEventOutcome btn btn-info fa fa-plus-circle btn-fill' onclick='AdverseEventOutcome();'> Specify Outcome</button> <button type='button' class='btnDelete btn btn-danger fa fa-minus-circle btn-fill' > Remove</button>"
                        };
                        rows.Add(i);
                    }
                    else
                    {
                        if (eventoutcome == "Died")
                        {
                            outcomeString = "<span class='text-danger'><strong>" + eventoutcome +
                                            "</strong></span> | <span class='text-info'><strong>" + outcomeDate.ToString("dd-MMM-yyy") + "</strong></span>";
                        }
                        else{
                            outcomeString = "<span class='text-primary'><strong>" + eventoutcome +
                                            "</strong></span> | <span class='text-info'><strong>" + outcomeDate.ToString("dd-MMM-yyy") + "</strong></span>";
                        }
                        string[] i = new string[6]
                        {
                            row["SeverityID"].ToString(),
                            row["EventName"].ToString(),
                            row["EventCause"].ToString(),
                            row["Severity"].ToString(),
                            row["Action"].ToString(),
                            outcomeString
                            //"<span class='text-info'>outcome:</span>"+eventoutcome+ "<span class='text-info'>outcome Date:</span>"+ outcomeDate
                        };
                        rows.Add(i);
                    }
                }
                else
                {

                    string[] i = new string[7]
                    {
                        row["SeverityID"].ToString(),row["AdverseEventId"].ToString(), row["EventName"].ToString(), row["EventCause"].ToString(),
                        row["Severity"].ToString(), row["Action"].ToString(),
                        "<button type='button' class='btnAddAdverseEventOutcome btn btn-info fa fa-plus-circle btn-fill' onclick='AdverseEventOutcome();'> Specify Outcome</button> <button type='button' class='btnDelete btn btn-danger fa fa-minus-circle btn-fill' > Remove</button>"
                    };
                    rows.Add(i);
                }


            }
            return rows;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public ArrayList LoadComplaints()
        {
            PatientEncounterLogic patientEncounter = new PatientEncounterLogic();

            DataTable theDT = patientEncounter.loadPatientEncounterComplaints(Session["ExistingRecordPatientMasterVisitID"].ToString() == "0" ? Session["PatientMasterVisitID"].ToString() : Session["ExistingRecordPatientMasterVisitID"].ToString(), Session["PatientPK"].ToString());
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
        public ArrayList LoadWorkPlan()
        {
            PatientEncounterLogic patientEncounter = new PatientEncounterLogic();

            DataTable theDT = patientEncounter.getPatientWorkPlan(Session["PatientPK"].ToString());
            ArrayList rows = new ArrayList();

            foreach (DataRow row in theDT.Rows)
            {
                string[] i = new string[2] { row["visitDate"].ToString(), row["clinicalNotes"].ToString() };
                rows.Add(i);
            }
            return rows;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public ArrayList GetChronicIllness()
        {
            PatientEncounterLogic patientEncounter = new PatientEncounterLogic();

            DataTable theDT = patientEncounter.loadPatientEncounterChronicIllness(Session["ExistingRecordPatientMasterVisitID"].ToString() == "0" ? Session["PatientMasterVisitID"].ToString() : Session["ExistingRecordPatientMasterVisitID"].ToString(), Session["PatientPK"].ToString());
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

            DataTable theDT = patientEncounter.loadPatientEncounterAllergies(Session["ExistingRecordPatientMasterVisitID"].ToString() == "0" ? Session["PatientMasterVisitID"].ToString() : Session["ExistingRecordPatientMasterVisitID"].ToString(), Session["PatientPK"].ToString());
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

            DataTable theDT = patientEncounter.loadPatientEncounterVaccines(Session["ExistingRecordPatientMasterVisitID"].ToString() == "0" ? Session["PatientMasterVisitID"].ToString() : Session["ExistingRecordPatientMasterVisitID"].ToString(), Session["PatientPK"].ToString());
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

            DataTable theDT = patientEncounter.loadPatientEncounterPhysicalExam(Session["ExistingRecordPatientMasterVisitID"].ToString() == "0" ? Session["PatientMasterVisitID"].ToString() : Session["ExistingRecordPatientMasterVisitID"].ToString(), Session["PatientPK"].ToString());
            ArrayList rows = new ArrayList();

            foreach (DataRow row in theDT.Rows)
            {
                string[] i = new string[7] { row["examTypeID"].ToString(), row["examID"].ToString(), row["findingID"].ToString(), row["examType"].ToString(), row["exam"].ToString(), row["findings"].ToString(), "<button type='button' class='btnDelete btn btn-danger fa fa-minus-circle btn-fill' > Remove</button>" };
                rows.Add(i);
            }
            return rows;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public ArrayList GetDiagnosis()
        {
            PatientEncounterLogic patientEncounter = new PatientEncounterLogic();

            DataTable theDT = patientEncounter.loadPatientEncounterDiagnosis(Session["ExistingRecordPatientMasterVisitID"].ToString() == "0" ? Session["PatientMasterVisitID"].ToString() : Session["ExistingRecordPatientMasterVisitID"].ToString(), Session["PatientPK"].ToString());
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

            DataTable theDT = patientEncounter.loadPatientPharmacyPrescription(Session["ExistingRecordPatientMasterVisitID"].ToString() == "0" ? Session["PatientMasterVisitID"].ToString() : Session["ExistingRecordPatientMasterVisitID"].ToString() );
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
        public ArrayList GetLatestPharmacyPrescriptionDetails()
        {
            PatientEncounterLogic patientEncounter = new PatientEncounterLogic();

            DataTable theDT = patientEncounter.loadPatientLatestPharmacyPrescription(Session["PatientPK"].ToString(), Session["AppLocationId"].ToString());
            ArrayList rows = new ArrayList();
            string remove = "<button type='button' class='btnDelete btn btn-danger fa fa-minus-circle btn-fill' > Remove</button>";
            foreach (DataRow row in theDT.Rows)
            {
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

            DataTable theDT = patientEncounter.getPharmacyPendingPrescriptions(Session["ExistingRecordPatientMasterVisitID"].ToString() == "0" ? Session["PatientMasterVisitID"].ToString() : Session["ExistingRecordPatientMasterVisitID"].ToString(), Session["PatientPK"].ToString());
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
        public ArrayList GetDrugList(string PMSCM,string treatmentPlan)
        {
            PatientEncounterLogic patientEncounter = new PatientEncounterLogic();

            DataTable theDT = patientEncounter.getPharmacyDrugList(PMSCM,treatmentPlan);
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
            var presentingComplaints = parser.Deserialize<List<Entities.CCC.Encounter.PatientEncounter.KeyValue>>(result);

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
            var allergies = parser.Deserialize<List<Entities.CCC.Encounter.PatientEncounter.KeyValue>>(result);

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
            var allergyReactions = parser.Deserialize<List<Entities.CCC.Encounter.PatientEncounter.KeyValue>>(result);

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
            var diagnosis = parser.Deserialize<List<Entities.CCC.Encounter.PatientEncounter.KeyValue>>(result);

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

            List<Entities.CCC.Encounter.PatientEncounter.PharmacyFields> lst = new List<Entities.CCC.Encounter.PatientEncounter.PharmacyFields>();
            lst = patientEncounter.getPharmacyCurrentRegimen(Session["PatientPK"].ToString());

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
            //PatientEncounterLogic patientEncounter = new PatientEncounterLogic();

            //DataTable theDT = patientEncounter.getPharmacyDrugSwitchInterruptionReason(TreatmentPlan);
            //ArrayList rows = new ArrayList();

            //foreach (DataRow row in theDT.Rows)
            //{
            //    string[] i = new string[2] { row["ItemId"].ToString(), row["DisplayName"].ToString() };
            //    rows.Add(i);
            //}
            //return rows;

            /////////////////////
            var result = LookupLogic.GetLookUpItemViewByMasterName(TreatmentPlan);

            JavaScriptSerializer parser = new JavaScriptSerializer();
            var regimen = parser.Deserialize<List<Entities.CCC.Encounter.PatientEncounter.KeyValue>>(result);

            ArrayList rows = new ArrayList();

            for (int i = 0; i < regimen.Count; i++)
            {
                string[] j = new string[2] { regimen[i].ItemId, regimen[i].DisplayName };
                rows.Add(j);
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

            /////////////////////
            //var result = LookupLogic.GetLookUpItemViewByMasterName(RegimenLine);

            //JavaScriptSerializer parser = new JavaScriptSerializer();
            //var regimen = parser.Deserialize<List<KeyValue>>(result);

            //ArrayList rows = new ArrayList();

            //for (int i = 0; i < regimen.Count; i++)
            //{
            //    string[] j = new string[2] { regimen[i].ItemId, regimen[i].DisplayName };
            //    rows.Add(j);
            //}
            //return rows;

        }

        [WebMethod(EnableSession = true)]
        public int savePatientPharmacy(string TreatmentProgram, string PeriodTaken, string TreatmentPlan, 
            string TreatmentPlanReason, string RegimenLine, string Regimen, string pmscm, string PrescriptionDate,
            string DispensedDate, string drugPrescription, string regimenText)
        {

            try
            {
                PatientEncounterLogic patientEncounter = new PatientEncounterLogic();

                int val = patientEncounter.saveUpdatePharmacy(Session["ExistingRecordPatientMasterVisitID"].ToString() == "0" ? Session["PatientMasterVisitID"].ToString() : Session["ExistingRecordPatientMasterVisitID"].ToString(), Session["PatientPK"].ToString(),
                    Session["AppLocationId"].ToString(), Session["AppUserId"].ToString(), Session["AppUserId"].ToString(),
                    Session["AppUserId"].ToString(), RegimenLine, Session["ModuleId"].ToString(), pmscm, drugPrescription,
                    TreatmentProgram, PeriodTaken, TreatmentPlan, TreatmentPlanReason, Regimen, regimenText, PrescriptionDate,
                    DispensedDate);
                return val;
            }
            catch (Exception e)
            {
                
                throw new Exception(e.Message);
            }

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
                Entities.CCC.Encounter.PatientEncounter.ZScores zsValues = new Entities.CCC.Encounter.PatientEncounter.ZScores();
                zsValues = patientEncounter.getZScores(Session["PatientPK"].ToString(), Convert.ToDouble(Session["Age"].ToString()), Session["Gender"].ToString(), Convert.ToDouble(height), Convert.ToDouble(weight));

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
        public string SavePatientAdherenceAssessment(string feelBetter, string carelessAboutMedicine, string feelWorse, string forgetMedicine, string takeMedicine, string stopMedicine, string underPressure, string difficultyRemembering)
        {
            PatientAdherenceAssessmentManager patientAdherenceAssessment = new PatientAdherenceAssessmentManager();
            int adherenceScore = 0;
            string adherenceRating = null;
            decimal mmas8Score;

            int patientId = Convert.ToInt32(Session["PatientPK"].ToString());
            int patientMasterVisitId = Convert.ToInt32(Session["ExistingRecordPatientMasterVisitID"].ToString() == "0" ? Session["PatientMasterVisitID"].ToString() : Session["ExistingRecordPatientMasterVisitID"].ToString());
            int createdBy = Convert.ToInt32(Session["AppUserId"].ToString());
            bool feel_Better = Convert.ToBoolean(Convert.ToInt32(feelBetter));
            bool careless_Medicine = Convert.ToBoolean(Convert.ToInt32(carelessAboutMedicine));
            bool feel_Worse = Convert.ToBoolean(Convert.ToInt32(feelWorse));
            bool forget_Medicine = Convert.ToBoolean(Convert.ToInt32(forgetMedicine));
            bool take_medicine = Convert.ToBoolean(Convert.ToInt32(takeMedicine));
            bool stop_Medicine = Convert.ToBoolean(Convert.ToInt32(stopMedicine));
            bool under_Pressure = Convert.ToBoolean(Convert.ToInt32(underPressure));
            decimal difficulty_Remembering = Convert.ToDecimal(difficultyRemembering);

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

            if (adherenceScore > 0)
            {
                mmas8Score = Convert.ToDecimal(adherenceScore) + Convert.ToDecimal(take_medicine) +
                             Convert.ToDecimal(stop_Medicine) + Convert.ToDecimal(under_Pressure) + Convert.ToDecimal(difficulty_Remembering);

                if (mmas8Score >= 1 && mmas8Score <= 2)
                {
                    adherenceRating = "Inadequate";
                }
                else if (mmas8Score >= 3 && mmas8Score <= 8)
                {
                    adherenceRating = "Poor";
                }
            }


            var history = patientAdherenceAssessment.GetActiveAdherenceAssessment(patientId);

            if (history.Count > 0)
            {
                history[0].DeleteFlag = true;
                patientAdherenceAssessment.UpdateAdherenceAssessment(history[0]);
            }

            int result;

            if (adherenceScore > 0)
            {
                result = patientAdherenceAssessment.AddPatientAdherenceAssessment(patientId, patientMasterVisitId,
                    createdBy, feel_Better, careless_Medicine, feel_Worse, forget_Medicine, take_medicine, stop_Medicine, under_Pressure, difficulty_Remembering);
            }
            else
            {
                result = patientAdherenceAssessment.AddPatientAdherenceAssessment(patientId, patientMasterVisitId,
                    createdBy, feel_Better, careless_Medicine, feel_Worse, forget_Medicine);
            }
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

        [WebMethod(EnableSession = true)]
        public string AddArtDistribution(int patientId, int patientMasterVisitId, string artRefillModel, bool missedArvDoses,
            int missedDosesCount, bool fatigue, bool fever, bool nausea, bool diarrhea, bool cough, bool rash,
            bool genitalSore, string otherSymptom, bool newMedication, string newMedicineText, bool familyPlanning, 
            string fpmethod, bool referredToClinic,  DateTime ? appointmentDate, int pregnancyStatus, int IsPatientArtDistributionDone)
        {
            try
            {
                var artDistribution = new PatientArtDistributionManager();

                if (IsPatientArtDistributionDone == 1)
                {
                    PatientArtDistribution patientArtDistribution = artDistribution.GetPatientArtDistributionByPatientIdAndVisitId(patientId, patientMasterVisitId);
                    patientArtDistribution.ArtRefillModel = artRefillModel;
                    patientArtDistribution.Cough = cough;
                    patientArtDistribution.Diarrhea = diarrhea;
                    patientArtDistribution.FamilyPlanning = familyPlanning;
                    patientArtDistribution.FamilyPlanningMethod = fpmethod;
                    patientArtDistribution.Fatigue = fatigue;
                    patientArtDistribution.Fever = fever;
                    patientArtDistribution.MissedArvDoses = missedArvDoses;
                    patientArtDistribution.GenitalSore = genitalSore;
                    patientArtDistribution.MissedArvDosesCount = missedDosesCount;
                    patientArtDistribution.Nausea = nausea;
                    patientArtDistribution.NewMedication = newMedication;
                    patientArtDistribution.NewMedicationText = newMedicineText;
                    patientArtDistribution.OtherSymptom = otherSymptom;
                    patientArtDistribution.PregnancyStatus = pregnancyStatus;
                    patientArtDistribution.Rash = rash;
                    patientArtDistribution.ReferedToClinic = referredToClinic;
                    patientArtDistribution.ReferedToClinicDate = appointmentDate;

                    Result = artDistribution.UpdatePatientArtDistribution(patientArtDistribution);
                }
                else
                {

                    var patientArvDistribution = new PatientArtDistribution()
                    {
                        PatientId = patientId,
                        PatientMasterVisitId = patientMasterVisitId,
                        ArtRefillModel = artRefillModel,
                        Cough = cough,
                        Diarrhea = diarrhea,
                        FamilyPlanning = familyPlanning,
                        FamilyPlanningMethod = fpmethod,
                        Fatigue = fatigue,
                        Fever = fever,
                        MissedArvDoses = missedArvDoses,
                        GenitalSore = genitalSore,
                        MissedArvDosesCount = missedDosesCount,
                        Nausea = nausea,
                        NewMedication = newMedication,
                        NewMedicationText = newMedicineText,
                        OtherSymptom = otherSymptom,
                        PregnancyStatus = pregnancyStatus,
                        Rash = rash,
                        ReferedToClinic = referredToClinic,
                        ReferedToClinicDate = appointmentDate,
                    };

                    Result = artDistribution.AddPatientArtDistribution(patientArvDistribution);
                }

                if (Result > 0)
                {
                    Msg = "Patient ART Distribution Added Successfully!";
                }
            }
            catch (Exception e)
            {
                Msg = e.Message;
            }
            return Msg;
        }

        [WebMethod(EnableSession = true)]
        public string GetArtDistributionForVisit()
        {
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<PatientArtDistribution, ArtDistributionDeTails>();
            });

            int patientId = Convert.ToInt32(HttpContext.Current.Session["PatientPK"]);
            int patientMasterVisitId = Convert.ToInt32(HttpContext.Current.Session["PatientMasterVisitId"]);

            PatientArtDistributionManager artDistributionManager = new PatientArtDistributionManager();

            PatientArtDistribution artDistribution = artDistributionManager.GetPatientArtDistributionByPatientIdAndVisitId(patientId, patientMasterVisitId);
            var results = Mapper.Map<ArtDistributionDeTails>(artDistribution);
            if (results != null)
            {
                results.DateReferedToClinic = String.Format("{0:dd-MMM-yyyy}", results.ReferedToClinicDate);

            }
            return new JavaScriptSerializer().Serialize(results);
        }
        
    }
}
