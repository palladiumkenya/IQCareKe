using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Common.Core.Models
{
    public class PersonEducation
    {
        public int Id { get; set; }
        public int PersonId { get; set; }

        public int EducationLevel { get; set; }

        public int EducationOutcome { get; set; }

        public bool Active { get; set; }

        public bool DeleteFlag { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }
        public string AuditData { get; set; }
    }
}
