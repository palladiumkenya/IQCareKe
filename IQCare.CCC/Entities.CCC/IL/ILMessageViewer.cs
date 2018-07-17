using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.CCC.IL
{
    [Serializable]
    [Table("vw_ILMessageViewer")]
    public class ILMessageViewer
    {
        [Key]
        public int Id { get; set; }

        public DateTime DateReceived { get; set; }

        public string SenderSystem { get; set; }

        public int SenderId { get; set; }

        public string Message { get; set; }

        public bool Processed { get; set; }

        public DateTime? DateProcessed { get; set; }

        public string LogMessage { get; set; }

        public string MessageType { get; set; }

        public bool IsSuccess { get; set; }
    }
}