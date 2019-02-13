using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using IQCare.Library;
using IQCare.Maternity.BusinessProcess.Queries.Maternity;
using IQCare.Maternity.Core.Domain.Maternity;
using IQCare.Maternity.Infrastructure.UnitOfWork;
using MediatR;
using Serilog;

namespace IQCare.Maternity.BusinessProcess.QueryHandlers
{
    public class GetPatientDrugAdministrationByIdQueryHandler : IRequestHandler<GetPatientDrugAdministrationById,Result<PatientDrugAdministrationViewModel>>
    {
        private readonly IMapper _mapper;
        private readonly IMaternityUnitOfWork _maternityUnitOfWork;

        public GetPatientDrugAdministrationByIdQueryHandler(IMaternityUnitOfWork maternityUnitOfWork, IMapper mapper)
        {
            _maternityUnitOfWork = maternityUnitOfWork;
            _mapper = mapper;
        }

        public Task<Result<PatientDrugAdministrationViewModel>> Handle(GetPatientDrugAdministrationById request, CancellationToken cancellationToken)
        {
            try
            {
                var administeredDrug = _maternityUnitOfWork.Repository<MaternalDrugAdministration>()
                        .Get(x => x.Id == request.Id).SingleOrDefault();

                var administeredDrugModel = _mapper.Map<PatientDrugAdministrationViewModel>(administeredDrug);

                return Task.FromResult(Result<PatientDrugAdministrationViewModel>.Valid(administeredDrugModel));
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                    $"An error occured while getting patient drug administration details with Id {request.Id}");

                return Task.FromResult(Result<PatientDrugAdministrationViewModel>.Invalid(ex.Message));
            }
        }
    }
}
