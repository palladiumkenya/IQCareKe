using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Interface.Administration
{
    public interface IUserRole
    {
        DataSet GetUserRoleList();
        DataSet GetUserGroupFeatureList(Int32 theSID,Int32 theFID);
        DataSet GetUserGroupFeatureListByID(int UserID);
       // int SaveUserGroupDetail(string GroupName, DataSet theDS, int UserID, int Flag);
        int SaveUserGroupDetail(int GroupID, String Groupname, DataSet theDS, int UserID, int Flag, int EnrollmentFlag, int PreCareEnd, int EditIdentifiers);
        //void UpdateUserGroup(int GroupId, DataSet theDS,  int UserID);
        void UpdateUserGroup(int GroupId, String Groupname, DataSet theDS, int UserID, int Flag, int EnrollmentFlag, int PreCareEnd, int EditIdentifiers);
    }

}
