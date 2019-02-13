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

        public int Id { get; private set; }
        public string Name { get; private set; }
        public DateTime DateCreated { get; private set; }
        public int CreatedBy { get; private set; }
        public bool DeleteFlag { get; private set; }


    }
}
