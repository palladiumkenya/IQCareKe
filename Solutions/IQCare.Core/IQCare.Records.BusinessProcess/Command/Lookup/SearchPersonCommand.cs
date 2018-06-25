using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Common.Core.Models;
using MediatR;

namespace IQCare.Records.BusinessProcess.Command
{
  public   class SearchPersonCommand : IRequest<Result<SearchPersonResponse>>
    {
        public string identificationNumber { get; set; }
        public string firstName { get; set; }
        public string middleName { get; set; }
        public string lastName { get; set; }

        public string EnrollmentNumber { get; set; }

        public bool NotClient { get; set; }
    }


    public class SearchPersonResponse
    {

        public List<PersonIdentifierView> PersonSearch { get; set; }
    }
}
