using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using IQCare.Lab.BusinessProcess.Queries;
using IQCare.Lab.Core.Models;
using IQCare.Lab.Infrastructure.Interface;
using IQCare.Library;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IQCare.Lab.BusinessProcess.QueryHandlers
{
   public class GetLabTestByOrderIdQueryHandler : IRequestHandler<GetLabTestByOrderId,Result<List<LabOrderTestViewModel>>>
   {
       private readonly IMapper _mapper;
       private readonly ILabUnitOfWork _labUnitOfWork;
        public GetLabTestByOrderIdQueryHandler(ILabUnitOfWork labUnitOfWork, IMapper mapper)
        {

            _labUnitOfWork = labUnitOfWork;
            _mapper = mapper;
        }
        public Task<Result<List<LabOrderTestViewModel>>> Handle(GetLabTestByOrderId request, CancellationToken cancellationToken)
        {
            try
            {
                var labOrder = _labUnitOfWork.Repository<LabOrder>().Get(x => x.Id == request.Id)
                        .Include(x => x.LabOrderTests).SingleOrDefault();

                if (labOrder == null)
                    return Task.FromResult(
                        Result<List<LabOrderTestViewModel>>.Invalid($"Lab order with Id {request.Id} not found"));

                var labOrderTests = _mapper.Map<List<LabOrderTestViewModel>>(labOrder.LabOrderTests);

                return Task.FromResult(Result<List<LabOrderTestViewModel>>.Valid(labOrderTests));
            }
            catch (Exception ex)
            {
                Log.Error(ex,$"An error occured while getting lab order tests with Id {request.Id}");
                return Task.FromResult(Result<List<LabOrderTestViewModel>>.Invalid(ex.Message));
            }
        }
    }
}
