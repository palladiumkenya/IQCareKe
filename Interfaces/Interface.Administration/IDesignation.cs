using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
namespace Interface.Administration
{
    public interface IDesignation
    {
        DataSet GetDesignation();
        int SaveNewDesignation(string DesignationName, int UserID,  int Sequence);
        int UpdateDesignation(int DesignationID, string DesignationName, int UserID, int DeleteFlag, int Sequence);
        void DeleteDesignation(int DesignationID);
        DataSet GetDesignationByID(int designationid);
    }
}
