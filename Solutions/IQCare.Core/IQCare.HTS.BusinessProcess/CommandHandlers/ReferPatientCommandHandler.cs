using IQCare.Common.Core.Models;
using IQCare.HTS.BusinessProcess.Commands;
using IQCare.HTS.Core.Model;
using IQCare.HTS.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Library;
using Microsoft.EntityFrameworkCore;

namespace IQCare.HTS.BusinessProcess.CommandHandlers
{
    public class ReferPatientCommandHandler : IRequestHandler<ReferPatientCommand, Result<ReferPatientResponse>>
    {
        private readonly IHTSUnitOfWork _hTSUnitOfWork;
        public ReferPatientCommandHandler(IHTSUnitOfWork hTSUnitOfWork)
        {
            _hTSUnitOfWork = hTSUnitOfWork ?? throw new ArgumentNullException(nameof(hTSUnitOfWork));
        }
        public async Task<Result<ReferPatientResponse>> Handle(ReferPatientCommand request, CancellationToken cancellationToken)
        {
            try
            {
                int referralId = 0;
                if (request.IsEdit)
                {
                    var referral = await _hTSUnitOfWork.Repository<Referral>().Get(x => x.PersonId == request.PersonId)
                        .ToListAsync();

                    if (referral.Count > 0)
                    {
                        referral[0].ToFacility = request.ReferredTo;
                        referral[0].ExpectedDate = request.DateToBeEnrolled;
                        referral[0].OtherFacility = request.OtherFacility;

                        _hTSUnitOfWork.Repository<Referral>().Update(referral[0]);
                        await _hTSUnitOfWork.SaveAsync();

                        referralId = referral[0].Id;

                        List<Core.Model.Tracing> tracings = new List<Core.Model.Tracing>();
                        request.Tracing.ForEach(t => tracings.Add(new Core.Model.Tracing
                        {
                            PersonID = request.PersonId,
                            TracingType = t.TracingType,
                            DateTracingDone = t.TracingDate,
                            Mode = t.Mode,
                            Outcome = t.Outcome,
                            Remarks = null,
                            CreatedBy = request.UserId,
                            DeleteFlag = false,
                            CreateDate = DateTime.Now
                        }));

                        await _hTSUnitOfWork.Repository<Core.Model.Tracing>().AddRangeAsync(tracings);
                        await _hTSUnitOfWork.SaveAsync();
                    }
                }
                else
                {
                    var referral = new Referral
                    {
                        PersonId = request.PersonId,
                        ReferralDate = DateTime.Now,
                        FromFacility = request.FromFacilityId,
                        FromServicePoint = request.ServiceAreaId,
                        ToServicePoint = request.ServiceAreaId,
                        ToFacility = request.ReferredTo,
                        ReferralReason = request.ReferralReason,
                        ReferredBy = request.UserId,
                        CreatedBy = request.UserId,
                        ExpectedDate = request.DateToBeEnrolled,
                        CreateDate = DateTime.Now,
                        DeleteFlag = false,
                        OtherFacility = request.OtherFacility
                    };

                    await _hTSUnitOfWork.Repository<Referral>().AddAsync(referral);
                    await _hTSUnitOfWork.SaveAsync();
                    referralId = referral.Id;

                    List<Core.Model.Tracing> tracings = new List<Core.Model.Tracing>();
                    request.Tracing.ForEach(t => tracings.Add(new Core.Model.Tracing
                    {
                        PersonID = request.PersonId,
                        TracingType = t.TracingType,
                        DateTracingDone = t.TracingDate,
                        Mode = t.Mode,
                        Outcome = t.Outcome,
                        Remarks = null,
                        CreatedBy = request.UserId,
                        DeleteFlag = false,
                        CreateDate = DateTime.Now
                    }));

                    await _hTSUnitOfWork.Repository<Core.Model.Tracing>().AddRangeAsync(tracings);
                    await _hTSUnitOfWork.SaveAsync();
                }

                _hTSUnitOfWork.Dispose();

                return Result<ReferPatientResponse>.Valid(new ReferPatientResponse {ReferralId = referralId });
            }
            catch (Exception ex)
            {
                return Result<ReferPatientResponse>.Invalid(ex.Message);
            }
        }
    }
}
