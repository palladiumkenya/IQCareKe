using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities.CCC.Encounter
{
    [Serializable]
    public class PatientEncounter
    {
        public string complaints { get; set; }

        [Serializable]
        public class AdverseEvents
        {
            public string adverseEvent { get; set; }
            public string medicineCausingAE { get; set; }
            public string adverseSeverity { get; set; }
            public string adverseAction { get; set; }

        }
        [Serializable]
        public class Vaccines
        {
            public string vaccine { get; set; }
            public string vaccineStage { get; set; }
            public string vaccinationDate { get; set; }
        }
        [Serializable]
        public class ChronicIlness
        {
            public string chronicIllness { get; set; }
            public string treatment { get; set; }
            public string dose { get; set; }
            public string duration { get; set; }
        }

    }
}
