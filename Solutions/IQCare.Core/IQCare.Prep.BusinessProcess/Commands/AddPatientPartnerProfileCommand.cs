using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Library;
using IQCare.Prep.Core.Models;
using MediatR;

namespace IQCare.Prep.BusinessProcess.Commands
{
    public class AddPatientPartnerProfileCommand :IRequest<Result<PatientProfileResponse>>
    {

        public int PatientId { get; set; }
        public List<PatientPartnerProfileList> patientPartnerProfiles { get; set; }

    }

    public class PatientPartnerProfileList
    {
        public int Id { get; set; }

        public int PatientId { get; set; }

     



        public DateTime? HivPositiveStatusDate { get; set; }

        public string CCCEnrollment { get; set; }
        public DateTime? PartnerARTStartDate { get; set; }
        public string HIVSeroDiscordantDuration { get; set; }

        public string SexWithoutCondoms { get; set; }
        public string NumberofChildren { get; set; }

        public string CCCNumber { get; set; }


        public int CreatedBy { get; set; }

        public Boolean DeleteFlag { get; set; }

    }

    public class PatientProfileResponse
    {
      

        public string Message { get; set; }
    }
}
