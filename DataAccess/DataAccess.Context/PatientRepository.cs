using System;

using Entities.PatientCore;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
namespace DataAccess.Context
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.MarshalByRefObject" />
    public class PatientRepository : MarshalByRefObject
    {
        /// <summary>
        /// The context
        /// </summary>
        internal PatientContext context;
        internal PatientHomePageContext patientHomeContext;
        /// <summary>
        /// Initializes a new instance of the <see cref="PatientRepository"/> class.
        /// </summary>
        public PatientRepository()
        {
            context = new PatientContext();
            patientHomeContext = new PatientHomePageContext();
        }
        IEnumerable<PatientAlert> GetPatientAlerts(int moduleId)
        {
            patientHomeContext.Configuration.ProxyCreationEnabled = false;
            var alerts = patientHomeContext.PatientAlert
                .Include(c => c.Query).Where(q=> q.Query.DeleteFlag == false)
                .Include(pa => pa.ServiceArea)
                .Where(c => c.ServiceAreaId == moduleId).OrderBy(v1 => v1.OrdRank);
            return alerts;

        }
        public virtual List<PatientAlert> FetchPatientAlerts(int moduleId)
        {
            return GetPatientAlerts(moduleId).ToList(); ;
           
        }
        public virtual PatientAlert GetAlert(int moduleId, int queryId)
        {
            return GetPatientAlerts(moduleId).Where(q => q.QueryId == queryId).FirstOrDefault();
        }
        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public virtual Patient Get(object id)
        {
            return context.Set<Patient>().Find(id);
        }

        //public override IQueryable<PatientVisit> Filter(System.Linq.Expressions.Expression<Func<PatientVisit, bool>> filter)
       
        //    return context.PatientVisit.Where(filter);
        //}
        public virtual List<PatientVisit> GetAllPatientVisits(int patientId)
        {
            var visits = context.Set<PatientVisit>().Where(v => v.PatientId == patientId).OrderByDescending(v1 => v1.VisitDate);
            return visits.ToList();
        }
        public virtual PatientVisit GetRecentPatientVisit(int patientId)
        {
            var visits = context.Set<PatientVisit>().OrderByDescending(v1 => v1.VisitDate).FirstOrDefault(v => v.PatientId == patientId);
            return visits;
        }
    }
}
