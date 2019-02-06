using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.PMTCT.Services.Interface.Triage
{
    public abstract class LmsZscoreAbstractCalculator 
    {
        public double CalculateZscore(object value,object median, object lambda,object sigma)
        {
            if (Math.Abs((double)value) > 0 && Math.Abs(Convert.ToDouble(lambda)) > 0)
            {
                return (Math.Pow((double)value / (double) Convert.ToDecimal(median),
                            Convert.ToDouble(lambda)) - 1) / ((double)sigma * Convert.ToDouble(lambda));
            }
            return Math.Log((double)value / (double) median) / (double)sigma;
        }
    }
}
