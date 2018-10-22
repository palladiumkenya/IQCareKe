using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Maternity.Core.Domain.Maternity
{
    public class MaternalPatientDischargeInformation
    {

        public MaternalPatientDischargeInformation(int outcomeId, int? patientEncounterId, int? patientMasterVisitId, int? outcomeStatus,
            string outcomeDescription)
        {
            OutcomeId = outcomeId;
            PatientEncounterId = patientEncounterId;
            PatientMasterVisitId = patientMasterVisitId;
            OutcomeStatus = outcomeStatus;
            OutcomeDescription = outcomeDescription;
        }
        public int OutcomeId { get; private set; }

        public int? PatientEncounterId { get; private set; }
        public int? PatientMasterVisitId { get; private set; }
        public int? OutcomeStatus { get; private set; }
        public string OutcomeDescription { get; private set; }
        public bool DeleteFlag { get; private set; }
    }
}
