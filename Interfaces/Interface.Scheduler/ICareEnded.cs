using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Interface.Scheduler
{
    public interface ICareEnded
    {
        DataSet GetDynamicControl(int ModuleId);
        int SaveGetDynamicControlDatat(string sqlquery ,string PatientId,string CareEndedDate);
        DataSet GetSavedFormData(int ModuleId, int VisitId);
        DataSet GetCareEndedDeathReason(int ModuleID);

    }
}
