using System;
using System.Collections.Generic;
using System.Text;
using Entities.Records;
using MediatR;
namespace IQCare.Common.BusinessProcess.Commands
{
    public class PersonRegistrationCommand:IRequest<Result<PersonRegistrationResponse>>
    {
        public Client Person;
    }

    public class Client
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }
        public int MaritalStatus { get; set; }
        public int Sex { get; set; }
        public string PersonId { get; set; }

      


        public int CreatedBy { get; set; }

        public bool DobPrecision { get; set; }
    }
    public class   PersonRegistrationResponse
    {
        public int  PersonId { get; set; }
        public string Message { get; set; }
    }
}