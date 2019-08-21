using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.Library;
using MediatR;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IQCare.Common.BusinessProcess.Commands.Relationship
{
    public class GetPatientRelationships : IRequest<Result<List<PatientRelationshipViewModel>>>
    {
        public int PatientId { get; set; }

    }

    public class PatientRelationshipViewModel
    {
        public int PatientId { get; set; }
        public string RelativeName { get; set; }
        public string Relationship { get; set; }
        public string RelativeSex { get; set; }
        public int RelativePersonId { get; set; }
        public int? RelativePatientId { get; set; }
        public string PatientSex { get; set; }
        public string PatientName { get; set; }
        public int PatientPersonId { get; set; }
    }


    public class GetPatientRelationshipsQueryHandler : IRequestHandler<GetPatientRelationships, Result<List<PatientRelationshipViewModel>>>
    {
        private readonly ICommonUnitOfWork _commonUnitOfWork;
        public GetPatientRelationshipsQueryHandler(ICommonUnitOfWork commonUnitOfWork)
        {
            _commonUnitOfWork = commonUnitOfWork;
        }
        public async Task<Result<List<PatientRelationshipViewModel>>> Handle(GetPatientRelationships request, CancellationToken cancellationToken)
        {
            try
            {
                var patientRelationships = await _commonUnitOfWork.Repository<PatientRelationshipView>().FromSql("GetPatientRelationships @PatientId = {0}", request.PatientId);

                var relationshipsViewModel = patientRelationships.Select(x => new PatientRelationshipViewModel
                {
                    PatientId = x.PatientId,
                    PatientPersonId = x.PatientPersonId,
                    Relationship = x.Relationship,
                    RelativeName = $"{x.RelativeFirstName} {x.RelativeLastName}",
                    RelativePersonId = x.RelativePersonId,
                    RelativeSex = x.RelativeSex,
                    RelativePatientId = x.RelativePatientId,
                    PatientName = $"{x.PatientFirstName} {x.PatientMiddleName} {x.PatientLastName}",
                    PatientSex = x.PatientSex
                }).ToList();

                return Result<List<PatientRelationshipViewModel>>.Valid(relationshipsViewModel);
            }
            catch (Exception ex)
            {
                Log.Error(ex,$"An error occured while getting patient relationships for Id {request.PatientId}");
                return Result<List<PatientRelationshipViewModel>>.Invalid(ex.Message);
            }
        }
    }
}
