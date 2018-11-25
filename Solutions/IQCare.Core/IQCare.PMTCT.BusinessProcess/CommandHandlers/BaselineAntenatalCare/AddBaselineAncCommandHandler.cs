using System;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Library;
using IQCare.PMTCT.BusinessProcess.Commands.BaselineANC;
using IQCare.PMTCT.Infrastructure;
using MediatR;
using Serilog;

namespace IQCare.PMTCT.BusinessProcess.CommandHandlers.BaselineAntenatalCare
{
    public class AddBaselineAncCommandHandler: IRequestHandler<AddBaselineAntenatalCareCommand, Result<Core.Models.BaselineAntenatalCare>>
    {
        private readonly IPmtctUnitOfWork _unitOfWork;

        public AddBaselineAncCommandHandler(IPmtctUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentException(nameof(unitOfWork));
        }


        public async Task<Result<Core.Models.BaselineAntenatalCare>> Handle(AddBaselineAntenatalCareCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    Core.Models.BaselineAntenatalCare baselineAntenatalCare = new Core.Models.BaselineAntenatalCare()
                    {
                        Id = 0,
                        PatientId = request.BaselineAntenatalCare.PatientId,
                        PatientMasterVisitId = request.BaselineAntenatalCare.PatientMasterVisitId,
                        BreastExamDone = request.BaselineAntenatalCare.BreastExamDone,
                        TreatedForSyphilis = request.BaselineAntenatalCare.TreatedForSyphilis,
                        CreateDate = DateTime.Now,
                        CreatedBy = request.BaselineAntenatalCare.CreatedBy,
                        DeleteFlag = false,
                        HivStatusBeforeAnc = request.BaselineAntenatalCare.HivStatusBeforeAnc
                    };

                    await _unitOfWork.Repository<Core.Models.BaselineAntenatalCare>().AddAsync(baselineAntenatalCare);
                    await _unitOfWork.SaveAsync();
                    
                    return Result<Core.Models.BaselineAntenatalCare>.Valid(baselineAntenatalCare);

                }
                catch (Exception e)
                {
                    Log.Error(e.Message);
                    return Result<Core.Models.BaselineAntenatalCare>.Invalid(e.Message);
                }
            }
        }
    }
}