using IQCare.Lab.Core.Models;
using IQCare.Library;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Lab.BusinessProcess.Queries
{
    public class GetAllViralLoadsQuery : IRequest<Result<List<PatientLabTracker>>>
    {
    }
}
