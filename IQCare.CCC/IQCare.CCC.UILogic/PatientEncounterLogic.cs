using Application.Presentation;
using Interface.CCC;
using IQCare.CCC.UILogic.Visit;
using IQCare.Events;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;
using static Entities.CCC.Encounter.PatientEncounter;

namespace IQCare.CCC.UILogic
{


    public class PatientEncounterLogic
    {
         private int result = 0;
       // private int encounterId = 0;
        private int encounterTypeId = 0;

        public int savePatientEncounterPresentingComplaints(string patientMasterVisitID, string patientID, string serviceID, string VisitDate, string VisitScheduled, string VisitBy, string anyComplaints, string Complaints, int TBScreening, int NutritionalStatus, int userId, string adverseEvent, string presentingComplaints)
        {
            IPatientEncounter patientEncounter = (IPatientEncounter)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPatientEncounter, BusinessProcess.CCC");
            PatientEncounterManager patientEncounterManager=new PatientEncounterManager();

            JavaScriptSerializer parser = new JavaScriptSerializer();

            var advEvent = parser.Deserialize<List<AdverseEvents>>(adverseEvent);
            var pComplaints = parser.Deserialize<List<PresentingComplaints>>(presentingComplaints);
            int val = patientEncounter.savePresentingComplaints(patientMasterVisitID, patientID, serviceID,VisitDate,VisitScheduled,VisitBy, anyComplaints, Complaints, TBScreening, NutritionalStatus, userId, advEvent, pComplaints);

            //Set the Visit Encounter Here
            //TODO: ALWAYS CHECK IF AN ENCOUNTER EXITS BEFRE ADDING:
            encounterTypeId = patientEncounterManager.GetPatientEncounterId("EncounterType", "ccc-encounter".ToLower());
            var foundEncounter= patientEncounterManager.GetEncounterIfExists(Convert.ToInt32(patientID),Convert.ToInt32(patientMasterVisitID),Convert.ToInt32(encounterTypeId));
            if (foundEncounter != null)
            {
                result = foundEncounter.Id;
            }
            else
            {
                if (val > 0)
                {
                    result = patientEncounterManager.AddpatientEncounter(Convert.ToInt32(patientID),
                        Convert.ToInt32(patientMasterVisitID),
                        patientEncounterManager.GetPatientEncounterId("EncounterType", "ccc-encounter".ToLower()), 203,
                        userId);
                }
            }           
            return (result > 0) ? val : 0;
        }

        public int savePatientEncounterTS(string patientMasterVisitID, string patientID, string serviceID, string VisitDate, string VisitScheduled, string VisitBy, int userId)
        {
            IPatientEncounter patientEncounter = (IPatientEncounter)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPatientEncounter, BusinessProcess.CCC");

            int val = patientEncounter.savePresentingComplaintsTS(patientMasterVisitID, patientID, serviceID, VisitDate, VisitScheduled, VisitBy, userId);
            return val;
        }

        public void savePatientEncounterChronicIllness(string masterVisitID, string patientID, string userID, string chronicIllness, string Vaccines, string Allergies)
        {
            IPatientEncounter patientEncounter = (IPatientEncounter)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPatientEncounter, BusinessProcess.CCC");
            JavaScriptSerializer parser = new JavaScriptSerializer();
            var chrIllness = parser.Deserialize<List<ChronicIlness>>(chronicIllness);
            var vacc = parser.Deserialize<List<Vaccines>>(Vaccines);
            var allergy = parser.Deserialize<List<Allergies>>(Allergies);
            int val = patientEncounter.saveChronicIllness(masterVisitID, patientID, userID, chrIllness, vacc, allergy);
        }

        public void savePatientEncounterPhysicalExam(string masterVisitID, string patientID, string userID, string physicalExam, string generalExam)
        {
            IPatientEncounter patientEncounter = (IPatientEncounter)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPatientEncounter, BusinessProcess.CCC");
            JavaScriptSerializer parser = new JavaScriptSerializer();
            var phyExam = parser.Deserialize<List<PhysicalExamination>>(physicalExam);
            List<string> generalExamList = generalExam.Split(',').ToList();
            int val = patientEncounter.savePhysicalEaxminations(masterVisitID, patientID, userID, phyExam, generalExamList);
        }

        public void savePatientManagement(string PatientMasterVisitID, string PatientID, string userID, string workplan, string ARVAdherence, string CTXAdherence, string phdp, string diagnosis)
        {
            IPatientEncounter patientEncounter = (IPatientEncounter)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPatientEncounter, BusinessProcess.CCC");
            JavaScriptSerializer parser = new JavaScriptSerializer();
            var diag = parser.Deserialize<List<Diagnosis>>(diagnosis);
            List<string> PHDPList = phdp.Split(',').ToList();
            int val = patientEncounter.savePatientManagement(PatientMasterVisitID,PatientID, userID, workplan, ARVAdherence,CTXAdherence, PHDPList, diag);
        }

        public PresentingComplaintsEntity loadPatientEncounter(string PatientMasterVisitID, string PatientID)
        {
            IPatientEncounter patientEncounter = (IPatientEncounter)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPatientEncounter, BusinessProcess.CCC");
            return patientEncounter.getPatientEncounter(PatientMasterVisitID, PatientID);
        }

        public DataTable loadPatientEncounterAdverseEvents(string PatientMasterVisitID, string PatientID)
        {
            IPatientEncounter patientEncounter = (IPatientEncounter)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPatientEncounter, BusinessProcess.CCC");
            return patientEncounter.getPatientEncounterAdverseEvents(PatientMasterVisitID, PatientID);
        }

        public DataTable loadPatientEncounterComplaints(string PatientMasterVisitID, string PatientID)
        {
            IPatientEncounter patientEncounter = (IPatientEncounter)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPatientEncounter, BusinessProcess.CCC");
            return patientEncounter.getPatientEncounterComplaints(PatientMasterVisitID, PatientID);
        }

        public DataTable loadPatientEncounterChronicIllness(string PatientMasterVisitID, string PatientID)
        {
            IPatientEncounter patientEncounter = (IPatientEncounter)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPatientEncounter, BusinessProcess.CCC");
            return patientEncounter.getPatientEncounterChronicIllness(PatientMasterVisitID, PatientID);
        }

        public DataTable loadPatientEncounterAllergies(string PatientMasterVisitID, string PatientID)
        {
            IPatientEncounter patientEncounter = (IPatientEncounter)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPatientEncounter, BusinessProcess.CCC");
            return patientEncounter.getPatientEncounterAllergies(PatientMasterVisitID, PatientID);
        }

        public DataTable loadPatientEncounterVaccines(string PatientMasterVisitID, string PatientID)
        {
            IPatientEncounter patientEncounter = (IPatientEncounter)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPatientEncounter, BusinessProcess.CCC");
            return patientEncounter.getPatientEncounterVaccines(PatientMasterVisitID, PatientID);
        }

        public DataTable loadPatientEncounterPhysicalExam(string PatientMasterVisitID, string PatientID)
        {
            IPatientEncounter patientEncounter = (IPatientEncounter)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPatientEncounter, BusinessProcess.CCC");
            return patientEncounter.getPatientEncounterPhysicalExam(PatientMasterVisitID, PatientID);
        }

        public DataTable loadPatientEncounterDiagnosis(string PatientMasterVisitID, string PatientID)
        {
            IPatientEncounter patientEncounter = (IPatientEncounter)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPatientEncounter, BusinessProcess.CCC");
            return patientEncounter.getPatientEncounterDiagnosis(PatientMasterVisitID, PatientID);
        }

        public DataTable loadPatientPharmacyPrescription(string PatientMasterVisitID)
        {
            IPatientPharmacy patientEncounter = (IPatientPharmacy)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPatientPharmacy, BusinessProcess.CCC");
            return patientEncounter.getPharmacyPrescriptionDetails(PatientMasterVisitID);
        }

        public DataTable loadPatientLatestPharmacyPrescription(string PatientID, string FacilityID)
        {
            IPatientPharmacy patientEncounter = (IPatientPharmacy)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPatientPharmacy, BusinessProcess.CCC");
            return patientEncounter.getLatestPharmacyPrescriptionDetails(PatientID, FacilityID);
        }

        public DataTable getPharmacyDrugList(string PMSCM,string treatmentPlan)
        {
            IPatientPharmacy patientEncounter = (IPatientPharmacy)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPatientPharmacy, BusinessProcess.CCC");
            return patientEncounter.getPharmacyDrugList(PMSCM,treatmentPlan);
        }

        public List<PharmacyFields> getPharmacyCurrentRegimen(string patientId)
        {
            IPatientPharmacy patientEncounter = (IPatientPharmacy)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPatientPharmacy, BusinessProcess.CCC");
            return patientEncounter.getPharmacyCurrentRegimen(patientId);

        }

        public List<DrugBatch> getPharmacyDrugBatch(string drugPk)
        {
            IPatientPharmacy patientEncounter = (IPatientPharmacy)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPatientPharmacy, BusinessProcess.CCC");
            return patientEncounter.getPharmacyDrugBatch(drugPk);
            
        }

        public DataTable getPharmacyDrugSwitchInterruptionReason(string treatmentPlan)
        {
            IPatientPharmacy patientEncounter = (IPatientPharmacy)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPatientPharmacy, BusinessProcess.CCC");
            return patientEncounter.getPharmacyDrugSubstitutionInterruptionReason(treatmentPlan);

        }

        public DataTable getPharmacyRegimens(string regimenLine)
        {
            IPatientPharmacy patientEncounter = (IPatientPharmacy)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPatientPharmacy, BusinessProcess.CCC");
            return patientEncounter.getPharmacyRegimens(regimenLine);

        }

        public DataTable getPharmacyPendingPrescriptions(string PatientMasterVisitID, string patientID)
        {
            IPatientPharmacy patientEncounter = (IPatientPharmacy)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPatientPharmacy, BusinessProcess.CCC");
            return patientEncounter.getPharmacyPendingPrescriptions(PatientMasterVisitID,patientID);

        }

        public DataTable getPatientWorkPlan(string patientID)
        {
            IPatientEncounter patientEncounter = (IPatientEncounter)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPatientEncounter, BusinessProcess.CCC");
            return patientEncounter.getPatientWorkPlan(patientID);

        }

        public string getPharmacyDrugMultiplier(string frequencyID)
        {
            try
            {
                IPatientPharmacy patientEncounter = (IPatientPharmacy)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPatientPharmacy, BusinessProcess.CCC");
                List<DrugFrequency> drg = patientEncounter.getPharmacyDrugFrequency();

                List<DrugFrequency> filteredList = drg.Where(x => x.id == frequencyID).ToList();

                return filteredList[0].multiplier;
            }
            catch
            {
                return "0";
            }
        }

        public List<PharmacyFields> getPharmacyFields(string PatientMasterVisitID)
        {
            IPatientPharmacy patientEncounter = (IPatientPharmacy)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPatientPharmacy, BusinessProcess.CCC");
            List<PharmacyFields> drg = patientEncounter.getPharmacyFields(PatientMasterVisitID);

            return drg;
        }

        public void getPharmacyTreatmentProgram(DropDownList ddl)
        {
            IPatientPharmacy patientEncounter = (IPatientPharmacy)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPatientPharmacy, BusinessProcess.CCC");
            List<KeyValue> kv = patientEncounter.getPharmacyTreatmentProgram();

            ddl.Items.Add(new ListItem("Select", "0"));
            if (kv != null && kv.Count > 0)
            {
                foreach (var item in kv)
                {
                    ddl.Items.Add(new ListItem(item.DisplayName, item.ItemId));
                }
            }
        }

        public PatientCategorizationParameters getPatientDSDParameters(string patientId)
        {
            IPatientEncounter patientEncounter = (IPatientEncounter)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPatientEncounter, BusinessProcess.CCC");
            PatientCategorizationParameters categorizationParameters = new PatientCategorizationParameters();
            DataSet theDS = patientEncounter.getPatientDSDParameters(patientId);

            if(theDS.Tables[0].Rows.Count > 0)
                categorizationParameters.age = Convert.ToDouble(theDS.Tables[0].Rows[0][0].ToString());
                

            if (theDS.Tables[1].Rows.Count > 0)
                categorizationParameters.BMI = Convert.ToDouble(theDS.Tables[1].Rows[0][0].ToString());

            if(theDS.Tables[2].Rows.Count > 0)
            {
                if(theDS.Tables[2].Rows[0][2].ToString() == "" && theDS.Tables[2].Rows[0][3].ToString() == "True")
                    categorizationParameters.VL = 50; //undetectable
                else
                    categorizationParameters.VL = Convert.ToDouble(theDS.Tables[2].Rows[0][2].ToString());
            }
            else
            {
                categorizationParameters.VL = 1001;
            }


            if (theDS.Tables[3].Rows.Count > 0)
                categorizationParameters.SameRegimen12Months = Convert.ToInt32(theDS.Tables[3].Rows[0][0].ToString());

            if (theDS.Tables[4].Rows.Count > 0 && theDS.Tables[4].Rows[0][0].ToString() != "")
                categorizationParameters.Completed6MonthsIPT = Convert.ToDouble(theDS.Tables[4].Rows[0][0].ToString());

            categorizationParameters.ActiveOIs = theDS.Tables[5].Rows.Count;

            return categorizationParameters;
        }

        public int isVisitScheduled(string patientId)
        {
            IPatientEncounter patientEncounter = (IPatientEncounter)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPatientEncounter, BusinessProcess.CCC");
            DataTable theDT = patientEncounter.isVisitScheduled(patientId);
            return theDT.Rows.Count;
        }

        public int saveUpdatePharmacy(string PatientMasterVisitID, string PatientId, string LocationID, string OrderedBy,
            string UserID, string DispensedBy, string RegimenLine, string ModuleID, string pmscmFlag, string prescription,
            string TreatmentProgram, string PeriodTaken, string TreatmentPlan, string TreatmentPlanReason, string Regimen,
            string regimenText, string prescriptionDate, string dispensedDate)
        {
            IPatientPharmacy patientEncounter = (IPatientPharmacy)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPatientPharmacy, BusinessProcess.CCC");
            PatientEncounterManager patientEncounterManager=new PatientEncounterManager();
            JavaScriptSerializer parser = new JavaScriptSerializer();
            int val=0;
            var drugPrescription = parser.Deserialize<List<DrugPrescription>>(prescription);

            string RegimenType = "";
            for(int i=0; i < drugPrescription.Count; i++)
            {
                if(!RegimenType.ToUpper().Contains(drugPrescription[i].DrugAbbr.ToUpper()))
                {
                    if (drugPrescription[i].DrugAbbr != "")
                        RegimenType += drugPrescription[i].DrugAbbr + "/";
                }
              
            }

            result= patientEncounter.saveUpdatePharmacy(PatientMasterVisitID, PatientId, LocationID, OrderedBy,
                UserID, RegimenType.TrimEnd('/'), DispensedBy, RegimenLine, ModuleID, drugPrescription, pmscmFlag,
                TreatmentProgram, PeriodTaken, TreatmentPlan, TreatmentPlanReason, Regimen, prescriptionDate,
                dispensedDate);

            //--  Raise event if result is>0 for sharing with IL

            if (result > 0)
            {
                MessageEventArgs arg=new MessageEventArgs()
                {
                    PatientId = Convert.ToInt32(PatientId),
                    EntityId = result, // the orderId
                    EventOccurred = "Prescription Raised",
                    MessageType = MessageType.DrugPrescriptionRaised,
                    FacilityId =0,
                    PatientMasterVisitId = Convert.ToInt32(PatientMasterVisitID) 
                };
                Publisher.RaiseEventAsync(this, arg).ConfigureAwait(false); // --
            }
            if (result > 0)
            {
               val= patientEncounterManager.AddpatientEncounter(Convert.ToInt32(PatientId),Convert.ToInt32(PatientMasterVisitID), patientEncounterManager.GetPatientEncounterId("EncounterType", "Pharmacy-encounter".ToLower()),204, Convert.ToInt32(UserID));
            }
            return (val > 0) ? result : 0;
           // return result;
        }

        public void EncounterHistory(TreeView TreeViewEncounterHistory, string patientID)
        {
            IPatientEncounter patientEncounter = (IPatientEncounter)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPatientEncounter, BusinessProcess.CCC");
            DataTable theDT = patientEncounter.getPatientEncounterHistory(patientID);

            int tmpYear = 0;
            int tmpMonth = 0;
            TreeNode root = new TreeNode();
            TreeNode theMRoot = new TreeNode();
            bool flagyear = true;

            foreach (DataRow theDR in theDT.Rows)
            {

                if (theDR["visitDate"] != DBNull.Value && theDR["visitDate"].ToString().Trim() != "" && ((DateTime)theDR["visitDate"]).Year != 1900)
                {
                    if (tmpYear != ((DateTime)theDR["visitDate"]).Year)
                    {
                        root = new TreeNode();
                        root.Text = ((DateTime)theDR["visitDate"]).Year.ToString();
                        root.Value = "";
                        if (flagyear)
                        {
                            root.Expand();
                            flagyear = false;
                        }
                        else
                        {
                            root.Collapse();
                        }
                        TreeViewEncounterHistory.Nodes.Add(root);
                        tmpYear = ((DateTime)theDR["visitDate"]).Year;
                        tmpMonth = 0;
                    }

                    if (tmpYear == ((DateTime)theDR["visitDate"]).Year && tmpMonth != ((DateTime)theDR["visitDate"]).Month)
                    {
                        theMRoot = new TreeNode();
                        theMRoot.Text = ((DateTime)theDR["visitDate"]).ToString("MMMM");
                        theMRoot.Value = "";
                        root.ChildNodes.Add(theMRoot);
                        tmpMonth = ((DateTime)theDR["visitDate"]).Month;
                    }

                    if (tmpYear == ((DateTime)theDR["visitDate"]).Year && tmpMonth == ((DateTime)theDR["visitDate"]).Month)
                    {
                        TreeNode theFrmRoot = new TreeNode();
                        theFrmRoot.Text = theDR["VisitName"].ToString() + " ( " + ((DateTime)theDR["visitDate"]).ToString(HttpContext.Current.Session["AppDateFormat"].ToString()) + " ) - " + theDR["UserName"].ToString();
                        string _VisitName = theDR["VisitName"].ToString();
                        
                        //if (Convert.ToString(Session["TechnicalAreaId"]) == Convert.ToString(theDR["Module"]) || theDR["VisitName"].ToString() == "Patient Registration")
                        //{
                        //    if (DQ != "")
                        //    {
                        //        theFrmRoot.ImageUrl = "~/images/15px-Yes_check.svg.png";
                        //    }
                        //    else if (theDR["CAUTION"].ToString() == "1")
                        //    {
                        //        theFrmRoot.ImageUrl = "~/images/caution.png";
                        //    }
                        //    else
                        //    {
                        //        theFrmRoot.ImageUrl = "~/Images/No_16x.ico";
                        //    }
                        //}
                        //else
                        //{
                        //    if ((_VisitName == "Pharmacy") || (_VisitName == "Laboratory") || (_VisitName == "Paediatric Pharmacy") || _VisitName.Contains("Service Request"))
                        //    {
                        //        if (Session["Paperless"].ToString() == "1")
                        //        {

                        //            if ((_VisitName == "Pharmacy") || (_VisitName == "Laboratory") || (_VisitName == "Paediatric Pharmacy") || _VisitName.Contains("Service Request"))
                        //            {
                        //                if (theDR["CAUTION"].ToString() == "1")
                        //                {
                        //                    theFrmRoot.ImageUrl = "~/images/caution.png";
                        //                }
                        //                else
                        //                    theFrmRoot.ImageUrl = "~/images/15px-Yes_check.svg.png";
                        //            }

                        //        }
                        //        else
                        //        {
                        //            if (DQ != "")
                        //            {
                        //                theFrmRoot.ImageUrl = "~/images/15px-Yes_check.svg.png";
                        //            }
                        //            else
                        //            {
                        //                theFrmRoot.ImageUrl = "~/Images/No_16x.ico";
                        //            }
                        //        }
                        //    }
                        //    else
                        //    {
                        //        //find if form is linked to this module

                        //        if (linkedForms != null && linkedForms.Select("VisitName = '" + _VisitName + "'").Length > 0)
                        //        {
                        //            if (DQ != "")
                        //            {
                        //                theFrmRoot.ImageUrl = "~/images/15px-Yes_check.svg.png";
                        //            }
                        //            else
                        //            {
                        //                theFrmRoot.ImageUrl = "~/Images/No_16x.ico";
                        //            }
                        //        }
                        //        else
                        //        {
                        //            theFrmRoot.ImageUrl = "~/Images/lock.jpg";
                        //            theFrmRoot.ImageToolTip = "You are Not Authorized to Access this Functionality";
                        //            theFrmRoot.SelectAction = TreeNodeSelectAction.None;
                        //        }
                        //    }
                        //}
                        //else
                        //{
                        //    if (Convert.ToInt32(theDR["Module"]) > 2)
                        //    {
                        //        theFrmRoot.ImageUrl = "~/Images/lock.jpg";
                        //        theFrmRoot.ImageToolTip = "You are Not Authorized to Access this Functionality";
                        //        theFrmRoot.SelectAction = TreeNodeSelectAction.None;
                        //    }

                        //    else if (Convert.ToString(Session["TechnicalAreaId"]) == Convert.ToString(theDR["Module"]) || (_VisitName == "Pharmacy") || 
                        //        (_VisitName == "Laboratory") || (_VisitName == "Paediatric Pharmacy") ||_VisitName.Contains("Service Request") )
                        //    {

                        //    }
                        //    else
                        //    {
                        //        theFrmRoot.ImageUrl = "~/Images/lock.jpg";
                        //        theFrmRoot.ImageToolTip = "You are Not Authorized to Access this Functionality";
                        //        theFrmRoot.SelectAction = TreeNodeSelectAction.None;
                        //    }
                        //}

                        if (theDR["VisitName"].ToString() == "CCC")
                        {
                            theFrmRoot.NavigateUrl = "PatientEncounter.aspx?visitId=" + theDR["visitID"].ToString();
                            theFrmRoot.ImageUrl = "~/images/15px-Yes_check.svg.png";
                        }
                        if (theDR["VisitName"].ToString() == "Triage")
                        {
                            theFrmRoot.NavigateUrl = "VitalSigns.aspx?visitId=" + theDR["visitID"].ToString();
                            theFrmRoot.ImageUrl = "~/images/15px-Yes_check.svg.png";
                        }
                        else if(theDR["VisitName"].ToString() == "Pharmacy")
                        {
                            theFrmRoot.NavigateUrl = "PharmacyPrescription.aspx?visitId=" + theDR["visitID"].ToString();

                            if(theDR["status"].ToString() == "2")
                            {
                                theFrmRoot.ImageUrl = "~/images/15px-Yes_check.svg.png";
                            }
                            else
                            {
                                theFrmRoot.ImageUrl = "~/images/caution.png";
                            }
                        }
                        else if (theDR["VisitName"].ToString() == "Lab")
                        {
                            theFrmRoot.NavigateUrl = "LabOrder.aspx?visitId=" + theDR["visitID"].ToString();
                            if (theDR["status"].ToString() == "Complete")
                            {
                                theFrmRoot.ImageUrl = "~/images/15px-Yes_check.svg.png";
                            }
                            else
                            {
                                theFrmRoot.ImageUrl = "~/images/caution.png";
                            }
                        }

                        theFrmRoot.Value = "";// Convert.ToInt32(PId) + "%" + theDR["OrderNo"].ToString() + "%" + theDR["LocationID"].ToString() + "%" + PtnARTStatus + "%" + theDR["Module"].ToString() + "%" + theDR["VisitName"].ToString();
                        theMRoot.ChildNodes.Add(theFrmRoot);
                    }
                }

            }

        }

        public ZScores getZScores(string patientId, double age, string gender, double height, double weight)
        {
            IPatientEncounter patientEncounter = (IPatientEncounter)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPatientEncounter, BusinessProcess.CCC");

            ZScoresParameters zsParam = new ZScoresParameters();
            ZScores zsValues = new ZScores();

            if (age < 15)
            {
                double bmi = 0;

                zsParam = patientEncounter.GetZScoreValues(patientId, gender, height.ToString());

                //////weight for Age//////////

                if (zsParam != null)
                {
                    //Weight for age calculation
                    if (zsParam.L_WA != 0 && weight != 0)
                        zsValues.weightForAge = ((Math.Pow((weight / zsParam.M_WA), zsParam.L_WA)) - 1) / (zsParam.S_WA * zsParam.L_WA);
                    else
                        zsValues.weightForAge = (Math.Log(weight / zsParam.M_WA)) / zsParam.S_WA;

                }
                else
                {
                    //lblWAClassification.Text = "Out of range";
                }

                ///////Weight for height calculation//////////////////////////////

                if (height <= 120 && height >= 45)
                {
                    try
                    {
                        if (zsParam != null)
                        {

                            if (zsParam.L_WH != 0 && weight != 0)
                                zsValues.weightForHeight = ((Math.Pow((weight / zsParam.M_WH), zsParam.L_WH)) - 1) / (zsParam.S_WH * zsParam.L_WH);
                            else
                                zsValues.weightForHeight = (Math.Log(weight / zsParam.M_WH)) / zsParam.S_WH;

                        }
                    }

                    catch (Exception)
                    {

                    }
                }

                ////BMIz (Z-Score Calculation)////////////////////////////

                if (zsParam != null)
                {

                    if (height != 0 && weight != 0)
                        bmi = weight / ((height / 100) * (height / 100));
                    else
                        bmi = 0;

                    if (zsParam.L_BMIz != 0)
                        zsValues.BMIz = ((Math.Pow((bmi / zsParam.M_BMIz), zsParam.L_BMIz)) - 1) / (zsParam.S_BMIz * zsParam.L_BMIz);
                    else
                        zsValues.BMIz = (Math.Log(bmi / zsParam.M_BMIz)) / zsParam.S_BMIz;

                    //lblBMIz.Text = string.Format("{0:f2}", BMIz);

                }


            }

            return zsValues;
            
            /////////////////////////////////////////////////////////

            ///////Height for age calculation/////////////////////////////
            //if (L != 0)
            //    HAz = ((Math.Pow((heightInCm / M), L)) - 1) / (S * L);
            //else
            //    HAz = (Math.Log(heightInCm / M)) / S;

            /////////////////////////////////////////////////////////////

        }

        public void GenerateExcel(string category)
        {
            //IPatientEncounter patientEncounter = (IPatientEncounter)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPatientEncounter, BusinessProcess.CCC");
            //DataTable theDT = patientEncounter.GenerateExcelDifferentiatedCare(category);

            //System.IO.Directory.CreateDirectory(@"C:\Reports");
            //Excel.ExcelUtlity obj = new Excel.ExcelUtlity();
            //obj.WriteDataTableToExcel(theDT, "Sheet1", "C:\\Reports\\" + (category.Replace("(","")).Replace(")","") + "_" + (DateTime.Now.ToString("dd/MM/yyyy").Replace("/","_")).Replace(":","_") + ".xlsx", "Details");
            //obj.openExcel("C:\\Reports\\" + (category.Replace("(", "")).Replace(")", "") + "_" + (DateTime.Now.ToString("dd/MM/yyyy").Replace("/","_")).Replace(":","_") + ".xlsx");
        }
    }
}
