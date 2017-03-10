using DataAccess.Base;
using Interface.CCC.Patient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Entities.PatientCore;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using DataAccess.Common;
using DataAccess.Entity;
using Entities.CCC.Enrollment;

namespace BusinessProcess.CCC.Patient
{
    public class BPatient : ProcessBase, IPatientManager
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext());
        internal int Result;

        public int AddPatient(PatientEntity patient)
        {
            int patientId = 0;
            ClsObject obj = new ClsObject();
            ClsUtility.Init_Hashtable();
            ClsUtility.AddExtendedParameters("@PersonId", SqlDbType.Int, patient.PersonId);
            ClsUtility.AddExtendedParameters("@ptn_pk", SqlDbType.Int, patient.ptn_pk);
            ClsUtility.AddExtendedParameters("@PatientIndex", SqlDbType.VarChar, patient.PatientIndex);
            ClsUtility.AddExtendedParameters("@DateOfBirth", SqlDbType.DateTime, patient.DateOfBirth);
            ClsUtility.AddExtendedParameters("@NationalId", SqlDbType.VarBinary, patient.NationalId);
            ClsUtility.AddExtendedParameters("@FacilityId", SqlDbType.Int, patient.FacilityId);
            ClsUtility.AddExtendedParameters("@UserId", SqlDbType.Int, patient.CreatedBy);
            ClsUtility.AddExtendedParameters("@Active", SqlDbType.Bit, patient.Active);


            DataTable dt = (DataTable)obj.ReturnObject(ClsUtility.theParams, "Patient_Insert", ClsUtility.ObjectEnum.DataTable);
            if (dt != null && dt.Rows.Count > 0)
            {
                patientId = Convert.ToInt32(dt.Rows[0]["Id"]);
            }

            return patientId;
            /*_unitOfWork.PatientRepository.Add(patient);
            Result = _unitOfWork.Complete();
            return patient.Id;*/
        }

        public int DeletePatient(int id)
        {
            throw new NotImplementedException();
        }

        public PatientEntity GetPatient(int id)
        {
            var patientInfo = _unitOfWork.PatientRepository.GetById(id);
            return patientInfo;
        }

        public int UpdatePatient(PatientEntity patient)
        {
            throw new NotImplementedException();
        }

        public List<PatientEntity> CheckPersonEnrolled(int persionId)
        {
            List<PatientEntity> person = _unitOfWork.PatientRepository.FindBy(x => x.PersonId == persionId).ToList();
            return person;
        }

        public int GetPatientType(int patientId)
        {
            var patientTypeId = _unitOfWork.PatientRepository.FindBy(x => x.Id == patientId & !x.DeleteFlag).Select(x => x.PatientType);  
                return patientTypeId.FirstOrDefault();          
        }
    }
}
