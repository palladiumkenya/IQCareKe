using DataAccess.Base;
using Interface.CCC.Patient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using DataAccess.Common;
using DataAccess.Entity;
using Entities.CCC.Enrollment;
using Entities.CCC.Lookup;

namespace BusinessProcess.CCC.Patient
{
    public class BPatient : ProcessBase, IPatientManager
    {
        //private readonly UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext());
       // internal int Result;

        public int AddPatient(PatientEntity patient)
        {
            int patientId = 0;
            ClsObject obj = new ClsObject();
            ClsUtility.Init_Hashtable();
            ClsUtility.AddExtendedParameters("@PersonId", SqlDbType.Int, patient.PersonId);
            ClsUtility.AddExtendedParameters("@ptn_pk", SqlDbType.Int, patient.ptn_pk);
            ClsUtility.AddExtendedParameters("@PatientIndex", SqlDbType.VarChar, patient.PatientIndex);
            ClsUtility.AddExtendedParameters("@DateOfBirth", SqlDbType.DateTime, patient.DateOfBirth);
            ClsUtility.AddExtendedParameters("@NationalId", SqlDbType.VarChar, patient.NationalId);
            ClsUtility.AddExtendedParameters("@FacilityId", SqlDbType.Int, patient.FacilityId);
            ClsUtility.AddExtendedParameters("@UserId", SqlDbType.Int, patient.CreatedBy);
            ClsUtility.AddExtendedParameters("@Active", SqlDbType.Bit, patient.Active);
            ClsUtility.AddExtendedParameters("@PatientType", SqlDbType.Int, patient.PatientType);
            ClsUtility.AddExtendedParameters("@DobPrecision", SqlDbType.Bit, patient.DobPrecision);


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

        public PatientPersonViewEntity GetPatient(int id)
        {
            //using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            //{
            //    var patientInfo = unitOfWork.PatientRepository.GetById(id);
            //    unitOfWork.Dispose();
            //    return patientInfo;
            //}     

            using (GreencardContext context = new GreencardContext())
            {

                var result = context.PatientPersonViewEntities.Where(pp => pp.Id == id).FirstOrDefault();


                return result;
            }
        }


        public int UpdatePatient(PatientEntity patient, int id)
        {
            int patientId = -1;
            ClsObject obj = new ClsObject();
            ClsUtility.Init_Hashtable();

            ClsUtility.AddExtendedParameters("@ptn_pk", SqlDbType.Int, patient.ptn_pk);
            ClsUtility.AddExtendedParameters("@DateOfBirth", SqlDbType.DateTime, patient.DateOfBirth);
            ClsUtility.AddExtendedParameters("@NationalId", SqlDbType.VarChar, patient.NationalId);
            ClsUtility.AddExtendedParameters("@FacilityId", SqlDbType.Int, patient.FacilityId);
            ClsUtility.AddExtendedParameters("@AuditData", SqlDbType.Xml, patient.AuditData);
            ClsUtility.AddExtendedParameters("@Id", SqlDbType.Int, id);

            DataTable dt = (DataTable)obj.ReturnObject(ClsUtility.theParams, "Patient_Update", ClsUtility.ObjectEnum.DataTable);
            if (dt != null && dt.Rows.Count > 0)
            {
                patientId = Convert.ToInt32(dt.Rows[0]["Id"]);
            }
            return patientId;
        }

        public PatientPersonViewEntity CheckPersonEnrolled(int personId)
        {
            using (GreencardContext context = new GreencardContext())
            {
              
                var result = context.PatientPersonViewEntities.Where(pp => pp.PersonId == personId).FirstOrDefault();
                

                return result;
            }
        }
      public PatientPersonViewEntity GetPatientEntityByPersonId(int personId)
        {
            using (GreencardContext context = new GreencardContext())
            {
               // PatientEntity patientEntity = null;
                var result = context.PatientPersonViewEntities.Where(pp => pp.PersonId == personId).FirstOrDefault();
                //List<PatientPersonViewEntity> person = unitOfWork.PatientPersonViewRepository..FindBy(x => x.PersonId == persionId).ToList();
                //unitOfWork.Dispose();
                //if (result != null)
                //{
                //    patientEntity = new PatientEntity() { Id= result.Id, PersonId=result.PersonId, DateOfBirth = result.DateOfBirth, DobPrecision= result.}
                //}

                return result;
            }
            //using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            //{
            //    PatientEntity entity = unitOfWork.PatientRepository.FindBy(x => x.PersonId == persionId).FirstOrDefault();
            //    unitOfWork.Dispose();
            //    return entity;
            //}
        }
        public int GetPatientType(int patientId)
        {

            using (GreencardContext context = new GreencardContext())
            {
                // PatientEntity patientEntity = null;
                var result = context.PatientPersonViewEntities.Where(pp => pp.Id == patientId).Select(x => x.PatientType).FirstOrDefault();
               

                return result;
            }

            //using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            //{
            //    var patientTypeId = unitOfWork.PatientRepository.FindBy(x => x.Id == patientId & !x.DeleteFlag)
            //                        .Select(x => x.PatientType).FirstOrDefault();
            //    unitOfWork.Dispose();
            //    return patientTypeId;
            //    //return patientTypeId.FirstOrDefault();
            //}
        }

        public void UpdatePatientType(int PatientId, int PatientType)
        {
            ClsObject obj = new ClsObject();
            ClsUtility.Init_Hashtable();

            ClsUtility.AddExtendedParameters("@PatientId", SqlDbType.Int, PatientId);
            ClsUtility.AddExtendedParameters("@PatientType", SqlDbType.Int, PatientType);

            int i = (int)obj.ReturnObject(ClsUtility.theParams, "PatientType_Update", ClsUtility.ObjectEnum.ExecuteNonQuery);
        }

        public List<PatientRegistrationLookup> GetPatientIdByPersonId(int personId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new LookupContext()))
            {
                List<PatientRegistrationLookup> patientRegistrationLookups = new List<PatientRegistrationLookup>();

                bool patientExists = unitOfWork.PatientRegistrationLookupRepository.FindBy(x => x.PersonId == personId)
                    .Any();
                if (patientExists)
                {
                    patientRegistrationLookups = unitOfWork.PatientRegistrationLookupRepository.FindBy(x => x.PersonId == personId).ToList();
                }
                unitOfWork.Dispose();
                return patientRegistrationLookups;
            }
        }

        public int GetPersonId(int patientId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new LookupContext()))
            {
                int personId = unitOfWork.PatientRegistrationLookupRepository.FindBy(x => x.Id == patientId)
                    .Select(x=>x.PersonId)
                    .FirstOrDefault();

                return personId;
            }
        }
    }
}
