﻿using Entities.CCC.Visit;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Entities.CCC.Baseline
{
    [Serializable]
    [Table("PatientDiagnosis")]
    public class PatientDiagnosis
    {
        [Key]
        public int Id { get; set; }

        public virtual int PatientId { get; set; }

        [ForeignKey("PatientId")]
        public virtual Entities.CCC.Enrollment.PatientEntity Patient { get; set; }

        public virtual int PatientMasterVisitId { get; set; }

        [ForeignKey("PatientMasterVisitId")]
        public virtual PatientMasterVisit PatientMasterVisit { get; set; }

        public int Diagnosis { get; set; }

        public string ManagementPlan { get; set; }
    }
}
