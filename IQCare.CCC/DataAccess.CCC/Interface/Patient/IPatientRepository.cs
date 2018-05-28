using DataAccess.Context;
using Entities.CCC;

namespace DataAccess.CCC.Interface.Patient
{
    public interface IPatientRepository : IRepository<Entities.CCC.Enrollment.PatientEntity>
    {
    }
    public interface IPatientPersonViewRepository : IRepository<Entities.CCC.Enrollment.PatientPersonViewEntity>
    {
    }
    public interface IPatientRelationshipViewRepository : IRepository<PatientRelationshipDTO>
    {
    }
}
