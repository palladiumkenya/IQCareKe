using Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
namespace Entities.CCC.Lookup
{

    [Serializable]
    [Table("mst_ICDCodes")]
    public class ICDCodeList
    {
        public int Id { get; set; }
        public string  SubBlockId { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public int? DeleteFlag { get; set; }
        public int? UserId { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int?  SystemId { get; set; }




    }
}
