using Application.Presentation;
using Interface.CCC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using static Entities.CCC.Encounter.PatientEncounter;

namespace IQCare.CCC.UILogic
{
    public class PatientEncounterLogic
    {
        public int savePatientEncounterPresentingComplaints(string patientMasterVisitID, string patientID, string serviceID, string VisitDate, string VisitScheduled, string VisitBy, string Complaints, int TBScreening, int NutritionalStatus, string adverseEvent)
        {
            IPatientEncounter patientEncounter = (IPatientEncounter)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPatientEncounter, BusinessProcess.CCC");
            JavaScriptSerializer parser = new JavaScriptSerializer();
            var advEvent = parser.Deserialize<List<AdverseEvents>>(adverseEvent);
            //string[] fpMethodArray = fpMethod.Split(',');
            int val = patientEncounter.savePresentingComplaints(patientMasterVisitID, patientID, serviceID,VisitDate,VisitScheduled,VisitBy, Complaints, TBScreening, NutritionalStatus, advEvent);
            return val;
        }

        public void savePatientEncounterChronicIllness(string masterVisitID, string patientID, string chronicIllness, string Vaccines, string Allergies)
        {
            IPatientEncounter patientEncounter = (IPatientEncounter)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPatientEncounter, BusinessProcess.CCC");
            JavaScriptSerializer parser = new JavaScriptSerializer();
            var chrIllness = parser.Deserialize<List<ChronicIlness>>(chronicIllness);
            var vacc = parser.Deserialize<List<Vaccines>>(Vaccines);
            var allergy = parser.Deserialize<List<Allergies>>(Allergies);
            int val = patientEncounter.saveChronicIllness(masterVisitID, patientID, chrIllness, vacc, allergy);
        }

        public void savePatientEncounterPhysicalExam(string masterVisitID, string patientID, string physicalExam)
        {
            IPatientEncounter patientEncounter = (IPatientEncounter)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPatientEncounter, BusinessProcess.CCC");
            JavaScriptSerializer parser = new JavaScriptSerializer();
            var phyExam = parser.Deserialize<List<PhysicalExamination>>(physicalExam);
            int val = patientEncounter.savePhysicalEaxminations(masterVisitID, patientID, phyExam);
        }

        public void savePatientManagement(string PatientMasterVisitID, string PatientID, string ARVAdherence, string CTXAdherence, string nextAppointment, string appointmentType, string phdp, string diagnosis)
        {
            IPatientEncounter patientEncounter = (IPatientEncounter)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPatientEncounter, BusinessProcess.CCC");
            JavaScriptSerializer parser = new JavaScriptSerializer();
            var diag = parser.Deserialize<List<Diagnosis>>(diagnosis);
            List<string> PHDPList = phdp.Split(',').ToList();
            int val = patientEncounter.savePatientManagement(PatientMasterVisitID,PatientID,ARVAdherence,CTXAdherence,nextAppointment,appointmentType, PHDPList, diag);
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

        public DataTable getPharmacyDrugList(string regimenLine)
        {
            IPatientPharmacy patientEncounter = (IPatientPharmacy)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPatientPharmacy, BusinessProcess.CCC");
            return patientEncounter.getPharmacyDrugList(regimenLine);
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

        public string getPharmacyDrugMultiplier(string frequencyID)
        {
            IPatientPharmacy patientEncounter = (IPatientPharmacy)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPatientPharmacy, BusinessProcess.CCC");
            List<DrugFrequency> drg = patientEncounter.getPharmacyDrugFrequency();

            List<DrugFrequency>  filteredList = drg.Where(x => x.id == frequencyID).ToList();

            return filteredList[0].multiplier;
        }

        public List<PharmacyFields> getPharmacyFields(string PatientMasterVisitID)
        {
            IPatientPharmacy patientEncounter = (IPatientPharmacy)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPatientPharmacy, BusinessProcess.CCC");
            List<PharmacyFields> drg = patientEncounter.getPharmacyFields(PatientMasterVisitID);

            return drg;
        }

        public int saveUpdatePharmacy(string PatientMasterVisitID, string PatientId, string LocationID, string OrderedBy,
            string UserID, string DispensedBy, string RegimenLine, string ModuleID, string pmscmFlag, string prescription,
            string TreatmentProgram, string PeriodTaken, string TreatmentPlan, string TreatmentPlanReason, string Regimen,
            string regimenText)
        {
            IPatientPharmacy patientEncounter = (IPatientPharmacy)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPatientPharmacy, BusinessProcess.CCC");
            JavaScriptSerializer parser = new JavaScriptSerializer();
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

            return patientEncounter.saveUpdatePharmacy(PatientMasterVisitID, PatientId, LocationID, OrderedBy,
                UserID, RegimenType.TrimEnd('/'), DispensedBy, RegimenLine, ModuleID, drugPrescription, pmscmFlag,
                TreatmentProgram, PeriodTaken, TreatmentPlan, TreatmentPlanReason, Regimen);

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
                        theFrmRoot.ImageUrl = "~/images/15px-Yes_check.svg.png";
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
                        theFrmRoot.NavigateUrl = "PatientEncounter.aspx?visitId=" + theDR["visitID"].ToString();
                        theFrmRoot.Value = "";// Convert.ToInt32(PId) + "%" + theDR["OrderNo"].ToString() + "%" + theDR["LocationID"].ToString() + "%" + PtnARTStatus + "%" + theDR["Module"].ToString() + "%" + theDR["VisitName"].ToString();
                        theMRoot.ChildNodes.Add(theFrmRoot);
                    }
                }

            }

        }

    }
}
