using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.WebApi.PSmart
{
    [Serializable]
    public class psmartCard
    {
        public string CARD_SERIAL_NO { get; set; }
        public int PATIENTID { get; set; }
    }
}
