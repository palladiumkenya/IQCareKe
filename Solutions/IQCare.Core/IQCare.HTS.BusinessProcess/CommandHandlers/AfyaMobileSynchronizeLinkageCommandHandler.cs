using System;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Services;
using IQCare.Common.Infrastructure;
using IQCare.HTS.BusinessProcess.Commands;
using IQCare.HTS.BusinessProcess.Services;
using IQCare.HTS.Infrastructure;
using IQCare.Library;
using MediatR;
using Newtonsoft.Json;
using Serilog;

namespace IQCare.HTS.BusinessProcess.CommandHandlers
{
    public class AfyaMobileSynchronizeLinkageCommandHandler : IRequestHandler<AfyaMobileSynchronizeLinkageCommand, Result<string>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        private readonly IHTSUnitOfWork _htsUnitOfWork;

        public AfyaMobileSynchronizeLinkageCommandHandler(IHTSUnitOfWork htsUnitOfWork, ICommonUnitOfWork unitOfWork)
        {
            _htsUnitOfWork = htsUnitOfWork ?? throw new ArgumentNullException(nameof(htsUnitOfWork));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<string>> Handle(AfyaMobileSynchronizeLinkageCommand request, CancellationToken cancellationToken)
        {
            string afyaMobileId = String.Empty;

            RegisterPersonService registerPersonService = new RegisterPersonService(_unitOfWork);
            EncounterTestingService encounterTestingService = new EncounterTestingService(_unitOfWork, _htsUnitOfWork);

            using (var trans = _htsUnitOfWork.Context.Database.BeginTransaction())
            {
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

                    int providerId = request.PLACER_DETAIL.PROVIDER_ID;
                    //check if person already exists
                    var identifiers = await registerPersonService.getPersonIdentifiers(afyaMobileId, 10);
                    if (identifiers.Count > 0)
                    {
                        DateTime dateLinkageEnrolled = DateTime.ParseExact(request.LINKAGE.DATE_ENROLLED, "yyyyMMdd", null);
                        string linkageCCCNumber = request.LINKAGE.CCC_NUMBER;
                        string linkageFacility = request.LINKAGE.FACILITY;
                        string healthWorker = request.LINKAGE.HEALTH_WORKER;
                        string carde = request.LINKAGE.CARDE;
                        string ARTStartDate = request.LINKAGE.ARTStartDate;
                        string remarks = request.LINKAGE.REMARKS;

                        DateTime? artstartDate = null;
                        if (!string.IsNullOrWhiteSpace(ARTStartDate))
                        {
                            artstartDate = DateTime.ParseExact(ARTStartDate, "yyyyMMdd", null);
                        }

                        var previousLinkage = await encounterTestingService.GetPersonLinkage(identifiers[0].PersonId);
                        if (previousLinkage.Count > 0)
                        {
                            previousLinkage[0].ArtStartDate = artstartDate;
                            previousLinkage[0].LinkageDate = dateLinkageEnrolled;
                            previousLinkage[0].CCCNumber = linkageCCCNumber;
                            previousLinkage[0].Facility = linkageFacility;
                            previousLinkage[0].HealthWorker = healthWorker;
                            previousLinkage[0].Cadre = carde;
                            previousLinkage[0].Comments = remarks;

                            await encounterTestingService.UpdatePersonLinkage(previousLinkage[0]);
                        }
                        else
                        {
                            //add Client Linkage
                            var clientLinkage = await encounterTestingService.AddLinkage(identifiers[0].PersonId, dateLinkageEnrolled,
                                linkageCCCNumber, linkageFacility, providerId, healthWorker, carde, remarks, artstartDate);
                        }
                    }
                    else
                    {
                        //update message has been processed
                        await registerPersonService.UpdateAfyaMobileInbox(afyaMobileMessage.Id, afyaMobileId, true, DateTime.Now, $"Person with afyaMobileId: {afyaMobileId} could not be found", false);
                        return Result<string>.Invalid($"Person with afyaMobileId: {afyaMobileId} could not be found");
                    }

                    //update message has been processed
                    await registerPersonService.UpdateAfyaMobileInbox(afyaMobileMessage.Id, afyaMobileId, true, DateTime.Now, $"Successfully synchronized HTS Linkage for afyamobileid: {afyaMobileId}", true);
                    trans.Commit();
                    return Result<string>.Valid($"Successfully synchronized HTS Linkage for afyamobileid: {afyaMobileId}");
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    Log.Error($"Failed to synchronize Hts Referral for clientid: {afyaMobileId} " + ex.Message + " " + ex.InnerException);
                    return Result<string>.Invalid($"Failed to synchronize Hts Referral for clientid: {afyaMobileId} " + ex.Message + " " + ex.InnerException);
                }
            }
        }
    }
}