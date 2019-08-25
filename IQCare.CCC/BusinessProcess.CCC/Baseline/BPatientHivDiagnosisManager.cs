using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DataAccess.Base;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using DataAccess.Common;
using DataAccess.Entity;
using Entities.CCC.Baseline;
using Interface.CCC.Baseline;

namespace BusinessProcess.CCC.Baseline
{
    public class BPatientHivDiagnosisManager:ProcessBase,IPatientHivDiagnosisManager
    {
        //private readonly UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext());
        internal int Result;

        public int AddPatientHivDiagnosis(PatientHivDiagnosis patientHivDiagnosis)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                unitOfWork.PatientDiagnosisHivHistoryRepository.Add(patientHivDiagnosis);
                Result = unitOfWork.Complete();
                unitOfWork.Dispose();
                return Result;
            }
        }

        public int UpdatePatientHivDiagnosis(PatientHivDiagnosis patientHivDiagnosis)
        {

            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var patientDiagnosis =unitOfWork.PatientDiagnosisHivHistoryRepository.FindBy(x => x.PatientId == patientHivDiagnosis.PatientId & !x.DeleteFlag).FirstOrDefault();
                if (patientDiagnosis != null)
                {
                    //if (patientDiagnosis.ArtInitiationDate != null)
                    //{
                        patientDiagnosis.ArtInitiationDate = patientHivDiagnosis.ArtInitiationDate;
                    //}
                    patientDiagnosis.EnrollmentDate = patientHivDiagnosis.EnrollmentDate;
                    patientDiagnosis.EnrollmentWhoStage = patientHivDiagnosis.EnrollmentWhoStage;
                    patientDiagnosis.HivDiagnosisDate = patientHivDiagnosis.HivDiagnosisDate;
                    patientDiagnosis.HistoryARTUse = patientHivDiagnosis.HistoryARTUse;
                    unitOfWork.PatientDiagnosisHivHistoryRepository.Update(patientDiagnosis);
                    Result = unitOfWork.Complete();
                }
                unitOfWork.Dispose();
                return Result;
            }
        }

        public void UpdateBlueCardBaseline(int? ptn_pk, DateTime dateOfHivDiagnosis, DateTime? artInitiationDate, DateTime? dateOfEnrollment, int locationId, int whostage)
        {
            ClsObject obj = new ClsObject();
            ClsUtility.Init_Hashtable();
            ClsUtility.AddExtendedParameters("@ptn_pk", SqlDbType.Int, ptn_pk);
            ClsUtility.AddExtendedParameters("@DateOfHivDiagnosis", SqlDbType.DateTime, dateOfHivDiagnosis);
            ClsUtility.AddExtendedParameters("@ArtInitiationDate", SqlDbType.DateTime, artInitiationDate);
            ClsUtility.AddExtendedParameters("@DateOfEnrollment", SqlDbType.DateTime, dateOfEnrollment);
            ClsUtility.AddExtendedParameters("@locationId", SqlDbType.Int, locationId);
            ClsUtility.AddExtendedParameters("@WHOStage", SqlDbType.Int, whostage);

            DataTable dt = (DataTable)obj.ReturnObject(ClsUtility.theParams, "sp_UpdateBlueCardBaselineHistory", ClsUtility.ObjectEnum.DataTable);
        }

        public int DeletePatientHivDiagnosis(int id)
        {

            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var item = unitOfWork.PatientDiagnosisHivHistoryRepository.GetById(id);
                unitOfWork.PatientDiagnosisHivHistoryRepository.Remove(item);
                Result = unitOfWork.Complete();
                unitOfWork.Dispose();
                return Result;
            }
        }

        public List<PatientHivDiagnosis> GetPatientHivDiagnosis(int patientId)
        {

            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var ptnDiagnosis = unitOfWork.PatientDiagnosisHivHistoryRepository.FindBy(x => x.PatientId == patientId).ToList();
                unitOfWork.Dispose();
                return ptnDiagnosis;
            }
        }

        public int CheckIfDiagnosisExists(int patientId)
        {

            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                int id = 0;
                var recordExists =
                    unitOfWork.PatientDiagnosisHivHistoryRepository.FindBy(
                            x => x.PatientId == patientId & !x.DeleteFlag)
                        .Select(x => x.Id).FirstOrDefault();
                recordExists = (recordExists > 1) ? id = recordExists : id = 0;
                unitOfWork.Dispose();
                return id;
            }
        }

    }
}
