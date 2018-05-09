using DataAccess.Context;
using Entities.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Records.Interface
{
   public interface IPatientLookupRepository:IRepository<PatientLookup>
    {
        PatientLookup GetGenderId(int patientId);
        PatientLookup GetPatientById(int patientId);
    }
}
