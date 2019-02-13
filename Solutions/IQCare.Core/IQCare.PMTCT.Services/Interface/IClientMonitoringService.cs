
using IQCare.PMTCT.Core.Models;
using System.Threading.Tasks;

namespace IQCare.PMTCT.Services.Interface
{
    public interface IClientMonitoringService
    {
        Task<int> AddPatientWhoStage(PatientWhoStage patientWhoStage);

        Task<int> AddPatientScreening(PatientScreening patientScreening);

        Task<int> AddPatientClinicalNotes(PatientClinicalNotes patientClinicalNotes);
        PatientScreening GetPatientScreening(int patientId, int patientMasterVisitId, int screeningId);
        PatientWhoStage GetPatientWhoStage(int patientId, int patientMasterVisitId);
    }
}
