using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IQCare.DTO.PatientAppointment;
using Entities.CCC.Interoperability;
using IQCare.DTO.CommonEntities;

namespace IQCare.CCC.UILogic.Interoperability
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

                    if (patientMessage != null)
                    {
                        appointmentScheduling.PATIENT_IDENTIFICATION = appointmentScheduling.PATIENT_IDENTIFICATION == null ? new PATIENTIDENTIFICATION() : appointmentScheduling.PATIENT_IDENTIFICATION;
                        appointmentScheduling.APPOINTMENT_INFORMATION = appointmentScheduling.APPOINTMENT_INFORMATION == null ? new List<APPOINTMENT_INFORMATION>() : appointmentScheduling.APPOINTMENT_INFORMATION;
                        appointmentScheduling.PATIENT_IDENTIFICATION.PATIENT_NAME = appointmentScheduling.PATIENT_IDENTIFICATION.PATIENT_NAME == null ? new PATIENTNAME() : appointmentScheduling.PATIENT_IDENTIFICATION.PATIENT_NAME;
                        appointmentScheduling.PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID = appointmentScheduling.PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID == null ? new List<INTERNALPATIENTID>() : appointmentScheduling.PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID;

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
                        appointmentScheduling.PATIENT_IDENTIFICATION.EXTERNAL_PATIENT_ID.ID = String.Empty;
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
    }
}
