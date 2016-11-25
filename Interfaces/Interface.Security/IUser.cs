using System;
using System.Data;
using System.Collections;

namespace Interface.Security
{
    public interface IUser
    {
       
        DataTable GetFacilityList();
        DataSet GetFacilitySettings();
        DataSet GetUserCredentials(string UserName, int LocationId, int SystemId);
        DataTable GetEmployeeDetails();
        int UpdateAppointmentStatus(string Currentdat, int locationid);
    }
}
