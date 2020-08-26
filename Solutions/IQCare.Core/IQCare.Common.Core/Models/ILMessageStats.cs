using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Common.Core.Models
{
    public class ILMessageStats
    {
        public Int64 RowID { get; set; }

        public string MessageType { get; set; }

        public int Count { get; set; }

        public bool? IsSuccess { get; set; }
    }
}
