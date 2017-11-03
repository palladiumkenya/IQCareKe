using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.Base;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using Entities.CCC.Interoperability;
using Interface.CCC.Interoperability;

namespace BusinessProcess.CCC.Interoperability
{
    public class BPatientAppointmentMessage : ProcessBase, IPatientAppointmentMessageManager
    {
        public PatientAppointmentMessage GetPatientAppointmentMessageById(int appointmentId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new LookupContext()))
            {
                var appointmentMessage = unitOfWork.PatientAppointmentMessageRepository
                    .FindBy(x => x.AppointmentId == appointmentId).FirstOrDefault();
                unitOfWork.Dispose();
                return appointmentMessage;
            }
        }
    }
}
