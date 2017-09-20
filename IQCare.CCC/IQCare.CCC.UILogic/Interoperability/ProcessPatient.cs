using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.CCC.Enrollment;
using IQCare.CCC.UILogic.Enrollment;
using IQCare.DTO;

namespace IQCare.CCC.UILogic.Interoperability
{
    public class ProcessPatient
    {
        public string Update(int patientId, int? ptn_pk, DateTime dateOfBirth, string nationalId, int facilityId, string auditData, PatientEntryPoint entryPoints, int entryPointId, string entryPointAuditData, DateTime enrollmentDate, List<DTOIdentifier> internalPatientIdentifiers)
        {
            try
            {
                var patientManager = new PatientManager();
                var patientEntryPointManager = new PatientEntryPointManager();
                var patientIdentifierManager = new PatientIdentifierManager();
                var patientEnrollmentManager = new PatientEnrollmentManager();

                PatientEntity updatePatient = new PatientEntity();
                updatePatient.ptn_pk = ptn_pk;
                updatePatient.DateOfBirth = dateOfBirth;
                updatePatient.NationalId = nationalId;
                updatePatient.FacilityId = facilityId;
                updatePatient.AuditData = auditData;

                patientManager.UpdatePatient(updatePatient, patientId);

                if (entryPoints == null)
                {
                    patientEntryPointManager.addPatientEntryPoint(patientId, entryPointId, 1);
                }
                else
                {
                    entryPoints.EntryPointId = entryPointId;
                    entryPoints.AuditData = entryPointAuditData;
                    patientEntryPointManager.UpdatePatientEntryPoint(entryPoints);   
                }

                foreach (var item in internalPatientIdentifiers)
                {
                    if (item.IdentifierType == "CCC_NUMBER" && item.AssigningAuthority == "CCC")
                    {
                        var identifiersByPatientId = patientIdentifierManager.GetPatientEntityIdentifiersByPatientId(patientId, 1);

                        if (identifiersByPatientId.Count > 0)
                        {
                            foreach (var entityIdentifier in identifiersByPatientId)
                            {
                                int enrollmentId = entityIdentifier.PatientEnrollmentId;

                                PatientEntityEnrollment entityEnrollment = patientEnrollmentManager.GetPatientEntityEnrollment(enrollmentId);
                                List<PatientEntityEnrollment> listEnrollment = new List<PatientEntityEnrollment>();
                                listEnrollment.Add(entityEnrollment);
                                var enrollmentAuditData = AuditDataUtility.AuditDataUtility.Serializer(listEnrollment);

                                entityEnrollment.EnrollmentDate = enrollmentDate;
                                entityEnrollment.AuditData = enrollmentAuditData;

                                patientEnrollmentManager.updatePatientEnrollment(entityEnrollment);

                                var entityIdentifierAuditData = AuditDataUtility.AuditDataUtility.Serializer(identifiersByPatientId);
                                entityIdentifier.IdentifierValue = item.IdentifierValue;
                                entityIdentifier.AuditData = entityIdentifierAuditData;
                                patientIdentifierManager.UpdatePatientIdentifier(entityIdentifier);
                            }
                        }
                        else
                        {
                            int patientEnrollmentId = patientEnrollmentManager.addPatientEnrollment(patientId, enrollmentDate.ToString(), 1);
                            int patientEntryPointId = patientEntryPointManager.addPatientEntryPoint(patientId, entryPointId, 1);
                            int patientIdentifierId = patientIdentifierManager.addPatientIdentifier(patientId, patientEnrollmentId, 1, item.IdentifierValue);
                        }
                    }
                }

                return "Successfully updated patient";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
