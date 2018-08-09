using System;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.Infrastructure;
using IQCare.Common.Services;
using IQCare.Library;
using IQCare.PMTCT.BusinessProcess.Commands;
using IQCare.PMTCT.Core;
using IQCare.PMTCT.Infrastructure;
using MediatR;
using Serilog;

namespace IQCare.PMTCT.BusinessProcess.CommandHandlers
{
    public class VisitDetailsCommandHandler : IRequestHandler<VisitDetailsCommand, Result<VisitDetailsCommandResult>>
    {
        private readonly IPmtctUnitOfWork _pmtctUnitOfWork;
        private readonly ICommonUnitOfWork _commonUnitOfWork;
        public VisitDetailsCommandHandler(IPmtctUnitOfWork pmtctUnitOfWork, ICommonUnitOfWork commonUnitOfWork)
        {
            _pmtctUnitOfWork = pmtctUnitOfWork ?? throw new ArgumentNullException(nameof(pmtctUnitOfWork));
            _commonUnitOfWork = commonUnitOfWork ?? throw new ArgumentNullException(nameof(commonUnitOfWork));
        }

        public async Task<Result<VisitDetailsCommandResult>> Handle(VisitDetailsCommand request, CancellationToken cancellationToken)
        {
            using (_pmtctUnitOfWork)
            {
                try
                {
                    //Patient Master Visit
                    PatientMasterVisitService patientMasterVisitService = new PatientMasterVisitService(_commonUnitOfWork);
                    var patientMasterVisit = await patientMasterVisitService.Add(request.PatientId, 1,DateTime.Today, 0,request.VisitDate, request.VisitDate, 0,0,request.VisitType,0);

                    //Patient Pregnancy

                    return Result<VisitDetailsCommandResult>.Valid(new VisitDetailsCommandResult()
                    {
                        PatientMasterVisitId =  patientMasterVisit.Id,
                        PregancyId = 1
                    });
                }
                catch (Exception e)
                {
                    Log.Error(e.Message + " " + e.InnerException);
                    return Result<VisitDetailsCommandResult>.Invalid(e.Message);
                }
            }
        }
    }
}