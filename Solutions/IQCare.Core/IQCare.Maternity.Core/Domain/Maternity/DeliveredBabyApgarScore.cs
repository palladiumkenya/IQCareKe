using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Maternity.Core.Domain.Maternity
{
    public class DeliveredBabyApgarScore
    {
        public DeliveredBabyApgarScore()
        {

        }
        public DeliveredBabyApgarScore(int apgarScoreId, int birthInformationId, int score)
        {
            ApgarScoreId = apgarScoreId;
            DeliveredBabyBirthInformationId = birthInformationId;
            Score = score;
        }
        public int Id { get; private set; }
        public int ApgarScoreId { get; private set; }
        public int Score { get; set; }
        public int DeliveredBabyBirthInformationId { get; private set; }
        public virtual DeliveredBabyBirthInformation DeliveredBabyBirthInformation { get; set; }
    }
}
