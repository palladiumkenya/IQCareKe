using DataAccess.Base;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using Entities.CCC.Encounter;
using Interface.CCC.Encounter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessProcess.CCC
{
    public class BPatientPartners : ProcessBase, IPatientPartners
    {
        public PatientPartner addPatientPartner(PatientPartner patientpartner)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                unitOfWork.PatientPartnersRepository.Add(patientpartner);
                unitOfWork.Complete();
                unitOfWork.Dispose();
                return patientpartner;
            }
        }

        public PatientPartner GetPatientPartner(int patientId, int patientMasterVisitId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var PatientPartner = unitOfWork.PatientPartnersRepository.FindBy(
                    x => x.PatientId == patientId && x.PatientMasterVisitId == patientMasterVisitId ).FirstOrDefault();
                unitOfWork.Dispose();
                return PatientPartner;
            }
        }
        public List<PatientPartner> GetPatientPartners(int patientId, int patientMasterVisitId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var OI = unitOfWork.PatientPartnersRepository.FindBy(
                    x => x.PatientId == patientId && x.PatientMasterVisitId == patientMasterVisitId).ToList();
                unitOfWork.Dispose();
                return OI.ToList();
            }
        }

        public PatientPartner GetPatientPartnerbyId(int entityId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var patientpartner = unitOfWork.PatientPartnersRepository.GetById(entityId);
                unitOfWork.Dispose();
                return patientpartner;
            }
        }

       public PatientPartner UpdatePatientPartner(PatientPartner patientpartner)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                unitOfWork.PatientPartnersRepository.Update(patientpartner);
                unitOfWork.Complete();
                unitOfWork.Dispose();
                return patientpartner;
            }
        }
    }
}

