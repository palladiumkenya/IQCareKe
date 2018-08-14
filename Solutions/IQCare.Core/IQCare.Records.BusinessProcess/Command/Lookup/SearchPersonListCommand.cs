using IQCare.Common.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace IQCare.Records.BusinessProcess.Command.Lookup
{
    public class SearchPersonListCommand: IRequest<Result<SearchPersonListResponse>>
    {
        public string identificationNumber { get; set; }
        public string firstName { get; set; }
        public string middleName { get; set; }
        public string lastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string FullName { get; set; }
        public string MobileNumber { get; set; }
    }

    public class SearchPersonListResponse
    {
        public List<PersonListView> PersonSearch { get; set; }
    }

    public class SearchQuery
    {
        public string IdentifierValue { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string MobileNumber { get; set; }
    }
}
