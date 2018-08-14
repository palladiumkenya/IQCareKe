using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IQCare.Common.Services
{
    public class PersonMaritalStatusService
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        public PersonMaritalStatusService(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<PersonMaritalStatus> AddPersonMaritalStatus(PersonMaritalStatus personMaritalStatus)
        {
            try
            {
                await _unitOfWork.Repository<PersonMaritalStatus>().AddAsync(personMaritalStatus);
                await _unitOfWork.SaveAsync();
                return personMaritalStatus;
            }
            catch (Exception e)
            {
                Log.Error(e.Message + " " + e.InnerException);
                throw e;
            }
        }

        public async Task<List<PersonMaritalStatus>> GetPersonMaritalStatus(int personId)
        {
            try
            {
                var personMaritalStatuses = await _unitOfWork.Repository<PersonMaritalStatus>()
                    .Get(x => x.PersonId == personId && x.DeleteFlag == false).ToListAsync();
                return personMaritalStatuses;
            }
            catch (Exception e)
            {
                Log.Error(e.Message + " " + e.InnerException);
                throw e;
            }
        }

        public async Task<PersonMaritalStatus> UpdatePersonMaritalStatus(PersonMaritalStatus personMaritalStatus)
        {
            try
            {
                _unitOfWork.Repository<PersonMaritalStatus>().Update(personMaritalStatus);
                await _unitOfWork.SaveAsync();
                return personMaritalStatus;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}