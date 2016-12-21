using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Core.Model;

namespace VisitManagement.Core.Model
{
    public class PatientEncounter :BaseEntity
    {
        public int PatientId { get; set; }
        public int ptn_pk { get; set; }
        public int PatientMasterVisitId { get; set; }
        public DateTime EncounterStarTime { get; set; }
        public DateTime EncounterEndTime { get; set; }
        public int ServiceId { get; set; }
    }
}
