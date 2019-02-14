using System;
using IQCare.Common.BusinessProcess.Commands.Setup;
using IQCare.Common.Core.Models;
using IQCare.Library;
using MediatR;

namespace IQCare.Common.BusinessProcess.Commands.Matrix
{
    public class MatrixCommand : IRequest<Result<EmrMatrixResponse>>
    {
      
    }

    public class EmrMatrixResponse
    {
        public DateTime? LastLoginDate { get; set; }
        public string EmrVersion { get; set; }
        public string EmrName { get; set; }
        public DateTime? LastMoH731RunDate { get; set; }
    }
}