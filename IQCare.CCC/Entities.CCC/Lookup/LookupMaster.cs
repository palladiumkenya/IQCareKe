using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.CCC.Lookup
{
    [Serializable]
    [Table("lookupMaster")]

   public class LookupMaster
    {
        public int Id { get; set; }
        public  string Name { get; set; }
        public string Displayname { get; set; }
        public int DeleteFlag { get; set; }
    }
}
