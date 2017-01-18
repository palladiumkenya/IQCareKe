using Entities.Common;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Common
{
    [Serializable]
    public class PersonContact : IAuditEntity
    {
        public int Id { get; set; }
        [ForeignKey("Person")]
        public virtual int PersonId { get; set; }
   
        public virtual Person Person { get; set; }
        public string PostalAddress { get; set; }
        public string MobileNo { get; set; }

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
