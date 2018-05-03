using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands.Encounter;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IQCare.Common.BusinessProcess.CommandHandlers.Encounter
{
    public class AddPatientScreeningCommandHandler : IRequestHandler<AddPatientScreeningCommand, Result<AddPatientScreeningResponse>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        public AddPatientScreeningCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<AddPatientScreeningResponse>> Handle(AddPatientScreeningCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    for (int i = 0; i < request.ScreeningType.Count; i++)
                    {
                        var screening = request.ScreeningType[i];
                        var screeningType = await _unitOfWork.Repository<LookupItemView>().Get(x => x.MasterName == "TbScreening").FirstOrDefaultAsync();
                        int screeningTypeId = screeningType != null ? screeningType.MasterId : 0;


                        PatientScreening patientConsent = new PatientScreening()
                        {
                            PatientId = request.PatientId,
                            PatientMasterVisitId = request.PatientMasterVisitId,
                            ScreeningTypeId = screeningTypeId,
                            ScreeningDone = true,
                            ScreeningDate = request.ScreeningDate,
                            ScreeningCategoryId = null,
                            ScreeningValueId = screening.Value,
                            Comment = null,
                            Active = true,
                            DeleteFlag = false,
                            CreatedBy = request.UserId,
                            CreateDate = DateTime.Now,
                            VisitDate = request.ScreeningDate
                        };

                        await _unitOfWork.Repository<PatientScreening>().AddAsync(patientConsent);
                        await _unitOfWork.SaveAsync();
                    }

                    _unitOfWork.Dispose();

                    return Result<AddPatientScreeningResponse>.Valid(new AddPatientScreeningResponse()
                    {
                        IsScreeningDone = true
                    });
                }
                catch (Exception e)
                {
                    Log.Error(e.Message);
                    return Result<AddPatientScreeningResponse>.Invalid(e.Message);
                }
            }
        }
    }
}