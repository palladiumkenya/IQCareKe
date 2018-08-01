using System;

namespace IQCare.Common.Core.Models
{
    public class AfyaMobileInbox
    {
        public int Id { get; set; }
        public DateTime DateReceived { get; set; }
        public string AfyamobileId { get; set; }
        public string Message { get; set; }
        public bool Processed { get; set; }
        public DateTime? DateProcessed { get; set; }
        public string LogMessage { get; set; }
    }
}