using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.CCC.IL
{
    [Serializable]
    [Table("vw_ILMessengerLog")]
    public class IlMessengerLog
    {
        [Key]
        public string Uid { get; set; }
        public string MessageType { get; set; }
        public DateTime? DateGenerated { get; set; }
        public DateTime? DateReceived { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
        public string ErorrLog { get; set; }
    }
}