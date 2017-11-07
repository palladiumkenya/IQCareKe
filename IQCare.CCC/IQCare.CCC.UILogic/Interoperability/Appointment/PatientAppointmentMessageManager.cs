using System;
using Application.Presentation;
using Entities.CCC.Interoperability;
using Interface.CCC.Interoperability;

namespace IQCare.CCC.UILogic.Interoperability.Appointment
{
    public class PatientAppointmentMessageManager
    {
        IPatientAppointmentMessageManager _mgr = (IPatientAppointmentMessageManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.Interoperability.BPatientAppointmentMessage, BusinessProcess.CCC");

        public PatientAppointmentMessage GetPatientAppointmentMessageById(int appointmentId)
        {
            try
            {
                return _mgr.GetPatientAppointmentMessageById(appointmentId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
