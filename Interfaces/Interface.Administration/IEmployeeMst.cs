using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
namespace Interface.Administration
{
    public interface IEmployeeMst
    {
        DataSet GetEmployee();
        DataSet GetEmployeeDropDowns();
        DataSet GetEmployeeForID(int EmployeeID);
        DataTable SaveNewEmployee(string FirstName, string LastName, int DesignationID,int EmployeeID,int DeleteFlag, int UserID);
        //int UpdateEmployee(int EmployeeID, int DesignationID, string FirstName, string LastName, int UserID, int DeleteFlag);
    }
}
