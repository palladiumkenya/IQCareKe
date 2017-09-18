using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.HTS
{
    [Serializable]
    public class Client
    {
        [Key]
        public int Id { get; set; }
        public int PersonId { get; set; }
        public int KeyPopId { get; set; }
        public string KeyPopOther { get; set; }
        public string HtsNumber { get; set; }
    }
}
