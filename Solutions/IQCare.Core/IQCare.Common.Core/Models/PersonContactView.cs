using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Common.Core.Models
{
    public class PersonContactView
    {
        public int Id { get; set; }

        public int PersonId { get; set; }

        public string PhysicalAddress { get; set; }

        public string MobileNumber { get; set; }

        public string AlternativeNumber { get; set; }

        public string EmailAddress { get; set; }

        public bool Active { get; set; }

        public Int32 DeleteFlag { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
