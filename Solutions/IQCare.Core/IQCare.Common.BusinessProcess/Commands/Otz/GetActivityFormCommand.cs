using IQCare.Common.Core.Models;
using IQCare.Library;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Common.BusinessProcess.Commands.Otz
{
    public class GetActivityFormCommand : IRequest<Result<OtzActivityFormsView>>
    {
        public int Id { get; set; }
    }
}
