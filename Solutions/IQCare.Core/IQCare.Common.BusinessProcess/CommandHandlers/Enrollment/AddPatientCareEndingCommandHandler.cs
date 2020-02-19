using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands.Enrollment;
using IQCare.Common.BusinessProcess.Services;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.Library;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;


namespace IQCare.Common.BusinessProcess.CommandHandlers.Enrollment
{
   public class AddPatientCareEndingCommandHandler:IRequestHandler<AddPatientCareEndingCommand,Result<AddPatientCareEndingResponse>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;

        public int Id { get; set; }

        public string Message { get; set; }
        public AddPatientCareEndingCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }



        public async Task<Result<AddPatientCareEndingResponse>> Handle (AddPatientCareEndingCommand request, CancellationToken cancellationToken)
        {
            try
            {
                int PatientEnrollmentId;
                var result = await _unitOfWork.Repository<PatientEnrollment>().Get(x =>
                        x.PatientId == request.PatientId && x.ServiceAreaId == request.ServiceAreaId &&
                        x.DeleteFlag == false && !x.CareEnded).FirstOrDefaultAsync();
              
                 if (result != null)
                    {

                        PatientEnrollmentId = result.Id;

                        if (PatientEnrollmentId > 0)
                        {
                        var PatientCare = await _unitOfWork.Repository<PatientCareEnding>().Get(x => x.PatientMasterVisitId == request.PatientMasterVisitId && x.PatientId == request.PatientId &&
                        x.DeleteFlag == false && x.PatientEnrollmentId == result.Id).FirstOrDefaultAsync();
                        if (PatientCare != null)
                        {
                            PatientCare.PatientId = request.PatientId;
                            PatientCare.PatientMasterVisitId = request.PatientMasterVisitId;
                            PatientCare.ExitReason = request.DisclosureReason;
                            PatientCare.ExitDate = request.CareEndedDate;
                            PatientCare.CareEndingNotes = request.Specify;
                            PatientCare.DateOfDeath = request.DeathDate;

                            _unitOfWork.Repository<PatientCareEnding>().Update(PatientCare);
                            await _unitOfWork.SaveAsync();
                            if (PatientCare.Id > 0)
                            {
                                Id = PatientCare.Id;
                            }
                            Message += "Patient Information has been updated";

                        }
                        else
                        {

                            PatientCareEnding pc = new PatientCareEnding()

                            {
                                PatientId = request.PatientId,
                                PatientMasterVisitId = request.PatientMasterVisitId,
                                PatientEnrollmentId = PatientEnrollmentId,
                                ExitReason = request.DisclosureReason,
                                ExitDate = request.CareEndedDate,
                                CareEndingNotes = request.Specify,
                                DateOfDeath = request.DeathDate,
                                Active = false,
                                DeleteFlag = false,
                                CreatedBy = request.UserId,
                                CreateDate = DateTime.Now,

                            };

                            await _unitOfWork.Repository<PatientCareEnding>().AddAsync(pc);
                            await _unitOfWork.SaveAsync();

                            if (pc.Id > 0)
                            {
                                Id = Id;
                            }

                            Message += "Patient has been successfully careended";

                        }
                          

                        
                        }

                        var patientenrollment = await _unitOfWork.Repository<PatientEnrollment>().Get(x => x.Id == PatientEnrollmentId
                      ).FirstOrDefaultAsync();

                        if (patientenrollment != null)
                        {
                            patientenrollment.CareEnded = true;
                            _unitOfWork.Repository<PatientEnrollment>().Update(patientenrollment);
                            await _unitOfWork.SaveAsync();

                        }


                    }

                    else
                    {
                        Message += "Patient has not been successfully careended";
                    }

                
                    
                

                return Result<AddPatientCareEndingResponse>.Valid(new AddPatientCareEndingResponse()
                {
                    Id = Id,
                    Message = Message
                });

            }
            catch(Exception ex)
            {
                return Result<AddPatientCareEndingResponse>.Invalid(ex.Message);
            }

        }
    }
}
