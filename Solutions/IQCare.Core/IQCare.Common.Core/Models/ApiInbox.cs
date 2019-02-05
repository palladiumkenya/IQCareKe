using System;

namespace IQCare.Common.Core.Models
{
    public class ApiInbox
    {
        public int Id { get; set; }
        public string uid { get; set; }
        public DateTime DateReceived { get; set; }
        public int SenderId { get; set; }
        // public string AfyamobileId { get; set; }
        public string Message { get; set; }
        public bool Processed { get; set; }
        public DateTime? DateProcessed { get; set; }
        public string LogMessage { get; set; }
        public string MessageType { get; set; }
        public bool IsSuccess { get; set; }
    }
}