using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands.Encounter;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using MediatR;

namespace IQCare.Common.BusinessProcess.CommandHandlers.Encounter
{
    public class PatientScreeningCommandHandler : IRequestHandler<PatientScreeningCommand, Result<PatientScreeningResponse>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        public PatientScreeningCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<PatientScreeningResponse>> Handle(PatientScreeningCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using (_unitOfWork)
                {
                    List<PatientScreening> screenings = new List<PatientScreening>();
                    request.Screening.ForEach(t => screenings.Add(new PatientScreening
                    {
                        PatientId = t.PatientId,
                        PatientMasterVisitId = t.PatientMasterVisitId,
                        ScreeningTypeId = t.ScreeningTypeId,
                        ScreeningDone = true,
                        ScreeningDate = t.ScreeningDate,
                        ScreeningCategoryId = t.ScreeningCategoryId,
                        ScreeningValueId = t.ScreeningValueId,
                        Comment = null,
                        Active = true,
                        DeleteFlag = false,
                        CreatedBy = t.UserId,
                        CreateDate = DateTime.Now,
                        VisitDate = t.ScreeningDate
                    }));

                    await _unitOfWork.Repository<PatientScreening>().AddRangeAsync(screenings);
                    await _unitOfWork.SaveAsync();

                    return Result<PatientScreeningResponse>.Valid(new PatientScreeningResponse()
                    {
                        ScreeningDone = true
                    });
                }
            }
            catch (Exception e)
            {
                return Result<PatientScreeningResponse>.Invalid(e.Message);
            }
        }
    }
}