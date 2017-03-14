using DataAccess.Base;
using Entities.CCC.Triage;
using Interface.CCC;
using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;

namespace BusinessProcess.CCC
{
    public class BPatientVitals : ProcessBase, IPatientVitals
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext());
        private int _result;
        public int AddPatientVitals(PatientVital p)
        {
            _unitOfWork.PatientVitalsRepository.Add(p);
            _result = _unitOfWork.Complete();
            return _result;
        }

        public PatientVital GetPatientVitals(int id)
        {
            PatientVital vital = _unitOfWork.PatientVitalsRepository.GetById(id);
            return vital;
        }

        public void DeletePatientVitals(int id)
        {
            PatientVital vital = _unitOfWork.PatientVitalsRepository.GetById(id);
            _unitOfWork.PatientVitalsRepository.Remove(vital);
            _unitOfWork.Complete();
        }

        public int UpdatePatientVitals(PatientVital p)
        {
            var vitals=new PatientVital()
            {
                Temperature = p.Temperature,
                RespiratoryRate = p.RespiratoryRate,
                HeartRate = p.HeartRate,
                BpSystolic = p.BpSystolic,
                Bpdiastolic = p.Bpdiastolic,
                Height = p.Height,
                Weight = p.Weight,
                Muac = p.Muac,
                SpO2 = p.SpO2

            };
            _unitOfWork.PatientVitalsRepository.Update(vitals);
            _result = _unitOfWork.Complete();
            return _result;
        }

        public PatientVital GetByPatientId(int patientId)
        {
            PatientVital vital = _unitOfWork.PatientVitalsRepository.GetByPatientId(patientId);
            return vital;
        }

        public List<PatientVital> GetCurrentPatientVital(int patientId)
        {
            var vitals =
                    _unitOfWork.PatientVitalsRepository.FindBy(x => x.PatientId == patientId)
                        .OrderBy(x => x.Id).Take(1)
                        .ToList()
                ;
            return vitals;
        }
    }
}