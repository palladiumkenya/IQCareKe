using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Interface.FormBuilder
{
    public interface IImportExport
    {
        DataTable ExportIQCareDB(int LocationId, DateTime FromDate, DateTime ToDate);
        void ImportIQCareData(Int32 LocationId, string theFileName);
        DataSet ExportSCMIQCare();
        void BulkInsert(DataTable dt, string tablename);
        void ImportSCM(DataTable dt, string TableName);
        void DBBackUpImportData();
    }
}
