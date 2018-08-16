using IQCare.Common.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Common.BusinessProcess.CommandHandlers.Encounter
{
    public class EditPatientPhysicalExaminationCommand : IRequest<Result<EditPatientPhysicalExamResponse>>
    {
        public PatientPhysicalExamination PatientPhysicalExamination;
    }

    public class EditPatientPhysicalExamResponse
    {
        public int PatientPhysicalExamId { get; set; }
    }
}
