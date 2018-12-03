using IQCare.Common.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Library;

namespace IQCare.Records.BusinessProcess.Command
{
  
   public class AddRegistrationPatientCommand : IRequest<Result<AddRegistrationPatientResponse>>
        {
            public int PersonId { get; set; }
            public DateTime DateOfBirth { get; set; }
            public int UserId { get; set; }

          public string NationalId { get; set; }
            public DateTime RegistrationDate { get; set; }
        }

        public class AddRegistrationPatientResponse
        {
            public int PatientId { get; set; }
        }
    }
