using System.Collections.Generic;
using IQCare.Common.Core.Models;
using IQCare.Library;
using MediatR;

namespace IQCare.Common.BusinessProcess.Commands.Ovc
{
  public   class GetCaregiverViewCommmand : IRequest<Result<List<CaregiverView>>>
    {

        public int PatientId { get; set; }
    }
}
