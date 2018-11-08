using IQCare.Common.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.PMTCT.BusinessProcess.Queries
{
    public class GetPatientChronicIllnessInfo : IRequest<Result<IEnumerable<PatientChronicIllnessViewModel>>>
    {
        public int PatientId { get; set; }
    }

    public class PatientChronicIllnessViewModel
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
        public string ChronicIllness { get; set; }
        public int ChronicIllnessId { get; set; }
        public string Treatment { get; set; }
        public int Dose { get; set; }
        public int Duration { get; set; }
        public bool DeleteFlag { get; set; }
        public DateTime OnsetDate { get; set; }
        public int ? Active { get; set; }
        public int CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
