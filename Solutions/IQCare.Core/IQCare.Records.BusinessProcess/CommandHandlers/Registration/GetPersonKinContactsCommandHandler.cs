using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.Records.BusinessProcess.Command.Registration;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonKinContactsView = IQCare.Common.Core.Models.PersonKinContactsView;

namespace IQCare.Records.BusinessProcess.CommandHandlers.Registration
{
    public class GetPersonKinContactsCommandHandler : IRequestHandler<GetPersonKinContactsCommand, Library.Result<List<PersonKinContactsView>>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        public GetPersonKinContactsCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Library.Result<List<PersonKinContactsView>>> Handle(GetPersonKinContactsCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    StringBuilder sql = new StringBuilder();
                    sql.Append("exec pr_OpenDecryptedSession;");
                    sql.Append($"SELECT * FROM [dbo].[PersonKinContactsView] WHERE DeleteFlag = 0 AND PersonId = {request.PersonId};");
                    sql.Append("exec [dbo].[pr_CloseDecryptedSession];");

                    var result = await _unitOfWork.Repository<PersonKinContactsView>().FromSql(sql.ToString());
                    for (int i = 0; i < result.Count; i++)
                    {
                        var genderList = await _unitOfWork.Repository<LookupItemView>()
                            .Get(x => x.ItemId == result[i].Sex && x.MasterName == "Gender").ToListAsync();

                        var contactCategoryList = await _unitOfWork.Repository<LookupItemView>().Get(x =>
                            x.ItemId == result[i].ContactCategory && x.MasterName == "ContactCategory").ToListAsync();

                        List<LookupItemView> contactRelationshipList = new List<LookupItemView>();
                        if (result[i].ContactRelationship.HasValue && result[i].ContactRelationship.Value > 0)
                        {
                            contactRelationshipList = await _unitOfWork.Repository<LookupItemView>().Get(x =>
                                    x.ItemId == result[i].ContactRelationship.Value && x.MasterName == "KinRelationship")
                                .ToListAsync();
                        }

                        result[i].GenderList = genderList;
                        result[i].ContactCategoryList = contactCategoryList;
                        result[i].ContactRelationshipList = contactRelationshipList;
                    }

                    return Library.Result<List<PersonKinContactsView>>.Valid(result);
                }
                catch (Exception e)
                {
                    return Library.Result<List<PersonKinContactsView>>.Invalid(e.Message);
                }
            }
        }
    }
}