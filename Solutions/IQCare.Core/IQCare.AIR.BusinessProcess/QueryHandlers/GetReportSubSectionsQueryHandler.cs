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
                       .Get(x => x.ReportSectionId == request.SectionId).AsEnumerable();

                var subSectionsModel = _mapper.Map<List<ReportSubSectionViewModel>>(reportSubSections);

                return Task.FromResult(Result<List<ReportSubSectionViewModel>>.Valid(subSectionsModel));
            }
            catch (Exception ex)
            {
                return Task.FromResult(Result<List<ReportSubSectionViewModel>>.Invalid(ex.Message));
            }
        }
    }
}
