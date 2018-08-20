using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Common.Core.Models
{
   public  class PersonEmergencyContact
    {
        public int Id { get; set; }

        public int? PersonId { get; set; }

        public int EmergencyContactPersonId { get; set; }

        public string MobileContact { get; set; }

        public int? ContactType { get; set; }
        public bool DeleteFlag { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }
        public string AuditData { get; set; }

        public bool? RegisteredToClinic { get; set; }
    }

}
