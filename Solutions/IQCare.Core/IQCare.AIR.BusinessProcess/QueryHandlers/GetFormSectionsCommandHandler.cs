using AutoMapper;
using IQCare.AIR.BusinessProcess.Queries;
using IQCare.AIR.Infrastructure.UnitOfWork;
using IQCare.Library;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IQCare.AIR.BusinessProcess.Services;
using IQCare.AIR.Core.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Serilog;

namespace IQCare.AIR.BusinessProcess.QueryHandlers
{

    public class GetFormSectionsCommandHandler : IRequestHandler<GetFormSectionsQuery, Result<List<SectionViewModel>>>
    {
        private readonly IAirUnitOfWork _airUnitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger = Log.ForContext<GetFormSectionsCommandHandler>();

        public GetFormSectionsCommandHandler(IAirUnitOfWork airUnitOfWork, IMapper mapper)
        {
            _airUnitOfWork = airUnitOfWork ?? throw new ArgumentNullException(nameof(airUnitOfWork));
            _mapper = mapper;
        }

        public Task<Result<List<SectionViewModel>>> Handle(GetFormSectionsQuery request, CancellationToken cancellationToken)
           {
              try{
            var reportSections = _airUnitOfWork.Repository<ReportSection>().Get(x => x.DeleteFlag == false && x.ReportingFormId==request.Id)
                    .Include(x=>x.ReportingForm).OrderByDescending(x=>x.DateCreated).AsEnumerable();
                var reportSectionModel = _mapper.Map<List<SectionViewModel>>(reportSections);
                return Task.FromResult(Result<List<SectionViewModel>>.Valid(reportSectionModel));
            }
            catch(Exception ex)
            {
                string errorMessage = "An error occured fetching form Sections";
                _logger.Error(errorMessage, ex);

                return Task.FromResult(Result<List<SectionViewModel>>.Invalid(errorMessage));
            }
            }
    }
}
