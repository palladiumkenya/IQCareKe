using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.Base;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using Entities.CCC.Lookup;
using Interface.CCC.Lookup;

namespace BusinessProcess.CCC.Lookup
{
    public class BPatientServiceEnrollmentLookupManager : ProcessBase, IPatientServiceEnrollmentLookupManager
    {
        public List<PatientServiceEnrollmentLookup> GetPatientServiceEnrollments(int personId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new LookupContext()))
            {
                var serviceEnrollments =
                    unitOfWork.PatientServiceEnrollmentLookupRepository.FindBy(x => x.PersonId == personId).ToList();
                unitOfWork.Dispose();
                return serviceEnrollments;
            }
        }
    }
}
