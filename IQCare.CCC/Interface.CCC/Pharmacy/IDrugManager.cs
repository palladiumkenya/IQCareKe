using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities.CCC.pharmacy;

namespace Interface.CCC.Pharmacy
{
    public interface IDrugManager
    {
        int AddDrug(Drug drug);
        int UpdateDrug(Drug drug);
        void DeleteDrug(int id);
        Drug GetDrug(int id);
        List<Drug> GetDrugsByName(string name);
    }
}
