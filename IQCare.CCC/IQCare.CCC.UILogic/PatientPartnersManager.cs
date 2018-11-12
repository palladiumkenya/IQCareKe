using Application.Presentation;
using Entities.CCC.Encounter;
using Interface.CCC.Encounter;

namespace IQCare.CCC.UILogic
{
    public   class PatientPartnersManager
    {
        IPatientPartners _mgr = (IPatientPartners)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPatientPartners, BusinessProcess.CCC");

        public PatientPartner addPatientPartner(PatientPartner patientPartner)
        {
            return _mgr.addPatientPartner(patientPartner);
        }

        public  PatientPartner GetPatientPartner(int patientid,int patientMasterVisitId)
        {
            return _mgr.GetPatientPartner(patientid, patientMasterVisitId);
        }

        public PatientPartner GetPatientPartnerbyId(int entityid)
        {
            return _mgr.GetPatientPartnerbyId(entityid);
        }
        public PatientPartner UpdatePatientPartner(PatientPartner pat)
        {
            return _mgr.UpdatePatientPartner(pat);
        }
    }
}
