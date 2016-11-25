using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;


namespace Interface.Scheduler
{
    public interface IHomeVisit
    {
        DataSet GetFieldsforGrid(int Patient_ID);
        DataSet GetFieldsforAdd(int Patient_ID);
        DataSet GetFieldsforEdit(int Patient_ID, int HomeVisitID, DataTable theCustomFieldData);
        DataSet SaveHomeVisit(int LocationID, int ptn_pk, string PatientCHW, string PatientAlternateCHW, int hvPerWeek1, int hvPerWeek2, int hvPerWeek3, int hvPerWeek4, int VisitsPerWeek, int Duration, DateTime StartDate, int UserId, int HomeVisitID, int Flag, int DataQualityFlag, DataTable theCustomFieldData);
        int DeleteHomeVisitForms(string FormName,int OrderNo, int PatientId, int UserID);
        DataSet GetEmployees(int Id);
        
    }
}
