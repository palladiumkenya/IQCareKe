

using IQCare.Common.Core.Models;
using IQCare.PMTCT.BusinessProcess.Commands;
using IQCare.PMTCT.Core.Models;
using IQCare.PMTCT.Infrastructure;
using IQCare.PMTCT.Services;
using MediatR;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace IQCare.PMTCT.BusinessProcess.CommandHandlers
{
    public class PatientEducationExaminationCommandHandler:IRequestHandler<PatientEducationExaminationCommand, Library.Result<PatientEducationExaminationResponse>>
    {
        private readonly ICommonUnitOfWork _commonUnitOfWork;
        private readonly IPmtctUnitOfWork _unitOfWork;
        private int result = 0;

        public PatientEducationExaminationCommandHandler(ICommonUnitOfWork commonUnitOfWork, IPmtctUnitOfWork unitOfWork)
        {
            _commonUnitOfWork = commonUnitOfWork ?? throw new ArgumentNullException(nameof(commonUnitOfWork)); 
            _unitOfWork=unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Library.Result<PatientEducationExaminationResponse>> Handle(PatientEducationExaminationCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    PatientEducationExaminationService _service = new PatientEducationExaminationService(_unitOfWork, _commonUnitOfWork);

                    var breastExamId =await  _commonUnitOfWork.Repository<LookupItemView>().Get(x => x.ItemName == "Breast Exam").Select(x => x.ItemId).FirstOrDefaultAsync();
                    var examinationTypeId =await  _commonUnitOfWork.Repository<LookupItemView>().Get(x => x.MasterName == "GeneralExamination").Select(x => x.MasterId).FirstOrDefaultAsync();

                    PatientPhysicalExamination breastExam = new PatientPhysicalExamination()
                    {
                        PatientId = request.PatientId,
                        PatientMasterVisitId = request.PatientMasterVisitId,
                        ExamId = breastExamId,
                        ExaminationTypeId = examinationTypeId,
                        FindingId = request.BreastExamDone
                    };

                    int breastExamResult = await _service.AddPatientPhysicalExamination(breastExam);

                    var syphillisExamId = await _commonUnitOfWork.Repository<LookupItemView>().Get(x => x.ItemName == "Treated Syphilis").Select(x => x.ItemId).FirstOrDefaultAsync();
                    PatientPhysicalExamination syphillisExam = new PatientPhysicalExamination()
                    {
                        PatientId = request.PatientId,
                        PatientMasterVisitId = request.PatientMasterVisitId,
                        ExamId = syphillisExamId,
                        ExaminationTypeId = examinationTypeId,
                        FindingId = request.TreatedSyphilis
                    };

                    int syphillisResultId = await _service.AddPatientPhysicalExamination(syphillisExam);

                    List<PatientEducation> patientCounselling = new List<PatientEducation>();

                    foreach (var item in request.CounsellingTopics)
                    {
                        PatientEducation data = new PatientEducation
                        {
                            PatientId = item.PatientId,
                            PatientMasterVisitId = item.PatientMasterVisitId,
                            CounsellingTopicId = item.CounsellingTopicId,
                            CounsellingDate = item.CounsellingDate,
                            Description = item.Description
                        };
                        patientCounselling.Add(data);
                    }

                    int patientEducationResultId = await _service.AddPatientEducation(patientCounselling);

                    if (breastExamResult > 0 & syphillisResultId > 0 & patientEducationResultId > 0)
                    {
                        result = 1;
                    }

                    return Library.Result<PatientEducationExaminationResponse>.Valid(new PatientEducationExaminationResponse() { resultId = 0 });
                }
                catch (Exception e)
                {
                    Log.Error(e.Message);
                    return Library.Result<PatientEducationExaminationResponse>.Invalid(e.Message);
                }
            }
            

        }
    }
}
