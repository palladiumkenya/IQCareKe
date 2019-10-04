using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Queue.Core.Models
{
   public  class LookupItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public bool DeleteFlag { get; set; }

    }
}
