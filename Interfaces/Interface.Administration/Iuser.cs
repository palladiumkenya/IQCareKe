using System;
using System.Data;
using System.Collections;


namespace Interface.Administration
{
    public interface Iuser
    {
        DataSet FillDropDowns();
        int SaveNewUser(string FName, string LName, string UserName, string Password, int UserId, int EmpId, Hashtable UserGroup);
        DataSet GetUserList();
        DataSet GetUserRecord(int UserId);
        void UpdateUserRecord(string FName, string LName, string UserName, string Password, int UserId, int OperatorId, int EmpId, Hashtable UserGroup);
        //void DeleteUserRecord(int UserId);
        int DeleteUserRecord(int UserId);
    }

}
