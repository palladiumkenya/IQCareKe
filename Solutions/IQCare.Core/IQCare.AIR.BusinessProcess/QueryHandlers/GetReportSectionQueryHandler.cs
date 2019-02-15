using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using IQCare.AIR.BusinessProcess.Queries;
using IQCare.AIR.Core.Domain;
using IQCare.AIR.Infrastructure.UnitOfWork;
using IQCare.Library;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IQCare.AIR.BusinessProcess.QueryHandlers
{
    public class GetReportSectionByFormIdQueryHandler : IRequestHandler<GetReportSectionQuery, Result<List<ReportSectionViewModel>>>
    {
        private readonly IAirUnitOfWork _airUnitOfWork;
        private readonly IMapper _mapper;
        public GetReportSectionByFormIdQueryHandler(IAirUnitOfWork airUnitOfWork, IMapper mapper)
        {
            _airUnitOfWork = airUnitOfWork;
            _mapper = mapper;
        }
        public Task<Result<List<ReportSectionViewModel>>> Handle(GetReportSectionQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var reportSections = _airUnitOfWork.Repository<ReportSection>()
                    .Get(x => x.ReportingFormId == request.FormId).Include(x => x.ReportSubSections).AsEnumerable();

                var reportSectionViewModel = reportSections.Select(x => new ReportSectionViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    DateCreated = x.DateCreated,
                    ReportingFormId = x.ReportingFormId,
                    ReportSubSections = _mapper.Map<List<ReportSubSectionViewModel>>(x.ReportSubSections)
                }).ToList();

                return Task.FromResult(Result<List<ReportSectionViewModel>>.Valid(reportSectionViewModel));
            }
            catch (Exception ex)
            {
                return Task.FromResult(Result<List<ReportSectionViewModel>>.Invalid(ex.Message));
            }
        }
    }
}
