using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands.Enrollment;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IQCare.Common.BusinessProcess.CommandHandlers.Enrollment
{
    public class EnrollClientCommandHandler : IRequestHandler<EnrollClientCommand, Result<EnrollClientResponse>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        public EnrollClientCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public async Task<Result<EnrollClientResponse>> Handle(EnrollClientCommand request, CancellationToken cancellationToken)
        {
            using (var trans = _unitOfWork.Context.Database.BeginTransaction())
            {
                try
                {
                    var enrollmentVisitType = await _unitOfWork.Repository<LookupItemView>().Get(x => x.MasterName == "VisitType" && x.ItemName == "Enrollment").FirstOrDefaultAsync();
                    int? visitType = enrollmentVisitType != null ? enrollmentVisitType.ItemId : 0;
                    var patientMasterVisit = new PatientMasterVisit()
                    {
                        PatientId = request.ClientEnrollment.PatientId,
                        ServiceId = request.ClientEnrollment.ServiceAreaId,
                        Start = DateTime.Now,
                        End = null,
                        Active = false,
                        VisitDate = DateTime.Now,
                        //VisitScheduled = 0,
                        //VisitBy = 108,
                        VisitType = visitType,
                        Status = 1,
                        CreateDate = DateTime.Now,
                        DeleteFlag = false,
                        CreatedBy = request.ClientEnrollment.CreatedBy
                    };

                    await _unitOfWork.Repository<PatientMasterVisit>().AddAsync(patientMasterVisit);
                    await _unitOfWork.SaveAsync();

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

                    _unitOfWork.Dispose();

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