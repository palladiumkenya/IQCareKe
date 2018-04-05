using System;
using System.Collections.Generic;
using Entities.CCC.psmart;

namespace Interface.WebApi
{
    public interface IImmunizationTrackerManager
    {
        int AddImmunizationTracker(DateTime dateAdministered, string antigenAdministered, int personId, int ptnPk);
        int EditImmunizationTracker(ImmunizationTracker immunizationTracker);


        
    }
}