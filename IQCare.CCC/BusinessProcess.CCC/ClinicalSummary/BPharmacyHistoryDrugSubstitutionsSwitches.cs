using System;
using System.Collections.Generic;
using System.Data;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using Interface.CCC.ClinicalSummary;

namespace BusinessProcess.CCC.ClinicalSummary
{
    public class BPharmacyHistoryDrugSubstitutionsSwitches : ProcessBase, IPharmacyHistoryDrugSubstitutionsSwitches
    {
        public List<PharmacyHistory> GetPharmacyDrugsSubstitutionsSwitchesData(int ptn_pk)
        {
            List<PharmacyHistory> dataList = new List<PharmacyHistory>();
            ClsObject obj = new ClsObject();
            ClsUtility.Init_Hashtable();
            ClsUtility.AddExtendedParameters("@ptn_pk", SqlDbType.Int, ptn_pk);

            DataTable dt = (DataTable)obj.ReturnObject(ClsUtility.theParams, "Pharmacy_History", ClsUtility.ObjectEnum.DataTable);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dataRow in dt.Rows)
                {
                    var dispensedByDate = dataRow["DispensedByDate"].ToString();
                    var regimentype = dataRow["regimentype"];

                    dataList.Add(new PharmacyHistory()
                    {
                        DispensedByDate = !string.IsNullOrWhiteSpace(dispensedByDate)?DateTime.Parse(dispensedByDate): (DateTime?) null,
                        regimentype = regimentype.ToString()
                    });
                }
            }

            return dataList;
        }
    }
}