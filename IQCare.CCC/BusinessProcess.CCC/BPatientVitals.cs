using DataAccess.Base;
using Entities.CCC.Triage;
using Interface.CCC;
using System.Collections.Generic;
using System.Linq;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;

namespace BusinessProcess.CCC
{
    public class BPatientVitals : ProcessBase, IPatientVitals
    {
       // private readonly UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext());
        private int _result;
        public int AddPatientVitals(PatientVital p)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {

                unitOfWork.PatientVitalsRepository.Add(p);
                _result = unitOfWork.Complete();
                unitOfWork.Dispose();
                return _result;
            }
     
        }

        public PatientVital GetPatientVitals(int patientId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var vitals =
                            unitOfWork.PatientVitalsRepository.FindBy(x => x.PatientId == patientId)
                                .OrderBy(x => x.Id)
                                .FirstOrDefault();
                unitOfWork.Dispose();
                return vitals;
            }

        }

        public void DeletePatientVitals(int id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                PatientVital vital = unitOfWork.PatientVitalsRepository.GetById(id);
                unitOfWork.PatientVitalsRepository.Remove(vital);
                unitOfWork.Complete();
                unitOfWork.Dispose();
            }
    
        }

        public int UpdatePatientVitals(PatientVital p)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var vitals = new PatientVital()
                {
                    Temperature = p.Temperature,
                    RespiratoryRate = p.RespiratoryRate,
                    HeartRate = p.HeartRate,
                    BpSystolic = p.BpSystolic,
                    Bpdiastolic = p.Bpdiastolic,
                    Height = p.Height,
                    Weight = p.Weight,
                    Muac = p.Muac,
                    SpO2 = p.SpO2,
                    BMIZ = p.BMIZ,
                    WeightForAge = p.WeightForAge,
                    WeightForHeight = p.WeightForHeight,
                    VisitDate = p.VisitDate


                };
                unitOfWork.PatientVitalsRepository.Update(vitals);
                _result = unitOfWork.Complete();
                unitOfWork.Dispose();
                return _result;
            }

        }

        public PatientVital GetByPatientId(int patientId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                PatientVital vital =
                                        unitOfWork.PatientVitalsRepository.FindBy(x => x.PatientId == patientId & !x.DeleteFlag)
                                            .OrderByDescending(x => x.Id)
                                            .FirstOrDefault();
                unitOfWork.Dispose();
                return vital;
            }
      
        }

        public List<PatientVital> GetCurrentPatientVital(int patientId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var vitals =
                            unitOfWork.PatientVitalsRepository.FindBy(x => x.PatientId == patientId)
                                .OrderBy(x => x.Id)
                                .ToList();
                unitOfWork.Dispose();
                return vitals;
            }
        }

        public PatientVital GetPatientVitalsByMasterVisitId(int patientId, int patientMasterVisitId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var vitals =
                    unitOfWork.PatientVitalsRepository.FindBy(
                            x => x.PatientId == patientId && x.PatientMasterVisitId == patientMasterVisitId)
                        .OrderByDescending(x => x.Id)
                        .FirstOrDefault();
                unitOfWork.Dispose();
                return vitals;
            }
        }

        public PatientVital GetPatientVitalsBaseline(int patientId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var vitals =
                    unitOfWork.PatientVitalsRepository.FindBy(x => x.PatientId == patientId)
                        .OrderBy(x => x.Id)
                        .FirstOrDefault();
                unitOfWork.Dispose();
                return vitals;
            }
        }
    }
}