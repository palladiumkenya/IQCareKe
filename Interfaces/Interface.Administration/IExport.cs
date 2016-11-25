using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Interface.Administration
{
    public interface IExport
    {
        DataSet GetPatientResultstxtXsl(Int32 year, DateTime fromdate, DateTime todate);
        DataTable RunQuery(string theSQL);
        DataTable GetViewByGroup(int GroupId);
        DataTable GetColumnNames(string ViewName);
        DataTable GetUniqueExportID(int ptn_pk);
        DataTable GetExportPtnPk(string ViewName);
        DataTable MakeExportID(string Ptn_Pk_List);
    }
}
