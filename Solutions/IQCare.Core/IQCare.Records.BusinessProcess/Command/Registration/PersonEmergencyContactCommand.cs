using IQCare.Common.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCareRecords.Common.BusinessProcess.Command
{
    public class PersonEmergencyContactCommand : IRequest<Result<AddPersonEmergencyContactResponse>>
    {
        public List<EmergencyContact> Emergencycontact { get; set; }
    }

    public class EmergencyContact
    {
        public int PersonId { get; set; }
        public int RegisteredPersonId { get; set; }
        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public string Lastname { get; set; }
        public int Gender { get; set; }
        public string MobileContact { get; set; }
        public int CreatedBy { get; set; }
        public int RelationshipType { get; set; }

        public int ContactCategory { get; set; }

        public int Consent { get; set; }

        public string ConsentDecline { get; set; }
        public int PosId { get; set; }
    }

    public class AddPersonEmergencyContactResponse
    {
        public string Message { get; set; }
        public int PersonEmergencyContactId { get; set; }
    }
}
