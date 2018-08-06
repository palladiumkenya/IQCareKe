using System;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands.PersonCommand;
using IQCare.Common.BusinessProcess.Services;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IQCare.Common.BusinessProcess.CommandHandlers.PersonCommand
{
    public class AddPatientCommandHandler : IRequestHandler<AddPatientCommand, Result<AddPatientResponse>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        public AddPatientCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<AddPatientResponse>> Handle(AddPatientCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using (_unitOfWork)
                {
                    RegisterPersonService registerPersonService = new RegisterPersonService(_unitOfWork);

                    var registeredPerson = await registerPersonService.GetPerson(request.PersonId);
                    var gender = await _unitOfWork.Repository<LookupItemView>().Get(x => x.ItemId == registeredPerson.Sex && x.MasterName == "Gender")
                        .ToListAsync();
                    var maritalStatusName = await _unitOfWork.Repository<LookupItemView>()
                        .Get(x => x.ItemId == 58 && x.MasterName == "MaritalStatus").ToListAsync();

                    var mstResult = await registerPersonService.InsertIntoBlueCard(registeredPerson.FirstName, registeredPerson.LastName,
                        registeredPerson.LastName, DateTime.Now, maritalStatusName[0].ItemName, "", "", gender[0].ItemName, "EXACT", registeredPerson.DateOfBirth, request.UserId);

                    var patient = await registerPersonService.AddPatient(request.PersonId, request.UserId, mstResult[0].Ptn_Pk);
                    

                    return Result<AddPatientResponse>.Valid(new AddPatientResponse()
                    {
                        PatientId = patient.Id
                    });
                }
            }
            catch (Exception e)
            {
                return Result<AddPatientResponse>.Invalid(e.Message);
            }
        }
    }
}