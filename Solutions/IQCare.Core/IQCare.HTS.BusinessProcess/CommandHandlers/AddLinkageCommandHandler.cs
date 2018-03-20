using System;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.Core.Models;
using IQCare.HTS.BusinessProcess.Commands;
using IQCare.HTS.Core.Model;
using IQCare.HTS.Infrastructure;
using MediatR;

namespace IQCare.HTS.BusinessProcess.CommandHandlers
{
    public class AddLinkageCommandHandler : IRequestHandler<AddLinkageCommand, Result<AddLinkageResponse>>
    {
        private readonly IHTSUnitOfWork _unitOfWork;

        public AddLinkageCommandHandler(IHTSUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<AddLinkageResponse>> Handle(AddLinkageCommand request, CancellationToken cancellationToken)
        {
            PatientLinkage patientLinkage = new PatientLinkage()
            {
                PersonId = request.PersonId,
                LinkageDate = request.DateEnrolled,
                CCCNumber = request.CCCNumber,
                Facility = request.FacilityId,
                Enrolled = true,
                DeleteFlag = false,
                CreatedBy = request.UserId,
                CreateDate = DateTime.Now
            };

            await _unitOfWork.Repository<PatientLinkage>().AddAsync(patientLinkage);
            await _unitOfWork.SaveAsync();

            _unitOfWork.Dispose();

            return Result<AddLinkageResponse>.Valid(new AddLinkageResponse
            {
                LinkageId = patientLinkage.Id
            });
        }
    }
}