using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Records
{
    [Serializable]
    [Table("County")]
   public class LookupCounty
    {
        public int Id { get; set; }

        public int CountyId { get; set; }

        public string CountyName { get; set; }

        public int SubcountyId { get; set; }
        public string SubcountyName { get; set; }
        public int WardId { get; set; }
        public string WardName { get; set; }

    }
}
