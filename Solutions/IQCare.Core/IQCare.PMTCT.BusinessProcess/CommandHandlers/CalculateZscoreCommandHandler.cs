using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Library;
using IQCare.PMTCT.BusinessProcess.Commands;
using IQCare.PMTCT.Services.Interface.Triage;
using MediatR;
using Serilog;

namespace IQCare.PMTCT.BusinessProcess.CommandHandlers
{
    public class CalculateZscoreCommandHandler : IRequestHandler<CalculateZscoreCommand, Result<ZscoreCalculationResult>>
    {
        private readonly IEnumerable<IZscoreCalculator> _zscoreCalculators;
        private readonly IGetZscoreLmsParameters _zscoreLmsParametersFetcher;

        private readonly ZscoreType[] _activeZscoreTypes = {
            ZscoreType.Bmiz,
            ZscoreType.HeightForAge,
            ZscoreType.WeightForAge,
            ZscoreType.WeightForHeight
        };

        public CalculateZscoreCommandHandler(IEnumerable<IZscoreCalculator> zscoreCalculators,
            IGetZscoreLmsParameters zscoreLmsParametersFetcher)
        {
            _zscoreCalculators = zscoreCalculators;
            _zscoreLmsParametersFetcher = zscoreLmsParametersFetcher;
        }

        public Task<Result<ZscoreCalculationResult>> Handle(CalculateZscoreCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var zscoreCalculationResult = new ZscoreCalculationResult();

                var zScoreCalculationResultType = typeof(ZscoreCalculationResult);

                foreach (var scoreType in _activeZscoreTypes)
                {
                    var zscoreCalculator = _zscoreCalculators.SingleOrDefault(x => x.IsValidScoreType(scoreType));
                    if (zscoreCalculator == null)
                        continue;

                    var lmsParameter = _zscoreLmsParametersFetcher.GetLmsParameter(scoreType,
                        new SprocSearchParams
                        {
                            DateOfBirth = request.DateOfBirth,
                            Sex = request.Sex,
                            Height = (int)request.Height
                        });

                    if (lmsParameter == null)
                        continue;

                    var zscore = zscoreCalculator.CalculateZscore(new ZScoreCalculationInfo
                    {
                        Height = request.Height,
                        LmsParameter = lmsParameter,
                        Weight = request.Weight,
                        ZscoreType = scoreType
                    });

                    zScoreCalculationResultType.GetProperty(scoreType.ToString()).SetValue(zscoreCalculationResult, Math.Round(zscore,2));

                }

                return Task.FromResult(Result<ZscoreCalculationResult>.Valid(zscoreCalculationResult));
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occured while calculating zscore");
                return Task.FromResult(Result<ZscoreCalculationResult>.Invalid(ex.Message));
            }
        }
    }
}
