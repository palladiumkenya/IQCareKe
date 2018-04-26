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
                    HtsScreeningOptions htsScreeningOptions = new HtsScreeningOptions()
                    {
                        PersonId = request.PersonId,
                        Occupation = request.Occupation,
                        ScreeningDate = request.ScreeningDate,
                        BookingDate = request.BookingDate
                    };

                    await _unitOfWork.Repository<HtsScreeningOptions>().AddAsync(htsScreeningOptions);
                    await _unitOfWork.SaveAsync();

                    List<PatientScreening> screenings = new List<PatientScreening>();
                    foreach (var t in request.Screening) {
                        if (t.ScreeningValueId != 0)
                        {
                            screenings.Add(new PatientScreening
                            {
                                PatientId = request.PatientId,
                                PatientMasterVisitId = request.PatientMasterVisitId,
                                ScreeningTypeId = t.ScreeningTypeId,
                                ScreeningDone = true,
                                ScreeningDate = request.ScreeningDate,
                                ScreeningCategoryId = t.ScreeningCategoryId,
                                ScreeningValueId = t.ScreeningValueId,
                                Comment = null,
                                Active = true,
                                DeleteFlag = false,
                                CreatedBy = request.UserId,
                                CreateDate = DateTime.Now,
                                VisitDate = request.ScreeningDate
                            });
                        }
                    }

                    await _unitOfWork.Repository<PatientScreening>().AddRangeAsync(screenings);
                    await _unitOfWork.SaveAsync();

                    List<HtsScreening> htsScreenings = new List<HtsScreening>();
                    foreach (var screening in screenings)
                    {
                        HtsScreening htsScreening = new HtsScreening()
                        {
                            PersonId = request.PersonId,
                            PatientScreeningId = screening.Id,
                            HtsScreeningOptionsId = htsScreeningOptions.Id
                        };

                        htsScreenings.Add(htsScreening);
                    }

                    await _unitOfWork.Repository<HtsScreening>().AddRangeAsync(htsScreenings);
                    await _unitOfWork.SaveAsync();

                    _unitOfWork.Dispose();

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