using IQCare.Library;
using IQCare.PMTCT.Core.Models;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.Commands
{

    public class ReferralAppointmentServiceCommand: IRequest<Result<ReferralAppointmentCommandResponse>>
    {
       public PatientReferral patientReferral;
        public PatientAppointment patientAppointment;
    }

    public class ReferralAppointmentCommandResponse
    {
        public int ReferralAppointmentId { get; set; }
    }
}
