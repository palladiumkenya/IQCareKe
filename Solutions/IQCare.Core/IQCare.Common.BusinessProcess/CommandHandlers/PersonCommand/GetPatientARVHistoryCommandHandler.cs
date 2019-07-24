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
    public class GetPatientARVHistoryCommandHandler : IRequestHandler<GetPatientARVHistoryCommand, Result<PatientARVHistory>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        private int PatientMasterVisitId;
        private int  PatientId;
        public GetPatientARVHistoryCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<PatientARVHistory>> Handle(GetPatientARVHistoryCommand request, CancellationToken cancellationToken)
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
                        var aRVHistory = await rs.GetPatientARVHistory(PatientId, PatientMasterVisitId);


                        
                    




                    return Result<PatientARVHistory>.Valid(aRVHistory);


                }
                catch (Exception ex)
                {

                    return Result<PatientARVHistory>.Invalid(ex.Message);

                }

            }
        }
    }
}
