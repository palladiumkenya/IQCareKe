using IQCare.Common.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;


namespace IQCare.Records.BusinessProcess.Command
{
    public class GetPersonDetailsCommand :IRequest<Result<GetPersonDetailsResponse>>
    {

        public int PersonId { get; set; }
    }


    public class GetPersonDetailsResponse
    {
        public Person personDetail { get; set; }

        public PersonEducation personEducation { get; set; }

        public  PersonOccupation personOccupation { get; set; }

        public PersonMaritalStatus personMaritalStatus { get; set; }


        public PersonLocation personLocation { get; set; }

        public PersonContactView personContact { get; set; }

        public List<PersonEmergencyView> PersonEmergencyView { get; set; }

        public PersonIdentifier personIdentifier { get; set; }

        public Patient patient { get; set; }

    } 
}
