using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.AIR.Core.Domain
{
    public class ReportSection
    {
        public ReportSection()
        {
            
        }
        

        public int Id { get; private set; }
        public int ReportingFormId { get; set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public bool Active { get; private set; }
        public DateTime DateCreated { get; private set; }
        public DateTime ? DateUpdated { get; private set; }
        public int CreatedBy { get; private set; }
        public bool DeleteFlag { get; private set; }
        public virtual ReportingForm ReportingForm { get; set; }
        public  virtual  ICollection<ReportSubSection> ReportSubSections { get; set; }

        public void UpdateSection(bool activate)
        {
            Active = activate;
            DateUpdated = DateTime.Now;
        }


    }


}
