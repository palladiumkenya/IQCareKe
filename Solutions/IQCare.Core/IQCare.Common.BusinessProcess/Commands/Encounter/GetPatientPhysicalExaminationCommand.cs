using IQCare.Common.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Common.BusinessProcess.CommandHandlers.Encounter
{
   public class GetPatientPhysicalExaminationCommand: IRequest<Result<GetPatientPhysicamExamResponse>>
    {
        public int patientId { get; set; }
    }

    public class GetPatientPhysicamExamResponse
    {
        public PatientPhysicalExamination PatientPhysicamExamination; 
    }

}
