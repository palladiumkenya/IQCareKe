
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
    public class AddPatientTransferInCommandHandler : IRequestHandler<AddPatientTransferInCommand, Result<AddPatientTransferInResponse>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        public AddPatientTransferInCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<AddPatientTransferInResponse>> Handle(AddPatientTransferInCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using (_unitOfWork)
                {
                    int Id = 0;
                    int PatientMasterVisitId = 0;
                    RegisterPersonService registerPersonService = new RegisterPersonService(_unitOfWork);
                    var enrollmentVisitType = await _unitOfWork.Repository<LookupItemView>().Get(x => x.MasterName == "VisitType" && x.ItemName == "Enrollment").FirstOrDefaultAsync();
                    int? visitType = enrollmentVisitType != null ? enrollmentVisitType.ItemId : 0;
                    DateTime TreatmentStartDate;
                    if(request.TreatmentStartDate  == null)
                    {
                        TreatmentStartDate = request.TransferInDate;
                    }
                    else
                    {
                        TreatmentStartDate = request.TreatmentStartDate;
                    }
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
                            previousPatientEnrollment[0].TransferIn = true;

                            _unitOfWork.Repository<PatientEnrollment>().Update(previousPatientEnrollment[0]);


                        }

                        var transferin = await registerPersonService.GetPatientTransferIn(request.PatientId, PatientMasterVisitId);
                        if (transferin != null)
                        {
                            transferin.TransferInDate = request.TransferInDate;
                            transferin.TransferInNotes = request.TransferInNotes;
                            transferin.TreatmentStartDate = TreatmentStartDate;
                            transferin.CountyFrom = request.CountyFrom;
                            transferin.CurrentTreatment = request.CurrentTreatment;
                            transferin.ServiceAreaId = request.ServiceId;
                            transferin.MflCode = request.MflCode;
                            transferin.DeleteFlag = request.DeleteFlag;
                            transferin.FacilityFrom = request.FacilityFrom;

                            var results = await registerPersonService.UpdatePatientTransferIn(transferin);
                            Id = results.Id;
                        }
                        else
                        {
                            PatientTransferIn pt = new PatientTransferIn();

                            pt.TransferInDate = request.TransferInDate;
                            pt.TransferInNotes = request.TransferInNotes;
                            pt.TreatmentStartDate = TreatmentStartDate;
                            pt.FacilityFrom = request.FacilityFrom;
                            pt.CountyFrom = request.CountyFrom;
                            pt.CurrentTreatment = request.CurrentTreatment;
                            pt.ServiceAreaId = request.ServiceId;
                            pt.MflCode = request.MflCode;
                            pt.PatientId = request.PatientId;
                            pt.PatientMasterVisitId = PatientMasterVisitId;
                            pt.CreateDate = DateTime.Now;
                            pt.CreatedBy = request.CreatedBy;
                            pt.DeleteFlag = request.DeleteFlag;
                            var results = await registerPersonService.AddPatientTransferIn(pt);
                            Id = results.Id;
                        }
                    }
                    return Result<AddPatientTransferInResponse>.Valid(new AddPatientTransferInResponse()
                    {
                        TransferInId = Id
                    });
                }

            }

            catch (Exception ex)
            {
                return Result<AddPatientTransferInResponse>.Invalid(ex.Message);
            }


        }
    }
}