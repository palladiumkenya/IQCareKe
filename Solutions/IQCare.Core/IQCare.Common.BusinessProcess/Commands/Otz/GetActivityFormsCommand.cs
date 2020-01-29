using IQCare.Common.Core.Models;
using IQCare.Library;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Common.BusinessProcess.Commands.Otz
{
    public class GetActivityFormsCommand : IRequest<Result<List<OtzActivityFormsView>>>
    {
        public int PatientId { get; set; }
    }
}
