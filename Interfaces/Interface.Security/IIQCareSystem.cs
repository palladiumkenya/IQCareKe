using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Entities.Administration;

namespace Interface.Security
{
    public interface IIQCareSystem
    {
        DataSet GetSystemCache();
        void RefreshReportingTables(int Drop);
        DateTime SystemDate();
        int DataBaseBackup(string Path, int Location, int Deidentified);
        DataSet GetBackupFiles(string Path);
        DataSet GetBackupSets(string Path);
        int RestoreDataBase(string Path, int FileNo);
        string GetServerInstance();
        DataTable GetIQCareSystems(int theFlag);
        SystemVersion GetSystemVersion();
    }
}
