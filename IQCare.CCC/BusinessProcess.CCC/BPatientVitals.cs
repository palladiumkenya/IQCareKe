using DataAccess.Base;
using Entities.CCC.Triage;
using Interface.CCC;
using System.Collections.Generic;
using System.Linq;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using DataAccess.Entity;
using DataAccess.Common;
using System.Data;
using System;

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
                return p.Id;
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

        public List<PatientVital> GetAllPatientVitals(int patientId)
        {
            lock (this)
            {
                ClsObject PatientEncounter = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@PatientId", SqlDbType.Int, patientId.ToString());

                DataTable theDT = (DataTable)PatientEncounter.ReturnObject(ClsUtility.theParams, "sp_getAllPatientVitals", ClsUtility.ObjectEnum.DataTable);

                List<PatientVital> list = new List<PatientVital>();

                for (int i = 0; i < theDT.Rows.Count; i++)
                {
                    PatientVital pv = new PatientVital();
                    pv.PatientId = Convert.ToInt32(theDT.Rows[i]["patientid"]);
                    pv.Height = Convert.ToDecimal(theDT.Rows[i]["height"]);
                    pv.Weight = Convert.ToDecimal(theDT.Rows[i]["weight"]);
                    pv.BMI = Convert.ToDecimal(theDT.Rows[i]["bmi"]);
                    pv.CreateDate = Convert.ToDateTime(theDT.Rows[i]["createdate"]);
                    
                    list.Add(pv);
                }

                return list;
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