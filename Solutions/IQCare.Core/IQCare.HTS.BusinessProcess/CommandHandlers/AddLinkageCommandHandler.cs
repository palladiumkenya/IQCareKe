using System;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.Core.Models;
using IQCare.HTS.BusinessProcess.Commands;
using IQCare.HTS.Core.Model;
using IQCare.HTS.Infrastructure;
using MediatR;
using Serilog;

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
            try
            {
                int patientLinkageId = 0;

                if (request.IsEdit)
                {
                    var patientLinkage = await _unitOfWork.Repository<PatientLinkage>().FindByIdAsync(request.Id);
                    if (patientLinkage != null)
                    {
                        patientLinkage.LinkageDate = request.DateEnrolled;
                        patientLinkage.CCCNumber = request.CCCNumber;
                        patientLinkage.Facility = request.Facility;
                        patientLinkage.HealthWorker = request.HealthWorker;
                        patientLinkage.Cadre = request.Carde;

                        _unitOfWork.Repository<PatientLinkage>().Update(patientLinkage);
                        await _unitOfWork.SaveAsync();

                        patientLinkageId = patientLinkage.Id;
                    }
                }
                else
                {
                    PatientLinkage patientLinkage = new PatientLinkage()
                    {
                        PersonId = request.PersonId,
                        LinkageDate = request.DateEnrolled,
                        CCCNumber = request.CCCNumber,
                        Facility = request.Facility,
                        Enrolled = true,
                        DeleteFlag = false,
                        CreatedBy = request.UserId,
                        CreateDate = DateTime.Now,
                        HealthWorker = request.HealthWorker,
                        Cadre = request.Carde
                    };

                    await _unitOfWork.Repository<PatientLinkage>().AddAsync(patientLinkage);
                    await _unitOfWork.SaveAsync();

                    patientLinkageId = patientLinkage.Id;
                }

                _unitOfWork.Dispose();

                return Result<AddLinkageResponse>.Valid(new AddLinkageResponse
                {
                    LinkageId = patientLinkageId
                });
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                return Result<AddLinkageResponse>.Invalid(e.Message);
            }
        }
    }
}