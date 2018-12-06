using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Library;
using IQCare.Maternity.BusinessProcess.Commands.PNC;
using IQCare.Maternity.Core.Domain.PNC;
using IQCare.Maternity.Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IQCare.Maternity.BusinessProcess.CommandHandlers
{
    public class UpdatePatientExaminationCommandHandler : IRequestHandler<UpdatePatientExaminationCommand, Result<PatientExaminationResponse>>
    {
        IMaternityUnitOfWork _maternityUnitOfWork;

        public UpdatePatientExaminationCommandHandler(IMaternityUnitOfWork maternityUnitOfWork)
        {
            _maternityUnitOfWork = maternityUnitOfWork;
        }

        public async Task<Result<PatientExaminationResponse>> Handle(UpdatePatientExaminationCommand request, CancellationToken cancellationToken)
        {
            using (_maternityUnitOfWork)
            {
                try
                {
                    List<PhysicalExamination> postNatalExaminations = new List<PhysicalExamination>();

                    foreach (var postNatalExamResult in request.PostNatalExamResults)
                    {
                        var physicalExaminations = await _maternityUnitOfWork.Repository<PhysicalExamination>().Get(x =>
                                x.PatientId == request.PatientId &&
                                x.PatientMasterVisitId == request.PatientMasterVisitId &&
                                x.ExaminationTypeId == request.ExaminationTypeId &&
                                x.ExamId == postNatalExamResult.ExamId)
                            .ToListAsync();

                        if (physicalExaminations.Count > 0)
                        {
                            physicalExaminations[0].FindingId = postNatalExamResult.FindingId;
                            physicalExaminations[0].FindingsNotes = postNatalExamResult.FindingsNotes;

                            _maternityUnitOfWork.Repository<PhysicalExamination>().Update(physicalExaminations[0]);
                            await _maternityUnitOfWork.SaveAsync();
                        }
                        else
                        {
                            PhysicalExamination postNatalExamination = new PhysicalExamination()
                            {
                                PatientId = request.PatientId,
                                PatientMasterVisitId = request.PatientMasterVisitId,
                                ExaminationTypeId = request.ExaminationTypeId,
                                DeleteFlag = false,
                                CreateDate = DateTime.Now,
                                CreateBy = request.CreateBy,
                                ExamId = postNatalExamResult.ExamId,
                                FindingId = postNatalExamResult.FindingId,
                                FindingsNotes = postNatalExamResult.FindingsNotes,
                            };
                            postNatalExaminations.Add(postNatalExamination);
                        }
                    }

                    await _maternityUnitOfWork.Repository<PhysicalExamination>().AddRangeAsync(postNatalExaminations);
                    await _maternityUnitOfWork.SaveAsync();

                    return Result<PatientExaminationResponse>.Valid(new PatientExaminationResponse()
                    {
                        Message = "Successfully updated patient examination"
                    });
                }
                catch (Exception e)
                {
                    Log.Error("Error updating patient examination " + e.Message + " " + e.InnerException);
                    return Result<PatientExaminationResponse>.Invalid("Error updating patient examination " + e.Message);
                }
            }
        }
    }
}