using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Records
{
    [Serializable]
    [Table("LookupItem")]
    public class LookupItem
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int DeleteFlag { get; set; }
    }
}
