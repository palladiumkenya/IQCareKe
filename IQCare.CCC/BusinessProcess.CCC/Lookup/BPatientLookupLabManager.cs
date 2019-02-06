using DataAccess.Base;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using Entities.CCC.Lookup;
using Interface.CCC.Lookup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessProcess.CCC.Lookup
{
    public class BPatientLookupLabManager : ProcessBase,IPatientLookupLabManager
    {
       public List<PatientLab>  GetPatientLabs(int patientId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new LookupContext()))
            {
                var patientLabList = unitOfWork.PatientLookupLabsRepository
                    .FindBy(x => x.PatientId == patientId ).OrderByDescending(x=>x.VisitID)
                    .ToList();
                unitOfWork.Dispose();
                return patientLabList;
            }
        }
    }
}
