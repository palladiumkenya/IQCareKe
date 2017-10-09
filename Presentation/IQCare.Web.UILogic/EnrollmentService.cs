using System;
using Entities.Administration;
using Interface.Clinical;
using Application.Presentation;
using System.Data;
using Entities.PatientCore;
using System.Collections.Generic;
using System.Linq;

namespace IQCare.Web.UILogic
{
    public class EnrollmentService
    {
        /// <summary>
        /// Gets the patient identifier.
        /// </summary>
        /// <value>
        /// The patient identifier.
        /// </value>
        public readonly int PatientId;

        public List<PatientIdentifier> Identifiers
        {
            get;
            private set;
        }

        /// <summary>
        /// Res the activate patient.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="moduleId">The module identifier.</param>
        public void ReActivatePatient(CurrentSession session, int moduleId)
        {
            if (this.PatientId == session.CurrentPatient.Id)
            {
                IPatientHome ReactivatePtnMgr;
                ReactivatePtnMgr = (IPatientHome)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientHome, BusinessProcess.Clinical");
                DataSet theDS1 = ReactivatePtnMgr.ReActivatePatient(this.PatientId, moduleId);
            }
        }

        public EnrollmentService(int patientId)
        {
            this.PatientId = patientId;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="EnrollmentService"/> class.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        public List<PatientEnrollment> GetPatientEnrollment(CurrentSession session)
        {
            IPatientRegistration PatientManager;
            PatientManager = (IPatientRegistration)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientRegistration, BusinessProcess.Clinical");
            DataTable dt = PatientManager.GetPatientServiceLines(this.PatientId, session.Facility.Id);

            string[] columnNames = { "PatientId", "LocationId", "ModuleId", "DisplayName", "EnrollmentDate", "CareStatus", "ExitReason" };

            string[] fieldNames = { "PatientId","FieldName", "IdentifierName", "IdentifierValue","FieldId"};

            DataTable dv = dt.DefaultView.ToTable(true, columnNames);

            DataTable dF = dt.DefaultView.ToTable(true, fieldNames);

            this.Identifiers = (from id in dF.AsEnumerable()
                                select new PatientIdentifier()
                                {
                                    PatientId = Convert.ToInt32(id["PatientId"]),
                                    Identifier = new Identifier()
                                    {
                                        Name = id["FieldName"].ToString(),
                                        Description = id["IdentifierName"].ToString(),
                                        Id =  Convert.ToInt32(id["FieldId"])

                                    },
                                    Value = id["IdentifierValue"].ToString()
                                }).Distinct().ToList();

            var x = (from row in dv.AsEnumerable()
                     select new PatientEnrollment()
                     {
                         PatientId = Convert.ToInt32(row["PatientId"]),
                         ServiceAreaId = Convert.ToInt32(row["ModuleId"]),
                         EnrollmentDate = Convert.ToDateTime(row["EnrollmentDate"]),
                         CareStatus = row["CareStatus"].ToString(),
                         ExitReason = row["ExitReason"].ToString(),
                         ServiceArea = session.Facility.Modules.Where(m => m.Id == Convert.ToInt32(row["ModuleId"])).DefaultIfEmpty(null).FirstOrDefault(),
                         Identifiers = (from id in dt.AsEnumerable()
                                        where id["ModuleId"].ToString() == row["ModuleId"].ToString()
                                        select new PatientIdentifier()
                                        {
                                            PatientId = Convert.ToInt32(id["PatientId"]),
                                            Identifier = new Identifier()
                                            {
                                                Name = id["FieldName"].ToString(),
                                                Description = id["IdentifierName"].ToString()

                                            },
                                            Value = id["IdentifierValue"].ToString()
                                        }).ToList()
                     });

            return x.ToList();
        }
    }
}
