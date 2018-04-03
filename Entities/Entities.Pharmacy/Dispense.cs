using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities.Pharmacy
{
    [Serializable]
    public class Dispense
    {
        public int PrescriptionItemId { get; set; }
        public virtual Prescription  Prescription {get;set;}
        public DateTime? DispenseDate { get { return Prescription.DispensedDate; } }

        public PharmacyItem ItemDispensed { get; set; }

    }
}
