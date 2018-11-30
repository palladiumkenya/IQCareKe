using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using Serilog;

namespace IQCare.Common.BusinessProcess.Services
{
    public class GetPatientDetails
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        public GetPatientDetails(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<List<PatientLookupView>> GetPatientByPatientId(int patientId)
        {
            try
            {
                var sql = "exec pr_OpenDecryptedSession;" +
                          $"SELECT * FROM Api_PatientsView WHERE PatientId = {patientId}; " +
                          $"exec [dbo].[pr_CloseDecryptedSession];";

                var result = await _unitOfWork.Repository<PatientLookupView>().FromSql(sql);
                return result;
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                return null;
            }
        }

        public async Task<List<PatientLookupView>> GetPatientByPersonId(int personId)
        {
            try
            {
                var sql = "exec pr_OpenDecryptedSession;" +
                          $"SELECT * FROM Api_PatientsView WHERE PersonId = {personId}; " +
                          $"exec [dbo].[pr_CloseDecryptedSession];";

                var result = await _unitOfWork.Repository<PatientLookupView>().FromSql(sql);
                return result;
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                return null;
            }
        }
    }
}