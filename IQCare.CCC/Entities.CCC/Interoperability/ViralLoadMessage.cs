using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.CCC.Interoperability
{
    [Serializable]
    [Table("Api_ViralLoadMessage")]
    public class ViralLoadMessage
    {
        [Key]
        public int Id { get; set; }
    }
}