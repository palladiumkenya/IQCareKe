using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.Base;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using Entities.CCC.Encounter;
using Interface.CCC.Encounter;

namespace BusinessProcess.CCC
{
    public class BPatientCareEnding : ProcessBase, IPatientCareEnding
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext());
        public int AddPatientCareEnding(PatientCareEnding patientCareEnding)
        {
            _unitOfWork.PatientCareEndingRepository.Add(patientCareEnding);
            _unitOfWork.Complete();
            return patientCareEnding.Id;
        }

        public List<PatientCareEnding> GetPatientCareEndings(int patientId)
        {
            return _unitOfWork.PatientCareEndingRepository.FindBy(x => x.PatientId == patientId && !x.Active).ToList();
        }
    }
}
