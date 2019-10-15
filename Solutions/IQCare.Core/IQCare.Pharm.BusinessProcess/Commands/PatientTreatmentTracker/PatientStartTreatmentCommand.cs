using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Pharm.Core.Models;
using IQCare.Pharm.Infrastructure;
using IQCare.Library;
using MediatR;

namespace IQCare.Pharm.BusinessProcess.Commands.PatientTreatmentTracker
{
    public class PatientStartTreatmentCommand : IRequest<Result<PatientStartTreatmentResponse>>
    {

        public int PatientId { get; set; }
    }

    public class PatientStartTreatmentResponse
    {
            public bool StartTreatment { get; set; }
    }


   
}
