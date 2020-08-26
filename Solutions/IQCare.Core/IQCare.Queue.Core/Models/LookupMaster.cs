using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Queue.Core.Models
{
    public class LookupMaster
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public Boolean DeleteFlag { get; set; }
    }
}
