using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.Base;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using Entities.CCC.Interoperability;
using Interface.CCC.Interoperability;

namespace BusinessProcess.CCC.Interoperability
{
    public class BPatientVitalsMessage : ProcessBase, IPatientVitalsMessageManager
    {
        public PatientVitalsMessage GetPatientVitalsMessageByPatientIdPatientMasterVisitId(int patientId, int patientMasterVisitId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new LookupContext()))
            {
                var patientVitalsMessage = unitOfWork.PatientVitalsMessageRepository
                    .FindBy(x => x.PatientId == patientId && x.PatientMasterVisitId == patientMasterVisitId)
                    .FirstOrDefault();
                unitOfWork.Dispose();
                return patientVitalsMessage;
            }
        }
    }
}
