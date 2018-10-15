using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.PMTCT.Core.Models.HEI
{
    public class HEiPatientIcfAction
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
        public int SputumSmear { get; set; }
        public int ChestXray { get; set; }
        public bool? StartAntiTb { get; set; }
        public int InvitationOfContacts { get; set; }
        public bool? EvaluatedForIpt { get; set; }
        public bool DeleteFlag { get; set; }
        public int CreatedBy { get; set; }
       public DateTime CreateDate { get; set; }
       public int GeneXpert { get; set; }
    }
}
