using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities.Common
{
    [Serializable]
    public class BaseEntity : IAuditEntity
    {
        
        public string AuditData { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreatedBy { get; set; }
        public bool DeleteFlag { get; set; }
        protected BaseEntity()
        {
            CreateDate = Convert.ToDateTime(DateTime.Now.ToString("u"));
            CreatedBy = 1;
        }
        
    }
}
