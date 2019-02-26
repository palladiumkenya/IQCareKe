using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Maternity.Core.Domain.Maternity
{
    public class PatientDischargeInformationView
    {
        public PatientDischargeInformationView()
        {

        }

        public int Id { get; set; }
        public int? PatientMasterVisitId { get; set; }
        public string OutcomeStatus { get; set; }
        public int OutcomeStatusId { get; set; }       
        public DateTime? DateDischarged { get; set; }
        public string OutcomeDescription { get; set; }
        public int CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public bool DeleteFlag { get; set; }
    }
}
