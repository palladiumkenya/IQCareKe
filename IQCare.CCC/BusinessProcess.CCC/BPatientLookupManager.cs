using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using DataAccess.CCC.Repository;
using Entities.CCC.Lookup;
using Interface.CCC.Lookup;
using Application.Common;
using DataAccess.Base;
using DataAccess.CCC.Context;
using DataAccess.Common;
using DataAccess.Entity;
using Entities.CCC;

namespace BusinessProcess.CCC
{
    public class BPatientLookupManager :ProcessBase, IPatientLookupmanager
    {
       // private readonly UnitOfWork _unitOfWork = new UnitOfWork(new LookupContext());
        private readonly Utility _utility = new Utility();

        public PatientLookup GetPatientDetailsLookup(int id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new LookupContext()))
            {
                var patientDetails = unitOfWork.PatientLookupRepository
                .FindBy(x => x.Id == id ).DefaultIfEmpty(null).FirstOrDefault();

                return patientDetails;
            }

        }

        public PatientLookup GetPatientByPersonId(int personId)
        {
            using (UnitOfWork u = new UnitOfWork(new LookupContext()))
            {
                return u.PatientLookupRepository.FindBy(x => x.PersonId == personId).ToList().FirstOrDefault();
            }
        }
      
        public List<PatientLookup> GetPatientSearchPayload(string isEnrolled)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new LookupContext()))
            {
                List<PatientLookup> patientLookups = new List<PatientLookup>();

                Expression<Func<PatientLookup, bool>> expresionFinal = c => c.Id > 0;

                if (!string.IsNullOrWhiteSpace(isEnrolled))
                {
                    switch (isEnrolled)
                    {
                        case "notEnrolledClients":
                            Expression<Func<PatientLookup, bool>> expressionPatientStatus =
                                c => c.PatientStatus.ToLower().Contains("not enrolled");
                            expresionFinal = PredicateBuilder.And(expresionFinal, expressionPatientStatus);
                            //Expression<Func<PatientLookup, bool>> expressionPatientMax = 
                            break;
                        default:
                            Expression<Func<PatientLookup, bool>> expressionPatientStatusEnrolled =
                                c => c.PatientStatus.ToLower().Contains("active") || c.PatientStatus.ToLower().Contains("death") || c.PatientStatus.ToLower().Contains("losttofollowup") || c.PatientStatus.ToLower().Contains("transfer out") || c.PatientStatus.ToLower().Contains("hiv negative");
                            expresionFinal = PredicateBuilder.And(expresionFinal, expressionPatientStatusEnrolled);
                            break;
                    }
                }

                patientLookups = unitOfWork.PatientLookupRepository.Filter(expresionFinal).Take(PredicateBuilder.MaxRecord).ToList();

                return patientLookups;


                //var patientSearchDetails = unitOfWork.PatientLookupRepository.FindBy(x=> x.PatientStatus =="not enrolled").ToList();

                //return patientSearchDetails;
            }

        }

        public List<PatientLookup> GetPatientSearchPayload(string patientId,string isEnrolled, string firstName, string middleName, string lastName)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new LookupContext()))
            {
                List<PatientLookup> patientLookups = new List<PatientLookup>();

                Expression<Func<PatientLookup, bool>> expresionFinal = c=>c.Id > 0;

                if (!string.IsNullOrEmpty(patientId.Trim()))
                {
                    Expression<Func<PatientLookup, bool>> expressionPatientId =
                        c => c.EnrollmentNumber.ToString().Contains(patientId.Trim().ToString());

                    expresionFinal = PredicateBuilder.And(expresionFinal, expressionPatientId);
                }

                if (!string.IsNullOrWhiteSpace(firstName))
                {
                    Expression<Func<PatientLookup, bool>> expressionFirstName =
                        c => c.FirstName.ToLower().Contains(firstName.ToLower());

                    expresionFinal = PredicateBuilder.And(expresionFinal, expressionFirstName);
                }

                if (!string.IsNullOrWhiteSpace(middleName))
                {
                    Expression<Func<PatientLookup, bool>> expressionMiddleName =
                        c => c.MiddleName.ToLower().Contains(middleName.ToLower());

                    expresionFinal = PredicateBuilder.And(expresionFinal, expressionMiddleName);
                }

                if (!string.IsNullOrWhiteSpace(lastName))
                {
                    Expression<Func<PatientLookup, bool>> expressionLastName =
                        c => c.LastName.ToLower().Contains(lastName.ToLower());

                    expresionFinal = PredicateBuilder.And(expresionFinal, expressionLastName);
                }

                if (!string.IsNullOrWhiteSpace(isEnrolled))
                {
                    switch (isEnrolled)
                    {
                        case "notEnrolledClients":
                            Expression<Func<PatientLookup, bool>> expressionPatientStatus =
                                c => c.PatientStatus.ToLower().Contains("not enrolled");
                            expresionFinal = PredicateBuilder.And(expresionFinal, expressionPatientStatus);
                            break;
                        default:
                            Expression<Func<PatientLookup, bool>> expressionPatientStatusEnrolled =
                                c => c.PatientStatus.ToLower().Contains("active");
                            expresionFinal = PredicateBuilder.And(expresionFinal, expressionPatientStatusEnrolled);
                            break;                            
                    }
                }

                patientLookups = unitOfWork.PatientLookupRepository.Filter(expresionFinal).ToList();

                return patientLookups;
            }
        }

        public List<PatientLookup> GetPatientSearchPayloadWithParameter(string patientId, string fname, string mname, string lname, DateTime doB, int sex, int facility,  int start, int length)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new LookupContext()))
            {
                var result = unitOfWork.PatientLookupRepository.GetAll();
                unitOfWork.Dispose();
                return result.ToList();
            }
        }

        public List<PatientLookup> GetPatientListByParams(int patientId, string firstName, string middleName, string lastName,
            int sex)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new LookupContext()))
            {
                List<PatientLookup> patientLookups;
                if (!System.String.IsNullOrEmpty(middleName))
                {
                    patientLookups = unitOfWork.PatientLookupRepository
                        .FindBy(x => x.FirstName.ToLower().Contains(firstName.ToLower()) &&
                                     x.MiddleName.ToLower().Contains(middleName.ToLower()) &&
                                     x.LastName.ToLower().Contains(lastName.ToLower()) && x.Id != patientId).ToList();
                }
                else
                {
                    patientLookups = unitOfWork.PatientLookupRepository
                        .FindBy(x => x.FirstName.ToLower().Contains(firstName.ToLower()) &&
                                     x.LastName.ToLower().Contains(lastName.ToLower()) && x.Id != patientId).ToList();
                }

                if (sex > 0)
                {
                    patientLookups = patientLookups.Where(y => y.Sex == sex).ToList();
                }

                return patientLookups;
            }
        }



        public int GetTotalpatientCount()
        {
            int totalCount = 0;
            ClsObject obj = new ClsObject();
            ClsUtility.Init_Hashtable();

            DataTable dt = (DataTable)obj.ReturnObject(ClsUtility.theParams, "CCC_GetPatientCount", ClsUtility.ObjectEnum.DataTable);
            if (dt != null && dt.Rows.Count > 0)
            {
                totalCount = Convert.ToInt32(dt.Rows[0]["PatientCount"]);
            }
            //using (UnitOfWork unitOfWork = new UnitOfWork(new LookupContext()))
            //{
            //    totalCount = unitOfWork.PatientLookupRepository.Count();//.GetAll().Count();

            //    unitOfWork.Dispose();

            //}
            return totalCount;

        }
        public PatientLookup GetGenderID(int patientId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new LookupContext()))
            {
                PatientLookup ptnLookup =unitOfWork.PatientLookupRepository.GetGenderId(patientId);

                unitOfWork.Dispose();

                return ptnLookup;
            }
        }

        public int GetPatientTypeId(int patientId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new LookupContext()))
            {
                int patientTypeId= unitOfWork.PatientLookupRepository.FindBy(x => x.Id == patientId)
                            .Select(x => x.PatientType)
                            .FirstOrDefault();
                unitOfWork.Dispose();

                return patientTypeId;
            }
        }

        public int GetPatientSexId(int patientId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new LookupContext()))
            {
                int sexId = unitOfWork.PatientLookupRepository.FindBy(x => x.Id == patientId).Select(y => y.Sex)
                    .FirstOrDefault();
                unitOfWork.Dispose();
                return sexId;
            }
        }

        public PatientLookup GetPatientByCccNumber(string cccNumber)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new LookupContext()))
            {
                PatientLookup patientLookup = unitOfWork.PatientLookupRepository.FindBy(x => x.EnrollmentNumber == cccNumber).FirstOrDefault();
                unitOfWork.Dispose();
                return patientLookup;
            }
        }

        public List<PatientRelationshipDTO> GetPatientRelationshipView(int patientId)
        {
            using(ViewContext context = new ViewContext())
            {
                return context.PatientRelationshipList.Where(p => p.PatientId == patientId).ToList();
            }
        }
    }
}
