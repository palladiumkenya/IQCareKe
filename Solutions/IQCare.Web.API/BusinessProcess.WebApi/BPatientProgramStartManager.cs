using System;
using DataAccess.Base;
using DataAccess.WebApi;
using DataAccess.WebApi.Repository;
using Entities.CCC.PSmart;
using Interface.WebApi;

namespace BusinessProcess.WebApi
{
    public class BPatientProgramStartManager :ProcessBase,IPatientProgramStartManager
    {
        private int _result = 0;

        public int AddPatientProgramStart(PatientProgramStart patientProgramStart)
        {
            try
            {
                using (UnitOfWork unitOfWork = new UnitOfWork(new PsmartContext()))
                {
                    unitOfWork.PatientProgramStartRepository.Add(patientProgramStart);
                    _result = unitOfWork.Complete();
                    unitOfWork.Dispose();
                    return patientProgramStart.Ptn_pk;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public int EditPatientProgramStart(PatientProgramStart patientProgramStart)
        {
            try
            {
                using (UnitOfWork unitOfWork = new UnitOfWork(new PsmartContext()))
                {
                    unitOfWork.PatientProgramStartRepository.Update(patientProgramStart);
                    _result = unitOfWork.Complete();
                    unitOfWork.Dispose();
                    return _result;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}