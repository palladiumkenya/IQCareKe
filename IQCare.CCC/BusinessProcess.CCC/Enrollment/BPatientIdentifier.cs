using DataAccess.Base;
using Entities.CCC.Enrollment;
using Interface.CCC.Enrollment;
using DataAccess.CCC.Repository;
using DataAccess.CCC.Context;
using System.Collections.Generic;
using System.Linq;
using System;
using Interface.CCC.Lookup;

namespace BusinessProcess.CCC.Enrollment
{
    public class BPatientIdentifier : ProcessBase, IPatientIdentifierManager
    {
       // private readonly UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext());
        internal int Result;

        public int AddPatientIdentifier(PatientEntityIdentifier patientIdentifier)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                _unitOfWork.PatientIdentifierRepository.Add(patientIdentifier);
                Result = _unitOfWork.Complete();
                var Id= patientIdentifier.Id;
                _unitOfWork.Dispose();
                return Id;
            }

        }

        public int DeletePatientIdentifier(int id)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var identifier = _unitOfWork.PatientIdentifierRepository.GetById(id);
                _unitOfWork.PatientIdentifierRepository.Remove(identifier);
                Result = _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return Result;
            }
        }

        public int UpdatePatientIdentifier(PatientEntityIdentifier patientIdentifier)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                _unitOfWork.PatientIdentifierRepository.Update(patientIdentifier);
                Result= _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return Result;
            }
        }


        public List<PatientEntityIdentifier> GetPatientEntityIdentifiers(int patientId, int patientEnrollmentId, int identifierTypeId)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
              var IdentifierList=  _unitOfWork.PatientIdentifierRepository.FindBy(x =>
                            x.PatientId == patientId && x.PatientEnrollmentId == patientEnrollmentId &&
                            x.IdentifierTypeId == identifierTypeId).ToList();
                _unitOfWork.Dispose();
                return IdentifierList;
            }
        }

        public List<PatientEntityIdentifier> GetEntityIdentifiersByPatientIdEnrollmentId(int patientId,
            int patientEnrollmentId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var identifierList = unitOfWork.PatientIdentifierRepository
                    .FindBy(x => x.PatientId == patientId && x.PatientEnrollmentId == patientEnrollmentId).ToList();
                unitOfWork.Dispose();
                return identifierList;
            }
        }

        public List<PatientEntityIdentifier> GetAllPatientEntityIdentifiers(int patientId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var identifierList = unitOfWork.PatientIdentifierRepository
                    .FindBy(x => x.PatientId == patientId).ToList();
                unitOfWork.Dispose();
                return identifierList;
            }
        }

        public List<PatientEntityIdentifier> CheckIfIdentifierNumberIsUsed(string identifierValue, int identifierTypeId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var identifiers =
                    unitOfWork.PatientIdentifierRepository.FindBy(
                        x => x.IdentifierValue == identifierValue && x.IdentifierTypeId == identifierTypeId && x.DeleteFlag == false).ToList();
                unitOfWork.Dispose();
                return identifiers;
            }
        }

        public List<PatientEntityIdentifier> GetPatientEntityIdentifiersByPatientId(int patientId, int identifierTypeId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var patientIdentifiers = unitOfWork.PatientIdentifierRepository
                    .FindBy(x => x.PatientId == patientId && x.IdentifierTypeId == identifierTypeId).ToList();
                unitOfWork.Dispose();
                return patientIdentifiers;
            }
        }

        public PatientEntityIdentifier GetPatientByCardSerialNumber(string cardSerialNumber)
        {
             
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var patientIdentifier = unitOfWork.PatientIdentifierRepository
                    .FindBy(x => x.IdentifierValue == cardSerialNumber).FirstOrDefault();
                unitOfWork.Dispose();
                return patientIdentifier;
            }
        }
    }
}
