using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.Infrastructure;
using IQCare.Common.Services;
using IQCare.Library;
using IQCare.Records.BusinessProcess.Command.Registration;
using MediatR;
using Serilog;
using PersonMaritalStatus = IQCare.Common.Core.Models.PersonMaritalStatus;

namespace IQCare.Records.BusinessProcess.CommandHandlers.Registration
{
    public class AddPersonMaritalStatusCommandHandler : IRequestHandler<AddPersonMaritalStatusCommand, Result<PersonMaritalStatus>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        public AddPersonMaritalStatusCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<PersonMaritalStatus>> Handle(AddPersonMaritalStatusCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    PersonMaritalStatusService personMaritalStatusService = new PersonMaritalStatusService(_unitOfWork);
                    List<PersonMaritalStatus>  personMaritalStatuses = await personMaritalStatusService.GetPersonMaritalStatus(request.PersonId);
                    PersonMaritalStatus personMaritalStatus = new PersonMaritalStatus();
                    if (personMaritalStatuses.Count > 0)
                    {
                        personMaritalStatuses[0].MaritalStatusId = request.MaritalStatusId;
                        personMaritalStatus = await personMaritalStatusService.UpdatePersonMaritalStatus(personMaritalStatuses[0]);
                    }
                    else
                    {
                        PersonMaritalStatus maritalStatus = new PersonMaritalStatus()
                        {
                            PersonId = request.PersonId,
                            MaritalStatusId = request.MaritalStatusId,
                            CreateDate = DateTime.Now,
                            CreatedBy = request.CreatedBy,
                            DeleteFlag = false,
                            Active = false
                        };
                        personMaritalStatus = await personMaritalStatusService.AddPersonMaritalStatus(maritalStatus);
                    }
                    return Result<PersonMaritalStatus>.Valid(personMaritalStatus);
                }
                catch (Exception e)
                {
                    Log.Error(e.Message + " " + e.InnerException);
                    return Result<PersonMaritalStatus>.Invalid(e.Message);
                }
            }
        }
    }
}