﻿using Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Entities.CCC.Enrollment
{
    [Serializable]
    [Table("Patient")]
    public class PatientEntity : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Person")]
        public int PersonId { get; set; }

        public int ptn_pk { get; set; }

        public string PatientIndex { get; set; }

        public int FacilityId { get; set; }

        public bool Active { get; set; }

        public virtual Person Person { get; set; }

    }
}
