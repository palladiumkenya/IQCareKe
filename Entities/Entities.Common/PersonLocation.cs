using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Common
{
    [Serializable]
    public  class PersonLocation:IAuditEntity
    {
        public int Id { get; set; }
        [ForeignKey("Person")]
        public int PersonId { get; set; }
        public virtual Person Person { get; set; }
        public int County { get; set; }
        public int? SubCounty { get; set; }
        public int? Ward { get; set; }
        public string Village { get; set; }
        public string Estate { get; set; }
        public string LandMark { get; set; }
        public string NearestHealthCentre { get; set; }

        public int CreatedBy
        {
            get; set;
        }

        public DateTime CreateDate
        {
            get; set;
        }

        public bool DeleteFlag
        {
            get; set;
        }

        public string AuditData
        {
            get; set;
        }
    }
}
