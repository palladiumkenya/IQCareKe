using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Library;
using IQCare.PMTCT.Core.Models;
using MediatR;
using System;

namespace IQCare.PMTCT.BusinessProcess.Commands.Education
{
    public class DeletePatientEducationCommand : IRequest<Result<DeletePatientEducationCommandResult>>
    {
        
        public int Id { get; set; }
        
      
    }

    public class DeletePatientEducationCommandResult
    {
        public int PatientCounsellingId { get; set; }
    }
}
