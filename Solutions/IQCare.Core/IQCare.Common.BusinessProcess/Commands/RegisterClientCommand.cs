using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Common.Core.Models;
using MediatR;

namespace IQCare.Common.BusinessProcess.Commands
{
    public class RegisterClientCommand : IRequest<Result<RegisterClientResponse>>
    {
        // Client
        public Client Person { get; set; }

        //Contact
        //public Contact Contact { get; set; }

        ////PersonPopulation
        //public PersonPopulation PersonPopulation { get; set; }
    }

    public class Client
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int MaritalStatus { get; set; }
        public int Sex { get; set; }
        public bool IsPartner { get; set; }
        public int? PatientId { get; set; }
        public int CreatedBy { get; set; }
    }

    public class Contact
    {
        public string PhoneNumber { get; set; }
        public string Landmark { get; set; }
    }

    public class PersonPopulation
    {
        public int KeyPopulation { get; set; }
    }
}
