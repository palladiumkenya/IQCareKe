using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using Common.Core.Model;

namespace VisitManagement.Core.Model
{
    [System.Data.Linq.Mapping.Table(Name = "PatientMasterVisit")]

    public class PatientMasterVisit:BaseEntity
    {
        public int PatientId { get; set; }
        public int serviceId { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public Boolean status { get; set; }
    }
}
