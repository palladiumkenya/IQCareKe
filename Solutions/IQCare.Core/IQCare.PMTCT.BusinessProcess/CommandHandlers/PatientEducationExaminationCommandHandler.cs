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
using IQCare.Common.Infrastructure;
using Microsoft.EntityFrameworkCore;


namespace IQCare.PMTCT.BusinessProcess.CommandHandlers
{
    public class PatientEducationExaminationCommandHandler:IRequestHandler<PatientEducationExaminationCommand, Library.Result<PatientEducationExaminationResponse>>
    {
        private readonly ICommonUnitOfWork _commonUnitOfWork;
        private readonly IPmtctUnitOfWork _unitOfWork;
        public int result = 0;

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
                    int patientEducationResultId = 0;

                    var breastExamId =await  _commonUnitOfWork.Repository<LookupItem>().Get(x => x.Name == "Breast Exam").Select(x => x.Id).FirstOrDefaultAsync();
                    var examinationTypeId =await  _commonUnitOfWork.Repository<LookupItemView>().Get(x => x.MasterName == "GeneralExamination").Select(x => x.MasterId).FirstOrDefaultAsync();

                    Core.Models.PhysicalExamination breastExam = new Core.Models.PhysicalExamination()
                    {
                        PatientId = request.PatientId,
                        PatientMasterVisitId = request.PatientMasterVisitId,
                        ExamId = breastExamId,
                        ExaminationTypeId = examinationTypeId,
                        FindingId = request.BreastExamDone,
                        CreateDate = DateTime.Now
                    };

                    int breastExamResult = await _service.AddPatientPhysicalExamination(breastExam);

                    var syphillisExamId = await _commonUnitOfWork.Repository<LookupItem>().Get(x => x.Name == "Treated Syphilis").Select(x => x.Id).FirstOrDefaultAsync();
                    Core.Models.PhysicalExamination syphillisExam = new Core.Models.PhysicalExamination()
                    {
                        PatientId = request.PatientId,
                        PatientMasterVisitId = request.PatientMasterVisitId,
                        ExamId = syphillisExamId,
                        ExaminationTypeId = examinationTypeId,
                        FindingId = request.TreatedSyphilis,
                        CreateDate = DateTime.Now
                    };

                    int syphillisResultId = await _service.AddPatientPhysicalExamination(syphillisExam);

                    List<PatientEducation> patientCounselling = new List<PatientEducation>();
                    List<PatientEducation> patientEducationExists = _unitOfWork.Repository<PatientEducation>()
                        .Get(x => x.PatientId == request.PatientId).ToList();

                    if (request.CounsellingTopics.Count > 0)
                    {
                        foreach (var item in request.CounsellingTopics)
                        {
                            if (item.CounsellingTopicId > 0)
                            {
                                bool itemExists = patientEducationExists.Exists(x =>
                                    x.CounsellingTopicId == item.CounsellingTopicId &&
                                    x.CounsellingDate == item.CounsellingDate);
                                if (!itemExists)
                                {
                                    PatientEducation data = new PatientEducation
                                    {
                                        PatientId = request.PatientId,
                                        PatientMasterVisitId = request.PatientMasterVisitId,
                                        CounsellingTopicId = item.CounsellingTopicId,
                                        CounsellingDate = item.CounsellingDate,
                                        Description = item.Description,
                                        CreateDate = DateTime.Now,
                                        CreatedBy = request.CreatedBy

                                    };
                                    patientCounselling.Add(data);
                                }
                                
                            }
                        }

                         patientEducationResultId = await _service.AddPatientEducation(patientCounselling);

                    }


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
