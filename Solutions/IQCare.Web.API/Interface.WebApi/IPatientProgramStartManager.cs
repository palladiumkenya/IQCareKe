using Entities.CCC.psmart;

namespace Interface.WebApi
{
    public interface IPatientProgramStartManager
    {
        int AddPatientProgramStart(PatientProgramStart patientProgramStart);
        int EditPatientProgramStart(PatientProgramStart patientProgramStart);
    }
}