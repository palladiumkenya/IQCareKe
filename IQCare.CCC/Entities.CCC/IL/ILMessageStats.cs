using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.CCC.IL
{
    [Serializable]
    [Table("vw_ILMessageStats")]
    public class ILMessageStats
    {
        [Key]
        public Int64 RowID { get; set; }

        public string MessageType { get; set; }

        public int Count { get; set; }

        public bool? IsSuccess { get; set; }
    }
}