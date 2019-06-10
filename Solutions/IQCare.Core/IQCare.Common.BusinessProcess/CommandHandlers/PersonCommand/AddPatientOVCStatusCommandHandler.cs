using System;
using System.Collections.Generic;
using System.Text;
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
    public class AddPatientOVCStatusCommandHandler : IRequestHandler<AddPatientOVCStatusCommand, Result<AddPatientOVCStatusResponse>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        public AddPatientOVCStatusCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<AddPatientOVCStatusResponse>> Handle(AddPatientOVCStatusCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using (_unitOfWork)
                {
                    int Id = 0;
                    RegisterPersonService registerPersonService = new RegisterPersonService(_unitOfWork);

                    var registeredPatientOVCStatus = await registerPersonService.GetPatientOVCStatusByPersonId(request.PersonId);

                    if (registeredPatientOVCStatus != null)
                    {
                        registeredPatientOVCStatus.Active = request.Active;
                        registeredPatientOVCStatus.CreatedBy = request.CreatedBy;
                        registeredPatientOVCStatus.Deleteflag = request.Deleteflag;
                        registeredPatientOVCStatus.InSchool = request.InSchool;

                    }
                    else
                    {
                        PatientOVCStatus po = new PatientOVCStatus();
                        po.PersonId = request.PersonId;
                     
                       po.InSchool = request.InSchool;
                        po.Orphan = request.Orphan;
                        po.Active = request.Active;
                        po.Deleteflag = request.Deleteflag;
                        po.CreatedBy = request.CreatedBy;
                        po.CreateDate = DateTime.Now;
                        po.GuardianId = 0;

                        var patientovcstatus =await registerPersonService.AddPatientOVCStatus(po);
                        Id = patientovcstatus.Id;

                    }

                    return Result<AddPatientOVCStatusResponse>.Valid(new AddPatientOVCStatusResponse()
                    {
                        OVCStatusId = Id
                    });

                }

            }
            catch (Exception ex)
            {
                return Result<AddPatientOVCStatusResponse>.Invalid(ex.Message);
            }
        }

    }

}
