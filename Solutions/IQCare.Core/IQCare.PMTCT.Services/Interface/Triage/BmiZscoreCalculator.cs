using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.PMTCT.Services.Interface.Triage
{
    public class BmiZscoreCalculator : LmsZscoreAbstractCalculator, IZscoreCalculator
    {
        public bool IsValidScoreType(ZscoreType scoreType)
        {
            return scoreType == ZscoreType.Bmiz;
        }

        public double CalculateZscore(ZScoreCalculationInfo calculationInfo)
        {
            if (!IsValidScoreType(calculationInfo.ZscoreType))
                   return 0;

            var bmi = Math.Round(calculationInfo.Weight / Math.Pow((calculationInfo.Height / 100), 2), 2);

            return CalculateZscore(bmi, calculationInfo.LmsParameter.Median,
                calculationInfo.LmsParameter.Lambda, calculationInfo.LmsParameter.Sigma);
        }
    }
}
