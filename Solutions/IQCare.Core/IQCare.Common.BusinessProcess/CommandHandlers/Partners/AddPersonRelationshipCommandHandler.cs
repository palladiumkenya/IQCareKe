using System;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands.Relationship;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.Library;
using MediatR;
using Remotion.Linq.Utilities;

namespace IQCare.Common.BusinessProcess.CommandHandlers.Partners
{
    public class AddPersonRelationshipCommandHandler : IRequestHandler<AddPersonRelationshipCommand, Result<AddPersonRelationshipResponse>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        public AddPersonRelationshipCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<AddPersonRelationshipResponse>> Handle(AddPersonRelationshipCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using (_unitOfWork)
                {
                    PersonRelationship personRelationship = new PersonRelationship();
                    personRelationship.PersonId = request.PersonId;
                    personRelationship.PatientId = request.PatientId;
                    personRelationship.RelationshipTypeId = request.RelationshipTypeId;
                    personRelationship.CreatedBy = request.UserId;
                    personRelationship.CreateDate = DateTime.Now;


                    await _unitOfWork.Repository<PersonRelationship>().AddAsync(personRelationship);
                    await _unitOfWork.SaveAsync();

                    _unitOfWork.Dispose();

                    return Result<AddPersonRelationshipResponse>.Valid(new AddPersonRelationshipResponse()
                    {
                        PersonRelationshipId = personRelationship.Id
                    });
                }
            }
            catch (Exception e)
            {
                return Result<AddPersonRelationshipResponse>.Invalid(e.Message);
            }
        }
    }
}