using IQCare.Library;
using IQCare.PMTCT.Core.Models;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.Commands
{

    public class ReferralAppointmentServiceCommand: IRequest<Result<ReferralAppointmentCommandResponse>>
    {
       public PatientReferral PatientReferral;
        public PatientAppointment PatientAppointment;
        public int CreatedBy { get; set; }
    }

    public class ReferralAppointmentCommandResponse
    {
        public int ReferralAppointmentId { get; set; }
    }
}
