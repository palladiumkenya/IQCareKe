using System;
using DataAccess.Base;
using Entities.CCC.Enrollment;
using Interface.CCC.Enrollment;
using DataAccess.CCC.Repository;
using DataAccess.CCC.Context;

namespace BusinessProcess.CCC.Enrollment
{
    public class BPatientIdentifier : ProcessBase, IPatientIdentifierManager
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext());
        internal int Result;

        public int AddPatientIdentifier(PatientEntityIdentifier patientIdentifier)
        {
            _unitOfWork.PatientIdentifierRepository.Add(patientIdentifier);
            Result = _unitOfWork.Complete();
            return patientIdentifier.Id;
        }

        public int DeletePatientIdentifier(int id)
        {
            throw new NotImplementedException();
        }

        public int UpdatePatientIdentifier(PatientEntityIdentifier patientIdentifier)
        {
            throw new NotImplementedException();
        }
    }
}
