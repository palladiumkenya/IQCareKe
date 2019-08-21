using System;
using System.Collections.Generic;
using IQCare.Library;
using IQCare.Prep.Core.Models;
using MediatR;

namespace IQCare.Prep.BusinessProcess.Commands
{
   public  class GetHivPartnerProfileCommand :IRequest<Result<GetHivPartnerProfileResponse>>
    {

        public int PatientId { get; set; }
       
    }


    public class GetHivPartnerProfileResponse
    {
        public List<PatientPartnerProfile> patientProfiles { get; set; }
    }
}
