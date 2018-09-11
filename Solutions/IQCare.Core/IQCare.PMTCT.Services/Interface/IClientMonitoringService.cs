
using IQCare.PMTCT.Core.Models;
using System.Threading.Tasks;

namespace IQCare.PMTCT.Services.Interface
{
    public interface IClientMonitoringService
    {
        Task<int> AddPatientWhoStage(PatientWHOStage patientWhoStage);

        Task<int> AddPatientScreening(PatientScreening patientScreening);

        Task<int> AddPatientClinicalNotes(PatientClinicalNotes patientClinicalNotes);
    }
}
