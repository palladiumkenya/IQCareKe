using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Library;
using IQCare.PMTCT.BusinessProcess.Commands.VisitDetails;
using IQCare.PMTCT.Infrastructure;
using MediatR;
using Serilog;

namespace IQCare.PMTCT.BusinessProcess.CommandHandlers.VisitDetails
{
    public class AddVisitDetailsCommandHandler: IRequestHandler<AddVisitDetailsCommand, Result<Core.Models.VisitDetails>>
    {
        private readonly IPmtctUnitOfWork _unitOfWork;

        public AddVisitDetailsCommandHandler(IPmtctUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentException(nameof(unitOfWork));
        }

        public async Task<Result<Core.Models.VisitDetails>> Handle(AddVisitDetailsCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    Core.Models.VisitDetails visitDetails = new Core.Models.VisitDetails()
                    {
                        CreateDate = DateTime.Now,
                        CreatedBy = request.VisitDetails.CreatedBy,
                        DaysPostPartum = request.VisitDetails.DaysPostPartum,
                        DeleteFlag = false,
                        PatientId = request.VisitDetails.PatientId,
                        PatientMasterVisitId = request.VisitDetails.PatientMasterVisitId,
                        VisitNumber = request.VisitDetails.VisitNumber,
                        VisitType = request.VisitDetails.VisitType                       
                    };

                    await _unitOfWork.Repository<Core.Models.VisitDetails>().AddAsync(visitDetails);
                    await _unitOfWork.SaveAsync();
                    
                    return Result<Core.Models.VisitDetails>.Valid(visitDetails);
                }
                catch (Exception e)
                {
                    Log.Error(e.Message);
                    return Result<Core.Models.VisitDetails>.Invalid(e.Message);
                }
            }
        }
    }
}