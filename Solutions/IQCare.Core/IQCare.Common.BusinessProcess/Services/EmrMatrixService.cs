
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IQCare.Common.BusinessProcess.Services
{
    public class EmrMatrixService
    {
        private readonly ICommonUnitOfWork _unitOfWork;

        public EmrMatrixService(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<EmrMatrix> GetCurrentEmrMatrix()
        {
            try
            {
                EmrMatrix currentMatrix = await _unitOfWork.Repository<EmrMatrix>().Get().FirstOrDefaultAsync();
                return currentMatrix;
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                return null;
            }
        }

    }
}