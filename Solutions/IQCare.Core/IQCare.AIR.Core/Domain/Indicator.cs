using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.AIR.Core.Domain
{
    public class Indicator
    {
        public Indicator()
        {
            
        }

        public int Id { get; private set; }
        public string Code { get; private set; }
        public string Name { get; private set; }
        public int DataTypeId { get; private set; }
        public int ReportSubSectionId { get; private set; }
        public DateTime DateCreated { get; private set; }
        public DateTime? DateUpdated { get; private set; }
        public int CreatedBy { get; private set; }
        public virtual  IndicatorResultDataType DataType { get; set; }
        public virtual ReportSubSection ReportSubSection { get; set; }
        public virtual ICollection<IndicatorResult> IndicatorResults { get; set; }
    }
}
