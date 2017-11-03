using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IQCare.DTO;
using IQCare.DTO.PatientAppointment;

namespace IQCare.CCC.UILogic.Interoperability
{
    public class ProcessPatientAppointmentMessage : IInteropDTO<PatientAppointSchedulingDTO>
    {
        public PatientAppointSchedulingDTO Get(int entityId)
        {
            return AppointmentMessage.Get(entityId);
        }

        public string Save(PatientAppointSchedulingDTO t)
        {
            throw new NotImplementedException();
        }

        public string Update(PatientAppointSchedulingDTO t)
        {
            throw new NotImplementedException();
        }
    }
}
