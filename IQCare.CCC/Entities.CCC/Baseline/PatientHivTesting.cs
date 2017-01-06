using System;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Common;

namespace Entities.CCC.Baseline
{
    [Serializable]
    [Table("HIVTesting")]

    public class PatientHivTesting:BaseObject
    {
        public int PersonId { get; set; }
        public int BaselineResult { get; set; }
        public DateTime BaselineDate { get; set; }
        public DateTime TestingDate { get; set; }
        public bool ReferredToCare { get; set; }
        public string CccNumber { get; set; }
        public int EnrollmentId { get; set; }
    }
}
