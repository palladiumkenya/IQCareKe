using IQCare.Common.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCareRecords.Common.BusinessProcess.Command
{
    public class PersonEmergencyContactCommand : IRequest<Result<AddPersonEmergencyContactResponse>>
    {

        public List<EmergencyContact> emergencycontacts;

       

    }

    public class EmergencyContact
    {
        public int PersonId { get; set; }

        public string firstname { get; set; }

        public string middlename { get; set; }

        public string lastname { get; set; }

        public int gender { get; set; }

        public int EmergencyContactPersonId { get; set; }

        public string MobileContact { get; set; }


        public int ConsentType { get; set; }

        public int ConsentValue { get; set; }



        public string ConsentReason { get; set; }
        public int CreatedBy { get; set; }

        public bool DeleteFlag { get; set; }

        public int RelationshipType { get; set; }

    }

    public class AddPersonEmergencyContactResponse
    {
        public string Message { get; set; }
        public int PersonEmergencyContactId { get; set; }
    }
}
