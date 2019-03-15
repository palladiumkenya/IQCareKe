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
    public class GetReportSubSectionsQueryHandler : IRequestHandler<GetReportSubSectionsQuery, Result<List<ReportSubSectionViewModel>>>
    {
        private readonly IAirUnitOfWork _airUnitOfWork;
        private readonly IMapper _mapper;
        public GetReportSubSectionsQueryHandler(IAirUnitOfWork airUnitOfWork, IMapper mapper)
        {
            _airUnitOfWork = airUnitOfWork;
            _mapper = mapper;
        }
        public Task<Result<List<ReportSubSectionViewModel>>> Handle(GetReportSubSectionsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var reportSubSections = _airUnitOfWork.Repository<ReportSubSection>()
                    .Get(x => x.ReportSectionId == request.SectionId).Include(x => x.Indicators).AsEnumerable();

                var subSectionsModel = reportSubSections.Select(x => new ReportSubSectionViewModel()
                {
                    Id = x.Id,
                    DateCreated = x.DateCreated,
                    Name = x.Name,
                    ReportSectionId = x.ReportSectionId,
                    Indicators = _mapper.Map<List<IndicatorViewModel>>(x.Indicators)
                }).ToList();

                return Task.FromResult(Result<List<ReportSubSectionViewModel>>.Valid(subSectionsModel));
            }
            catch (Exception ex)
            {
                return Task.FromResult(Result<List<ReportSubSectionViewModel>>.Invalid(ex.Message));
            }
        }
    }
}
