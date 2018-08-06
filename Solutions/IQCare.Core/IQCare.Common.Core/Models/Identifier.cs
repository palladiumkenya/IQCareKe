using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Common.Core.Models
{
   public class Identifier
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string DisplayName { get; set; }
        public string DataType { get; set; }
        public string PrefixType { get; set; }
        public string SuffixType { get; set; }


        public string AuditData { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreatedBy { get; set; }
        public bool DeleteFlag { get; set; }


    }
}
