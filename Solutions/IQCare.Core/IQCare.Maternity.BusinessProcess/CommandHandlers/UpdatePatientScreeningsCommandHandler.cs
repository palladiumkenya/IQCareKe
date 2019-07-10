using System;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Library;
using IQCare.Maternity.BusinessProcess.Commands.PNC;
using IQCare.Maternity.Core.Domain.PNC;
using IQCare.Maternity.Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IQCare.Maternity.BusinessProcess.CommandHandlers
{
    public class UpdatePatientScreeningsCommandHandler : IRequestHandler<UpdatePatientScreeningsCommand, Result<UpdatePatientScreeningsResult>>
    {
        IMaternityUnitOfWork _maternityUnitOfWork;

        public UpdatePatientScreeningsCommandHandler(IMaternityUnitOfWork maternityUnitOfWork)
        {
            _maternityUnitOfWork = maternityUnitOfWork;
        }

        public async Task<Result<UpdatePatientScreeningsResult>> Handle(UpdatePatientScreeningsCommand request, CancellationToken cancellationToken)
        {
            using (_maternityUnitOfWork)
            {
                try
                {
                    for (int i = 0; i < request.Screenings.Count; i++)
                    {
                        var screenings = await _maternityUnitOfWork.Repository<PatientScreening>().Get(x =>
                            x.PatientId == request.PatientId && x.PatientMasterVisitId == request.PatientMasterVisitId &&
                            x.DeleteFlag == false && x.ScreeningCategoryId == request.Screenings[i].ScreeningCategoryId && 
                            x.ScreeningTypeId == request.Screenings[i].ScreeningTypeId).ToListAsync();

                        if (screenings.Count > 0)
                        {
                            screenings[0].ScreeningValueId = request.Screenings[i].ScreeningValueId;

                            _maternityUnitOfWork.Repository<PatientScreening>().Update(screenings[0]);
                            await _maternityUnitOfWork.SaveAsync();
                        }
                        else
                        {
                            var screening = new PatientScreening
                            {
                                PatientId = request.PatientId,
                                PatientMasterVisitId = request.PatientMasterVisitId,
                                DeleteFlag = false,
                                CreateDate = DateTime.Now,
                                CreatedBy = request.CreatedBy,
                                Comment = "",
                                Active = false,
                                ScreeningCategoryId = request.Screenings[i].ScreeningCategoryId,
                                ScreeningDate = request.VisitDate,
                                ScreeningDone = true,
                                ScreeningTypeId = request.Screenings[i].ScreeningTypeId,
                                ScreeningValueId = request.Screenings[i].ScreeningValueId,
                                VisitDate = request.VisitDate
                            };
                            await _maternityUnitOfWork.Repository<PatientScreening>().AddAsync(screening);
                            await _maternityUnitOfWork.SaveAsync();
                        }
                    }

                    return Result<UpdatePatientScreeningsResult>.Valid(new UpdatePatientScreeningsResult()
                    {
                        Message = "Successfully updated patient screening"
                    });
                }
                catch (Exception e)
                {
                    Log.Error(e, $"An error occured while trying to update patient screening for patientId: {request.PatientId} and patientMasterVisitId {request.PatientMasterVisitId}");
                    return Result<UpdatePatientScreeningsResult>.Invalid($"An error occured while trying to update patient screening for patientId: {request.PatientId} and patientMasterVisitId {request.PatientMasterVisitId}");
                }
            }
        }
    }
}