using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using Interface.CCC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using static Entities.CCC.Encounter.PatientEncounter;

namespace BusinessProcess.CCC
{
    public class BPatientPharmacy : ProcessBase, IPatientPharmacy
    {
        public int saveUpdatePharmacy(string PatientMasterVisitID, string PatientId, string LocationID, string OrderedBy,
            string UserID, string RegimenType, string DispensedBy, string RegimenLine, string ModuleID, 
            List<DrugPrescription> drugPrescription, string pmscmFlag, string TreatmentProgram,
            string PeriodTaken, string TreatmentPlan, string TreatmentPlanReason, string Regimen, string prescriptionDate,
            string dispensedDate)
        {
            lock (this)
            {
                ClsObject PatientEncounter = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@PatientMasterVisitID", SqlDbType.Int, PatientMasterVisitID);
                ClsUtility.AddParameters("@PatientId", SqlDbType.Int, PatientId);
                ClsUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID);
                ClsUtility.AddParameters("@OrderedBy", SqlDbType.Int, OrderedBy);
                ClsUtility.AddParameters("@UserID", SqlDbType.Int, UserID);
                ClsUtility.AddParameters("@RegimenType", SqlDbType.VarChar, RegimenType);
                ClsUtility.AddParameters("@DispensedBy", SqlDbType.Int, DispensedBy);
                ClsUtility.AddParameters("@RegimenLine", SqlDbType.Int, RegimenLine);
                ClsUtility.AddParameters("@ModuleID", SqlDbType.Int, ModuleID);

                ClsUtility.AddParameters("@TreatmentProgram", SqlDbType.Int, TreatmentProgram);
                ClsUtility.AddParameters("@PeriodTaken", SqlDbType.Int, PeriodTaken);
                ClsUtility.AddParameters("@TreatmentPlan", SqlDbType.Int, TreatmentPlan);
                ClsUtility.AddParameters("@TreatmentPlanReason", SqlDbType.Int, TreatmentPlanReason);
                ClsUtility.AddParameters("@Regimen", SqlDbType.Int, Regimen);
                ClsUtility.AddParameters("@PrescribedDate", SqlDbType.VarChar, prescriptionDate);
                ClsUtility.AddParameters("@DispensedDate", SqlDbType.VarChar, dispensedDate);


                DataRow theDR = (DataRow)PatientEncounter.ReturnObject(ClsUtility.theParams, "sp_SaveUpdatePharmacy_GreenCard", ClsUtility.ObjectEnum.DataRow);
                string ptn_pharmacy_pk = theDR[0].ToString();

                /////////////////////////////////////////////////
                ClsObject deletePharm = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@ptn_pharmacy_pk", SqlDbType.Int, ptn_pharmacy_pk);

                int k = (int)deletePharm.ReturnObject(ClsUtility.theParams, "sp_DeletePharmacyPrescription_GreenCard", ClsUtility.ObjectEnum.ExecuteNonQuery);
                ////////////////////////////////////////////////

                foreach (var drg in drugPrescription)
                {
                    if (drg.DrugId != "")
                    {
                        if (drg.qtyDisp == "")
                            drg.qtyDisp = "0";

                        ClsObject drugPres = new ClsObject();
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@ptn_pharmacy_pk", SqlDbType.Int, ptn_pharmacy_pk);
                        ClsUtility.AddParameters("@DrugId", SqlDbType.Int, drg.DrugId);
                        ClsUtility.AddParameters("@BatchId", SqlDbType.Int, drg.BatchId);


                        if (drg.FreqId != null)
                        {
                            ClsUtility.AddParameters("@FreqId", SqlDbType.VarChar, drg.FreqId);
                        }
                        if (drg.Dose != null)
                        {
                            ClsUtility.AddParameters("@Dose", SqlDbType.VarChar, drg.Dose);
                        }
                        if (drg.Morning != null)
                        {
                            ClsUtility.AddParameters("@Morning", SqlDbType.VarChar, drg.Morning == "" ? "0" : drg.Morning);
                        }
                        if (drg.Midday != null)
                        {
                            ClsUtility.AddParameters("@Midday", SqlDbType.VarChar, drg.Midday == "" ? "0" : drg.Midday);
                        }
                        if (drg.Evening != null)
                        {
                            ClsUtility.AddParameters("@Evening", SqlDbType.VarChar, drg.Evening == "" ? "0" : drg.Evening);
                        }
                        if (drg.Night != null)
                        {
                            ClsUtility.AddParameters("@Night", SqlDbType.VarChar, drg.Night == "" ? "0" : drg.Night);
                        }

                        ClsUtility.AddParameters("@Duration", SqlDbType.VarChar, drg.Duration);
                        ClsUtility.AddParameters("@qtyPres", SqlDbType.VarChar, drg.qtyPres);
                        ClsUtility.AddParameters("@qtyDisp", SqlDbType.VarChar, drg.qtyDisp);
                        ClsUtility.AddParameters("@prophylaxis", SqlDbType.VarChar, drg.prophylaxis);
                        ClsUtility.AddParameters("@pmscm", SqlDbType.VarChar, pmscmFlag);
                        ClsUtility.AddParameters("@UserID", SqlDbType.VarChar, UserID);

                        int j = (int)drugPres.ReturnObject(ClsUtility.theParams, "sp_SaveUpdatePharmacyPrescription_GreenCard", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }

                return Convert.ToInt32(ptn_pharmacy_pk) ;
            }

            
        }

        public DataTable getPharmacyDrugList(string PMSCM,string treatmentplan)
        {
            lock (this)
            {
                ClsObject PatientEncounter = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@pmscm", SqlDbType.VarChar, PMSCM);
                ClsUtility.AddParameters("@tp", SqlDbType.VarChar, treatmentplan);

                return (DataTable)PatientEncounter.ReturnObject(ClsUtility.theParams, "sp_getPharmacyDrugList", ClsUtility.ObjectEnum.DataTable);
            }
        }

        public List<PharmacyFields> getPharmacyCurrentRegimen(string patientId)
        {
            lock (this)
            {
                ClsObject PatientEncounter = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@PatientID", SqlDbType.VarChar, patientId);

                DataTable theDT = (DataTable)PatientEncounter.ReturnObject(ClsUtility.theParams, "sp_getCurrentRegimen", ClsUtility.ObjectEnum.DataTable);

                List<PharmacyFields> lst = new List<PharmacyFields>();
                if(theDT.Rows.Count > 0)
                {
                    PharmacyFields flds = new PharmacyFields();
                    flds.RegimenLine = theDT.Rows[0]["RegimenLineId"].ToString();
                    flds.Regimen = theDT.Rows[0]["RegimenId"].ToString();

                    lst.Add(flds);
                }

                return lst;
            }
        }

        public List<DrugFrequency> getPharmacyDrugFrequency()
        {
            lock (this)
            {
                ClsObject PatientEncounter = new ClsObject();
                ClsUtility.Init_Hashtable();

                DataTable theDT = (DataTable)PatientEncounter.ReturnObject(ClsUtility.theParams, "sp_getPharmacyDrugFrequency", ClsUtility.ObjectEnum.DataTable);

                List<DrugFrequency> list = new List<DrugFrequency>();

                for (int i = 0; i < theDT.Rows.Count; i++)
                {
                    DrugFrequency drg = new DrugFrequency();
                    drg.id = theDT.Rows[i]["id"].ToString();
                    drg.frequency = theDT.Rows[i]["name"].ToString();
                    drg.multiplier = theDT.Rows[i]["multiplier"].ToString();
                    list.Add(drg);
                }

                return list;
            }
        }

        public List<DrugBatch> getPharmacyDrugBatch(string DrugPk)
        {
            lock (this)
            {
                ClsObject PatientEncounter = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@DrugPk", SqlDbType.Int, DrugPk);

                DataTable theDT = (DataTable)PatientEncounter.ReturnObject(ClsUtility.theParams, "sp_getPharmacyBatch", ClsUtility.ObjectEnum.DataTable);

                List<DrugBatch> list = new List<DrugBatch>();

                for (int i = 0; i < theDT.Rows.Count; i++)
                {
                    DrugBatch drg = new DrugBatch();
                    drg.id = theDT.Rows[i]["id"].ToString();
                    drg.batch = theDT.Rows[i]["Name"].ToString();
                    list.Add(drg);
                }

                return list;
            }
        }

        public DataTable getPharmacyDrugSubstitutionInterruptionReason(string TreatmentPlan)
        {
            lock (this)
            {
                ClsObject PatientEncounter = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@TreatmentPlan", SqlDbType.Int, TreatmentPlan);

                return (DataTable)PatientEncounter.ReturnObject(ClsUtility.theParams, "sp_getPharmacyDrugSwitchSubReasons", ClsUtility.ObjectEnum.DataTable);

            }
        }

        public DataTable getPharmacyRegimens(string regimenLine)
        {
            lock (this)
            {
                ClsObject PatientEncounter = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@regimenLine", SqlDbType.Int, regimenLine);

                return (DataTable)PatientEncounter.ReturnObject(ClsUtility.theParams, "sp_getPharmacyRegimens", ClsUtility.ObjectEnum.DataTable);

            }
        }

        public DataTable getPharmacyPrescriptionDetails(string patientMasterVisitID)
        {
            lock (this)
            {
                ClsObject PatientEncounter = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@PatientMasterVisitID", SqlDbType.Int, patientMasterVisitID);

                return (DataTable)PatientEncounter.ReturnObject(ClsUtility.theParams, "sp_getPatientPharmacyPrescription", ClsUtility.ObjectEnum.DataTable);

            }
        }

        public DataTable getLatestPharmacyPrescriptionDetails(string PatientID, string FacilityID)
        {
            lock (this)
            {
                ClsObject PatientEncounter = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@PatientId", SqlDbType.Int, PatientID);
                ClsUtility.AddParameters("@FacilityId", SqlDbType.Int, FacilityID);

                return (DataTable)PatientEncounter.ReturnObject(ClsUtility.theParams, "sp_getPatientLatestPharmacyPrescription", ClsUtility.ObjectEnum.DataTable);

            }
        }

        public DataTable getPharmacyPendingPrescriptions(string patientMasterVisitID, string PatientID)
        {
            lock (this)
            {
                ClsObject PatientEncounter = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@PatientMasterVisitID", SqlDbType.Int, patientMasterVisitID);
                ClsUtility.AddParameters("@PatientID", SqlDbType.Int, PatientID);

                return (DataTable)PatientEncounter.ReturnObject(ClsUtility.theParams, "sp_getPendingPrescriptions", ClsUtility.ObjectEnum.DataTable);

            }
        }

        public List<PharmacyFields> getPharmacyFields(string patientMasterVisitID)
        {
            lock (this)
            {
                ClsObject PatientEncounter = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@PatientMasterVisitID", SqlDbType.Int, patientMasterVisitID);

                DataTable theDT = (DataTable)PatientEncounter.ReturnObject(ClsUtility.theParams, "sp_getPharmacyFields", ClsUtility.ObjectEnum.DataTable);

                List<PharmacyFields> list = new List<PharmacyFields>();

                for (int i = 0; i < theDT.Rows.Count; i++)
                {
                    PharmacyFields drg = new PharmacyFields();
                    drg.TreatmentProgram = theDT.Rows[i]["ProgID"].ToString();
                    drg.PeriodTaken = theDT.Rows[i]["pharmacyPeriodTaken"].ToString();
                    drg.TreatmentPlan = theDT.Rows[i]["TreatmentStatusId"].ToString();
                    drg.TreatmentPlanReason = theDT.Rows[i]["TreatmentStatusReasonId"].ToString();
                    drg.RegimenLine = theDT.Rows[i]["RegimenLineId"].ToString();
                    drg.Regimen = theDT.Rows[i]["RegimenId"].ToString();
                    drg.prescriptionDate = theDT.Rows[i]["OrderedByDate"].ToString();
                    drg.dispenseDate = theDT.Rows[i]["DispensedByDate"].ToString();
                    list.Add(drg);
                }

                return list;
            }
        }

        public List<KeyValue> getPharmacyTreatmentProgram()
        {
            lock (this)
            {
                ClsObject PatientEncounter = new ClsObject();
                ClsUtility.Init_Hashtable();

                DataTable theDT = (DataTable)PatientEncounter.ReturnObject(ClsUtility.theParams, "sp_getTreatmentProgram", ClsUtility.ObjectEnum.DataTable);

                List<KeyValue> list = new List<KeyValue>();

                for (int i = 0; i < theDT.Rows.Count; i++)
                {
                    KeyValue kv = new KeyValue();
                    kv.ItemId = theDT.Rows[i]["id"].ToString();
                    kv.DisplayName = theDT.Rows[i]["name"].ToString();
                    list.Add(kv);
                }

                return list;
            }
        }
    }
}
