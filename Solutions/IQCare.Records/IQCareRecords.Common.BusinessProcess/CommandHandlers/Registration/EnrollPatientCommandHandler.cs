using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using Entities.Records;
using IQCareRecords.Common.BusinessProcess.Commands;
using System.Threading.Tasks;
using System.Threading;
using IQCare.Records.UILogic;
using IQCare.Records.UILogic.Enrollment;
using Entities.Records.Enrollment;
using IQCare.Records.UILogic.Visit;


namespace IQCareRecords.Common.BusinessProcess.CommandHandlers.Registration
{
    public class EnrollPatientCommandHandler : IRequestHandler<EnrollPatientCommand, Result<AddEnrollPatientResponse>>
    {
        public int patientId { get; set; }
        public string msg { get; set; }
        private int patientMasterVisitId { get; set; }
        private int patientEnrollmentId { get; set; }
        private int patientIdentifierId { get; set; }
        private int patientEntryPointId { get; set; }

        
        public async Task<Result<AddEnrollPatientResponse>> Handle(EnrollPatientCommand request, CancellationToken cancellationToken)
        {
            try
            {
               String sDate = DateTime.Now.ToString();
                DateTime datevalue = Convert.ToDateTime(sDate);
                var patientMasterVisitManager = new PatientMasterVisitManager();
                var patientEnrollmentManager = new PatientEnrollmentManager();
                var personLookUp = new PersonLookUpManager();
                var patientEntryPointManager = new PatientEntryPointManager();
                var patientLookUpManager= new PatientLookupManager();
                var patientManager = new PatientManager();
                var patientIdentifierManager = new PatientIdentifierManager();
                var personContactLookUpManager = new PersonContactLookupManager();
                var lookupLogic = new LookupLogic();
                var personContacts = new List<PersonContactLookUp>();
                var identifiersObjects = request.identifiersList;
                List<PatientLookup> isPersonEnrolled = await Task.Run(() => patientLookUpManager.GetPatientByPersonId(request.PersonId));
                string dobPrecision = String.IsNullOrEmpty(request.DobPrecision) ? "false" : "true";
                foreach (var item in identifiersObjects)
                {
                    var identifiers = await Task.Run(() => patientIdentifierManager.CheckIfIdentifierNumberIsUsed(item.Value, Convert.ToInt32(item.Key)));
                    if (identifiers.Count > 0)
                    {
                        foreach (var items in identifiers)
                        {
                            if (isPersonEnrolled.Count > 0)
                            {
                                if (items.PatientId == isPersonEnrolled[0].Id)
                                {

                                }
                                else
                                {
                                    var exception = new Exception("No: " + item.Value + " already exists");
                                    msg = exception.Message.ToString();
                                    throw exception;
                                }
                            }
                            else
                            {
                                var exception = new Exception("No: " + item.Value + " already exists");
                                msg = exception.Message.ToString();
                                throw exception;
                            }
                        }
                    }
                }

                if (isPersonEnrolled.Count == 0)
                {

                    List<PatientRegistrationLookup> patientsByPersonId = await Task.Run(() => patientManager.GetPatientIdByPersonId(request.PersonId));
                    var patientIndex = datevalue.Year.ToString() + '-' + request.PersonId;
                    PatientEntity patient = new PatientEntity();
                    if (patientsByPersonId.Count > 0)
                    {
                        patient.FacilityId = request.facilityId;
                        patient.DateOfBirth = DateTime.Parse(request.DateofBirth);
                        patient.NationalId = request.nationalId;
                        patient.ptn_pk = patientsByPersonId[0].ptn_pk > 0 ? patientsByPersonId[0].ptn_pk : 0;

                        patientManager.UpdatePatient(patient, patientsByPersonId[0].Id);
                        patientId = patientsByPersonId[0].Id;
                    }
                    else
                    {
                        patient.PersonId = request.PersonId;
                        patient.ptn_pk = 0;
                        patient.FacilityId = request.facilityId;
                        patient.PatientType = request.PatientType;
                        patient.PatientIndex = patientIndex;
                        patient.DateOfBirth = DateTime.Parse(request.DateofBirth);
                        patient.NationalId = (request.nationalId);
                        patient.Active = true;
                        patient.CreatedBy = request.UserId;
                        patient.CreateDate = DateTime.Now;
                        patient.DeleteFlag = false;
                        patient.DobPrecision = bool.Parse(dobPrecision);

                        patientId = patientManager.AddPatient(patient);
                    }
                    if (patientId > 0)
                    {
                        var visitTypes = await Task.Run(() => lookupLogic.GetItemIdByGroupAndItemName("VisitType", "Enrollment"));
                        var visitType = 0;
                        if (visitTypes.Count > 0)
                        {
                            visitType = visitTypes[0].ItemId;
                        }

                        //Add enrollment visit
                        patientMasterVisitId =
                            patientMasterVisitManager.AddPatientMasterVisit(patientId, request.UserId, visitType);
                        //Enroll Patient to service
                        patientEnrollmentId = patientEnrollmentManager.addPatientEnrollment(patientId, request.EnrollmentDate, request.UserId);
                        //Add enrollment entry point
                        patientEntryPointId = patientEntryPointManager.addPatientEntryPoint(patientId, request.EntryPointId, request.UserId);



                        if (patientMasterVisitId > 0)
                        {
                            foreach (var item in identifiersObjects)
                            {
                                patientIdentifierId = await Task.Run(() => patientIdentifierManager.addPatientIdentifier(patientId,
                                patientEnrollmentId, Convert.ToInt32(item.Key), item.Value, request.facilityId));
                                //Get User Details to be used in BLUE CARD
                            }

                            msg = "Succefully enrolled patient.";

                        }
                    }

                    {
                        var patientLookManager = new PatientLookupManager();
                        List<PatientLookup> patientlook = await Task.Run(() => patientLookManager.GetPatientByPersonId(request.PersonId));

                        if (patientlook.Count > 0)
                        {
                            int ptn_pk = patientlook[0].Id;

                            List<PatientEntity> listPatient = new List<PatientEntity>();
                            var entity = await Task.Run(() => patientlook.ConvertAll(x => new PatientEntity { Id = x.Id, Active = x.Active, DateOfBirth = x.DateOfBirth, ptn_pk = x.ptn_pk, PatientType = x.PatientType, PatientIndex = x.PatientIndex, NationalId = x.NationalId, FacilityId = x.FacilityId }));
                            var patientAuditData = await Task.Run(() => AuditDataUtility.Serializer(entity));

                            PatientEntity updatePatient = new PatientEntity();
                            updatePatient.ptn_pk = patientlook[0].ptn_pk;
                            updatePatient.DateOfBirth = patientlook[0].DateOfBirth;
                            updatePatient.NationalId = request.nationalId;
                            updatePatient.FacilityId = patientlook[0].FacilityId;


                            //listPatient.Add(entity);
                            updatePatient.AuditData = patientAuditData;
                            //var enrollmentAuditData = AuditDataUtility.Serializer(patient);

                            await Task.Run(() => patientManager.UpdatePatient(updatePatient, patientlook[0].Id));

                            int patientMasterVisitId = await Task.Run(() => patientMasterVisitManager.PatientMasterVisitCheckin(patientlook[0].Id, request.UserId));
                            int PatientMasterVisitId = patientMasterVisitId;

                            List<PatientEntryPoint> entryPoints = await Task.Run(() => patientEntryPointManager.GetPatientEntryPoints(patientlook[0].Id));

                            if (entryPoints.Count > 0)
                            {
                                var entryPointAuditData = await Task.Run(() => AuditDataUtility.Serializer(entryPoints));

                                entryPoints[0].EntryPointId = request.EntryPointId;
                                entryPoints[0].AuditData = entryPointAuditData;

                                await Task.Run(() => patientEntryPointManager.UpdatePatientEntryPoint(entryPoints[0]));
                            }
                            foreach (var item in identifiersObjects)
                            {
                                var identifiersByPatientId = await Task.Run(() => patientIdentifierManager
                                    .GetPatientEntityIdentifiersByPatientId(patientlook[0].Id, Convert.ToInt32(item.Key)));

                                if (identifiersByPatientId.Count > 0)
                                {
                                    foreach (var entityIdentifier in identifiersByPatientId)
                                    {
                                        int enrollmentId = entityIdentifier.PatientEnrollmentId;

                                        PatientEntityEnrollment entityEnrollment =
                                          await Task.Run(() => patientEnrollmentManager.GetPatientEntityEnrollment(enrollmentId));
                                        List<PatientEntityEnrollment> listEnrollment = new List<PatientEntityEnrollment>();
                                        await Task.Run(() => listEnrollment.Add(entityEnrollment));
                                        var enrollmentAuditData = await Task.Run(() => AuditDataUtility.Serializer(listEnrollment));

                                        entityEnrollment.EnrollmentDate = DateTime.Parse(request.EnrollmentDate);
                                        entityEnrollment.AuditData = enrollmentAuditData;

                                        patientEnrollmentManager.updatePatientEnrollment(entityEnrollment);

                                        var entityIdentifierAuditData = AuditDataUtility.Serializer(identifiersByPatientId);
                                        entityIdentifier.IdentifierValue = item.Value;
                                        entityIdentifier.AuditData = entityIdentifierAuditData;
                                        patientIdentifierManager.UpdatePatientIdentifier(entityIdentifier, request.facilityId);
                                    }
                                }
                                else
                                {
                                    patientEnrollmentId = await Task.Run(() => patientEnrollmentManager.addPatientEnrollment(patientlook[0].Id, request.EnrollmentDate, request.UserId));
                                    patientEntryPointId = await Task.Run(() => patientEntryPointManager.addPatientEntryPoint(patientlook[0].Id, request.EntryPointId, request.UserId));
                                    patientIdentifierId = await Task.Run(() => patientIdentifierManager.addPatientIdentifier(patientlook[0].Id,
                                        patientEnrollmentId, Convert.ToInt32(item.Key), item.Value, request.facilityId));
                                }
                            }
                        }


                       
                    }

                }


                return Result<AddEnrollPatientResponse>.Valid(new AddEnrollPatientResponse()
                {
                    IdentifierValue = Convert.ToString(patientEnrollmentId),
                    IdentifierId = patientIdentifierId,
                    PatientId = patientId,
                    Message = msg
                }

                 );
            }
            catch (Exception ex)
            {
                return Result<AddEnrollPatientResponse>.Invalid(ex.Message);
            }


        }
    }
}
