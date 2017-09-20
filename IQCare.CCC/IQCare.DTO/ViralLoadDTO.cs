using System;

namespace IQCare.DTO
{
    public class ViralLoadDto
    {
         public string Id { get; set; }
          public string IdentifierType { get; set; }
          public string AssigningAuthourity { get; set; }
          public DateTime DateSampleCollected { get; set; }
          public DateTime DateSampleTested { get; set; }
          public string VlResult { get; set; }
          public string  SampleType { get; set; }
          public string Justification { get; set; }
          public string  Regimen { get; set; }
          public string  LabTestedIn { get; set; }
    }
}
