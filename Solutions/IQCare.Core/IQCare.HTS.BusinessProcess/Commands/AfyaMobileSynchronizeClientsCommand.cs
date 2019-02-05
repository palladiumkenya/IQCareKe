using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using IQCare.Common.BusinessProcess.Commands.Setup;
using IQCare.Library;
using MediatR;

namespace IQCare.HTS.BusinessProcess.Commands
{
    public class AfyaMobileSynchronizeClientsCommand : IRequest<Result<string>>
    {
        [Required, ValidateObject]
        public MESSAGE_HEADER MESSAGE_HEADER { get; set; }
        [Required, ValidateObject]
        public List<NEWAFYAMOBILECLIENT> CLIENTS { get; set; }
    }
}