using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IQCare.Common.Services
{
    public class PersonDetailsViewService
    {
        private readonly ICommonUnitOfWork _commonUnitOfWork;

        public PersonDetailsViewService(ICommonUnitOfWork commonUnitOfWork)
        {
            _commonUnitOfWork = commonUnitOfWork ?? throw new ArgumentNullException(nameof(commonUnitOfWork));
        }

        public async Task<List<PersonDetailsView>> GetPersonDetails(int personId)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("exec pr_OpenDecryptedSession;");
                sql.Append("select * from [dbo].[PersonDetailsView] where Id =" + personId + ";");
                sql.Append("exec [dbo].[pr_CloseDecryptedSession];");

                var personDetails = await _commonUnitOfWork.Repository<PersonDetailsView>().FromSql(sql.ToString());
                return personDetails;
            }
            catch (Exception e)
            {
                Log.Error(e.Message + " " + e.InnerException);
                throw e;
            }
        }
    }
}