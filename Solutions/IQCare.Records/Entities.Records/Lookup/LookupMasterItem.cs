using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Records
{
    [Serializable]
    [Table("lookupMasterItem")]
    public class LookupMasterItem
    {
        public int Id { get; set; }
        public int LookupmasterId { get; set; }
        public int LookupItemId { get; set; }
        public string DisplayName { get; set; }
        public decimal OrdRank { get; set; }
    }
}
