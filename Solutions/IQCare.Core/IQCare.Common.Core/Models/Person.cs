using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Text;

namespace IQCare.Common.Core.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MidName { get; set; }
        public string LastName { get; set; }
        public int Sex { get; set; }
        public bool Active { get; set; }
        public bool DeleteFlag { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreatedBy { get; set; }
        public SqlXml AuditData { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool DobPrecision { get; set; }
    }
}