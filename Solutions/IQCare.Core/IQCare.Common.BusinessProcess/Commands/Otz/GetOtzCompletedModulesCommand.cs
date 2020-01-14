using IQCare.Common.Core.Models;
using IQCare.Library;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Common.BusinessProcess.Commands.Otz
{
    public class GetOtzCompletedModulesCommand : IRequest<Result<List<OtzCompletedModulesView>>>
    {
        public int PatientId { get; set; }
    }
}
