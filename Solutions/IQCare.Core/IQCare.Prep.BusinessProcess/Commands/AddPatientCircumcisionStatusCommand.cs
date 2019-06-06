using System;
using IQCare.Library;
using IQCare.Prep.Core.Models;
using MediatR;

namespace IQCare.Prep.BusinessProcess.Commands
{
    public class AddPatientCircumcisionStatusCommand : IRequest<Result<PatientCircumcisionStatus>>
    {
        public int? Id { get; set; }
        public int PatientId { get; set; }
        public int ClientCircumcised { get; set; }
        public int ReferredToVMMC { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }
        public bool DeleteFlag { get; set; }
    }
}