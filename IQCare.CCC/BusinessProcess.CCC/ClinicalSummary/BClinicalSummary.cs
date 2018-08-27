using DataAccess.Base;
using Interface.CCC.ClinicalSummary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessProcess.CCC.ClinicalSummary
{
    public class BClinicalSummary : ProcessBase, IClinicalSummaryForm
    {
        public IClinicalSummaryForm GetClinicalSummaryData(int ptn_pk, int Visit_Id, int LocationID)
        {
            throw new NotImplementedException();
        }

        public int SaveUpdateClinicalSummaryData(IClinicalSummaryForm obj, int userID)
        {
            throw new NotImplementedException();
        }
    }
}
