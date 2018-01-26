using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.Base;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using Entities.CCC.Triage;
using Interface.CCC.Triage;

namespace BusinessProcess.CCC.Triage
{
    public class BPregnancyOutcomeLookup : ProcessBase, IPregnancyOutcomeLookupManager
    {
        public PregnancyOutcomeLookup GetLastPregnancyOutcomeLookup(int patientId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new LookupContext()))
            {
                var pregnancyOutcome = unitOfWork.PregnancyOutcomeLookupRepository.FindBy(x => x.PatientId == patientId)
                    .OrderByDescending(y => y.Id).FirstOrDefault();
                unitOfWork.Dispose();
                return pregnancyOutcome;
            }
        }
    }
}
