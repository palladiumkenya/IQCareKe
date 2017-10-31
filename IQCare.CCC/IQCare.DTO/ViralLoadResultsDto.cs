using System;
using System.Collections.Generic;

namespace IQCare.DTO
{
    public class ViralLoadResultsDto
    {
        public ViralLoadResultsDto()
        {
            MesssageHeader = new MessageHeader();
            PatientIdentification = new DtoPatientIdentification();
            ViralLoadResult = new List<VLoadlResult>();
        }

        public MessageHeader MesssageHeader { get; set; }
        public DtoPatientIdentification PatientIdentification { get; set; }
        public List<VLoadlResult> ViralLoadResult { get; set; }
    }

    public class VLoadlResult
    {
        public DateTime DateSampleCollected { get; set; }
        public DateTime DateSampleTested { get; set; }
        public string VlResult { get; set; }
        public string SampleType { get; set; }
        public string Justification { get; set; }
        public string Regimen { get; set; }
        public string LabTestedIn { get; set; }
    }

    public class MessageHeader
    {
        public string SendingApplication { get; set; }
        public string SendingFacility { get; set; }
        public string ReceivingApplication { get; set; }
        public string ReceivingFacility { get; set; }
        public DateTime MessageDatetime { get; set; }
        public string Security { get; set; }
        public string MessageType { get; set; }
        public string ProcessingId { get; set; }
    }





}
