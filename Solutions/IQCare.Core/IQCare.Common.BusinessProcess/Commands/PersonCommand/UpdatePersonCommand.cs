using System;
using IQCare.Common.Core.Models;
using MediatR;

namespace IQCare.Common.BusinessProcess.Commands.PersonCommand
{
    public class UpdatePersonCommand : IRequest<Result<Person>>
    {
        public int PersonId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Sex { get; set; }
        public bool IsPartner { get; set; }
        public int? PatientId { get; set; }
        public int CreatedBy { get; set; }
        public int PosId { get; set; }
    }
}