using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Pharm.Core.Models
{
    public class RegimenMap
    {
        public int RegimenMap_Pk { get; set; }

        public int Ptn_Pk { get; set; }

        public int LocationID { get; set; }

        public int Visit_pk { get; set; }

        public Int64 Drug_Pk { get; set; }

        public string RegimenType { get; set; }

        public int OrderId { get; set; }

        public int DeleteFlag { get; set; }

        public int UserId { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }


        public int RegimenId { get; set; }
    }
}
