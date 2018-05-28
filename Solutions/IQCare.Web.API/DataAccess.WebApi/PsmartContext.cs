using System.Data.Common;
using System.Data.Entity;
using DataAccess.Base;
using DataAccess.Context;
using Entities.CCC.PSmart;
using Entities.PSmart;
using Entity.WebApi.PSmart;
using PatientVisit = Entities.CCC.Visit.PatientVisit;

namespace DataAccess.WebApi
{
    public class PsmartContext:BaseContext
    {
        public PsmartContext() : base((DbConnection)DataMgr.GetConnection(), true)
        {
            Configuration.ProxyCreationEnabled = false;
            // DataMgr.OpenDecryptedSession(base.Database.Connection);
            Configuration.LazyLoadingEnabled = false;
            Database.SetInitializer<PsmartContext>(null);
        }

        public DbSet<Psmart_Store> PsmartStores { get; set; }
        public DbSet<PsmartAuthUser> IQUser { get; set; }
        public DbSet<PsmartEligibleList> SmartCardPatientLists { get; set; }

        public DbSet<SHR> Shrs { get; set; }

        public DbSet<EXTERNALPATIENTID> Externalpatientids { get; set; }
        public DbSet<INTERNALPATIENTID> Internalpatientids { get; set; }
        public DbSet<PHYSICALADDRESS> Physicaladdresss { get; set; }
        public DbSet<PATIENTADDRESS> Patientaddresss { get; set; }
        public DbSet<PATIENTNAME> Patientnames { get; set; }
        public DbSet<MOTHERNAME> Mothernames { get; set; }
        public DbSet<MOTHERIDENTIFIER> Motheridentifiers { get; set; }
        public DbSet<MOTHERDETAILS> Motherdetailss { get; set; }
        public DbSet<PATIENTIDENTIFICATION> Patientidentifications { get; set; }
        public DbSet<NOKNAME> Noknames { get; set; }
        public DbSet<NEXTOFKIN> Nextofkins { get; set; }
        public DbSet<Entities.CCC.PSmart.PROVIDERDETAILS> Providerdetailss { get; set; }
        public DbSet<HIVTEST> Hivtests { get; set; }
        public DbSet<IMMUNIZATION> Immunizations { get; set; }
        public DbSet<CARDDETAILS> Carddetailss { get; set; }

        public DbSet<TransactionLog> TransactionLogs { get; set; }
        public DbSet<MstPatient>  MstPatients { get; set; }
        public DbSet<HivTestTracker>  HivTestTrackers { get; set; }
        public DbSet<FamilyInfo> FamilyInfos { get; set; }
        public DbSet<ImmunizationTracker> ImmunizationTrackers { get; set; }
        public DbSet<PatientProgramStart> PatientProgramStarts { get; set; }
        public DbSet<PatientVisit> PatientVisits { get; set; }
        public DbSet<MotherDetailsView> Motherdetails { get; set; }

    }
}