using System.ComponentModel.DataAnnotations;
using IQCare.Library;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace IQCare.Common.BusinessProcess.Commands.Setup
{
    public class NEWCLIENT
    {
        [Required, ValidateObject]
        public PATIENT_IDENTIFICATION PATIENT_IDENTIFICATION { get; set; }
        public ENCOUNTERS ENCOUNTER { get; set; }
    }

    public class NEWAFYAMOBILECLIENT
    {
        [Required, ValidateObject]
        public PATIENT_IDENTIFICATION PATIENT_IDENTIFICATION { get; set; }
    }

    public class PARTNER
    {
        public PARTNER_FAMILY_PATIENT_IDENTIFICATION PATIENT_IDENTIFICATION { get; set; }
        public PARTNER_ENCOUNTER ENCOUNTER { get; set; }
    }

    public class NEWPARTNER
    {
        public PARTNER_FAMILY_PATIENT_IDENTIFICATION PATIENT_IDENTIFICATION { get; set; }
    }

    public class FAMILY
    {
        public PARTNER_FAMILY_PATIENT_IDENTIFICATION PATIENT_IDENTIFICATION { get; set; }
        public FAMILY_ENCOUNTER ENCOUNTER { get; set; }
    }
}