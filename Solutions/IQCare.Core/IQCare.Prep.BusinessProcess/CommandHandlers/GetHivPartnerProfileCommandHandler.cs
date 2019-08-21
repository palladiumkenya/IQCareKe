using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Library;
using IQCare.Prep.BusinessProcess.Commands;
using IQCare.Prep.Core.Models;
using IQCare.Prep.Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Linq;

namespace IQCare.Prep.BusinessProcess.CommandHandlers
{
    public class GetHivPartnerProfileCommandHandler : IRequestHandler<GetHivPartnerProfileCommand, Result<GetHivPartnerProfileResponse>>
    {
        private readonly IPrepUnitOfWork _prepUnitOfWork;


        public GetHivPartnerProfileCommandHandler(IPrepUnitOfWork prepUnitOfWork)
        {
            _prepUnitOfWork = prepUnitOfWork;

        }

        public async Task<Result<GetHivPartnerProfileResponse>> Handle(GetHivPartnerProfileCommand request, CancellationToken cancellationToken)
        {
            using (_prepUnitOfWork)
            {
                try
                {
                    var prepstatus = await _prepUnitOfWork.Repository<PatientPartnerProfile>().Get(x =>
                       x.PatientId == request.PatientId  &&
                       x.DeleteFlag == false).ToListAsync();

                    return Result<GetHivPartnerProfileResponse>.Valid(new GetHivPartnerProfileResponse
                    {
                        patientProfiles = prepstatus
                    });
                }
                catch (Exception ex)
                {
                    Log.Error($"An error occured while trying to get prep status for PatientId: {request.PatientId} and PatientEncounterId: {request.PatientId}, exception: {ex.Message} {ex.InnerException}");
                    return Result<GetHivPartnerProfileResponse>.Invalid($"An error occured while trying to get prep status for PatientId: {request.PatientId}");

                }

            }
        }
    }
}
