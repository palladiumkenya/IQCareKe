using System.Collections.Generic;
using IQCare.Common.Core.Models;
using MediatR;
using ServiceAreaIdentifiers = IQCare.Common.Core.Models.ServiceAreaIdentifiers;

namespace IQCare.Common.BusinessProcess.Commands.Lookup
{
    public class GetServiceAreaIdentifiersCommand : IRequest<Library.Result<ServiceAreaIdentifiersResponse>>
    {
        public int ServiceAreaId { get; set; }
    }

    public class ServiceAreaIdentifiersResponse
    {
        public List<ServiceAreaIdentifiers> ServiceAreaIdentifiers { get; set; }
        public List<Identifier> Identifiers { get; set; }
    }
}