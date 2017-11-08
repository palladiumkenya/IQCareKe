namespace IQCare.WebApi.Logic.MappingEntities.drugs
{
    public  class MessageHeaderEntity
    {
        public string SendingApplication { get; set; }
        public string SendingFacility { get; set; }
        public string ReceivingApplication { get; set; }
        public string ReceivingFacility { get; set; }
        public string MessageDatetime { get; set; }
        public string Security { get; set; }
        public string MessageType { get; set; }
        public string ProcessingId { get; set; }
    }
}
