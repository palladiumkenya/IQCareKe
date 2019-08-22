using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.Library;
using MediatR;
using Serilog;

namespace IQCare.Common.BusinessProcess.Commands.Relationship
{
    public class GetPersonSecondaryRelationshipCommand : IRequest<Result<List<PatientRelationshipViewModel>>>
    {
        public int PersonId { get; set; }
    }

    public class GetPersonSecondaryRelationshipCommandHandler : IRequestHandler<GetPersonSecondaryRelationshipCommand,Result<List<PatientRelationshipViewModel>>>
    {
        private readonly ICommonUnitOfWork _commonUnitOfWork;
        public GetPersonSecondaryRelationshipCommandHandler(ICommonUnitOfWork commonUnitOfWork)
        {
            _commonUnitOfWork = commonUnitOfWork;
        }

        public async Task<Result<List<PatientRelationshipViewModel>>> Handle(GetPersonSecondaryRelationshipCommand request, CancellationToken cancellationToken)
        {
            using (_commonUnitOfWork)
            {
                try
                {
                    var patientRelationships = await _commonUnitOfWork.Repository<PatientRelationshipView>().FromSql("GetPatientRelationships @PersonId = {0}", request.PersonId);

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
                    Log.Error(ex, $"An error occured while getting Person relationships for Id {request.PersonId}");
                    return Result<List<PatientRelationshipViewModel>>.Invalid(ex.Message);
                }
            }
        }
    }
}