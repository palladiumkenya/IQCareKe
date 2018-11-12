using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface.CCC.ClinicalSummary
{
   public interface IClinicalSummaryForm
    {
        IClinicalSummaryForm GetClinicalSummaryData(int ptn_pk, int Visit_Id, int LocationID);
        int SaveUpdateClinicalSummaryData(IClinicalSummaryForm obj, int userID);
    }
}
