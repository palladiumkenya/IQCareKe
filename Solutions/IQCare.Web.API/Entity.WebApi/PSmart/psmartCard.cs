using System;

namespace Entity.WebApi.PSmart
{
    [Serializable]
    public class psmartCard
    {
        public string CARD_SERIAL_NO { get; set; }
        public int PATIENTID { get; set; }
    }
}
