using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace IQCare.Maternity.Core.Domain.Maternity
{
    public class DeliveredBabyApgarScoreView
    {
        public DeliveredBabyApgarScoreView()
        {

        }
        public int Id { get; private set; }
        public int ApgarScoreId { get; private set; }
        public string ApgarScoreType { get; private set; }
        public int Score { get; private set; }
        public int DeliveredBabyBirthInformationId { get; private set; }

        public string FormatApgarScore()
        {
            string apgarScoreMinutes = Regex.Match(ApgarScoreType, @"\d+").Value; // Regex to get a digit value from an expression

            return $"{Score} in {apgarScoreMinutes}";
        }
    }
}
