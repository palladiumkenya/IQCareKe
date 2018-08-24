using System;
using System.Threading.Tasks;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using Serilog;

namespace IQCare.Common.Services
{
    public class PersonContactsService
    {
        private readonly ICommonUnitOfWork _commonUnitOfWork;

        public PersonContactsService(ICommonUnitOfWork commonUnitOfWork)
        {
            _commonUnitOfWork = commonUnitOfWork ?? throw new ArgumentNullException(nameof(commonUnitOfWork));
        }

        public async Task<PersonTreatmentSupporter> Add(int personId, int contactId, int userId, int contactCategory, int contactRelationship)
        {
            try
            {
                PersonTreatmentSupporter personTreatmentSupporter = new PersonTreatmentSupporter()
                {
                    PersonId = personId,
                    SupporterId = contactId,
                    CreatedBy = userId,
                    CreateDate = DateTime.Now,
                    DeleteFlag = false,
                    ContactCategory = contactCategory,
                    ContactRelationship = contactRelationship
                };

                await _commonUnitOfWork.Repository<PersonTreatmentSupporter>().AddAsync(personTreatmentSupporter);
                await _commonUnitOfWork.SaveAsync();
                return personTreatmentSupporter;
            }
            catch (Exception e)
            {
                Log.Error(e.Message + " " + e.InnerException);
                throw e;
            }
        }
    }
}