using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.PMTCT.Services.Interface.Triage
{

    public enum ZscoreType
    {
        WeightForAge = 1,
        WeightForHeight,
        Bmiz,
        HeightForAge
    }

    public class ZScoreCalculationInfo
    {
        public ZscoreType ZscoreType { get; set; }
        public LmsParameter LmsParameter { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
    }


    public interface IZscoreCalculator
    {
        bool IsValidScoreType(ZscoreType scoreType);
        double CalculateZscore(ZScoreCalculationInfo calculationInfo);
    }

}
