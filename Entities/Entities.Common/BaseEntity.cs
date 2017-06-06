using System;
using System.Web;
using System.Xml.Serialization;

namespace Entities.Common
{
    [Serializable]
    public class BaseEntity : IAuditEntity
    {
        [XmlIgnore]
        public string AuditData { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreatedBy { get; set; }
        public bool DeleteFlag { get; set; }
        protected BaseEntity()
        {
            CreateDate = Convert.ToDateTime(DateTime.Now);
            //CreatedBy =Convert.ToInt16(HttpContext.Current.Session["AppUserId"].ToString());
            //Session["AppUserId"];
        }
        
    }
}
