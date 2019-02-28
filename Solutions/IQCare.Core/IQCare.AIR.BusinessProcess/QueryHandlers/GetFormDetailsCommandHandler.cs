using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using IQCare.AIR.BusinessProcess.Queries;
using IQCare.AIR.BusinessProcess.Services;
using IQCare.AIR.Core.Domain;
using IQCare.AIR.Infrastructure.UnitOfWork;
using IQCare.Library;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IQCare.AIR.BusinessProcess.QueryHandlers
{
    public class GetReportingFormDetailsCommandHandler : IRequestHandler<GetReportingFormDetailsQuery, Result<ReportingFormViewModel>>
    {
        private readonly IAirUnitOfWork _airUnitOfWork;
        private readonly IMapper _mapper;


        public GetReportingFormDetailsCommandHandler(IAirUnitOfWork airUnitOfWork, IMapper mapper)
        {
            _airUnitOfWork = airUnitOfWork ?? throw new ArgumentNullException(nameof(airUnitOfWork));
            _mapper = mapper;
        }


        public Task<Result<ReportingFormViewModel>> Handle(GetReportingFormDetailsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var reportingForm = _airUnitOfWork.Repository<ReportingForm>().Get(x => x.Id == request.Id && x.DeleteFlag == false)
                    .Include(x => x.ReportSections)
                    .ThenInclude(r => r.ReportSubSections)
                    .ThenInclude(s => s.Indicators)
                    .ThenInclude(i => i.DataType)
                    .SingleOrDefault();

                if (reportingForm == null)
                    return Task.FromResult(Result<ReportingFormViewModel>.Invalid($"Reporting form with Id {request.Id} not found"));


                var reportingFormViewModel = new ReportingFormViewModel()
                {
                    Id = reportingForm.Id,
                    Name = reportingForm.Name,
                    DateCreated = reportingForm.DateCreated,
                    ReportSections = BuildReportSectionModel(reportingForm.ReportSections)
                };


                return Task.FromResult(Result<ReportingFormViewModel>.Valid(reportingFormViewModel));
            }
            catch (Exception ex)
            {
                return Task.FromResult(Result<ReportingFormViewModel>.Invalid(ex.Message));
            }


        }

        private List<ReportSectionViewModel> BuildReportSectionModel(ICollection<ReportSection> sections)
        {
            if (sections == null || !sections.Any())
                return new List<ReportSectionViewModel>();

            return sections.Where(x=>x.DeleteFlag==false && x.Active==true).Select(x => new ReportSectionViewModel()
            {
                Id = x.Id,
                DateCreated = x.DateCreated,
                Name = x.Name,
                ReportingFormId = x.ReportingFormId,
                ReportSubSections = BuildSubSectionsModel(x.ReportSubSections)
            }).ToList();

        }

        private List<ReportSubSectionViewModel> BuildSubSectionsModel(ICollection<ReportSubSection> subSections)
        {
            if (subSections == null || !subSections.Any())
                return new List<ReportSubSectionViewModel>();

            return subSections.Select(s => new ReportSubSectionViewModel()
            {
                Id = s.Id,
                Name = s.Name,
                ReportSectionId = s.ReportSectionId,
                DateCreated = s.DateCreated,
                Indicators = _mapper.Map<List<IndicatorViewModel>>(s.Indicators.OrderBy(i => i.Id))
            }).ToList();
        }
    }
}
