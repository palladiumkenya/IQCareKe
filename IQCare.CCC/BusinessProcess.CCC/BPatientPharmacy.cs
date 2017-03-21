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
            List<DrugPrescription> drugPrescription, string pmscmFlag)
        {
            lock (this)
            {
                ClsObject PatientEncounter = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@PatientMasterVisitID", SqlDbType.Int, PatientMasterVisitID);
                ClsUtility.AddParameters("@PatientId", SqlDbType.Int, PatientId);
                ClsUtility.AddParameters("@LocationID", SqlDbType.VarChar, LocationID);
                ClsUtility.AddParameters("@OrderedBy", SqlDbType.VarChar, OrderedBy);
                ClsUtility.AddParameters("@UserID", SqlDbType.VarChar, UserID);
                ClsUtility.AddParameters("@RegimenType", SqlDbType.VarChar, RegimenType);
                ClsUtility.AddParameters("@DispensedBy", SqlDbType.VarChar, DispensedBy);
                ClsUtility.AddParameters("@RegimenLine", SqlDbType.VarChar, RegimenLine);
                ClsUtility.AddParameters("@ModuleID", SqlDbType.VarChar, ModuleID);

                DataRow theDR = (DataRow)PatientEncounter.ReturnObject(ClsUtility.theParams, "sp_SaveUpdatePharmacy_GreenCard", ClsUtility.ObjectEnum.DataRow);
                string ptn_pharmacy_pk = theDR[0].ToString();

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
                        ClsUtility.AddParameters("@FreqId", SqlDbType.VarChar, drg.FreqId);
                        ClsUtility.AddParameters("@Dose", SqlDbType.VarChar, drg.Dose);
                        ClsUtility.AddParameters("@Duration", SqlDbType.VarChar, drg.Duration);
                        ClsUtility.AddParameters("@qtyPres", SqlDbType.VarChar, drg.qtyPres);
                        ClsUtility.AddParameters("@qtyDisp", SqlDbType.VarChar, drg.qtyDisp);
                        ClsUtility.AddParameters("@pmscm", SqlDbType.VarChar, pmscmFlag);
                        ClsUtility.AddParameters("@UserID", SqlDbType.VarChar, UserID);

                        int j = (int)drugPres.ReturnObject(ClsUtility.theParams, "sp_SaveUpdatePharmacyPrescription_GreenCard", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }

                return 0;
            }

            
        }

        public DataTable getPharmacyDrugList(string regimenLine)
        {
            lock (this)
            {
                ClsObject PatientEncounter = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@regimenLine", SqlDbType.Int, regimenLine);

                return (DataTable)PatientEncounter.ReturnObject(ClsUtility.theParams, "sp_getPharmacyDrugList", ClsUtility.ObjectEnum.DataTable);
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
    }
}
