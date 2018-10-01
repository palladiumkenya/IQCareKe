using Entities.PatientCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Interface.Records
{
    public interface IPatientMaritalStatusManager
    {
        int AddPatientMaritalStatus(PatientMaritalStatus patientMarital);
        int UpdatePatientMaritalStatus(PatientMaritalStatus patientMarital);
        int DeletePatientMaritalStatus(int id);
        List<PatientMaritalStatus> GetAllMaritalStatuses(int personId);
        PatientMaritalStatus GetFirstPatientMaritalStatus(int personId);
        PatientMaritalStatus GetCurrentPatientMaritalStatus(int personId);
    }
}
