using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.Base;
using DataAccess.CCC.Context;
using DataAccess.CCC.Interface;
using DataAccess.CCC.Repository;
using Entities.CCC.Encounter;
using Interface.CCC.Encounter;

namespace BusinessProcess.CCC
{
    public class BPatientAdherenceAssessment : ProcessBase, IPatientAdherenceAssessessment
    {
        public int AddPatientAdherenceAssessment(PatientAdherenceAssessment patientAdherenceAssessment)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                unitOfWork.PatientAdherenceAssessmentRepository.Add(patientAdherenceAssessment);
                unitOfWork.Complete();
                unitOfWork.Dispose();
                return patientAdherenceAssessment.Id;
            }
        }
    }
}
