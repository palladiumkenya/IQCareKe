using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.PMTCT.Services.Interface.Triage
{
    public abstract class LmsZscoreAbstractCalculator 
    {
        public double CalculateZscore(double value,double median, double lambda,double sigma)
        {
            if (Math.Abs(value) > 0 && Math.Abs(lambda) > 0)
            {
                return (Math.Pow(value / (double) Convert.ToDecimal(median),
                            lambda) - 1) / (sigma * lambda);
            }
            return Math.Log(value / median) / sigma;
        }
    }
}
