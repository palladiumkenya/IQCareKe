using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.AIR.Core.Domain
{
    public class ReportSubSection
    {
        public ReportSubSection()
        {
                
        }

        public int Id { get; private set; }
        public int ReportSectionId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public bool Active { get; private set; }
        public DateTime DateCreated { get; private set; }
        public DateTime ? DateUpdated { get; private set; }
        public bool DeleteFlag { get; private set; }
        public int  CreatedBy { get; private set; }
        public virtual ReportSection ReportSection { get;  set; }
        public  virtual  ICollection<Indicator> Indicators { get; set; }
    }
}
