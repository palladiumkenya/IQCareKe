using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.PMTCT.Core.Models
{
   public class PatientDrugAdministration
    {
      public int Id { get; set; }
      public int PatientId { get; set; }
      public int PatientMasterVisitId { get; set; }
      public int DrugAdministered { get; set; }
      public int Value { get; set; }
     public string  Description { get; set; }
     public int  DeleteFlag { get; set; }
     public int CreatedBy { get; set; }
    }
}
