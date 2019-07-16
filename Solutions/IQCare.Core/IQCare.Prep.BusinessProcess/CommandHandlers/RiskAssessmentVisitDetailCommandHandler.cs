using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Prep.BusinessProcess.Commands;
using IQCare.Prep.Core.Models;
using IQCare.Library;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using IQCare.Prep.Infrastructure;
using IQCare.Prep.Infrastructure.UnitOfWork;
using System.Threading;
using System.Threading.Tasks;

namespace IQCare.Prep.BusinessProcess.CommandHandlers
{
    public class RiskAssessmentVisitDetailCommandHandler :IRequestHandler<RiskAssessmentVisitDetailCommand,Result<RiskAssessmentResponse>>
    {

        private readonly IPrepUnitOfWork _prepUnitOfWork;
        private readonly ILogger logger = Log.ForContext<RiskAssessmentVisitDetailCommandHandler>();
        public string message;
        public int pmvId;
        public RiskAssessmentVisitDetailCommandHandler(IPrepUnitOfWork prepUnitOfWork)
        {
            _prepUnitOfWork = prepUnitOfWork ?? throw new ArgumentNullException(nameof(prepUnitOfWork));
        }


        public async Task<Result<RiskAssessmentResponse>> Handle (RiskAssessmentVisitDetailCommand request,CancellationToken cancellationToken)
        {
            try
                {


                if (request.PatientMasterVisitId > 0)
                {
                    int PatientMasterVisitId = request.PatientMasterVisitId;



                    if (request.riskAssessments.Count > 0)
                    {
                        foreach (var ra in request.riskAssessments)
                        {


                            var RiskAssessments = await _prepUnitOfWork.Repository<RiskAssessment>().Get(x => x.PatientMasterVisitId == PatientMasterVisitId && x.PatientId == request.PatientId
                              && x.Id == ra.Id).ToListAsync();


                            if (RiskAssessments != null && RiskAssessments.Count > 0)
                            {
                                foreach (var rass in RiskAssessments)
                                {
                                    rass.DeleteFlag = ra.DeleteFlag;
                                    rass.RiskAssessmentId = ra.RiskAssessmentid;
                                    rass.RiskAssessmentValue = ra.Value;
                                    rass.AssessmentDate = ra.Date;
                                    rass.Comment = ra.Comment;
                                    _prepUnitOfWork.Repository<RiskAssessment>().Update(rass);

                                    await _prepUnitOfWork.SaveAsync();
                                }



                            }
                            else
                            {
                                RiskAssessment riskass = new RiskAssessment();
                                riskass.PatientMasterVisitId = PatientMasterVisitId;
                                riskass.PatientId = request.PatientId;
                                riskass.RiskAssessmentId = ra.RiskAssessmentid;
                                riskass.RiskAssessmentValue = ra.Value;
                                riskass.DeleteFlag = ra.DeleteFlag;
                                riskass.CreateDate = DateTime.Now;
                                riskass.Comment = ra.Comment;
                                riskass.AssessmentDate = ra.Date;
                                await _prepUnitOfWork.Repository<RiskAssessment>().AddAsync(riskass);
                                await _prepUnitOfWork.SaveAsync();

                            }

                        }
                        _prepUnitOfWork.Save();

                        message += "The Risk Assessment Form Has Been Updated";
                    }

                    if (request.ClinicalNotes.Count > 0)
                    {
                        foreach (var clinicalnotes in request.ClinicalNotes)
                        {

                            var clinicalNotes = await _prepUnitOfWork.Repository<PatientClinicalNotes>().Get(x => x.PatientMasterVisitId == PatientMasterVisitId && x.PatientId == request.PatientId
                              && x.Id == clinicalnotes.Id).ToListAsync();
                            if (clinicalNotes != null && clinicalNotes.Count > 0)
                            {
                                foreach (var clinicaln in clinicalNotes)
                                {





                                    clinicaln.ClinicalNotes = clinicalnotes.Comment;
                                    clinicaln.CreatedBy = request.UserId;
                                    clinicaln.CreateDate = DateTime.Now;
                                    clinicaln.DeleteFlag = clinicalnotes.DeleteFlag;
                                    clinicaln.ServiceAreaId = clinicaln.ServiceAreaId;
                                    clinicaln.PatientId = request.PatientId;
                                    clinicaln.PatientMasterVisitId = PatientMasterVisitId;



                                    _prepUnitOfWork.Repository<PatientClinicalNotes>().Update(clinicaln);

                                    await _prepUnitOfWork.SaveAsync();
                                }



                            }
                            else
                            {
                                if (!String.IsNullOrEmpty(clinicalnotes.Comment))
                                {
                                    PatientClinicalNotes pcn = new PatientClinicalNotes();
                                    pcn.ClinicalNotes = clinicalnotes.Comment;
                                    pcn.CreatedBy = request.UserId;
                                    pcn.CreateDate = DateTime.Now;
                                    pcn.DeleteFlag = clinicalnotes.DeleteFlag;
                                    pcn.ServiceAreaId = clinicalnotes.ServiceAreaId;
                                    pcn.PatientId = request.PatientId;
                                    pcn.PatientMasterVisitId = PatientMasterVisitId;

                                    await _prepUnitOfWork.Repository<PatientClinicalNotes>().AddAsync(pcn);
                                    await _prepUnitOfWork.SaveAsync();

                                }

                            }
                            _prepUnitOfWork.Save();




                        }


                    }

                }

                    return Result<RiskAssessmentResponse>.Valid(new RiskAssessmentResponse
                    {
                        PatientMasterVisitId = pmvId,
                        Message = message
                    });
                }
                catch(Exception ex)
                {
                    {
                        string message = $"An error  has Occured" + ex.Message;

                      
                        return await Task.FromResult(Result<RiskAssessmentResponse>.Invalid(message));
                    }

                }
            }
        }


    }

