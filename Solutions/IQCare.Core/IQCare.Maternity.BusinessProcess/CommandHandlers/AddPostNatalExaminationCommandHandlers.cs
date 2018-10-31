using IQCare.Library;
using IQCare.Maternity.BusinessProcess.Commands.PNC;
using MediatR;
using System;
using Serilog;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Maternity.Core.Domain.PNC;
using AutoMapper;
using IQCare.Maternity.Infrastructure.UnitOfWork;
using System.Collections.Generic;

namespace IQCare.Maternity.BusinessProcess.CommandHandlers
{
    public class AddPostNatalExaminationCommandHandlers : IRequestHandler<AddPostNatalExaminationCommand, Result<PostnatalExamResultsResponse>>
    {
        IMaternityUnitOfWork _maternityUnitOfWork;
   
        ILogger logger = Log.ForContext<AddPatientDiagnosisCommandHandler>();

        public AddPostNatalExaminationCommandHandlers(IMaternityUnitOfWork maternityUnitOfWork)
        {
            _maternityUnitOfWork = maternityUnitOfWork;
        }

        public async Task<Result<PostnatalExamResultsResponse>> Handle(AddPostNatalExaminationCommand request, CancellationToken cancellationToken)
        {
            using (_maternityUnitOfWork)
            {
                try
                {
                    List<PhysicalExamination> postNatalExaminations = new List<PhysicalExamination>();

                    foreach (var results in request.PostNatalExamResults)
                    {
                        PhysicalExamination postNatalExamination = new PhysicalExamination()
                        {
                            PatientId = request.PatientId,
                            PatientMasterVisitId = request.PatientMasterVisitId,
                            ExaminationTypeId = request.ExaminationTypeId,
                            DeleteFlag = false,
                            CreateDate = DateTime.Now,
                            CreateBy = request.CreateBy,
                            ExamId = results.ExamId,
                            FindingId = results.FindingId,
                            FindingsNotes = results.FindingsNotes,
                        };
                        postNatalExaminations.Add(postNatalExamination);

                    }
                    await _maternityUnitOfWork.Repository<PhysicalExamination>().AddRangeAsync(postNatalExaminations);
                    await _maternityUnitOfWork.SaveAsync();
                    _maternityUnitOfWork.Dispose();
                    return Result<PostnatalExamResultsResponse>.Valid(new PostnatalExamResultsResponse
                    {
                        PatientId = request.PatientId
                    });
                }
                catch (Exception e)
                {
                    Log.Error(e.Message);
                    return Result<PostnatalExamResultsResponse>.Invalid(e.Message);
                }
            }
        }

    }


}
