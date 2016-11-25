using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;

namespace Interface.FormBuilder
{
    public interface IHomePageConfiguration
    {
        DataSet GetTechnicalArea();
        DataSet GetIndicatorQueryResult(int HomePageId);
        DataSet GetHomePageIndicatorQuery(int ModuleId, int Published);
        int SaveHomePageIndicator(DataSet dsSaveIndicatorQuery, string Flag);
        int DeleteIndicator(int ID, int flag);
        String ParseSQLStatement(string sqlstr);
        String ParseSQLColoumns(string sqlstr);
        int StatusUpdate(Hashtable ht);
    }
}
