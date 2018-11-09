using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Common.Core.Models;
using IQCare.Library;
using MediatR;
namespace IQCare.Records.BusinessProcess.Command
{
    public class SearchPersonContactListCommand : IRequest<Result<SearchResponse>>
    {
        public string identificationNumber { get; set; }
        public string firstName { get; set; }
        public string middleName { get; set; }
        public string lastName { get; set; }

        public string EnrollmentNumber { get; set; }
    }
    public class SearchResponse
    {
        public List<ContactsListView> PersonSearch { get; set; }
    }
}
