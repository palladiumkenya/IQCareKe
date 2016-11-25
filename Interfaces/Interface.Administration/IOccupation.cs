using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
namespace Interface.Administration
{
    public interface IOccupation
    {
        DataSet GetOccupation();
        int SaveNewOccupation(string OccupationName, int UserID,  int Sequence);
        int UpdateOccupation(int OccupationID, string OccupationName, int UserID, int DeleteFlag, int Sequence);
        void DeleteOccupation(int OccupationID);
        DataSet GetOccupationByID(int occid);
    }
}
