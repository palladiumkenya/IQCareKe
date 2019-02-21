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
    public class AfyaMobileSynchronizeReferralCommandHandler : IRequestHandler<AfyaMobileSynchronizeReferralCommand, Result<string>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        private readonly IHTSUnitOfWork _htsUnitOfWork;

        public AfyaMobileSynchronizeReferralCommandHandler(IHTSUnitOfWork htsUnitOfWork, ICommonUnitOfWork unitOfWork)
        {
            _htsUnitOfWork = htsUnitOfWork ?? throw new ArgumentNullException(nameof(htsUnitOfWork));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<string>> Handle(AfyaMobileSynchronizeReferralCommand request, CancellationToken cancellationToken)
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

                    //check if person already exists
                    var identifiers = await registerPersonService.getPersonIdentifiers(afyaMobileId, 10);
                    if (identifiers.Count > 0)
                    {
                        // var person = await registerPersonService.GetPerson(identifiers[0].PersonId);
                        // var patient = await registerPersonService.GetPatientByPersonId(identifiers[0].PersonId);

                        //add referral
                        int providerId = request.PLACER_DETAIL.PROVIDER_ID;
                        DateTime dateToBeEnrolled = DateTime.ParseExact(request.REFERRAL.DATE_TO_BE_ENROLLED, "yyyyMMdd", null);
                        string facilityReferred = request.REFERRAL.REFERRED_TO;
                        var referralReason = await _unitOfWork.Repository<LookupItemView>()
                            .Get(x => x.MasterName == "ReferralReason" &&
                                      x.ItemName == "CCCEnrollment").ToListAsync();
                        var searchFacility = await encounterTestingService.SearchFacility(facilityReferred);
                        var previousReferrals = await encounterTestingService.GetReferralByPersonId(identifiers[0].PersonId);
                        int MFLCode = 0;
                        if (searchFacility.Count > 0)
                        {
                            MFLCode = Convert.ToInt32(searchFacility[0].MFLCode);

                            if (previousReferrals.Count > 0)
                            {
                                previousReferrals[0].ToFacility =
                                    Convert.ToInt32(searchFacility[0].MFLCode);
                                previousReferrals[0].OtherFacility = "";
                                previousReferrals[0].ExpectedDate = dateToBeEnrolled;

                                await encounterTestingService.UpdateReferral(previousReferrals[0]);
                            }
                            else
                            {
                                var facility = await encounterTestingService.GetCurrentFacility();
                                if (facility.Count > 0)
                                {
                                    await encounterTestingService.AddReferral(identifiers[0].PersonId,
                                        facility[0].FacilityID, 2, MFLCode,
                                        referralReason[0].ItemId, providerId, dateToBeEnrolled, "");
                                }
                            }
                        }
                    }
                    else
                    {
                        //update message has been processed
                        await registerPersonService.UpdateAfyaMobileInbox(afyaMobileMessage.Id, afyaMobileId, true, DateTime.Now, $"Person with afyaMobileId: {afyaMobileId} could not be found", false);
                        return Result<string>.Invalid($"Person with afyaMobileId: {afyaMobileId} could not be found");
                    }

                    //update message has been processed
                    await registerPersonService.UpdateAfyaMobileInbox(afyaMobileMessage.Id, afyaMobileId, true, DateTime.Now, $"Successfully synchronized HTS Referral for afyamobileid: {afyaMobileId}", true);
                    trans.Commit();
                    return Result<string>.Valid($"Successfully synchronized HTS Referral for afyamobileid: {afyaMobileId}");
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    Log.Error(ex.Message);
                    return Result<string>.Invalid($"Failed to synchronize Hts Referral for clientid: {afyaMobileId} " + ex.Message + " " + ex.InnerException);
                }
            }
        }
    }
}