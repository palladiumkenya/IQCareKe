using System;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands.Encounter;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.Library;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IQCare.Common.BusinessProcess.CommandHandlers.Encounter
{
    public class UpdatePatientScreeningCommandHandler : IRequestHandler<UpdatePatientScreeningCommand, Result<UpdatePatientScreeningResponse>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        public UpdatePatientScreeningCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public async Task<Result<UpdatePatientScreeningResponse>> Handle(UpdatePatientScreeningCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    for (int i = 0; i < request.ScreeningType.Count; i++)
                    {
                        var screening = request.ScreeningType[i];
                        var screeningType = await _unitOfWork.Repository<LookupItemView>().Get(x => x.MasterName == request.ScreeningType[i].Key).FirstOrDefaultAsync();
                        int screeningTypeId = screeningType != null ? screeningType.MasterId : 0;


                        var patientScreening = await _unitOfWork.Repository<PatientScreening>().Get(x =>
                            x.PatientId == request.PatientId &&
                            x.PatientMasterVisitId == request.PatientMasterVisitId &&
                            x.ScreeningTypeId == screeningTypeId).FirstOrDefaultAsync();

                        if (patientScreening != null)
                        {
                            patientScreening.ScreeningValueId = screening.Value;
                            patientScreening.ScreeningDate = request.ScreeningDate;

                            _unitOfWork.Repository<PatientScreening>().Update(patientScreening);
                            await _unitOfWork.SaveAsync();
                        }
                        else
                        {
                            PatientScreening patientScreen = new PatientScreening()
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

                            await _unitOfWork.Repository<PatientScreening>().AddAsync(patientScreen);
                            await _unitOfWork.SaveAsync();
                        }
                    }
                    return Result<UpdatePatientScreeningResponse>.Valid(new UpdatePatientScreeningResponse()
                    {
                        isUpdateSuccessful = true
                    });
                }
                catch (Exception e)
                {
                    Log.Error(e.Message);
                    return Result<UpdatePatientScreeningResponse>.Invalid(e.Message);
                }
            }
        }
    }
}