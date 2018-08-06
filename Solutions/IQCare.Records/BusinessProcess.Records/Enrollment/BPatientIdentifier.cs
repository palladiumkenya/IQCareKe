using DataAccess.Base;
using DataAccess.Records;
using DataAccess.Records.Context;
using Entities.Records.Enrollment;
using Interface.Records.Enrollment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessProcess.Records.Enrollment
{
   
        public class BPatientIdentifier : ProcessBase, IPatientIdentifierManager
        {
            // private readonly UnitOfWork _unitOfWork = new UnitOfWork(new RecordContext());
            internal int Result;

            public int AddPatientIdentifier(PatientEntityIdentifier patientIdentifier)
            {
                using (UnitOfWork _unitOfWork = new UnitOfWork(new RecordContext()))
                {
                    _unitOfWork.PatientIdentifierRepository.Add(patientIdentifier);
                    Result = _unitOfWork.Complete();
                    var Id = patientIdentifier.Id;
                    _unitOfWork.Dispose();
                    return Id;
                }

            }

            public int DeletePatientIdentifier(int id)
            {
                using (UnitOfWork _unitOfWork = new UnitOfWork(new RecordContext()))
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
                using (UnitOfWork _unitOfWork = new UnitOfWork(new RecordContext()))
                {
                    _unitOfWork.PatientIdentifierRepository.Update(patientIdentifier);
                    Result = _unitOfWork.Complete();
                    _unitOfWork.Dispose();
                    return Result;
                }
            }


            public List<PatientEntityIdentifier> GetPatientEntityIdentifiers(int patientId, int patientEnrollmentId, int identifierTypeId)
            {
                using (UnitOfWork _unitOfWork = new UnitOfWork(new RecordContext()))
                {
                    var IdentifierList = _unitOfWork.PatientIdentifierRepository.FindBy(x =>
                                  x.PatientId == patientId && x.PatientEnrollmentId == patientEnrollmentId &&
                                  x.IdentifierTypeId == identifierTypeId).ToList();
                    _unitOfWork.Dispose();
                    return IdentifierList;
                }
            }

            public List<PatientEntityIdentifier> GetEntityIdentifiersByPatientIdEnrollmentId(int patientId,
                int patientEnrollmentId)
            {
                using (UnitOfWork unitOfWork = new UnitOfWork(new RecordContext()))
                {
                    var identifierList = unitOfWork.PatientIdentifierRepository
                        .FindBy(x => x.PatientId == patientId && x.PatientEnrollmentId == patientEnrollmentId).ToList();
                    unitOfWork.Dispose();
                    return identifierList;
                }
            }

            public List<PatientEntityIdentifier> GetAllPatientEntityIdentifiers(int patientId)
            {
                using (UnitOfWork unitOfWork = new UnitOfWork(new RecordContext()))
                {
                    var identifierList = unitOfWork.PatientIdentifierRepository
                        .FindBy(x => x.PatientId == patientId).ToList();
                    unitOfWork.Dispose();
                    return identifierList;
                }
            }

            public List<PatientEntityIdentifier> CheckIfIdentifierNumberIsUsed(string identifierValue, int identifierTypeId)
            {
                using (UnitOfWork unitOfWork = new UnitOfWork(new RecordContext()))
                {
                    var identifiers =
                        unitOfWork.PatientIdentifierRepository.FindBy(
                            x => x.IdentifierValue == identifierValue && x.IdentifierTypeId == identifierTypeId).ToList();
                    unitOfWork.Dispose();
                    return identifiers;
                }
            }

            public List<PatientEntityIdentifier> GetPatientEntityIdentifiersByPatientId(int patientId, int identifierTypeId)
            {
                using (UnitOfWork unitOfWork = new UnitOfWork(new RecordContext()))
                {
                    var patientIdentifiers = unitOfWork.PatientIdentifierRepository
                        .FindBy(x => x.PatientId == patientId && x.IdentifierTypeId == identifierTypeId).ToList();
                    unitOfWork.Dispose();
                    return patientIdentifiers;
                }
            }
        }
    }
