using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands.PersonCommand;
using IQCare.Common.BusinessProcess.Services;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.Library;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IQCare.Common.BusinessProcess.CommandHandlers.PersonCommand
{
    public class GetPatientTransferInCommandHandler : IRequestHandler<GetPatientTransferInCommand, Result<PatientTransferIn>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        private int PatientMasterVisitId=0;
        private int PatientId=0;
        public GetPatientTransferInCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<PatientTransferIn>> Handle(GetPatientTransferInCommand request, CancellationToken cancellationToken)
        {

            using (_unitOfWork)
            {

                try
                {
                    RegisterPersonService rs = new RegisterPersonService(_unitOfWork);

                    var Patient = await rs.GetPatientByPersonId(request.PersonId);

                    if (Patient != null)
                    {
                        PatientId = Patient.Id;
                    }
                        var enrollmentVisitType = await _unitOfWork.Repository<LookupItemView>().Get(x => x.MasterName == "VisitType" && x.ItemName == "Enrollment").FirstOrDefaultAsync();
                        int? visitType = enrollmentVisitType != null ? enrollmentVisitType.ItemId : 0;

                        var enrollmentPatientMasterVisit =
                            await _unitOfWork.Repository<Core.Models.PatientMasterVisit>().Get(x =>
                            x.PatientId == PatientId && x.ServiceId == request.ServiceId && x.VisitType == visitType).ToListAsync();

                        if (enrollmentPatientMasterVisit.Count > 0)
                        {
                             PatientMasterVisitId = enrollmentPatientMasterVisit[0].Id;
                           

                        }
                        var TransferIn = await rs.GetPatientTransferIn(PatientId, PatientMasterVisitId);


                        return Result<PatientTransferIn>.Valid(TransferIn);
                    



                    

                    

                }
                catch (Exception ex)
                {

                    return Result<PatientTransferIn>.Invalid(ex.Message);

                }

            }


        }

    }
}
