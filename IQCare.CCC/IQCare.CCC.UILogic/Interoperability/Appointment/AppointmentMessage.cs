using Entities.CCC.Appointment;
using Entities.CCC.Interoperability;
using Entities.CCC.Lookup;
using IQCare.CCC.UILogic.Interoperability.Enrollment;
using IQCare.CCC.UILogic.Visit;
using IQCare.DTO.CommonEntities;
using IQCare.DTO.PatientAppointment;
using System;
using System.Collections.Generic;
using IQCare.CCC.UILogic.Enrollment;
using Entities.CCC.Enrollment;

namespace IQCare.CCC.UILogic.Interoperability.Appointment
{
    public class AppointmentMessage
    {
        public static PatientAppointSchedulingDTO Get(int entityId)
        {
            try
            {
                PatientAppointmentMessageManager appointmentManager = new PatientAppointmentMessageManager();
                PatientAppointmentMessage appointmentMessage = appointmentManager.GetPatientAppointmentMessageById(entityId);
                PatientAppointSchedulingDTO appointmentScheduling = new PatientAppointSchedulingDTO();

                if (appointmentMessage != null)
                {
                    PatientMessageManager patientMessageManager = new PatientMessageManager();
                    PatientMessage patientMessage = patientMessageManager.GetPatientMessageByEntityId(appointmentMessage.PatientId);
                    var personIdentifierManager = new PersonIdentifierManager();

                    if (patientMessage != null)
                    {
                        IdentifierManager identifierManager = new IdentifierManager();
                        Identifier identifier = identifierManager.GetIdentifierByCode("GODS_NUMBER");
                        var personIdentifiers = personIdentifierManager.GetPersonIdentifiers(patientMessage.Id, identifier.Id);

                        appointmentScheduling.PATIENT_IDENTIFICATION = appointmentScheduling.PATIENT_IDENTIFICATION == null ? new APPOINTMENTPATIENTIDENTIFICATION() : appointmentScheduling.PATIENT_IDENTIFICATION;
                        appointmentScheduling.APPOINTMENT_INFORMATION = appointmentScheduling.APPOINTMENT_INFORMATION == null ? new List<APPOINTMENT_INFORMATION>() : appointmentScheduling.APPOINTMENT_INFORMATION;
                        appointmentScheduling.PATIENT_IDENTIFICATION.PATIENT_NAME = appointmentScheduling.PATIENT_IDENTIFICATION.PATIENT_NAME == null ? new PATIENTNAME() : appointmentScheduling.PATIENT_IDENTIFICATION.PATIENT_NAME;
                        appointmentScheduling.PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID = appointmentScheduling.PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID == null ? new List<INTERNALPATIENTID>() : appointmentScheduling.PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID;
                        appointmentScheduling.PATIENT_IDENTIFICATION.EXTERNAL_PATIENT_ID = appointmentScheduling.PATIENT_IDENTIFICATION.EXTERNAL_PATIENT_ID == null ? new EXTERNALPATIENTID() : appointmentScheduling.PATIENT_IDENTIFICATION.EXTERNAL_PATIENT_ID;


                        INTERNALPATIENTID internalPatientId = new INTERNALPATIENTID();
                        internalPatientId.ID = patientMessage.IdentifierValue;
                        internalPatientId.IDENTIFIER_TYPE = "CCC_NUMBER";
                        internalPatientId.ASSIGNING_AUTHORITY = "CCC";

                        appointmentScheduling.PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID.Add(internalPatientId);
                        if (!String.IsNullOrWhiteSpace(patientMessage.NATIONAL_ID) && patientMessage.NATIONAL_ID != "99999999")
                        {
                            INTERNALPATIENTID internalNationalId = new INTERNALPATIENTID();
                            internalNationalId.ID = patientMessage.NATIONAL_ID;
                            internalNationalId.IDENTIFIER_TYPE = "NATIONAL_ID";
                            internalNationalId.ASSIGNING_AUTHORITY = "GOK";

                            appointmentScheduling.PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID.Add(internalNationalId);
                        }

                        //External Patient Id
                        appointmentScheduling.PATIENT_IDENTIFICATION.EXTERNAL_PATIENT_ID.ID = personIdentifiers.Count > 0 ? personIdentifiers[0].IdentifierValue : String.Empty;
                        appointmentScheduling.PATIENT_IDENTIFICATION.EXTERNAL_PATIENT_ID.ASSIGNING_AUTHORITY = "MPI";
                        appointmentScheduling.PATIENT_IDENTIFICATION.EXTERNAL_PATIENT_ID.IDENTIFIER_TYPE = "GODS_NUMBER";

                        //set names
                        appointmentScheduling.PATIENT_IDENTIFICATION.PATIENT_NAME.FIRST_NAME = !string.IsNullOrWhiteSpace(patientMessage.FIRST_NAME) ? patientMessage.FIRST_NAME : "";
                        appointmentScheduling.PATIENT_IDENTIFICATION.PATIENT_NAME.MIDDLE_NAME = !string.IsNullOrWhiteSpace(patientMessage.MIDDLE_NAME) ? patientMessage.MIDDLE_NAME : "";
                        appointmentScheduling.PATIENT_IDENTIFICATION.PATIENT_NAME.LAST_NAME = !string.IsNullOrWhiteSpace(patientMessage.LAST_NAME) ? patientMessage.LAST_NAME : "";

                        string appointmentReason = String.Empty;
                        string appointmentType = String.Empty;
                        string appointmentLocation = String.Empty;
                        string appointmentStatus = String.Empty;
                        switch (appointmentMessage.AppointmentReason)
                        {
                            case "Pharmacy Refill":
                                appointmentReason = "PHARMACY_REFILL";
                                break;
                            case "Treatment Preparation":
                                appointmentReason = "TREATMENT_PREP";
                                break;
                            case "Lab Tests":
                                appointmentReason = "LAB_TEST";
                                break;
                            case "Follow Up":
                                appointmentReason = "FOLLOWUP";
                                break;
                            default:
                                appointmentReason = "FOLLOWUP";
                                break;
                        }

                        switch (appointmentMessage.AppointmentType)
                        {
                            case "Standard Care":
                                appointmentType = "CLINICAL";
                                break;
                            case "Express Care":
                                appointmentType = "PHARMACY";
                                break;
                            case "Community Based Dispensing":
                                appointmentType = "PHARMACY";
                                break;
                            default:
                                appointmentType = "CLINICAL";
                                break;
                        }

                        switch (appointmentMessage.AppointmentReason)
                        {
                            case "Pharmacy Refill":
                                appointmentLocation = "PHARMACY";
                                break;
                            case "Treatment Preparation":
                                appointmentLocation = "CLINIC";
                                break;
                            case "Lab Tests":
                                appointmentLocation = "LAB";
                                break;
                            case "Follow Up":
                                appointmentLocation = "CLINIC";
                                break;
                            default:
                                appointmentLocation = "CLINIC";
                                break;
                        }

                        switch (appointmentMessage.AppointmentStatus)
                        {
                            case "PreviouslyMissed":
                                appointmentStatus = "MISSED";
                                break;
                            case "CareEnded":
                                appointmentStatus = "CANCELLED";
                                break;
                            case "Met":
                                appointmentStatus = "HONORED";
                                break;
                            case "Missed":
                                appointmentStatus = "MISSED";
                                break;
                            case "Pending":
                                appointmentStatus = "PENDING";
                                break;
                        }

                        //set appointment information
                        APPOINTMENT_INFORMATION appointmentInformation = new APPOINTMENT_INFORMATION()
                        {
                            PLACER_APPOINTMENT_NUMBER = new PLACER_APPOINTMENT_NUMBER()
                            {
                                NUMBER = appointmentMessage.AppointmentId.ToString(),
                                ENTITY = "IQCARE"
                            },
                            APPOINTMENT_REASON = appointmentReason,
                            APPOINTMENT_TYPE = appointmentType,
                            APPOINTMENT_DATE = appointmentMessage.AppointmentDate,
                            APPOINTMENT_PLACING_ENTITY = "IQCARE",
                            APPOINTMENT_LOCATION = appointmentLocation,
                            ACTION_CODE = "A",
                            APPOINTMENT_NOTE = appointmentMessage.Description,
                            APPOINTMENT_HONORED = appointmentStatus
                        };

                        appointmentScheduling.APPOINTMENT_INFORMATION.Add(appointmentInformation);
                    }
                    
                }


                return appointmentScheduling;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static string Save(PatientAppointSchedulingDTO appointmentScheduling)
        {
            try
            {
                PatientAppointmentManager manager = new PatientAppointmentManager();
                PatientLookupManager patientLookup = new PatientLookupManager();
                LookupLogic lookupLogic = new LookupLogic();
                PatientMasterVisitManager masterVisitManager = new PatientMasterVisitManager();
                var personIdentifierManager = new PersonIdentifierManager();
                var interopPlacerValuesManager = new InteropPlacerValuesManager();
                PatientLookup patient = new PatientLookup();
                string cccNumber = String.Empty;
                string appointmentReason = String.Empty;
                string appointmentStatus = String.Empty;
                string appointmentType = String.Empty;
                int interopUserId = InteropUser.UserId;
                foreach (var item in appointmentScheduling.PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID)
                {
                    if (item.IDENTIFIER_TYPE == "CCC_NUMBER" && item.ASSIGNING_AUTHORITY == "CCC")
                    {
                        cccNumber = item.ID;
                    }
                }
                string godsNumber = appointmentScheduling.PATIENT_IDENTIFICATION.EXTERNAL_PATIENT_ID.ID;

                if (!String.IsNullOrWhiteSpace(cccNumber))
                {
                    patient = patientLookup.GetPatientByCccNumber(cccNumber);
                    if (patient != null)
                    {
                        if (!string.IsNullOrWhiteSpace(godsNumber))
                        {
                            IdentifierManager identifierManager = new IdentifierManager();
                            Identifier identifier = identifierManager.GetIdentifierByCode("GODS_NUMBER");
                            var personIdentifiers = personIdentifierManager.GetPersonIdentifiers(patient.PersonId, identifier.Id);
                            if (personIdentifiers.Count == 0)
                            {
                                personIdentifierManager.AddPersonIdentifier(patient.PersonId, identifier.Id, godsNumber,interopUserId
                 );
                            }
                        }

                        int patientMasterVisitId = masterVisitManager.GetLastPatientVisit(patient.Id).Id;
                        int serviceAreaId = 0;
                        var areas = lookupLogic.GetItemIdByGroupAndItemName("ServiceArea", "MoH 257 GREENCARD");
                        if (null == areas || areas.Count == 0)
                        {
                            serviceAreaId = 203;
                        }
                        else { serviceAreaId = areas[0].ItemId;
                        }

                        foreach (var appointment in appointmentScheduling.APPOINTMENT_INFORMATION)
                        {
                            switch (appointment.APPOINTMENT_REASON)
                            {
                                case "PHARMACY_REFILL":
                                    appointmentReason = "Pharmacy Refill";
                                    break;
                                case "TREATMENT_PREP":
                                    appointmentReason = "Treatment Preparation";
                                    break;
                                case "LAB_TEST":
                                    appointmentReason = "Lab Tests";
                                    break;
                                case "FOLLOWUP":
                                    appointmentReason = "Follow Up";
                                    break;
                                default:
                                    appointmentReason = "Follow Up";
                                break;
                            }

                            switch (appointment.APPOINTMENT_HONORED)
                            {
                                case "HONORED":
                                    appointmentStatus = "Met";
                                    break;
                                case "MISSED":
                                    appointmentStatus = "Missed";
                                    break;
                                case "PENDING":
                                    appointmentStatus = "Pending";
                                    break;
                                case "CANCELLED":
                                    appointmentStatus = "CareEnded";
                                    break;
                                default:
                                    appointmentStatus = "Pending";
                                    break;
                            }

                            switch (appointment.APPOINTMENT_TYPE)
                            {
                                case "CLINICAL":
                                    appointmentType = "Standard Care";
                                    break;
                                case "PHARMACY":
                                    appointmentType = "Express Care";
                                    break;
                                case "INVESTIGATION":
                                    appointmentType = "Express Care";
                                    break;
                                default:
                                    appointmentType = "Standard Care";
                                    break;
                            }

                            int reasonId = lookupLogic.GetItemIdByGroupAndItemName("AppointmentReason", appointmentReason)[0].ItemId;
                            int statusId = lookupLogic.GetItemIdByGroupAndItemName("AppointmentStatus", appointmentStatus)[0].ItemId;
                            int differentiatedCareId = lookupLogic.GetItemIdByGroupAndItemName("DifferentiatedCare", appointmentType)[0].ItemId;

                            InteropPlacerTypeManager interopPlacerTypeManager = new InteropPlacerTypeManager();
                            int interopPlacerTypeId = interopPlacerTypeManager.GetInteropPlacerTypeByName(appointment.PLACER_APPOINTMENT_NUMBER.ENTITY).Id;

                            var interopPlacerValues = interopPlacerValuesManager.GetInteropPlacerValues(interopPlacerTypeId, 3, Convert.ToInt32(appointment.PLACER_APPOINTMENT_NUMBER.NUMBER));
                            if (interopPlacerValues != null)
                            {
                                PatientAppointment patientAppointment = manager.GetPatientAppointment(interopPlacerValues.EntityId);
                                patientAppointment.AppointmentDate = DateTime.ParseExact(appointment.APPOINTMENT_DATE, "yyyyMMdd", null);
                                patientAppointment.Description = appointment.APPOINTMENT_NOTE;
                                patientAppointment.DifferentiatedCareId = differentiatedCareId;
                                patientAppointment.ReasonId = reasonId;
                                patientAppointment.ServiceAreaId = serviceAreaId;
                                patientAppointment.StatusId = statusId;
                                manager.UpdatePatientAppointments(patientAppointment);
                            }
                            else
                            {
                                PatientAppointment patientAppointment = new PatientAppointment()
                                {
                                    PatientId = patient.Id,
                                    PatientMasterVisitId = patientMasterVisitId,
                                    AppointmentDate = DateTime.ParseExact(appointment.APPOINTMENT_DATE, "yyyyMMdd", null),
                                    Description = appointment.APPOINTMENT_NOTE,
                                    DifferentiatedCareId = differentiatedCareId,
                                    ReasonId = reasonId,
                                    ServiceAreaId = serviceAreaId,
                                    StatusId = statusId
                                };

                                int appointmentId = manager.AddPatientAppointments(patientAppointment,false);
                                InteropPlacerValues placerValues = new InteropPlacerValues()
                                {
                                    IdentifierType = 3,
                                    EntityId = appointmentId,
                                    InteropPlacerTypeId = interopPlacerTypeId,
                                    PlacerValue = Convert.ToInt32(appointment.PLACER_APPOINTMENT_NUMBER.NUMBER)
                                };
                                interopPlacerValuesManager.AddInteropPlacerValue(placerValues);
                            }
                        }
                    }
                    else
                    {
                        throw new Exception("patient with ccc_number: " + cccNumber + " not found");
                    }
                }
                else
                {
                    throw new Exception("null or empty ccc_number");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return "success";
        }

        public static string Update(PatientAppointSchedulingDTO appointmentScheduling)
        {
            try
            {
                PatientAppointmentManager manager = new PatientAppointmentManager();
                PatientLookupManager patientLookup = new PatientLookupManager();
                LookupLogic lookupLogic = new LookupLogic();
                PatientMasterVisitManager masterVisitManager = new PatientMasterVisitManager();
                var personIdentifierManager = new PersonIdentifierManager();
                var interopPlacerValuesManager = new InteropPlacerValuesManager();
                PatientLookup patient = new PatientLookup();
                string cccNumber = String.Empty;
                string appointmentReason = String.Empty;
                string appointmentStatus = String.Empty;
                string appointmentType = String.Empty;

                foreach (var item in appointmentScheduling.PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID)
                {
                    if (item.IDENTIFIER_TYPE == "CCC_NUMBER" && item.ASSIGNING_AUTHORITY == "CCC")
                    {
                        cccNumber = item.ID;
                    }
                }
                string godsNumber = appointmentScheduling.PATIENT_IDENTIFICATION.EXTERNAL_PATIENT_ID.ID;

                if (!String.IsNullOrWhiteSpace(cccNumber))
                {
                    patient = patientLookup.GetPatientByCccNumber(cccNumber);
                    if (patient != null)
                    {
                        if (!string.IsNullOrWhiteSpace(godsNumber))
                        {
                            IdentifierManager identifierManager = new IdentifierManager();
                            Identifier identifier = identifierManager.GetIdentifierByCode("GODS_NUMBER");
                            var personIdentifiers = personIdentifierManager.GetPersonIdentifiers(patient.PersonId, identifier.Id);
                            if (personIdentifiers.Count == 0)
                            {
                                personIdentifierManager.AddPersonIdentifier(patient.PersonId, identifier.Id, godsNumber, 1);
                            }
                        }

                        int patientMasterVisitId = masterVisitManager.GetLastPatientVisit(patient.Id).Id;
                        int serviceAreaId = lookupLogic.GetItemIdByGroupAndItemName("ServiceArea", "MoH 257 GREENCARD")[0].ItemId;


                        foreach (var appointment in appointmentScheduling.APPOINTMENT_INFORMATION)
                        {
                            switch (appointment.APPOINTMENT_REASON)
                            {
                                case "PHARMACY_REFILL":
                                    appointmentReason = "Pharmacy Refill";
                                    break;
                                case "TREATMENT_PREP":
                                    appointmentReason = "Treatment Preparation";
                                    break;
                                case "LAB_TEST":
                                    appointmentReason = "Lab Tests";
                                    break;
                                case "FOLLOWUP":
                                    appointmentReason = "Follow Up";
                                    break;
                                default:
                                    appointmentReason = "Follow Up";
                                    break;
                            }

                            switch (appointment.APPOINTMENT_HONORED)
                            {
                                case "HONORED":
                                    appointmentStatus = "Met";
                                    break;
                                case "MISSED":
                                    appointmentStatus = "Missed";
                                    break;
                                case "PENDING":
                                    appointmentStatus = "Pending";
                                    break;
                                case "CANCELLED":
                                    appointmentStatus = "CareEnded";
                                    break;
                                default:
                                    appointmentStatus = "Pending";
                                    break;
                            }

                            switch (appointment.APPOINTMENT_TYPE)
                            {
                                case "CLINICAL":
                                    appointmentType = "Standard Care";
                                    break;
                                case "PHARMACY":
                                    appointmentType = "Express Care";
                                    break;
                                case "INVESTIGATION":
                                    appointmentType = "Express Care";
                                    break;
                                default:
                                    appointmentType = "Standard Care";
                                    break;
                            }

                            int reasonId = lookupLogic.GetItemIdByGroupAndItemName("AppointmentReason", appointmentReason)[0].ItemId;
                            int statusId = lookupLogic.GetItemIdByGroupAndItemName("AppointmentStatus", appointmentStatus)[0].ItemId;
                            int differentiatedCareId = lookupLogic.GetItemIdByGroupAndItemName("DifferentiatedCare", appointmentType)[0].ItemId;

                            InteropPlacerTypeManager interopPlacerTypeManager = new InteropPlacerTypeManager();
                            int interopPlacerTypeId = interopPlacerTypeManager.GetInteropPlacerTypeByName(appointment.PLACER_APPOINTMENT_NUMBER.ENTITY).Id;

                            var interopPlacerValues = interopPlacerValuesManager.GetInteropPlacerValues(interopPlacerTypeId, 3, Convert.ToInt32(appointment.PLACER_APPOINTMENT_NUMBER.NUMBER));
                            if (interopPlacerValues != null)
                            {
                                PatientAppointment patientAppointment = manager.GetPatientAppointment(interopPlacerValues.EntityId);
                                patientAppointment.AppointmentDate = DateTime.ParseExact(appointment.APPOINTMENT_DATE, "yyyyMMdd", null);
                                patientAppointment.Description = appointment.APPOINTMENT_NOTE;
                                patientAppointment.DifferentiatedCareId = differentiatedCareId;
                                patientAppointment.ReasonId = reasonId;
                                patientAppointment.ServiceAreaId = serviceAreaId;
                                patientAppointment.StatusId = statusId;
                                manager.UpdatePatientAppointments(patientAppointment);
                            }
                            else
                            {
                                PatientAppointment patientAppointment = new PatientAppointment()
                                {
                                    PatientId = patient.Id,
                                    PatientMasterVisitId = patientMasterVisitId,
                                    AppointmentDate = DateTime.ParseExact(appointment.APPOINTMENT_DATE, "yyyyMMdd", null),
                                    Description = appointment.APPOINTMENT_NOTE,
                                    DifferentiatedCareId = differentiatedCareId,
                                    ReasonId = reasonId,
                                    ServiceAreaId = serviceAreaId,
                                    StatusId = statusId
                                };

                                int appointmentId = manager.AddPatientAppointments(patientAppointment);
                                InteropPlacerValues placerValues = new InteropPlacerValues()
                                {
                                    IdentifierType = 3,
                                    EntityId = appointmentId,
                                    InteropPlacerTypeId = interopPlacerTypeId,
                                    PlacerValue = Convert.ToInt32(appointment.PLACER_APPOINTMENT_NUMBER.NUMBER)
                                };
                                interopPlacerValuesManager.AddInteropPlacerValue(placerValues);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return String.Empty;
        }
    }
}
