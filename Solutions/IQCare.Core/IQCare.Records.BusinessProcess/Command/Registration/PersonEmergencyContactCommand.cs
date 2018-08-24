using IQCare.Common.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCareRecords.Common.BusinessProcess.Command
{
    public class PersonEmergencyContactCommand : IRequest<Result<AddPersonEmergencyContactResponse>>
    {
        private EmergencyContact _emergencycontact;

        public EmergencyContact Emergencycontact { get => _emergencycontact; set => _emergencycontact = value; }
    }

    public class EmergencyContact
    {
        public int PersonId { get; set; }
        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public string Lastname { get; set; }
        public int Gender { get; set; }
        public int EmergencyContactPersonId { get; set; }
        public string MobileContact { get; set; }
        public int ConsentType { get; set; }
        public int ConsentValue { get; set; }
        public string ConsentReason { get; set; }
        public int CreatedBy { get; set; }
        public bool DeleteFlag { get; set; }
        public int RelationshipType { get; set; }
        public bool? RegisteredToClinic { get; set; }
        public int EmgEmergencyContactType { get; set; }
        public int EmgNextofKinContactType { get; set; }
    }

    public class AddPersonEmergencyContactResponse
    {
        public string Message { get; set; }
        public int PersonEmergencyContactId { get; set; }
    }
}
