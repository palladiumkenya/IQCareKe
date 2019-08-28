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
using Newtonsoft.Json;
using Serilog;

namespace IQCare.HTS.BusinessProcess.CommandHandlers
{
    public class AfyaMobileSynchronizeTracingCommandHandler : IRequestHandler<AfyaMobileSynchronizeTracingCommand, Result<string>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        private readonly IHTSUnitOfWork _htsUnitOfWork;

        public AfyaMobileSynchronizeTracingCommandHandler(IHTSUnitOfWork htsUnitOfWork, ICommonUnitOfWork unitOfWork)
        {
            _htsUnitOfWork = htsUnitOfWork ?? throw new ArgumentNullException(nameof(htsUnitOfWork));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<string>> Handle(AfyaMobileSynchronizeTracingCommand request, CancellationToken cancellationToken)
        {
            string afyaMobileId = String.Empty;

            using (var trans = _htsUnitOfWork.Context.Database.BeginTransaction())
            {
                RegisterPersonService registerPersonService = new RegisterPersonService(_unitOfWork);
                EncounterTestingService encounterTestingService = new EncounterTestingService(_unitOfWork, _htsUnitOfWork);

                try
                {
                    //Person Identifier
                    for (int j = 0; j < request.INTERNAL_PATIENT_ID.Count; j++)
                    {
                        if (request.INTERNAL_PATIENT_ID[j].IDENTIFIER_TYPE == "AFYA_MOBILE_ID" && request.INTERNAL_PATIENT_ID[j].ASSIGNING_AUTHORITY == "AFYAMOBILE")
                        {
                            afyaMobileId = request.INTERNAL_PATIENT_ID[j].ID;
                        }
                    }
                    var afyaMobileMessage = await registerPersonService.AddAfyaMobileInbox(DateTime.Now, request.MESSAGE_HEADER.MESSAGE_TYPE, afyaMobileId, JsonConvert.SerializeObject(request), false);

                    //Tracing
                    var enrollmentTracing = await _unitOfWork.Repository<LookupItemView>()
                        .Get(x => x.MasterName == "TracingType" && x.ItemName == "Enrolment").FirstOrDefaultAsync();
                    int tracingType = enrollmentTracing.ItemId;
                    int providerId = request.PLACER_DETAIL.PROVIDER_ID;
                    string tracingRemarks = String.Empty;

                    //check if person already exists
                    var identifiers = await registerPersonService.getPersonIdentifiers(afyaMobileId, 10);
                    if (identifiers.Count > 0)
                    {
                        var person = await registerPersonService.GetPerson(identifiers[0].PersonId);
                        var patient = await registerPersonService.GetPatientByPersonId(identifiers[0].PersonId);

                        //check for client tracing
                        for (int j = 0; (request.TRACING != null && j < request.TRACING.Count); j++)
                        {
                            DateTime tracingDate = DateTime.Now;
                            try
                            {
                                tracingDate = DateTime.ParseExact(request.TRACING[j].TRACING_DATE, "yyyyMMdd", null);
                            }
                            catch (Exception e)
                            {
                                Log.Error($"Could not parse tracing TRACING_DATE: {request.TRACING[j].TRACING_DATE} as a valid date: Incorrect format, date should be in the following format yyyyMMdd");
                                throw new Exception($"Could not parse tracing TRACING_DATE: {request.TRACING[j].TRACING_DATE} as a valid date: Incorrect format, date should be in the following format yyyyMMdd");
                            }
                            int mode = request.TRACING[j].TRACING_MODE;
                            int outcome = request.TRACING[j].TRACING_OUTCOME;
                            int? reasonnotcontacted = request.TRACING[j].REASONNOTCONTACTED;
                            string reasonnotcontactedother = request.TRACING[j].REASONNOTCONTACTEDOTHER;

                            //add Client Tracing
                            var clientTracing = await encounterTestingService.addTracing(person.Id, tracingType,
                                tracingDate, mode, outcome,
                                providerId, tracingRemarks, null, null, null,reasonnotcontacted,reasonnotcontactedother);
                        }
                    }
                    else
                    {
                        //update message has been processed
                        await registerPersonService.UpdateAfyaMobileInbox(afyaMobileMessage.Id, afyaMobileId, true, DateTime.Now, $"Person with afyaMobileId: {afyaMobileId} could not be found", false);
                        return Result<string>.Invalid($"Person with afyaMobileId: {afyaMobileId} could not be found");
                    }

                    //update message has been processed
                    await registerPersonService.UpdateAfyaMobileInbox(afyaMobileMessage.Id, afyaMobileId, true, DateTime.Now, $"Successfully synchronized HTS Tracing for afyamobileid: {afyaMobileId}", true);
                    trans.Commit();
                    return Result<string>.Valid($"Successfully synchronized HTS Tracing for afyamobileid: {afyaMobileId}");
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    Log.Error($"Failed to synchronize Hts Tracing for clientid: {afyaMobileId} " + ex.Message + " " + ex.InnerException);
                    return Result<string>.Invalid($"Failed to synchronize Hts Tracing for clientid: {afyaMobileId} " + ex.Message + " " + ex.InnerException);
                }
            }
        }
    }
}