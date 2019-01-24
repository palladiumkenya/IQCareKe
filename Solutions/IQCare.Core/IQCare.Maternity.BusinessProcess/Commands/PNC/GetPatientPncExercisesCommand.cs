using IQCare.Library;
using IQCare.Maternity.Core.Domain.PNC;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Maternity.BusinessProcess.Commands.PNC
{
    public class GetPatientPncExercisesCommand : IRequest<Result<List<PatientPncExercises>>>
    {
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
    }
}
