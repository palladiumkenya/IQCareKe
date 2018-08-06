﻿using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using Interface.CCC.HIVEducation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessProcess.CCC.HIVEducation
{
    public class BHIVEducation : ProcessBase, IHIVEducation
    {
        public DataTable getCounsellingTopics(string counsellingtopics)
        {
            lock (this)
            {
                ClsObject PatientEncounter = new ClsObject(); // Entity
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@regimenLine", SqlDbType.Int, counsellingtopics);

                return (DataTable)PatientEncounter.ReturnObject(ClsUtility.theParams, "sp_getPharmacyRegimens", ClsUtility.ObjectEnum.DataTable);

            }
        }
    }
}
