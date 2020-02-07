using System;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands.PersonCommand;
using IQCare.Common.BusinessProcess.Services;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.Library;
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
                    var maritalStatus = await registerPersonService.GetPersonMaritalStatus(request.PersonId);
                    var maritalStatusName = "Single";
                    if (maritalStatus.Count > 0)
                    {
                        var matList = await _unitOfWork.Repository<LookupItemView>()
                            .Get(x => x.ItemId == maritalStatus[0].MaritalStatusId && x.MasterName == "MaritalStatus").ToListAsync();
                        if (matList.Count > 0)
                        {
                            maritalStatusName = matList[0].ItemName;
                        }
                    }
                    DateTime dob = DateTime.Now;
                    if (registeredPerson.DateOfBirth.HasValue)
                        dob = registeredPerson.DateOfBirth.Value;


                    int patientId = 0;
                    var registeredPatient =  await registerPersonService.GetPatientByPersonId(request.PersonId);
                    if (registeredPatient != null)
                    {
                        patientId = registeredPatient.Id;
                        await registerPersonService.UpdatePatient(patientId, dob, request.PosId, request.PatientType);
                    }
                    else
                    {
                        var mstResult = await registerPersonService.InsertIntoBlueCard(registeredPerson.FirstName, registeredPerson.LastName,
                            registeredPerson.MidName, request.EnrollmentDate, " ", 283, maritalStatusName, "", "", gender[0].ItemName, "EXACT", dob, request.UserId, request.PosId);

                        var patient = await registerPersonService.AddPatient(request.PersonId, request.UserId, mstResult[0].Ptn_Pk, "", request.PatientType);
                        patientId = patient.Id;
                    }

                    return Result<AddPatientResponse>.Valid(new AddPatientResponse()
                    {
                        PatientId = patientId
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