using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Library;
using IQCare.Prep.BusinessProcess.Commands;
using IQCare.Prep.Core.Models;
using IQCare.Prep.Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Linq;

namespace IQCare.Prep.BusinessProcess.CommandHandlers
{
   public  class GetEncounterToFillCommandHandler:IRequestHandler<GetEncounterToFillCommand,Result<ResponseEncounter>>
    {

        private readonly IPrepUnitOfWork _prepUnitOfWork;
        public string form;
        public PrepFormsView prepform;
        public GetEncounterToFillCommandHandler(IPrepUnitOfWork prepUnitOfWork)
        {
            _prepUnitOfWork = prepUnitOfWork;
        }

        public async Task<Result<ResponseEncounter>> Handle(GetEncounterToFillCommand request,CancellationToken cancellationToken)
        {
            try
            {
              

                    if (request.EmrMode.ToString().ToLower() == "poc")
                    {
                        var prepformview = await _prepUnitOfWork.Repository<PrepFormsView>().Get(x => x.PatientId == request.PatientId && x.VisitDate < request.VisitDate).OrderByDescending(x => x.EncounterId).ToListAsync();

                        if (prepformview.Count > 0)
                        {
                            prepform = prepformview[0];
                            if (prepformview[0].Form == "Initiation")
                            {
                                form = "Followup";

                                DateTime visitdate = Convert.ToDateTime(prepformview[0].VisitDate);
                                if (visitdate != null)
                                {
                                    DateTime newdate = visitdate.AddMonths(1);
                                    if (request.VisitDate > prepformview[0].VisitDate && request.VisitDate <= newdate.AddDays(7))
                                    {
                                        form = "Followup";
                                    }

                                    DateTime refilldate = visitdate.AddMonths(2);
                                    if (request.VisitDate > visitdate.AddMonths(1) && request.VisitDate <= refilldate.AddDays(7))
                                    {
                                        form = "MonthlyRefill";
                                    }
                                }

                            }

                            else if (prepformview[0].Form == "PrepEncounter")
                            {

                                if (prepformview[0].PrepStatusToday.ToLower() == "start")
                                {

                                    DateTime visitdate = Convert.ToDateTime(prepformview[0].VisitDate);

                                    DateTime newdate = visitdate.AddMonths(1);
                                    if (request.VisitDate > prepformview[0].VisitDate && request.VisitDate <= newdate.AddDays(7))
                                    {
                                        form = "MonthlyRefill";
                                    }


                                    DateTime newdatefollow = visitdate.AddMonths(2);
                                    if (request.VisitDate > visitdate.AddMonths(1) && request.VisitDate <= newdatefollow.AddDays(7))
                                    {
                                        form = "Followup";
                                    }



                                    DateTime newdaterefill = visitdate.AddMonths(3);
                                    if (request.VisitDate > visitdate.AddMonths(2) && request.VisitDate <= newdaterefill.AddDays(7))
                                    {
                                        form = "MonthlyRefill";
                                    }


                                    DateTime newdaterefill4 = visitdate.AddMonths(4);
                                    if (request.VisitDate > visitdate.AddMonths(3) && request.VisitDate <= newdaterefill4.AddDays(7))
                                    {
                                        form = "MonthlyRefill";
                                    }
                                    if(string.IsNullOrEmpty(form) == true )
                                     {
                                    form = "MonthlyRefill";
                                     }


                                }
                                else if (prepformview[0].PrepStatusToday.ToLower() == "restart")
                                {

                                    DateTime visitdate = Convert.ToDateTime(prepformview[0].VisitDate);

                                    DateTime newdate = visitdate.AddMonths(1);
                                    if (request.VisitDate > prepformview[0].VisitDate && request.VisitDate <= newdate.AddDays(7))
                                    {
                                        form = "Followup";
                                    }


                                    DateTime newdaterefill = visitdate.AddMonths(2);
                                    if (request.VisitDate > visitdate.AddMonths(1) && request.VisitDate <= newdaterefill.AddDays(7))
                                    {
                                        form = "MonthlyRefill";
                                    }



                                    DateTime newdatefollow = visitdate.AddMonths(3);
                                    if (request.VisitDate > visitdate.AddMonths(2) && request.VisitDate <= newdatefollow.AddDays(7))
                                    {
                                        form = "Followup";
                                    }



                                    DateTime newdaterefill2 = visitdate.AddMonths(4);
                                    if (request.VisitDate > visitdate.AddMonths(3) && request.VisitDate <= newdaterefill.AddDays(7))
                                    {
                                        form = "MonthlyRefill";
                                    }




                                    DateTime newdaterefill3 = visitdate.AddMonths(5);
                                    if (request.VisitDate > visitdate.AddMonths(4) && request.VisitDate <= newdaterefill3.AddDays(7))
                                    {
                                        form = "MonthlyRefill";
                                    }



                                    DateTime newdatefollowup2 = visitdate.AddMonths(6);
                                    if (request.VisitDate > visitdate.AddMonths(5) && request.VisitDate <= newdatefollowup2.AddDays(7))
                                    {
                                        form = "Followup";
                                    }
                                    if(String.IsNullOrEmpty(form)== true)
                                     {
                                    form = "Followup";
                                     }

                                }
                                else if (prepformview[0].PrepStatusToday.ToLower() == "continue")
                                {
                                    DateTime visitdate = Convert.ToDateTime(prepformview[0].VisitDate);
                                    DateTime newdaterefill = visitdate.AddMonths(1);
                                    if (request.VisitDate > visitdate && request.VisitDate <= newdaterefill.AddDays(7))
                                    {
                                        form = "MonthlyRefill";
                                    }

                                    DateTime newdaterefill2 = visitdate.AddMonths(2);
                                    if (request.VisitDate > visitdate.AddMonths(1) && request.VisitDate <= newdaterefill2.AddDays(7))
                                    {
                                        form = "MonthlyRefill";
                                    }

                                    DateTime newdatefollowup = visitdate.AddMonths(3);
                                    if (request.VisitDate > visitdate.AddMonths(2) && request.VisitDate <= newdatefollowup.AddDays(7))
                                    {
                                        form = "Followup";
                                    }

                                    if(String.IsNullOrEmpty(form)== true)
                                    {
                                    form = "MonthlyRefill";
                                    }
                                }
                            }

                            else if (prepformview[0].Form == "MonthlyRefill")
                            {
                                List<PrepFormsView> itemsafterremove = new List<PrepFormsView>();

                                itemsafterremove = prepformview.FindAll(x => x != prepformview[0] && x.Form == "MonthlyRefill" || x.Form == "PrepEncounter").OrderByDescending(x => x.EncounterId).ToList();

                                if (itemsafterremove.Count > 0)
                                {
                                    if (itemsafterremove[1].Form == "MonthlyRefill")
                                    {
                                        DateTime visitdate = Convert.ToDateTime(prepformview[0].VisitDate);
                                        DateTime newdatefollowup = visitdate.AddMonths(1);
                                        if (request.VisitDate > visitdate && request.VisitDate <= newdatefollowup.AddDays(7))
                                        {
                                            form = "Followup";
                                        }

                                    }
                                    else if (itemsafterremove[1].Form == "PrepEncounter")
                                    {
                                        DateTime visitdate = Convert.ToDateTime(prepformview[0].VisitDate);
                                        DateTime newdaterefill = visitdate.AddMonths(1);
                                        if (request.VisitDate > visitdate && request.VisitDate <= newdaterefill.AddDays(7))
                                        {
                                            form = "MonthlyRefill";
                                        }

                                        DateTime newdaterefill2 = visitdate.AddMonths(2);
                                        if (request.VisitDate > visitdate.AddMonths(1) && request.VisitDate <= newdaterefill2.AddDays(7))
                                        {
                                            form = "MonthlyRefill";
                                        }



                                        DateTime newdatefollowup = visitdate.AddMonths(3);
                                        if (request.VisitDate > visitdate.AddMonths(2) && request.VisitDate <= newdatefollowup.AddDays(7))
                                        {
                                            form = "Followup";
                                        }

                                    }
                                }



                                if (prepformview[1].Form == "MonthlyRefill")
                                {
                                    DateTime visitdate = Convert.ToDateTime(prepformview[0].VisitDate);
                                    DateTime newdatefollowup = visitdate.AddMonths(1);
                                    if (request.VisitDate > visitdate && request.VisitDate <= newdatefollowup.AddDays(7))
                                    {
                                        form = "Followup";
                                    }

                                }
                                else if (prepformview[1].Form == "PrepEncounter")
                                {
                                    DateTime visitdate = Convert.ToDateTime(prepformview[0].VisitDate);
                                    DateTime newdaterefill = visitdate.AddMonths(1);
                                    if (request.VisitDate > visitdate && request.VisitDate <= newdaterefill.AddDays(7))
                                    {
                                        form = "MonthlyRefill";
                                    }

                                    DateTime newdaterefill2 = visitdate.AddMonths(2);
                                    if (request.VisitDate > visitdate.AddMonths(1) && request.VisitDate <= newdaterefill2.AddDays(7))
                                    {
                                        form = "MonthlyRefill";
                                    }



                                    DateTime newdatefollowup = visitdate.AddMonths(3);
                                    if (request.VisitDate > visitdate.AddMonths(2) && request.VisitDate <= newdatefollowup.AddDays(7))
                                    {
                                        form = "Followup";
                                    }

                                }
                            }
                        }
                    }
                
           

                return Result<ResponseEncounter>.Valid(new ResponseEncounter
                {
                    PrepFormsView = prepform,
                    EncounterType = form
                });

            }
            catch (Exception ex)
            {
                return Result<ResponseEncounter>.Invalid(ex.Message);

            }
        }
    }
}
