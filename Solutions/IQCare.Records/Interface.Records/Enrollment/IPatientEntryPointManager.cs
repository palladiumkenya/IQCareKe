using Entities.Records.Enrollment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface.Records.Enrollment
{
    public interface IPatientEntryPointManager
    {
        int AddPatientEntryPoint(PatientEntryPoint patientEntryPoint);
        int UpdatePatientEntryPoint(PatientEntryPoint patientEntryPoint);
        int DeletePatientEntryPoint(int id);
        List<PatientEntryPoint> GetPatientEntryPoints(int patientId);
        List<PatientEntryPoint> GetPatientEntryPoints(int patientId, int serviceAreaId);
    }
}
