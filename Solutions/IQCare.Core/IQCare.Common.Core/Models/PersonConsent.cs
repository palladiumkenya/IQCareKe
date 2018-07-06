using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Common.Core.Models
{
   public  class PersonConsent
    {
        public int Id { get; set; }

        public int PersonId { get; set; }


        public int EmergencyContactId { get; set; }

        public int ConsentType { get; set; }

        public DateTime ConsentDate { get; set; }

        public int ConsentValue { get; set; }
        

        public string ConsentReason { get; set; }

        public bool Active { get; set; }

        public bool DeleteFlag { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreateDate { get; set; }

        public string AuditData { get; set; }
    }
}
