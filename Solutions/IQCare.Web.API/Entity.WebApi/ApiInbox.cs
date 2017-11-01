using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.WebApi
{
    [Serializable]
    [Table("API_Inbox")]
    public class ApiInbox
    {
        [Key]
        public int Id { get; set; }
        public DateTime DateReceived { get; set; }
        public int SenderId { get; set; }
        public string Message { get; set; }
        public bool Processed { get; set; }
        public DateTime ? DateProcessed { get; set; }
        public string LogMessage { get; set; }

    }
}
