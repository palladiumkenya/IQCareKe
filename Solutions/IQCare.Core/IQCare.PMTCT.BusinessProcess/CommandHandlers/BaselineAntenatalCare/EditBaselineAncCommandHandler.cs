using System;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Library;
using IQCare.PMTCT.BusinessProcess.Commands.BaselineANC;
using IQCare.PMTCT.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IQCare.PMTCT.BusinessProcess.CommandHandlers.BaselineAntenatalCare
{
    public class EditBaselineAncCommandHandler : IRequestHandler<EditBaselineAntenatalCareCommand, Result<Core.Models.BaselineAntenatalCare>>
    {
        private readonly IPmtctUnitOfWork _unitOfWork;

        public EditBaselineAncCommandHandler(IPmtctUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentException(nameof(unitOfWork));
        }

        public async Task<Result<Core.Models.BaselineAntenatalCare>> Handle(EditBaselineAntenatalCareCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    Core.Models.BaselineAntenatalCare baselineAntenatalCare = await _unitOfWork
                        .Repository<Core.Models.BaselineAntenatalCare>().Get(x =>
                            x.PatientMasterVisitId == request.PatientMasterVisitId &&
                            x.PatientId == request.PatientId).FirstOrDefaultAsync();

                    if (baselineAntenatalCare != null)
                    {
                        baselineAntenatalCare.BreastExamDone = request.BreastExamDone;
                        baselineAntenatalCare.TreatedForSyphilis = request.TreatedForSyphilis;
                        baselineAntenatalCare.HivStatusBeforeAnc = request.HivStatusBeforeAnc;
                        baselineAntenatalCare.PregnancyId = request.PregnancyId;
                        baselineAntenatalCare.TestedForSyphilis = request.TestedForSyphilis;
                        baselineAntenatalCare.SyphilisTestUsed = request.SyphilisTestUsed;
                        baselineAntenatalCare.SyphilisResults = request.SyphilisResults;
                    }
                    
                    _unitOfWork.Repository<Core.Models.BaselineAntenatalCare>().Update(baselineAntenatalCare);
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