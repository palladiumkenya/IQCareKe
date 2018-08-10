using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Common.BusinessProcess.Services;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCareRecords.Common.BusinessProcess.Command;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
namespace IQCareRecords.Common.BusinessProcess.CommandHandlers.Lookup
{
    public class GetCountiesCommandHandler:IRequestHandler<GetCountiesCommand,Result<AddCountyListResponse>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        List<CountyLookup> counties =new List<CountyLookup>();

        int CountyId;
        int SubcountyId;

        public GetCountiesCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork= unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }



        public async Task<Result<AddCountyListResponse>> Handle(GetCountiesCommand request,CancellationToken cancellationToken)
        {
            try {
                LookupLogic ll = new LookupLogic(_unitOfWork);
             
                    if(String.IsNullOrEmpty(request.CountyId))
                    {
                        CountyId = 0;
                    }
                    else
                    {
                        CountyId = Convert.ToInt32(request.CountyId) ;
                    }
                    if(String.IsNullOrEmpty(request.SubcountyId))
                    {
                        SubcountyId = 0;
                       
                    }
                    else
                    {
                        SubcountyId = Convert.ToInt32(request.SubcountyId);
                    }
                    if (CountyId == 0 && SubcountyId == 0)
                    {

                    counties = await ll.GetCountyList();
                      

                    }

                   
                    return Result<AddCountyListResponse>.Valid(new AddCountyListResponse()
                    {

                        Counties=counties,
                      
                    });

                
            }
            catch (Exception e)
            {
                return Result<AddCountyListResponse>.Invalid(e.Message);
              }
                //var sqlPatient = "exec pr_OpenDecryptedSession;" +
                //                 "Insert Into  Patient(ptn_pk,PersonId,PatientIndex,PatientType,FacilityId,Active,DateOfBirth,NationalId,DeleteFlag,CreatedBy,CreateDate,AuditData,DobPrecision)" +
                //                 $"Values(0, {request.PersonId}, {DateTime.Now.Year + '-' + request.PersonId}, 258, 13028, 1," +
                //                 $"'{request.DateOfBirth.ToString("yyyy-MM-dd")}', ENCRYPTBYKEY(KEY_GUID('Key_CTC'), '99999999'), 0, 1, GETDATE()," +
                //                 $"NULL, 1);" +
                //                 $"SELECT [Id],[ptn_pk],[PersonId],[PatientIndex],[PatientType],[FacilityId],[Active],[DateOfBirth]," +
                //                 $"[DobPrecision],CAST(DECRYPTBYKEY(NationalId) AS VARCHAR(50)) [NationalId],[DeleteFlag],[CreatedBy]," +
                //                 $"[CreateDate],[AuditData],[RegistrationDate] FROM [dbo].[Patient] WHERE Id = SCOPE_IDENTITY();" +
                //                 $"exec [dbo].[pr_CloseDecryptedSession];";

                //var patientInsert = await _unitOfWork.Repository<Patient>().FromSql(sqlPatient);
            }
        }
}
