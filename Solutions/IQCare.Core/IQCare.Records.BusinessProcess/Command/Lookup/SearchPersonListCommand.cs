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
    }

    public class SearchPersonListResponse
    {
        public List<PersonListView> PersonSearch { get; set; }
    }
}
