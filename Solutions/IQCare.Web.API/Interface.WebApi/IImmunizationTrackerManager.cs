using Entities.CCC.PSmart;
using System;

namespace Interface.WebApi
{
    public interface IImmunizationTrackerManager
    {
        int AddImmunizationTracker(DateTime dateAdministered, string antigenAdministered, int personId, int ptnPk);
        int EditImmunizationTracker(ImmunizationTracker immunizationTracker);


        
    }
}