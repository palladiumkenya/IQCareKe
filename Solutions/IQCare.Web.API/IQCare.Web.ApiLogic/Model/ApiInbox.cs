using System;
using System.ComponentModel.DataAnnotations;

namespace IQCare.Web.ApiLogic.Model
{
    [Serializable]
    //[Table("API_Inbox")]
    public class ApiInbox
    {
        [Key]
        public int Id { get; set; }
        public DateTime DateReceived { get; set; }
        public int SenderId { get; set; }
        public string Message { get; set; }
        public int Processed { get; set; }
        public DateTime DateProcessed { get; set; }
        public string LogMessage { get; set; }

    }
}
