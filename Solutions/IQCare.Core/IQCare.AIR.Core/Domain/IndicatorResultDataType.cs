using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.AIR.Core.Domain
{
    public class IndicatorResultDataType
    {
        public IndicatorResultDataType()
        {
            
        }

        public int Id { get; private set; }
        public string Type { get; private set; }
        public DateTime DateCreated { get; private set; }
        public int CreatedBy { get; private set; }
        public bool DeleteFlag { get; private set; }
        
    }
}
