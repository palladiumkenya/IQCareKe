using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IQCare.Queue.Core.Models
{
    public class LookupItemView
    {
        [Key]
        public Int64 RowID { get; set; }
        public int MasterId { get; set; }
        public int ItemId { get; set; }
        public string MasterName { get; set; }
        public string ItemName { get; set; }
        public string DisplayName { get; set; }
        public string ItemDisplayName { get; set; }
        public decimal OrdRank { get; set; }
    }
}
