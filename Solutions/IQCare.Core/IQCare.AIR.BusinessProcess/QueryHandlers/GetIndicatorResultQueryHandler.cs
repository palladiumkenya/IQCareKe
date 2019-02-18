using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using IQCare.AIR.BusinessProcess.Queries;
using IQCare.AIR.Core.Domain;
using IQCare.AIR.Infrastructure.Extensions;
using IQCare.AIR.Infrastructure.UnitOfWork;
using IQCare.Library;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IQCare.AIR.BusinessProcess.QueryHandlers
{
    public class GetIndicatorResultQueryHandler : IRequestHandler<GetIndicatorResultQuery, Result<ReportingFormIndicatorResultViewModel>>
    {
        private readonly IAirUnitOfWork _airUnitOfWork;
        private readonly IMapper _mapper;
        public GetIndicatorResultQueryHandler(IAirUnitOfWork airUnitOfWork, IMapper mapper)
        {
            _airUnitOfWork = airUnitOfWork;
            _mapper = mapper;
        }
        public Task<Result<ReportingFormIndicatorResultViewModel>> Handle(GetIndicatorResultQuery request, CancellationToken cancellationToken)
        {

            var reportingPeriodResult = _airUnitOfWork.Repository<ReportingPeriod>()
                .Get(x => x.Id == request.ReportingPeriodId)
                .Include(x => x.ReportingForm)
                .Include(x => x.IndicatorResults)
                .ThenInclude(x => x.Indicator)
                .ThenInclude(x => x.ReportSubSection)
                .ThenInclude(x => x.ReportSection).SingleOrDefault();

            var indicatorResultViewModel = new ReportingFormIndicatorResultViewModel()
            {
                Id = reportingPeriodResult.Id,
                ReportName = reportingPeriodResult.ReportingForm.Name,
                ReportDate = reportingPeriodResult.ReportDate,
                StrReportDate = reportingPeriodResult.ReportDate.GetMonthAndYearFromDate(),
                ReportSections = GetReportSectionModel(reportingPeriodResult.ReportingForm.ReportSections,
                    reportingPeriodResult.IndicatorResults)
            };

            return Task.FromResult(Result<ReportingFormIndicatorResultViewModel>.Valid(indicatorResultViewModel));

        }

        private List<ReportSectionViewModel> GetReportSectionModel(ICollection<ReportSection> reportSection, 
            ICollection<IndicatorResult> indicatorResults)
        {
            return reportSection.Select(x => new ReportSectionViewModel()
            {
                Id = x.Id,
                DateCreated = x.DateCreated,
                Name = x.Name,
                ReportingFormId = x.ReportingFormId,
                ReportSubSections = x.ReportSubSections.Select(r => new ReportSubSectionViewModel()
                {
                    Id = r.Id,
                    DateCreated = r.DateCreated,
                    Name = r.Name,
                    ReportSectionId = r.ReportSectionId,
                    Indicators = indicatorResults.Select(i => new IndicatorViewModel()
                    {
                        Id = i.Id,
                        Name = i.Indicator.Name,
                        Code = i.Indicator.Code,
                        Result = i.ResultNumeric.HasValue ? i.ResultNumeric.Value.ToString() : i.ResultText
                    }).ToList()
                }).ToList()
            }).ToList();
        }
    }
}
