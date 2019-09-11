using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IQCare.Queue.Core.Models
{
   public  class LookupMasterItem
    {
        [Key]
        public Int64 RowID { get; set; }
        public int LookupMasterId { get; set; }
        public int LookupItemId { get; set; }
        public string DisplayName { get; set; }

        public decimal OrdRank { get; set; }

    }
}
