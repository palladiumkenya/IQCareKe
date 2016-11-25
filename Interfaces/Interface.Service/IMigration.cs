using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Interface.Service
{
  public interface IMigration
    {
      string dbType(string DType, string dlength);
      string FString(string theInput);
      int UpsizeData(DataTable theDT, DataTable tblDataTypes, string DBName, string TableName);
      int MigrateData(string cmd, int Location, string DBName, int CommandId);
      DataSet GetProcedures(string Constr, string DBName);
    }
}
