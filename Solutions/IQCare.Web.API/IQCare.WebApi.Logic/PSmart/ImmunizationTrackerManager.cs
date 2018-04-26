using System;
using System.Collections.Generic;
using Application.Presentation;
using Entities.CCC.PSmart;
using Interface.WebApi;

namespace IQCare.WebApi.Logic.PSmart
{
    public class ImmunizationTrackerManager
    {
        private readonly IImmunizationTrackerManager immunizationTrackerManager = (IImmunizationTrackerManager)ObjectFactory.CreateInstance("BusinessProcess.WebApi.BImmunizationTrackerManager, BusinessProcess.WebApi");

        public int AddImmunizationTracker(DateTime dateAdministered, string antigenAdministered, int personId, int ptnPk)
        {
            return immunizationTrackerManager.AddImmunizationTracker(dateAdministered,antigenAdministered,personId,ptnPk);
        }

        public int EditImmunizationTracker(ImmunizationTracker immunizationTracker)
        {
            return immunizationTrackerManager.EditImmunizationTracker(immunizationTracker);
        }
    }
}