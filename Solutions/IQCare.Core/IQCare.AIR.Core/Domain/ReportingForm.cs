using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.AIR.Core.Domain
{

    public class ReportingForm
    {
        public ReportingForm()
        {
            
        }

        public ReportingForm(string name, int createdBy)
        {
            Name = name;
            CreatedBy = createdBy;
            DateCreated = DateTime.Now;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public DateTime DateCreated { get; private set; }
        public int CreatedBy { get; private set; }
        public bool DeleteFlag { get; private set; }
        public virtual ICollection<ReportSection> ReportSections { get; set; }

    }
}
