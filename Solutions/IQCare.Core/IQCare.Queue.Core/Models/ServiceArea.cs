using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Queue.Core.Models
{
    public class ServiceArea
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public string DisplayName { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreateDate { get; set; }

        public Boolean DeleteFlag { get; set; }

        public string AuditData { get; set; }
    }
}
