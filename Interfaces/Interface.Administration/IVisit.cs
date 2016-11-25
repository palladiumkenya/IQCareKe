using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
namespace Interface.Administration
{
    public interface IVisit
    {
        DataSet GetVisitTypeByID(int visitid);
        DataSet DeleteVisitType(int visitid);
        DataSet GetVisitType();
        int SaveNewVisitType(string VisitName, int UserID);
        int UpdateVisitType(int VisitTypeID, string VisitName, int UserID);
    }
}
