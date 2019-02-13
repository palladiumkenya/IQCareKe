using DataAccess.Base;
using DataAccess.Context;
using Entities.Common;
using Entities.Records;
using Entities.Records.Consent;
using Entities.Records.Enrollment;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataAccess.Records.Context
{
   public class RecordContext :BaseContext
    { 
        public RecordContext() :base((DbConnection)DataMgr.GetConnection(),true)
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
            Database.SetInitializer<RecordContext>(null);
        }


        public DbSet<PatientEntity> Patients { get; set; }
        public DbSet<PersonContact> PatientContacts { get; set; }

        public DbSet<PatientEntryPoint> PatientEntryPoint { get; set; }

        public DbSet<PatientMasterVisit> PatientMasterVisit { get; set; }

        public DbSet<Entities.Records.PatientVisit>  PatientVisit { get; set; }

        public DbSet<PatientEntityEnrollment> PatientEntityEnrollments { get; set; }

        public DbSet<PatientEntityIdentifier> PatientIdentifiers { get; set; }

        public DbSet<ServiceAreaIdentifiers> ServiceAreaIdentifiers { get; set;}

        public DbSet<PersonIdentifier> PersonIdentifiers { get; set; }


        public DbSet<Identifier> Identifiers { get; set; }

        public DbSet<ServiceArea> ServiceAreas { get; set; }

        public DbSet<PatientEncounter>  PatientEncounters { get; set; }

        public DbSet<PersonEmergencyContact> PersonEmergencyContacts { get; set; }

        public DbSet<PersonEducation> PersonEducations { get; }

        public DbSet<PersonOccupation> PersonOccupations { get; }


        public DbSet<PatientConsent> PatientConsents { get; }

        
        

    }
}
