using System;
using System.Collections.Generic;
using Entities.CCC.Triage;
using Interface.CCC.Triage;
using Application.Presentation;

namespace IQCare.CCC.UILogic.Triage
{
    public class PatientPregnancyManager
    {
        private IPatientPregnancyManager _PatientPregnancy = (IPatientPregnancyManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.Triage.BPatientPregnancyManager, BusinessProcess.CCC");


        public int AddPatientPregnancy(int patientId,int patientMasterVisitId,DateTime LMP,DateTime EDD,string gravidae,string parity,int outcome,DateTime dateOfOutcome,int userId)
        {
            try
            {
                var PG = new PatientPreganancy()
                {
                    PatientId = patientId,
                    PatientmasterVisitId = patientMasterVisitId,
                    LMP = LMP,
                    EDD = EDD,
                    Gravidae = gravidae,
                    parity = parity,
                    Outcome = outcome,
                    DateOfOutcome = dateOfOutcome,
                    CreatedBy=userId
                };
                return _PatientPregnancy.AddPatientPregnancy(PG);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int CheckIfPatientPregnancyExisists(int patientId)
        {
            try
            {
                return _PatientPregnancy.CheckIfPatientPregnancyExisists(patientId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int DeletePatientPregnancy(int id)
        {
            try
            {
                return _PatientPregnancy.DeletePatientPregnancy(id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<PatientPreganancy> GetPatientPregnancy(int patientId)
        {
            try
            {
                return _PatientPregnancy.GetPatientPregnancy(patientId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int UpdatePatientPreganacy(int id, DateTime LMP, DateTime EDD, string gravidae, string parity, int outcome, DateTime dateOfOutcome)
        {
            try
            {
                var PG = new PatientPreganancy()
                {
                    Id = id,
                    LMP = LMP,
                    EDD = EDD,
                    Gravidae = gravidae,
                    parity = parity,
                    Outcome = outcome,
                    DateOfOutcome = dateOfOutcome
                };
                return _PatientPregnancy.UpdatePatientPreganacy(PG);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
