using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands.Setup;
using IQCare.Common.Infrastructure;
using IQCare.Library;
using MediatR;
using Serilog;
using ServiceArea = IQCare.Common.Core.Models.ServiceArea;

namespace IQCare.Common.BusinessProcess.CommandHandlers.Setup
{
    public class GetServiceAreasCommandHandler : IRequestHandler<GetServiceAreasCommand, Result<List<ServiceArea>>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        public GetServiceAreasCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<List<ServiceArea>>> Handle(GetServiceAreasCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    var result = await _unitOfWork.Repository<ServiceArea>().GetAllAsync();
                    return Result<List<ServiceArea>>.Valid(result.ToList());
                }
                catch (Exception e)
                {
                    Log.Error(e.Message + " " + e.InnerException);
                    return Result<List<ServiceArea>>.Invalid(e.Message);
                }
            }
        }
    }

    public  class GetServiceAreaByNameQueryHandler : IRequestHandler<GetServiceAreaByName,Result<ServiceArea>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;

        public GetServiceAreaByNameQueryHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Task<Result<ServiceArea>> Handle(GetServiceAreaByName request, CancellationToken cancellationToken)
        {
            try
            {
                var serviceArea = _unitOfWork.Repository<ServiceArea>()
                        .Get(x => string.Equals(x.Name, request.Name, StringComparison.InvariantCultureIgnoreCase))
                        .SingleOrDefault();

                return Task.FromResult(Result<ServiceArea>.Valid(serviceArea));
            }
            catch (Exception ex)
            {
                return Task.FromResult(Result<ServiceArea>.Invalid(ex.Message));
            }
        }
    }
}