using System;
using IQCare.HTS.Core.Interfaces;
using IQCare.HTS.Core.Model;
using IQCare.SharedKernel.Infrastructure.Repository;

namespace IQCare.HTS.Infrastructure.Repository
{
    public class FormRepository : BaseRepository<Form, Guid>, IFormRepository
    {
        public FormRepository(HtsDbContext context) : base(context)
        {
        }
    }
}