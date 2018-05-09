using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Records
{
    [Serializable]
    [Table("lookupMaster")]

    public class LookupMaster
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Displayname { get; set; }
        public int DeleteFlag { get; set; }
    }
}
