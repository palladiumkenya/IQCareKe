using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities.Pharmacy;

namespace Interface.Pharmacy
{
    public interface IPharmacyDispense
    {
        string DispensePrescription(int prescription, Dispense dispense);
    }
}
