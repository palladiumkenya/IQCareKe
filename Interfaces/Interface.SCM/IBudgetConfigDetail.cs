using System;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Collections;

namespace Interface.SCM
{
    public interface IBudgetConfigDetail
    {
        DataSet GetBudgetConfigDetails(int DonorID, int ProgramID, int ProgramStartYear, int CodeID);
        DataSet GetBudgetConfigTotal(int CodeID);
        int SaveBudgetConfigDetails(DataTable BudgetConfigDataSet, DataTable CostAllocationDataSet, int UserId, Hashtable SelectedValues);
        int DeleteBudgetConfigDetails(int BudgetConfigID, int DeleteFlag, int UserId);
        DataSet GetDonorList();
        DataSet GetProgramList();
        DataSet GetProgramListByDonor(int DonorID);
        DataSet GetCostAllocation(int CodeID);
        DataSet GetHolisticBudgetView(int CodeID, int ProgramStartYear);
        DataSet GetHolisticBudgetViewDefaultYear();

        // from ICosting.cs  MED
        DataTable CalcAdminAndConsultFees(int year, int month);
        DataTable GetPatientVisitConfigForYear(int year, int userId);
        int SavePatientVisitConfigForYear(DataTable dtLabConfig, int year, int userId);

    }
}
