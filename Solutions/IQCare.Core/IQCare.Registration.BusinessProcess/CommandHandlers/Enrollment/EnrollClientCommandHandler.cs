using System;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.Core.Models;
using IQCare.Registration.BusinessProcess.Commands.Enrollment;
using IQCare.Registration.Core.Model;
using IQCare.Registration.Infrastructure;
using MediatR;

namespace IQCare.Registration.BusinessProcess.Handlers.Enrollment
{
    public class EnrollClientCommandHandler : IRequestHandler<EnrollClientCommand, Result<EnrollClientResponse>>
    {
        private readonly IRegistrationUnitOfWork _unitOfWork;
        public EnrollClientCommandHandler(IRegistrationUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public async Task<Result<EnrollClientResponse>> Handle(EnrollClientCommand request, CancellationToken cancellationToken)
        {
            using (var trans = _unitOfWork.Context.Database.BeginTransaction())
            {
                try
                {
                    var patientEnrollment = new PatientEnrollment()
                    {
                        PatientId = request.ClientEnrollment.PatientId,
                        ServiceAreaId = request.ClientEnrollment.ServiceAreaId,
                        EnrollmentDate = request.ClientEnrollment.DateOfEnrollment,
                        EnrollmentStatusId = 0,
                        TransferIn = false,
                        CareEnded = false,
                        DeleteFlag = false,
                        CreatedBy = request.ClientEnrollment.CreatedBy,
                        CreateDate = DateTime.Now

                    };

                    await _unitOfWork.Repository<PatientEnrollment>().AddAsync(patientEnrollment);
                    await _unitOfWork.SaveAsync();

                    var patientIdentifier = new PatientIdentifier()
                    {
                        PatientId = request.ClientEnrollment.PatientId,
                        PatientEnrollmentId = patientEnrollment.Id,
                        IdentifierTypeId = 8,
                        IdentifierValue = request.ClientEnrollment.EnrollmentNo,
                        DeleteFlag = false,
                        CreatedBy = request.ClientEnrollment.CreatedBy,
                        CreateDate = DateTime.Now,
                        Active = true

                    };

                    await _unitOfWork.Repository<PatientIdentifier>().AddAsync(patientIdentifier);
                    await _unitOfWork.SaveAsync();

                    trans.Commit();

                    return Result<EnrollClientResponse>.Valid(new EnrollClientResponse
                    {
                        IdentifierValue = request.ClientEnrollment.EnrollmentNo,
                        IdentifierId = patientIdentifier.Id
                    });
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    return Result<EnrollClientResponse>.Invalid(ex.Message);
                }
            }
        }
    }
}