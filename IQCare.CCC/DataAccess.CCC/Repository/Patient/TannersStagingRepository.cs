using System;
using System.Linq;
using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.Patient;
using DataAccess.Context;
using Entities.CCC.Tanners;
using System.Collections.Generic;

namespace DataAccess.CCC.Repository.Patient
{
    public class TannersStagingRepository:BaseRepository<PatientTannersStaging>, ITannersStagingRepository
    {
        private readonly GreencardContext _context;

        public TannersStagingRepository() : this(new GreencardContext())
        {

        }
        public TannersStagingRepository(GreencardContext context) : base(context)
        {
            _context = context;
        }
        public List<PatientTannersStaging> getTannersStaging(int patientId)
        {
            ITannersStagingRepository tannersRepository = new TannersStagingRepository();
            var tannersList = tannersRepository.GetAll().Where(x => x.PatientId == patientId);
            return tannersList.ToList();
        }
        public List<PatientTannersStaging> getPubicHair(int patientId, int pubicHair)
        {
            ITannersStagingRepository tannersRepository = new TannersStagingRepository();
            var tannersList = tannersRepository.GetAll().Where(x => x.PatientId == patientId && x.PubicHairId>pubicHair);
            return tannersList.ToList();
        }
        public List<PatientTannersStaging> getBreastsGenitals(int patientId, int breastsGenitals)
        {
            ITannersStagingRepository tannersRepository = new TannersStagingRepository();
            var tannersList = tannersRepository.GetAll().Where(x => x.PatientId == patientId && x.BreastsGenitalsId>breastsGenitals);
            return tannersList.ToList();
        }
        public List<PatientTannersStaging> getTannersHighestDate(int patientId, DateTime tannersStagingDate)
        {
            ITannersStagingRepository tannersRepository = new TannersStagingRepository();
            var tannersList = tannersRepository.GetAll().Where(x => x.PatientId == patientId && x.TannersStagingDate >= tannersStagingDate);
            return tannersList.ToList();
        }
    }
}
