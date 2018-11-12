using System;
using System.Linq;
using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.Patient;
using DataAccess.Context;
using Entities.CCC.Tanners;
using System.Collections.Generic;

namespace DataAccess.CCC.Repository.Patient
{
    public class RecordTannersStagingRepository: BaseRepository<TannersStaging>, IRecordTannersStagingRepository
    {
        private readonly GreencardContext _context;

        public RecordTannersStagingRepository() : this(new GreencardContext())
        {

        }
        public RecordTannersStagingRepository(GreencardContext context) : base(context)
        {
            _context = context;
        }
        public List<TannersStaging> getRecordTannersStaging(int patientId)
        {
            IRecordTannersStagingRepository tannersRepository = new RecordTannersStagingRepository();
            var tannersList = tannersRepository.GetAll().Where(x => x.PatientId == patientId);
            return tannersList.ToList();
        }
    }
}
