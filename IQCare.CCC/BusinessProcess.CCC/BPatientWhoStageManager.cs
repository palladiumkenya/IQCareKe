using System;
using DataAccess.Base;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using Entities.CCC.Encounter;
using Interface.CCC.Encounter;

namespace BusinessProcess.CCC
{
    public class BPatientWhoStageManager : ProcessBase, IPatientWhoStageManager
    {
        public int addPatientWhoStage(PatientWhoStage patientWhoStage)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                unitOfWork.PatientWhoStageRepository.Add(patientWhoStage);
                unitOfWork.Dispose();
                return patientWhoStage.Id;
            }
        }
    }
}
