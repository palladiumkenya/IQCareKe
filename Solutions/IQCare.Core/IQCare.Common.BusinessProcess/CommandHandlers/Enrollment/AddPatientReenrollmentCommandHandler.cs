using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Common.BusinessProcess.Services;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.Library;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands.Enrollment;
using System.Linq;

namespace IQCare.Common.BusinessProcess.CommandHandlers.Enrollment
{
    public class AddPatientReenrollmentCommandHandler  : IRequestHandler<AddPatientReenrollmentCommand ,Result<AddPatientReenrollmentResponse>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        private int idnumber { get; set; }

        private string message { get; set; }

        public AddPatientReenrollmentCommandHandler (ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<AddPatientReenrollmentResponse>> Handle(AddPatientReenrollmentCommand request ,CancellationToken cancellationToken)
        {
            try
            {
                var patientcareendings = await _unitOfWork.Repository<PatientCareEnding>().Get(x => x.PatientId == request.PatientId && !x.Active).ToListAsync();

                if (patientcareendings.Count > 0)
                {
                    foreach (var careend in patientcareendings)
                    {
                        careend.DeleteFlag = true;
                        careend.Active = true;

                        _unitOfWork.Repository<PatientCareEnding>().Update(careend);
                        await _unitOfWork.SaveAsync();
                    }

                }

                PatientReenrollment pr = new PatientReenrollment();
                pr.CreateDate = DateTime.Now;
                pr.CreatedBy = request.UserId;
                pr.DeleteFlag = false;
                pr.PatientId = request.PatientId;
                pr.ReenrollmentDate = request.EnrollmentDate;
                await _unitOfWork.Repository<PatientReenrollment>().AddAsync(pr);
                await _unitOfWork.SaveAsync();

                if (pr.Id > 0)
                {
                    idnumber = pr.Id;

                }

                var patientenrollment = await _unitOfWork.Repository<PatientEnrollment>().Get(x => x.PatientId == request.PatientId && x.ServiceAreaId == request.ServiceAreaId).OrderByDescending(x => x.Id).FirstOrDefaultAsync();
                if (patientenrollment != null)
                {
                    patientenrollment.CareEnded = false;

                    _unitOfWork.Repository<PatientEnrollment>().Update(patientenrollment);
                    await _unitOfWork.SaveAsync();
                    message += "Patient Successfully reenrolled";

                }

                return Result<AddPatientReenrollmentResponse>.Valid(new AddPatientReenrollmentResponse()
                {
                    Id = idnumber,
                    Message = message

                });
            }
            catch(Exception ex)
            {
                return Result<AddPatientReenrollmentResponse>.Invalid(ex.Message);
            }

        }
    }
}
