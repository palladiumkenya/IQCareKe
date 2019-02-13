using System;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Services;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.HTS.BusinessProcess.Commands;
using IQCare.HTS.BusinessProcess.Services;
using IQCare.HTS.Infrastructure;
using IQCare.Library;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IQCare.HTS.BusinessProcess.CommandHandlers
{
    public class AfyaMobileFamilyTracingEncounterCommandHandler : IRequestHandler<AfyaMobileFamilyTracingEncounterCommand, Result<string>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        private readonly IHTSUnitOfWork _htsUnitOfWork;

        public AfyaMobileFamilyTracingEncounterCommandHandler(IHTSUnitOfWork htsUnitOfWork, ICommonUnitOfWork commonUnitOfWork)
        {
            _htsUnitOfWork = htsUnitOfWork ?? throw new ArgumentNullException(nameof(htsUnitOfWork));
            _unitOfWork = commonUnitOfWork ?? throw new ArgumentNullException(nameof(commonUnitOfWork));
        }

        public async Task<Result<string>> Handle(AfyaMobileFamilyTracingEncounterCommand request, CancellationToken cancellationToken)
        {
            string afyaMobileId = string.Empty;
            string indexClientAfyaMobileId = string.Empty;

            using (var trans = _htsUnitOfWork.Context.Database.BeginTransaction())
            {
                try
                {
                    RegisterPersonService registerPersonService = new RegisterPersonService(_unitOfWork);
                    EncounterTestingService encounterTestingService = new EncounterTestingService(_unitOfWork, _htsUnitOfWork);

                    for (int j = 0; j < request.INTERNAL_PATIENT_ID.Count; j++)
                    {
                        if (request.INTERNAL_PATIENT_ID[j].IDENTIFIER_TYPE == "AFYA_MOBILE_ID")
                        {
                            afyaMobileId = request.INTERNAL_PATIENT_ID[j].ID;
                        }

                        if (request.INTERNAL_PATIENT_ID[j].IDENTIFIER_TYPE == "INDEX_CLIENT_AFYAMOBILE_ID")
                        {
                            indexClientAfyaMobileId = request.INTERNAL_PATIENT_ID[j].ID;
                        }
                    }

                    var indexClientIdentifiers = await registerPersonService.getPersonIdentifiers(indexClientAfyaMobileId, 10);
                    if (indexClientIdentifiers.Count > 0)
                    {
                        //Get Index client
                        var indexClient = await registerPersonService.GetPatientByPersonId(indexClientIdentifiers[0].PersonId);
                        var partnetPersonIdentifiers = await registerPersonService.getPersonIdentifiers(afyaMobileId, 10);
                        if (partnetPersonIdentifiers.Count > 0)
                        {
                            for (int j = 0; j < request.TRACING_ENCOUNTER.TRACING.Count; j++)
                            {
                                var lookupitem = await _unitOfWork.Repository<LookupItemView>()
                                    .Get(x => x.MasterName == "TracingType" && x.ItemName == "Family").ToListAsync();
                                int tracingType = lookupitem[0].ItemId;

                                DateTime tracingDate = DateTime.ParseExact(request.TRACING_ENCOUNTER.TRACING[j].TRACING_DATE, "yyyyMMdd", null);
                                int mode = request.TRACING_ENCOUNTER.TRACING[j].TRACING_MODE;
                                int outcome = request.TRACING_ENCOUNTER.TRACING[j].TRACING_OUTCOME;

                                DateTime? reminderDate = null;
                                if (!string.IsNullOrWhiteSpace(request.TRACING_ENCOUNTER.TRACING[j].REMINDER_DATE))
                                    reminderDate = DateTime.ParseExact(request.TRACING_ENCOUNTER.TRACING[j].REMINDER_DATE, "yyyyMMdd", null);
                                DateTime? tracingBookingDate = null;
                                if (!string.IsNullOrWhiteSpace(request.TRACING_ENCOUNTER.TRACING[j].BOOKING_DATE))
                                    tracingBookingDate = DateTime.ParseExact(request.TRACING_ENCOUNTER.TRACING[j].BOOKING_DATE, "yyyyMMdd", null);
                                int consent = request.TRACING_ENCOUNTER.TRACING[j].CONSENT;

                                var trace = await encounterTestingService.addTracing(partnetPersonIdentifiers[0].PersonId, tracingType, tracingDate, mode, outcome,
                                    1, null, consent, tracingBookingDate, reminderDate);
                            }
                        }
                        else
                        {
                            return Result<string>.Invalid($"Family member with afyamobileid: {afyaMobileId} could not be found");
                        }
                    }
                    else
                    {
                        return Result<string>.Invalid($"Index client with afyamobileid: {indexClientAfyaMobileId} could not be found for family member: {afyaMobileId}");
                    }

                    return Result<string>.Valid($"Successfully synchronized family tracing for afyamobileid: {afyaMobileId}");
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    Log.Error(ex.Message);
                    return Result<string>.Invalid($"Failed to synchronize family tracing for clientId: {afyaMobileId} " + ex.Message + " " + ex.InnerException);
                }
            }
        }
    }
}