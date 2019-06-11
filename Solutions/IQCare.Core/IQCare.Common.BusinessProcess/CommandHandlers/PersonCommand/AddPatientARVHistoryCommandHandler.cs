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
    public class AddPatientARVHistoryCommandHandler : IRequestHandler<AddPatientARVHistoryCommand, Result<AddPatientARVHistoryResponse>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        public AddPatientARVHistoryCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<AddPatientARVHistoryResponse>> Handle(AddPatientARVHistoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using (_unitOfWork)
                {
                    int Id = 0;
                    int PatientMasterVisitId = 0;

                    RegisterPersonService rs = new RegisterPersonService(_unitOfWork);

                    var enrollmentVisitType = await _unitOfWork.Repository<LookupItemView>().Get(x => x.MasterName == "VisitType" && x.ItemName == "Enrollment").FirstOrDefaultAsync();
                    int? visitType = enrollmentVisitType != null ? enrollmentVisitType.ItemId : 0;

                    var enrollmentPatientMasterVisit =
                        await _unitOfWork.Repository<Core.Models.PatientMasterVisit>().Get(x =>
                        x.PatientId == request.PatientId && x.ServiceId == request.ServiceId && x.VisitType == visitType).ToListAsync();

                    if (enrollmentPatientMasterVisit.Count > 0)
                    {
                        PatientMasterVisitId = enrollmentPatientMasterVisit[0].Id;
                        var previousPatientEnrollment = await _unitOfWork.Repository<PatientEnrollment>().Get(x =>
                                                x.PatientId == request.PatientId && x.ServiceAreaId == request.ServiceId && x.DeleteFlag == false)
                                            .ToListAsync();

                        if (previousPatientEnrollment.Count > 0)
                        {
                            var PatientARVHistory = await rs.GetPatientARVHistory(request.PatientId, PatientMasterVisitId);

                            if(PatientARVHistory !=null)
                            {
                                PatientARVHistory.Purpose = request.Purpose;
                                PatientARVHistory.Regimen = request.Regimen;
                                PatientARVHistory.Months = request.Months;
                                PatientARVHistory.InitiationDate = request.InitiationDate;
                                PatientARVHistory.TreatmentType = request.TreatmentType;
                                PatientARVHistory.Weeks = request.Weeks;
                                PatientARVHistory.Months = request.Months;


                                var results = await rs.UpdatePatientARVHistory(PatientARVHistory);
                                Id = results.Id;
                            }
                            else
                            {

                                PatientARVHistory part = new PatientARVHistory();
                                part.Purpose = request.Purpose;
                                part.CreateDate = DateTime.Now;
                                part.CreatedBy = request.CreatedBy;
                                part.DeleteFlag = request.DeleteFlag;
                                part.Months = request.Months;
                                part.InitiationDate = request.InitiationDate;
                                part.Weeks = request.Weeks;
                                part.Purpose = request.Purpose;
                                part.Regimen = request.Regimen;
                                part.TreatmentType = request.TreatmentType;
                                part.RegimenUse = request.RegimenUse;
                                part.PatientId = request.PatientId;
                                part.PatientMasterVisitId = PatientMasterVisitId;

                                var results = await rs.AddPatientARVHistory(part);
                                Id = results.Id;
                            }


                        }

                        
                          
                    }
                    return Result<AddPatientARVHistoryResponse>.Valid(new AddPatientARVHistoryResponse()
                    {
                        ARVHistoryId = Id
                    });
                }
            }
            catch(Exception ex)
            {
                return Result<AddPatientARVHistoryResponse>.Invalid(ex.Message);
            }
        }

    }
}
