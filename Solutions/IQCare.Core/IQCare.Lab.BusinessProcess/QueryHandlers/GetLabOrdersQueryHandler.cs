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
    public class GetLabOrdersQueryHandler : IRequestHandler<GetLabOrdersQuery, Result<List<LabOrderViewModel>>>
    {
        private readonly ILabUnitOfWork _labUnitOfWork;
        private readonly IMapper _mapper;
        public GetLabOrdersQueryHandler(ILabUnitOfWork labUnitOfWork, IMapper mapper)
        {
            _labUnitOfWork = labUnitOfWork;
            _mapper = mapper;
        }
        public Task<Result<List<LabOrderViewModel>>> Handle(GetLabOrdersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var labOrders = string.IsNullOrEmpty(request.OrderStatus)
                    ? _labUnitOfWork.Repository<LabOrder>().Get().Where(x => x.PatientId == request.PatientId)
                        .Include(l => l.LabOrderTests)
                    : _labUnitOfWork.Repository<LabOrder>().Get()
                        .Where(x => x.PatientId == request.PatientId && x.OrderStatus.Equals(request.OrderStatus,StringComparison.InvariantCultureIgnoreCase))
                        .Include(l => l.LabOrderTests);

                var labOrderModel = labOrders.Select(x => new LabOrderViewModel
                {
                    Id = x.Id,
                    DateCreated = x.CreateDate.ToShortDateString(),
                    PatientMasterVisitId = x.PatientMasterVisitId,
                    OrderNotes = x.ClinicalOrderNotes,
                    OrderNumber = x.OrderNumber,
                    OrderStatus = x.OrderStatus,
                    PatientId = x.PatientId,
                    LabTests = _mapper.Map<List<LabOrderTestViewModel>>(x.LabOrderTests)
                }).ToList();

                return Task.FromResult(Result<List<LabOrderViewModel>>.Valid(labOrderModel));
            }
            catch (Exception ex)
            {
                Log.Error(ex,"An error occured while fetching patient lab orders");
                return Task.FromResult(Result<List<LabOrderViewModel>>.Invalid(ex.Message));
            }
        }
    }
}
