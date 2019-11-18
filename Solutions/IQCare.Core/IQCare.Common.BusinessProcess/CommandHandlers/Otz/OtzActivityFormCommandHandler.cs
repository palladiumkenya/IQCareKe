using IQCare.Common.BusinessProcess.Commands.Otz;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.Library;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IQCare.Common.BusinessProcess.CommandHandlers.Otz
{
    public class OtzActivityFormCommandHandler : IRequestHandler<OtzActivityFormCommand, Result<OtzActivityFormResponse>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;

        public OtzActivityFormCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<OtzActivityFormResponse>> Handle(OtzActivityFormCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    if(request.OtzActivity.Any())
                    {
                        int patientMasterVisitId = 0;
                        var visit = await _unitOfWork.Repository<Core.Models.PatientMasterVisit>().Get(x => x.VisitDate == request.VisitDate).ToListAsync();
                        if(visit.Count == 0)
                        {
                            var patientMasterVisit = new Core.Models.PatientMasterVisit()
                            {
                                PatientId = request.PatientId,
                                ServiceId = request.ServiceId,
                                Start = request.VisitDate,
                                DeleteFlag = false,
                                CreatedBy = request.UserId,
                                CreateDate = DateTime.Now,
                                Active = true,
                                End = request.VisitDate,
                                Status = null,
                                VisitBy = null,
                                VisitDate = request.VisitDate,
                                VisitScheduled = null,
                                VisitType = null
                            };
                            await _unitOfWork.Repository<Core.Models.PatientMasterVisit>().AddAsync(patientMasterVisit);
                            await _unitOfWork.SaveAsync();

                            patientMasterVisitId = patientMasterVisit.Id;
                        }
                        else
                        {
                            patientMasterVisitId = visit[0].Id;
                        }

                        var activityFormId = 0;
                        var otzActivities = await _unitOfWork.Repository<OtzActivityForm>().Get(x => x.PatientMasterVisitId == patientMasterVisitId).ToListAsync();
                        if(otzActivities.Count == 0)
                        {
                            var otzActivityForm = new OtzActivityForm()
                            {
                                AttendedSupportGroup = request.AttendedSupportGroup,
                                DeleteFlag = false,
                                PatientMasterVisitId = patientMasterVisitId,
                                Remarks = request.Remarks,
                                UserId = request.UserId
                            };

                            await _unitOfWork.Repository<OtzActivityForm>().AddAsync(otzActivityForm);
                            await _unitOfWork.SaveAsync();

                            activityFormId = otzActivityForm.Id;
                        } 
                        else
                        {
                            otzActivities[0].AttendedSupportGroup = request.AttendedSupportGroup;
                            otzActivities[0].Remarks = request.Remarks;
                            otzActivities[0].UserId = request.UserId;

                            _unitOfWork.Repository<OtzActivityForm>().Update(otzActivities[0]);
                            await _unitOfWork.SaveAsync();
                            
                            activityFormId = otzActivities[0].Id;
                        }

                        List<OtzActivityTopics> otzActivityTopics = new List<OtzActivityTopics>();
                        request.OtzActivity.ForEach(x => otzActivityTopics.Add(new OtzActivityTopics
                        {
                            ActivityFormId = activityFormId,
                            DateCompleted = x.DateCompleted,
                            TopicId = x.TopicId
                        }));

                        await _unitOfWork.Repository<OtzActivityTopics>().AddRangeAsync(otzActivityTopics);
                        await _unitOfWork.SaveAsync();
                    }

                    return Result<OtzActivityFormResponse>.Valid(new OtzActivityFormResponse()
                    {
                        Message = $"Successfully added new otz activity forms"
                    });
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "An error occured while trying to add new otz activity form");
                    return Result<OtzActivityFormResponse>.Invalid(ex.Message);
                }
            }
        }
    }
}
