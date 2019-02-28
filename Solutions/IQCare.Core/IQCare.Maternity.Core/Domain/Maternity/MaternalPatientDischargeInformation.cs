using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Maternity.Core.Domain.Maternity
{
    public class MaternalPatientDischargeInformation
    {

        public MaternalPatientDischargeInformation(int? patientMasterVisitId, int? outcomeStatus,
            string outcomeDescription, int createdBy, DateTime ? dateDischarged)
        {
            PatientMasterVisitId = patientMasterVisitId;
            OutcomeStatus = outcomeStatus;
            OutcomeDescription = outcomeDescription;
            CreatedBy = createdBy;
            DateCreated = DateTime.Now;
            DateDischarged = dateDischarged;
        }
        public int Id { get; private set; }
        public int? PatientMasterVisitId { get; private set; }
        public int? OutcomeStatus { get; private set; }
        public DateTime ? DateDischarged { get; private set; }
        public string OutcomeDescription { get; private set; }
        public int CreatedBy { get; private set; }
        public DateTime DateCreated { get; private set; }
        public bool DeleteFlag { get; private set; }

        public void Update(int ? outcomeStatus, DateTime ? dateDischarged, string outcomeDescription)
        {
            OutcomeStatus = outcomeStatus;
            DateDischarged = dateDischarged;
            OutcomeDescription = outcomeDescription;
        }
    }
}
