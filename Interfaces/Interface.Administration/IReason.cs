using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
namespace Interface.Administration
{
    public interface IReason
    {
         DataSet GetReason();
        DataSet GetReasonByID(int reasonid);
        DataSet DeleteReasonCatg(int reasontypeid);
        DataSet DeleteReason(int reasonid);
         DataSet GetReasonCategory();
        DataSet GetReasonCategoryByID(int reasontypeid);
        int SaveNewReason(string ReasonName, int CategoryID, int SRNo, int UserID);
         int SaveNewReasonCategory(string ReasonCategoryName, int UserID);
        int UpdateReason(int ReasonID, string ReasonName, int CategoryID, int SRNo, int UserID, int DeleteFlag);
        int UpdateReasonCategory(int ReasonCategoryID, string ReasonCategoryName, int UserID);

    }
}
