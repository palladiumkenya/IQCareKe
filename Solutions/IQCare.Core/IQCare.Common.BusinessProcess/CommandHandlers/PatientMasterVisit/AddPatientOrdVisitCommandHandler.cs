using System;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands.PatientMasterVisit;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.Library;
using MediatR;
using Serilog;

namespace IQCare.Common.BusinessProcess.CommandHandlers.PatientMasterVisit
{
    public class AddPatientOrdVisitCommandHandler : IRequestHandler<AddPatientOrdVisitCommand, Result<OrdVisit>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;

        public AddPatientOrdVisitCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<OrdVisit>> Handle(AddPatientOrdVisitCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    OrdVisit ordVisit = new OrdVisit()
                    {
                        Ptn_Pk = request.Ptn_Pk,
                        LocationID = request.LocationID,
                        VisitDate = request.VisitDate,
                        VisitType = 12,
                        DataQuality = 1,
                        DeleteFlag = false,
                        UserID = request.UserID,
                        CreateDate = DateTime.Now,

                    };

                    await _unitOfWork.Repository<OrdVisit>().AddAsync(ordVisit);
                    await _unitOfWork.SaveAsync();

                    return Result<OrdVisit>.Valid(ordVisit);
                }
                catch (Exception e)
                {
                    Log.Error(e.Message + " " + e.InnerException);
                    return Result<OrdVisit>.Invalid(e.Message);
                }
            }
        }
    }
}