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
                        PatientId = request.PatientId,
                        PatientMasterVisitId = request.PatientMasterVisitId,
                        BreastExamDone = request.BreastExamDone,
                        TreatedForSyphilis = request.TreatedForSyphilis,
                        CreateDate = DateTime.Now,
                        CreatedBy = request.CreatedBy,
                        DeleteFlag = false,
                        HivStatusBeforeAnc = request.HivStatusBeforeAnc,
                        TestedForSyphilis = request.TestedForSyphilis,
                        SyphilisTestUsed = request.SyphilisTestUsed,
                        SyphilisResults = request.SyphilisResults
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