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
                    List<PatientMasterVisit> masterVisits = new List<PatientMasterVisit>();

                    if (request.PatientMasterVisitId > 0)
                    {
                        masterVisits = await _prepUnitOfWork.Repository<PatientMasterVisit>().Get(
                           x => x.PatientId == request.PatientId && x.Id == request.PatientMasterVisitId && x.ServiceId == request.ServiceAreaId).ToListAsync();

                    }
                    else{

                        masterVisits = await _prepUnitOfWork.Repository<PatientMasterVisit>().Get(
                         x => x.PatientId == request.PatientId && x.VisitDate.Value.Day == request.VisitDate.Day && x.VisitDate.Value.Month==request.VisitDate.Month && x.VisitDate.Value.Year==request.VisitDate.Year && x.ServiceId == request.ServiceAreaId).ToListAsync();

                    }

                    if(masterVisits.Count > 0 )
                    {
                        var PatientEncounters = await _prepUnitOfWork.Repository<PatientEncounter>().Get(x => x.PatientMasterVisitId == masterVisits[0].Id)
                            .ToListAsync();
                      if(PatientEncounters ==null && PatientEncounters.Count> 0)
                         {
                        PatientEncounter patientEncounter = new PatientEncounter()
                        {
                            PatientId = request.PatientId,
                            EncounterTypeId = request.EncounterTypeId,
                            Status = 0,
                            PatientMasterVisitId = masterVisits[0].Id,
                            EncounterStartTime = request.VisitDate,
                            EncounterEndTime = request.VisitDate,
                            ServiceAreaId = request.ServiceAreaId,
                            CreatedBy = request.UserId,
                            CreateDate = DateTime.Now


                        };
                        await _prepUnitOfWork.Repository<PatientEncounter>().AddAsync(patientEncounter);
                        await _prepUnitOfWork.SaveAsync();
                        _prepUnitOfWork.Save();

                       }
                        int PatientMasterVisitId = masterVisits[0].Id;
                       
                        
                        if (request.riskAssessments.Count > 0)
                        {
                            foreach(var ra  in request.riskAssessments)
                            {

                          
                                var RiskAssessments = await _prepUnitOfWork.Repository<RiskAssessment>().Get(x => x.PatientMasterVisitId == PatientMasterVisitId && x.PatientId == request.PatientId
                                  && x.Id == ra.Id).ToListAsync();

                            
                                if(RiskAssessments !=null && RiskAssessments.Count > 0)
                                {
                                   foreach(var rass in RiskAssessments)
                                    {
                                        rass.DeleteFlag = ra.DeleteFlag;
                                        rass.RiskAssessmentId = ra.RiskAssessmentid;
                                        rass.RiskAssessmentValue = ra.Value;
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
                                    riskass.CreateDate =DateTime.Now;
                                    riskass.Comment = ra.Comment;
                                    await _prepUnitOfWork.Repository<RiskAssessment>().AddAsync(riskass);
                                    await _prepUnitOfWork.SaveAsync();

                                }

                            }
                        _prepUnitOfWork.Save();

                        message += "The Risk Assessment Form Has Been Updated";
                        }
                        
                        if(request.ClinicalNotes.Count > 0)
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

                    else
                    {
                        PatientMasterVisit pmv = new PatientMasterVisit();
                        pmv.PatientId = request.PatientId;
                        pmv.ServiceId = request.ServiceAreaId;
                        pmv.Active = true;
                        pmv.CreateDate = DateTime.Now;
                        pmv.VisitDate = Convert.ToDateTime(request.VisitDate);
                        pmv.CreatedBy = request.UserId;
                        pmv.VisitType = 0;
                        pmv.Start = DateTime.Now;
                        pmv.End = DateTime.Now;


                       await _prepUnitOfWork.Repository<PatientMasterVisit>().AddAsync(pmv);
                        await _prepUnitOfWork.SaveAsync();
                    _prepUnitOfWork.Save();
                   

                        pmvId = pmv.Id;
                        PatientEncounter patientEncounter = new PatientEncounter()
                        {
                            PatientId = request.PatientId,
                            EncounterTypeId = request.EncounterTypeId,
                            Status = 0,
                            PatientMasterVisitId = pmv.Id,
                            EncounterStartTime = request.VisitDate,
                            EncounterEndTime = request.VisitDate,
                            ServiceAreaId = request.ServiceAreaId,
                            CreatedBy = request.UserId,
                            CreateDate = DateTime.Now


                        };
                        await _prepUnitOfWork.Repository<PatientEncounter>().AddAsync(patientEncounter);
                        await _prepUnitOfWork.SaveAsync();
                        _prepUnitOfWork.Save();

                    if (request.riskAssessments.Count > 0)
                        {
                            foreach (var ra in request.riskAssessments)
                            {

                                var RiskAssessList = await _prepUnitOfWork.Repository<RiskAssessment>().Get(x => x.PatientMasterVisitId ==pmv.Id && x.PatientId == request.PatientId
                                  && x.Id == ra.Id).ToListAsync();
                                if (RiskAssessList != null && RiskAssessList.Count > 0)
                                {
                                    foreach (var rass in RiskAssessList)
                                    {
                                     
                                        rass.DeleteFlag = ra.DeleteFlag;
                                        rass.RiskAssessmentId = ra.RiskAssessmentid;
                                        rass.RiskAssessmentValue = ra.Value;
                                        _prepUnitOfWork.Repository<RiskAssessment>().Update(rass);

                                        await _prepUnitOfWork.SaveAsync();
                                    _prepUnitOfWork.Save();
                                }



                                }
                                else
                                {
                                    RiskAssessment riskass = new RiskAssessment();
                                    riskass.PatientMasterVisitId = pmvId;
                                    riskass.PatientId = request.PatientId;
                                    riskass.RiskAssessmentId = ra.RiskAssessmentid;
                                    riskass.RiskAssessmentValue = ra.Value;
                                    riskass.DeleteFlag = ra.DeleteFlag;
                                    riskass.CreateDate = Convert.ToDateTime(DateTime.Now);
                                    riskass.Comment = ra.Comment;
                                    riskass.CreatedBy = request.UserId;
                                    

                                    await _prepUnitOfWork.Repository<RiskAssessment>().AddAsync(riskass);
                                await _prepUnitOfWork.SaveAsync();
                                  

                            }

                            }


                            message += "Risk Assessment Form Has Been Added Successfully";
                        }
                        if (request.ClinicalNotes.Count > 0)
                        {
                            foreach (var clinicalnotes in request.ClinicalNotes)
                            {

                                var clinicalNotes = await _prepUnitOfWork.Repository<PatientClinicalNotes>().Get(x => x.PatientMasterVisitId == pmvId && x.PatientId == request.PatientId
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
                                        clinicaln.PatientMasterVisitId = pmvId;


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
                                        pcn.PatientMasterVisitId = pmvId;

                                        await _prepUnitOfWork.Repository<PatientClinicalNotes>().AddAsync(pcn);
                                        await _prepUnitOfWork.SaveAsync();
                                    }

                                }

                            }

                        _prepUnitOfWork.Save();
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

