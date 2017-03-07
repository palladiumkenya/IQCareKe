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
    }
}
