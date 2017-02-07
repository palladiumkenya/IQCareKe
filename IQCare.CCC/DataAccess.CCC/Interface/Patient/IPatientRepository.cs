using DataAccess.Context;
using Entities.CCC.Enrollment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.CCC.Interface.Patient
{
    public interface IPatientRepository : IRepository<Entities.CCC.Enrollment.PatientEntity>
    {
    }
}
