using System;
using System.Data;
using System.Collections;


namespace Interface.Scheduler
{
    public interface IScheduler
    {
        DataSet GetAppointmentStatus();
        DataSet SearchPatientAppointment(string LName, string FName, int PatientID, string HospitalID, DateTime DOB, int Sex, int AppStatus);
       // DataSet SearchResultAppointStatus();
    }
}
