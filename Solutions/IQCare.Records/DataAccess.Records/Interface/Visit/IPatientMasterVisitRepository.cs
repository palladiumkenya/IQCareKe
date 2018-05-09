using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Base;
using DataAccess.Context;
using Entities.Records;

namespace DataAccess.Records.Interface
{
    public interface IPatientMasterVisitRepository:IRepository<PatientMasterVisit>
    {
        List<PatientMasterVisit> GetByDate(DateTime date);
    }
}
