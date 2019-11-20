using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands.PersonCommand;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.Library;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IQCare.Common.BusinessProcess.CommandHandlers.PersonCommand
{
    public class GetDuplicatePersonsCommandHandler : IRequestHandler<GetDuplicatePersonsCommand, Result<List<DuplicatePersonsPoco>>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        public GetDuplicatePersonsCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<List<DuplicatePersonsPoco>>> Handle(GetDuplicatePersonsCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    StringBuilder sql = new StringBuilder();
                    sql.Append("sp_GetDuplicatePatientRecords @matchFirstName, @matchMiddleName, @matchLastname, @matchSex, @matchEnrollmentNumber, @matchDOB, @matchEnrollmentDate, @matchARTStartDate, @matchHIVDiagnosisDate");

                    var matchFirstName = new SqlParameter();
                    matchFirstName.SqlDbType = SqlDbType.Bit;
                    matchFirstName.ParameterName = "@matchFirstName";
                    matchFirstName.Value = 1;

                    var matchMiddleName = new SqlParameter();
                    matchMiddleName.SqlDbType = SqlDbType.Bit;
                    matchMiddleName.ParameterName = "@matchMiddleName";
                    matchMiddleName.Value = 1;

                    var matchLastname = new SqlParameter();
                    matchLastname.SqlDbType = SqlDbType.Bit;
                    matchLastname.ParameterName = "@matchLastname";
                    matchLastname.Value = 1;

                    var matchSex = new SqlParameter();
                    matchSex.SqlDbType = SqlDbType.Bit;
                    matchSex.ParameterName = "@matchSex";
                    matchSex.Value = 1;


                    var matchEnrollmentNumber = new SqlParameter();
                    matchEnrollmentNumber.SqlDbType = SqlDbType.Bit;
                    matchEnrollmentNumber.ParameterName = "@matchEnrollmentNumber";
                    matchEnrollmentNumber.Value = 0;

                    var matchDOB = new SqlParameter();
                    matchDOB.SqlDbType = SqlDbType.Bit;
                    matchDOB.ParameterName = "@matchDOB";
                    matchDOB.Value = 0;


                    var matchEnrollmentDate = new SqlParameter();
                    matchEnrollmentDate.SqlDbType = SqlDbType.Bit;
                    matchEnrollmentDate.ParameterName = "@matchEnrollmentDate";
                    matchEnrollmentDate.Value = 0;


                    var matchARTStartDate = new SqlParameter();
                    matchARTStartDate.SqlDbType = SqlDbType.Bit;
                    matchARTStartDate.ParameterName = "@matchARTStartDate";
                    matchARTStartDate.Value = 0;

                    var matchHIVDiagnosisDate = new SqlParameter();
                    matchHIVDiagnosisDate.SqlDbType = SqlDbType.Bit;
                    matchHIVDiagnosisDate.ParameterName = "@matchHIVDiagnosisDate";
                    matchHIVDiagnosisDate.Value = 0;
                    

                    var duplicatePersons = await _unitOfWork.Context.Query<DuplicatePersonsPoco>().FromSql(sql.ToString(), parameters: new []
                    {
                        matchFirstName,
                        matchMiddleName,
                        matchLastname,
                        matchSex,
                        matchEnrollmentNumber,
                        matchDOB,
                        matchEnrollmentDate,
                        matchARTStartDate,
                        matchHIVDiagnosisDate

                    }).ToListAsync();

                    return Result<List<DuplicatePersonsPoco>>.Valid(duplicatePersons);
                }
                catch (Exception e)
                {
                    Log.Error(e,$"Could not fetch duplicate persons");
                    return Result<List<DuplicatePersonsPoco>>.Invalid($"Could not fetch duplicate persons");
                }
            }
        }
    }
}