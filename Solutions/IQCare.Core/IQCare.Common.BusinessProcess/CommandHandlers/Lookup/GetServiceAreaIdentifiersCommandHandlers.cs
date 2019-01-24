using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands.Lookup;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using ServiceAreaIdentifiers = IQCare.Common.Core.Models.ServiceAreaIdentifiers;

namespace IQCare.Common.BusinessProcess.CommandHandlers.Lookup
{
    public class GetServiceAreaIdentifiersCommandHandlers : IRequestHandler<GetServiceAreaIdentifiersCommand, Library.Result<ServiceAreaIdentifiersResponse>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        public GetServiceAreaIdentifiersCommandHandlers(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Library.Result<ServiceAreaIdentifiersResponse>> Handle(GetServiceAreaIdentifiersCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    var response = await _unitOfWork.Repository<ServiceAreaIdentifiers>()
                        .Get(x => x.ServiceAreaId == request.ServiceAreaId).ToListAsync();
                    var identifiers = await _unitOfWork.Repository<Identifier>().GetAllAsync();

                    return Library.Result<ServiceAreaIdentifiersResponse>.Valid(new ServiceAreaIdentifiersResponse()
                    {
                        ServiceAreaIdentifiers = response,
                        Identifiers = identifiers.ToList()
                    });
                }
                catch (Exception e)
                {
                    Log.Error(e.Message + " " + e.InnerException);
                    return Library.Result<ServiceAreaIdentifiersResponse>.Invalid(e.Message);
                }
            }
        }
    }
}