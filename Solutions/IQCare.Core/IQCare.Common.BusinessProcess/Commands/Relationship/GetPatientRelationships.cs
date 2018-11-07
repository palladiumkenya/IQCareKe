using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
               
                var patientRelationships = await _commonUnitOfWork.Repository<PatientRelationshipView>()
                    .FromSql(BuildPatientRelationshipsQuery(request.PatientId));

                var relationshipsViewModel = patientRelationships.Select(x => new PatientRelationshipViewModel
                {
                    PatientId = x.PatientId,
                    Relationship = x.Relationship,
                    RelativeName = $"{x.RelativeFirstName} {x.RelativeLastName}",
                    RelativePersonId = x.RelativePersonId,
                    RelativeSex = x.RelativeSex
                }).ToList();

                return Result<List<PatientRelationshipViewModel>>.Valid(relationshipsViewModel);
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        private string BuildPatientRelationshipsQuery(int patientId)
        {
            StringBuilder query = new StringBuilder();
            query.Append("exec pr_OpenDecryptedSession;");
            query.Append($"SELECT * FROM PatientRelationshipView WHERE PatientId = {patientId};");
            query.Append("exec [dbo].[pr_CloseDecryptedSession];");
            return query.ToString();
        }
    }
}
