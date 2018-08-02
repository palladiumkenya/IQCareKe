using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using IQCare.Common.BusinessProcess.Commands.Setup;
using IQCare.Common.Core.Models;
using IQCare.Library;
using MediatR;

namespace IQCare.HTS.BusinessProcess.Commands
{
    public class SynchronizeClientsCommand : IRequest<Common.Core.Models.Result<string>>
    {
        [Required, ValidateObject]
        public MESSAGE_HEADER MESSAGE_HEADER { get; set; }
        [Required, ValidateObject]
        public List<NEWCLIENT> CLIENTS { get; set; }
    }

    public class SynchronizeClientsResponse
    {
        public string afyaMobileId { get; set; }
    }
}