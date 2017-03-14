using DataAccess.Base;
using Entities.CCC.Enrollment;
using Interface.CCC.Enrollment;
using DataAccess.CCC.Repository;
using DataAccess.CCC.Context;
using System.Collections.Generic;
using System.Linq;

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
            var identifier = _unitOfWork.PatientIdentifierRepository.GetById(id);
            _unitOfWork.PatientIdentifierRepository.Remove(identifier);
          return  Result= _unitOfWork.Complete();
        }

        public int UpdatePatientIdentifier(PatientEntityIdentifier patientIdentifier)
        {
            var identifier=new PatientEntityIdentifier()
            {
                PatientEnrollmentId = patientIdentifier.PatientEnrollmentId,
                IdentifierTypeId = patientIdentifier.IdentifierTypeId,
                IdentifierValue = patientIdentifier.IdentifierValue
            };

            _unitOfWork.PatientIdentifierRepository.Update(identifier);
            return _unitOfWork.Complete();
        }


        public List<PatientEntityIdentifier> GetPatientEntityIdentifiers(int patientId, int patientEnrollmentId, int identifierTypeId)
        {
            return
                _unitOfWork.PatientIdentifierRepository.FindBy(
                    x =>
                        x.PatientId == patientId && x.PatientEnrollmentId == patientEnrollmentId &&
                        x.IdentifierTypeId == identifierTypeId).ToList();
        }
    }
}
