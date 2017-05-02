using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.Base;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using Entities.CCC.Lookup;
using Interface.CCC.Lookup;

namespace BusinessProcess.CCC.Lookup
{
    public class BPatientTreatmentSupporterLookupManager : ProcessBase, IPatientTreatmentSupporterLookupManager
    {
        public List<PatientTreatmentSupporterLookup> GetAllPatientTreatmentSupporter(int personId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new LookupContext()))
            {
                List<PatientTreatmentSupporterLookup> patientTreatmentSupporters = 
                    unitOfWork.PatientTreatmentSupporterLookupRepository.FindBy(
                        x => x.PersonId == personId & x.DeleteFlag == false)
                        .OrderByDescending(x => x.Id)
                        .ToList();

                unitOfWork.Dispose();
                return patientTreatmentSupporters;
            }
        }
    }
}
