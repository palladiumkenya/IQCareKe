using DataAccess.Context;
using Entities.Records.Enrollment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Records.Interface
{
    public interface IPatientEntryPointRepository: IRepository<PatientEntryPoint>
    {
    }
}
