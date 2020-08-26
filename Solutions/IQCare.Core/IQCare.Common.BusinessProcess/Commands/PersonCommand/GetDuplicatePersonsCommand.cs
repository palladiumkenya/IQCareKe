using System.Collections.Generic;
using IQCare.Common.Core.Models;
using IQCare.Library;
using MediatR;

namespace IQCare.Common.BusinessProcess.Commands.PersonCommand
{
    public class GetDuplicatePersonsCommand : IRequest<Result<List<DuplicatePersonsPoco>>>
    {
        public int matchFirstName { get; set; }
        public int matchMiddleName { get; set; }
        public int matchLastname { get; set; }
        public int matchSex { get; set; }
        public int matchDOB { get; set; }
    }
}