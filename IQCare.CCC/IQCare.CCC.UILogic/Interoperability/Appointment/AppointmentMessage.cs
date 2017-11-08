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
                        if (patientMessage.NATIONAL_ID != null && patientMessage.NATIONAL_ID != "99999999")
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

                        //set appointment information
                        APPOINTMENT_INFORMATION appointmentInformation = new APPOINTMENT_INFORMATION()
                        {
                            PLACER_APPOINTMENT_NUMBER = new PLACER_APPOINTMENT_NUMBER()
                            {
                                NUMBER = appointmentMessage.AppointmentId.ToString(),
                                ENTITY = "IQCARE"
                            },
                            APPOINTMENT_REASON = appointmentMessage.AppointmentReason,
                            APPOINTMENT_TYPE = "",
                            APPOINTMENT_DATE = appointmentMessage.AppointmentDate,
                            APPOINTMENT_PLACING_ENTITY = "IQCARE",
                            APPOINTMENT_LOCATION = "",
                            ACTION_CODE = "A",
                            APPOINTMENT_NOTE = appointmentMessage.Description,
                            APPOINTMENT_HONORED = appointmentMessage.AppointmentStatus
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
                PatientLookupManager patientLookup = new PatientLookupManager();
                LookupLogic lookupLogic = new LookupLogic();
                PatientMasterVisitManager masterVisitManager = new PatientMasterVisitManager();
                PatientLookup patient = new PatientLookup();
                string cccNumber = String.Empty;
                string appointmentReason = String.Empty;
                string appointmentStatus = String.Empty;

                foreach (var item in appointmentScheduling.PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID)
                {
                    if (item.IDENTIFIER_TYPE == "CCC_NUMBER" && item.ASSIGNING_AUTHORITY == "CCC")
                    {
                        cccNumber = item.ID;
                    }
                }

                if (!String.IsNullOrWhiteSpace(cccNumber))
                {
                    patient = patientLookup.GetPatientByCccNumber(cccNumber);
                    if (patient != null)
                    {
                        int patientMasterVisitId = masterVisitManager.GetLastPatientVisit(patient.Id).Id;
                        int differentiatedCareId = lookupLogic.GetItemIdByGroupAndItemName("DifferentiatedCare", "Standard Care")[0].ItemId;
                        int serviceAreaId = lookupLogic.GetItemIdByGroupAndItemName("ServiceArea", "MoH 257 GREENCARD")[0].ItemId;
                        

                        foreach (var appointment in appointmentScheduling.APPOINTMENT_INFORMATION)
                        {
                            switch (appointment.APPOINTMENT_REASON)
                            {
                                case "REGIMEN_REFILL":
                                    appointmentReason = "Pharmacy Refill";
                                    break;
                                default:
                                    appointmentReason = "Follow Up";
                                break;
                            }

                            switch (appointment.APPOINTMENT_HONORED)
                            {
                                case "N":
                                    appointmentStatus = "Missed";
                                    break;
                                case "Y":
                                    appointmentStatus = "Met";
                                    break;
                            }

                            int reasonId = lookupLogic.GetItemIdByGroupAndItemName("AppointmentReason", appointmentReason)[0].ItemId;
                            int statusId = lookupLogic.GetItemIdByGroupAndItemName("AppointmentStatus", appointmentStatus)[0].ItemId;

                            if (appointment.ACTION_CODE == "A")
                            {
                                PatientAppointmentManager manager = new PatientAppointmentManager();
                                PatientAppointment patientAppointment = new PatientAppointment()
                                {
                                    PatientId = patient.Id,
                                    PatientMasterVisitId = patientMasterVisitId,
                                    AppointmentDate = DateTime.ParseExact(appointment.APPOINTMENT_DATE, "yyyyMMdd", null),
                                    Description = appointment.APPOINTMENT_NOTE,
                                    DifferentiatedCareId = differentiatedCareId,
                                    ReasonId = reasonId,
                                    ServiceAreaId = serviceAreaId,
                                    StatusId = statusId,
                                };

                                manager.AddPatientAppointments(patientAppointment);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return String.Empty;
        }

        public static string Update(PatientAppointSchedulingDTO appointScheduling)
        {
            return String.Empty;
        }
    }
}
