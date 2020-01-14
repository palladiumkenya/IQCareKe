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
  public class PatientCheckinCommandHandler :IRequestHandler<PatientCheckinCommand ,Result<CheckinOutCome>>
    {
        private readonly IPrepUnitOfWork _prepUnitOfWork;

        public int Id { get; set; }

        public string message { get; set; }


        public PatientCheckinCommandHandler(IPrepUnitOfWork prepUnitOfWork)
        {
            _prepUnitOfWork = prepUnitOfWork ?? throw new ArgumentNullException(nameof(prepUnitOfWork));
        }

        public async Task<Result<CheckinOutCome>> Handle(PatientCheckinCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var servicecheckin = await _prepUnitOfWork.Repository<ServiceCheckin>().Get(x => x.PatientId == request.PatientId && x.ServiceId == request.ServiceId && x.VisitDate == request.VisitDate).FirstOrDefaultAsync();
                if(servicecheckin != null)
                {
                    servicecheckin.Start = DateTime.Now;
                    servicecheckin.Status = request.Status;
                    servicecheckin.VisitDate = request.VisitDate;
                    servicecheckin.PatientId = request.PatientId;
                    servicecheckin.ServiceId = request.ServiceId;
                    servicecheckin.EMRType = request.EmrType;
                    servicecheckin.Active = true;
                    servicecheckin.DeleteFlag = false ;
                    servicecheckin.CreatedBy = request.UserId;

                    _prepUnitOfWork.Repository<ServiceCheckin>().Update(servicecheckin);
                    await _prepUnitOfWork.SaveAsync();
                    Id = servicecheckin.Id;
                    message += "Successfully checked In";
                }
                else
                {
                    ServiceCheckin servi = new ServiceCheckin();
                    servi.Status = request.Status;
                    servi.VisitDate = request.VisitDate;
                    servi.PatientId = request.PatientId;
                    servi.ServiceId = request.ServiceId;
                    servi.EMRType = request.EmrType;
                    servi.Active = true;
                    servi.DeleteFlag = false;
                    servi.CreatedBy = request.UserId;
                    servi.Start = DateTime.Now;
                    servi.Status = request.Status;

                    await _prepUnitOfWork.Repository<ServiceCheckin>().AddAsync(servi);
                    await _prepUnitOfWork.SaveAsync();
                    Id = servi.Id;
                    message += "Succeessfully checked in";
                    
                }

                return Result<CheckinOutCome>.Valid(new CheckinOutCome()
                {
                    ServiceCheckInId = Id,
                    Message = message
                });
            }
            catch(Exception ex)
            {
                return Result<CheckinOutCome>.Invalid(ex.Message);

            }
        }
    }
}
