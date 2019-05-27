using System;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands.Enrollment;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.Library;
using MediatR;
using Serilog;

namespace IQCare.Common.BusinessProcess.CommandHandlers.Enrollment
{
    public class AddServiceEntryPointCommandHandler : IRequestHandler<AddServiceEntryPointCommand, Result<ServiceEntryPoint>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;

        public AddServiceEntryPointCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<ServiceEntryPoint>> Handle(AddServiceEntryPointCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    if (request.Id > 0)
                    {
                        var result = await _unitOfWork.Repository<ServiceEntryPoint>().FindByIdAsync(request.Id);
                        if (result != null)
                        {
                            result.EntryPointId = request.EntryPointId;

                            _unitOfWork.Repository<ServiceEntryPoint>().Update(result);
                            await _unitOfWork.SaveAsync();
                            return Result<ServiceEntryPoint>.Valid(result);
                        }

                        ServiceEntryPoint serviceEntryPoint = new ServiceEntryPoint()
                        {
                            PatientId = request.PatientId,
                            ServiceAreaId = request.ServiceAreaId,
                            EntryPointId = request.EntryPointId,
                            CreateDate = request.CreateDate,
                            Active = true,
                            CreatedBy = request.CreatedBy,
                            DeleteFlag = false
                        };

                        await _unitOfWork.Repository<ServiceEntryPoint>().AddAsync(serviceEntryPoint);
                        await _unitOfWork.SaveAsync();
                        return Result<ServiceEntryPoint>.Valid(serviceEntryPoint);
                    }
                    else
                    {
                        ServiceEntryPoint serviceEntryPoint = new ServiceEntryPoint()
                        {
                            PatientId = request.PatientId,
                            ServiceAreaId = request.ServiceAreaId,
                            EntryPointId = request.EntryPointId,
                            CreateDate = request.CreateDate,
                            Active = true,
                            CreatedBy = request.CreatedBy,
                            DeleteFlag = false
                        };

                        await _unitOfWork.Repository<ServiceEntryPoint>().AddAsync(serviceEntryPoint);
                        await _unitOfWork.SaveAsync();

                        return Result<ServiceEntryPoint>.Valid(serviceEntryPoint);
                    }
                }
                catch (Exception e)
                {
                    Log.Error($"Failed to add ServiceEntryPoint " + e.Message);
                    return Result<ServiceEntryPoint>.Invalid($"Failed to add ServiceEntryPoint " + e.Message + ", InnerException: " + e.InnerException);
                }
            }
        }
    }
}