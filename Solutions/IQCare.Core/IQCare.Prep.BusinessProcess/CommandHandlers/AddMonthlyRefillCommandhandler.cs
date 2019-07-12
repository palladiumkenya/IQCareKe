using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using IQCare.Library;
using IQCare.Prep.BusinessProcess.Commands;
using IQCare.Prep.Core.Models;
using IQCare.Prep.Infrastructure;
using IQCare.Prep.Infrastructure.UnitOfWork;
using MediatR;
using Serilog;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace IQCare.Prep.BusinessProcess.CommandHandlers
{
    public class AddMonthlyRefillCommandhandler : IRequestHandler<AddMonthlyRefillCommand, Result<MonthlyRefillResponse>>
    {

        private readonly IPrepUnitOfWork _prepUnitOfWork;

        public int Id { get; set; }

        private string message { get; set; }



        public AddMonthlyRefillCommandhandler(IPrepUnitOfWork prepUnitOfWork)
        {
            _prepUnitOfWork = prepUnitOfWork ?? throw new ArgumentNullException(nameof(prepUnitOfWork));
        }

        public async Task<Result<MonthlyRefillResponse>> Handle(AddMonthlyRefillCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var clinicalnotes = await _prepUnitOfWork.Repository<PatientClinicalNotes>().Get(x => x.PatientId == request.PatientId && x.ServiceAreaId == request.ServiceAreaId && x.PatientMasterVisitId == request.PatientMasterVisitId && x.DeleteFlag == false).ToListAsync();
                if (request.clinicalNotes.Count > 0)
                {
                    if (clinicalnotes.Count > 0)
                    {
                      foreach( var x  in clinicalnotes)
                        {

                            if (String.IsNullOrEmpty(request.clinicalNotes[0].remark) == true)
                            {
                                x.DeleteFlag = true;
                            }
                            else
                            {
                                x.DeleteFlag = false;
                                x.ClinicalNotes = request.clinicalNotes[0].remark;


                            }

                            _prepUnitOfWork.Repository<PatientClinicalNotes>().Update(x);
                            await _prepUnitOfWork.SaveAsync();
                            message += "Remarks is updated";
                      }
                    }
                    else
                    {
                        if (request.clinicalNotes.Count > 0)
                        {
                            if (String.IsNullOrEmpty(request.clinicalNotes[0].remark.ToString()) == false)
                            {
                                PatientClinicalNotes cl = new PatientClinicalNotes();

                                cl.ServiceAreaId = request.ServiceAreaId;
                                cl.ClinicalNotes = request.clinicalNotes[0].remark;
                                cl.DeleteFlag = false;
                                cl.PatientId = request.PatientId;
                                cl.PatientMasterVisitId = request.PatientMasterVisitId;
                                cl.CreatedBy = request.CreatedBy;
                                cl.CreateDate = DateTime.Now;

                                await _prepUnitOfWork.Repository<PatientClinicalNotes>().AddAsync(cl);
                                await _prepUnitOfWork.SaveAsync();

                                message += "Remarks is added successfully";
                            }
                        }
                    }
                }
                if (request.Adherence.Count > 0)
                {

                    var AdherenceOutcome = await _prepUnitOfWork.Repository<AdherenceOutcome>().Get(x => x.PatientId == request.PatientId && x.PatientMasterVisitId == request.PatientMasterVisitId && x.AdherenceType == request.Adherence[0].AdherenceType && x.DeleteFlag ==false).ToListAsync();


                    if (AdherenceOutcome.Count > 0)
                    {

                        foreach (var x in AdherenceOutcome)
                        {

                            if (x.AdherenceType == request.Adherence[0].AdherenceType && x.Score == request.Adherence[0].Score)
                            {
                                x.DeleteFlag = false;
                                x.CreateBy = request.CreatedBy;
                                x.CreateDate = DateTime.Now;

                                Id = x.Id;

                                _prepUnitOfWork.Repository<AdherenceOutcome>().Update(x);
                                await _prepUnitOfWork.SaveAsync();
                                message += "Adherence Outcome has been updated";


                            }
                            else
                            {
                                x.DeleteFlag = true;
                                _prepUnitOfWork.Repository<AdherenceOutcome>().Update(x);
                                await _prepUnitOfWork.SaveAsync();
                           

                                AdherenceOutcome adc = new AdherenceOutcome();
                                adc.PatientId = request.PatientId;
                                adc.PatientMasterVisitId = request.PatientMasterVisitId;
                                adc.Score = request.Adherence[0].Score;
                                adc.AdherenceType = request.Adherence[0].AdherenceType;
                                adc.CreateBy = request.CreatedBy;
                                adc.CreateDate = DateTime.Now;
                                await _prepUnitOfWork.Repository<AdherenceOutcome>().AddAsync(adc);
                                await _prepUnitOfWork.SaveAsync();

                                Id = adc.Id;
                                message += "Adherence Outcome has been Saved";

                            }

                            

                        }
                    }
                    else
                    {
                        AdherenceOutcome adc = new AdherenceOutcome();
                        adc.PatientId = request.PatientId;
                        adc.PatientMasterVisitId = request.PatientMasterVisitId;
                        adc.Score = request.Adherence[0].Score;
                        adc.AdherenceType = request.Adherence[0].AdherenceType;
                        adc.CreateBy = request.CreatedBy;
                        adc.CreateDate = DateTime.Now;
                        await _prepUnitOfWork.Repository<AdherenceOutcome>().AddAsync(adc);
                        await _prepUnitOfWork.SaveAsync();

                        Id = adc.Id;
                        message += "Adherence Outcome has been Saved";
                    }
                }
                else
                {
                    AdherenceOutcome adc = new AdherenceOutcome();
                    adc.PatientId = request.PatientId;
                    adc.PatientMasterVisitId = request.PatientMasterVisitId;
                    adc.Score = request.Adherence[0].Score;
                    adc.AdherenceType = request.Adherence[0].AdherenceType;
                    adc.CreateBy = request.CreatedBy;
                    adc.CreateDate = DateTime.Now;
                    await _prepUnitOfWork.Repository<AdherenceOutcome>().AddAsync(adc);
                    await _prepUnitOfWork.SaveAsync();

                    Id = adc.Id;
                    message += "Adherence Outcome has been Saved";
                }


                if (request.screeningdetail.Count > 0)
                {

                    var patientscreeninglist = await _prepUnitOfWork.Repository<PatientScreening>().Get(x => x.PatientId == request.PatientId && x.PatientMasterVisitId == request.PatientMasterVisitId).ToListAsync();
                    if (patientscreeninglist.Count > 0)
                    {
                        foreach(var x  in patientscreeninglist)
                        {
                            x.DeleteFlag = true;
                            _prepUnitOfWork.Repository<PatientScreening>().Update(x);
                            await _prepUnitOfWork.SaveAsync();
                        }

                        foreach (var detail in request.screeningdetail) //.ForEach(async x =>
                        {
                            var find = patientscreeninglist.Find(t => t.ScreeningTypeId == detail.ScreeningTypeId && t.ScreeningValueId == detail.ScreeningValueId);
                            if (find != null)
                            {
                                find.DeleteFlag = false;
                                find.VisitDate = request.VisitDate;
                                find.CreatedBy = request.CreatedBy;
                                find.Comment = detail.Comment;
                                _prepUnitOfWork.Repository<PatientScreening>().Update(find);
                                await _prepUnitOfWork.SaveAsync();
                            }
                            else
                            {

                                PatientScreening sc = new PatientScreening();
                                sc.DeleteFlag = false;
                                sc.VisitDate = request.VisitDate;
                                sc.ScreeningTypeId = detail.ScreeningTypeId;
                                sc.ScreeningValueId = detail.ScreeningValueId;
                                sc.ScreeningDone = true;
                                sc.Active = true;
                                sc.CreateDate = DateTime.Now;
                                sc.CreatedBy = request.CreatedBy;
                                sc.Comment = detail.Comment;
                                sc.PatientId = request.PatientId;
                                sc.PatientMasterVisitId = request.PatientMasterVisitId;
                                await _prepUnitOfWork.Repository<PatientScreening>().AddAsync(sc);
                                await _prepUnitOfWork.SaveAsync();

                            }
                        }

                        message += "Monthly Refill updated successfully";
                    }
                    else
                    {
                        foreach (var requestdetail in request.screeningdetail)
                        {
                            PatientScreening sc = new PatientScreening();
                            sc.DeleteFlag = false;
                            sc.VisitDate = request.VisitDate;
                            sc.ScreeningTypeId = requestdetail.ScreeningTypeId;
                            sc.ScreeningValueId = requestdetail.ScreeningValueId;
                            sc.ScreeningDone = true;
                            sc.Active = true;
                            sc.CreateDate = DateTime.Now;
                            sc.CreatedBy = request.CreatedBy;
                            sc.Comment = requestdetail.Comment;
                            sc.PatientId = request.PatientId;
                            sc.PatientMasterVisitId = request.PatientMasterVisitId;
                            await _prepUnitOfWork.Repository<PatientScreening>().AddAsync(sc);
                            await _prepUnitOfWork.SaveAsync();

                        }

                        message += "Monthly refill saved successfully";
                    }

                }

                return Result<MonthlyRefillResponse>.Valid(new MonthlyRefillResponse()
                {
                    Message = message,
                    Id = Id
                });

            }
            catch (Exception ex)
            {
                return Result<MonthlyRefillResponse>.Invalid(ex.Message);
            }


        }

    }
}
