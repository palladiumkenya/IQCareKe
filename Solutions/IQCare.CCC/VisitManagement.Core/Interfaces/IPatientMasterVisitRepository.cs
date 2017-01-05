using Common.Core.Interfaces;
using VisitManagement.Core.Model;

namespace VisitManagement.Core.Interfaces
{
    public interface IPatientMasterVisitRepository :IRepository<PatientMasterVisit>
    {
        PatientMasterVisit GetCurrentMasterVisitDetails(int patentId);
    }
}
