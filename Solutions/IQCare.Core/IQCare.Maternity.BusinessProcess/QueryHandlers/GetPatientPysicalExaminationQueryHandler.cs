using AutoMapper;
using IQCare.Library;
using IQCare.Maternity.BusinessProcess.Queries.PNC;
using IQCare.Maternity.Core.Domain.PNC;
using IQCare.Maternity.Infrastructure.UnitOfWork;
using MediatR;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.EntityFrameworkCore;


namespace IQCare.Maternity.BusinessProcess.QueryHandlers
{
    public class GetPatientPysicalExaminationQueryHandler : IRequestHandler<GetPhysicalExaminationQuery, Result<List<PhysicalExamination>>>
    {
        IMaternityUnitOfWork _maternityUnitOfWork;
        IMapper _mapper;
        ILogger logger = Log.ForContext<GetPatientPysicalExaminationQueryHandler>();

        public GetPatientPysicalExaminationQueryHandler(IMaternityUnitOfWork maternityUnitOfWork, IMapper mapper)
        {
            _maternityUnitOfWork = maternityUnitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<List<PhysicalExamination>>> Handle(GetPhysicalExaminationQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var physicalExaminations = await _maternityUnitOfWork.Repository<PhysicalExamination>().Get(x =>
                    x.PatientId == request.PatientId && x.PatientMasterVisitId == request.PatientMasterVisitId &&
                    x.DeleteFlag == false).ToListAsync();

                return Result<List<PhysicalExamination>>.Valid(physicalExaminations);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"An error occured while fetching patient physical exammination info{ request.PatientId}");
                return Result<List<PhysicalExamination>>.Invalid(ex.Message);
            }

        }
    }
}