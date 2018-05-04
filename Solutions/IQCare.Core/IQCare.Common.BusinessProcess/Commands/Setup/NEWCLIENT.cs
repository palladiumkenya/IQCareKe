using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace IQCare.Common.BusinessProcess.Commands.Setup
{
    public class NEWCLIENT
    {
        public PATIENT_IDENTIFICATION PATIENT_IDENTIFICATION { get; set; }
        public ENCOUNTERS ENCOUNTER { get; set; }
    }

    public class PARTNER
    {
        public PARTNER_FAMILY_PATIENT_IDENTIFICATION PATIENT_IDENTIFICATION { get; set; }
    }
}