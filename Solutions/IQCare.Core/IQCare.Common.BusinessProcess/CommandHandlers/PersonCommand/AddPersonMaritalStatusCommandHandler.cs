using System;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands.PersonCommand;
using IQCare.Common.BusinessProcess.Services;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using MediatR;

namespace IQCare.Common.BusinessProcess.CommandHandlers.PersonCommand
{
    public class AddPersonMaritalStatusCommandHandler : IRequestHandler<AddPersonMaritalStatusCommand, Result<AddPersonMaritalStatusResponse>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        public AddPersonMaritalStatusCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<AddPersonMaritalStatusResponse>> Handle(AddPersonMaritalStatusCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using (_unitOfWork)
                {
                    //PersonMaritalStatus personMaritalStatus = new PersonMaritalStatus()
                    //{
                    //    PersonId = request.PersonId,
                    //    MaritalStatusId = request.MaritalStatusId,
                    //    Active = true,
                    //    DeleteFlag = false,
                    //    CreatedBy = request.UserId,
                    //    CreateDate = DateTime.Now
                    //};

                    //await _unitOfWork.Repository<PersonMaritalStatus>().AddAsync(personMaritalStatus);
                    //await _unitOfWork.SaveAsync();
                    RegisterPersonService personService = new RegisterPersonService(_unitOfWork);
                    var personMaritalStatus = await personService.AddMaritalStatus(request.PersonId, request.MaritalStatusId, request.UserId);

                    _unitOfWork.Dispose();

                    return Result<AddPersonMaritalStatusResponse>.Valid(new AddPersonMaritalStatusResponse()
                    {
                        PersonMaritalStatusId = personMaritalStatus.Id
                    });
                }
            }
            catch (Exception e)
            {
                return Result<AddPersonMaritalStatusResponse>.Invalid(e.Message);
            }
        }
    }
}