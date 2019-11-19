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
    public class PatientCheckOutCommandHandler : IRequestHandler<PatientCheckOutCommand, Result<CheckOutResponse>>
    {
        private readonly IPrepUnitOfWork _prepUnitOfWork;

        public int Id { get; set; }

        public string message { get; set; }


        public PatientCheckOutCommandHandler(IPrepUnitOfWork prepUnitOfWork)
        {
            _prepUnitOfWork = prepUnitOfWork ?? throw new ArgumentNullException(nameof(prepUnitOfWork));
        }

        public async Task<Result<CheckOutResponse>> Handle(PatientCheckOutCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Id > 0)
                {
                    var servicechecked = await _prepUnitOfWork.Repository<ServiceCheckin>().Get(x => x.Id == request.Id).FirstOrDefaultAsync();

                    if (servicechecked != null)
                    {
                        servicechecked.Status = request.Status;
                        servicechecked.End = DateTime.Now;
                        servicechecked.DeleteFlag = request.DeleteFlag;

                        _prepUnitOfWork.Repository<ServiceCheckin>().Update(servicechecked);
                        await _prepUnitOfWork.SaveAsync();
                        Id = servicechecked.Id;
                        message = "Patient Successfully checked out";

                    }
                }
                else
                {
                    var serviceChecked = await _prepUnitOfWork.Repository<ServiceCheckin>().Get(x => x.PatientId == request.PatientId
                     && x.ServiceId == request.ServiceId && x.VisitDate.ToString() == request.VisitDate.ToString()).FirstOrDefaultAsync();

                    if (serviceChecked != null)
                    {
                        serviceChecked.Status = request.Status;
                        serviceChecked.End = DateTime.Now;
                        serviceChecked.DeleteFlag = request.DeleteFlag;
                        _prepUnitOfWork.Repository<ServiceCheckin>().Update(serviceChecked);
                        await _prepUnitOfWork.SaveAsync();
                        Id = serviceChecked.Id;
                        message = "Patient Successfully checked out";


                    }
                }



                return Result<CheckOutResponse>.Valid(new CheckOutResponse()
                {
                    Id = Id,
                    Message = message
                });

            }
            catch (Exception ex)
            {
                return Result<CheckOutResponse>.Invalid(ex.Message);
            }

        }
    }
    
}
