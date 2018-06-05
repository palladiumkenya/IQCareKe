using DataAccess.Context;
using DataAccess.WebApi.Interface;
using Entities.CCC.PSmart;

namespace DataAccess.WebApi.Repository
{
    public class ExternalPatientIdRepository : BaseRepository<EXTERNALPATIENTID>,IExternalPatientIdRepository
    {
        private readonly PsmartContext _context;

        public ExternalPatientIdRepository():this (new PsmartContext())
        {
         
        }

        public ExternalPatientIdRepository(PsmartContext context) : base(context)
        {
            _context = context;
        }
    }
}