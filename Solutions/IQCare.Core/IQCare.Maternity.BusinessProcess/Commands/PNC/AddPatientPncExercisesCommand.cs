using IQCare.Library;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Maternity.BusinessProcess.Commands.PNC
{
    public class AddPatientPncExercisesCommand : IRequest<Result<AddPatientPncExercisesResponse>>
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
        public int PncExercisesDoneId { get; set; }
        public int UserId { get; set; }
    }

    public class AddPatientPncExercisesResponse
    {
        public int PncExercisesId { get; set; }
    }
}
