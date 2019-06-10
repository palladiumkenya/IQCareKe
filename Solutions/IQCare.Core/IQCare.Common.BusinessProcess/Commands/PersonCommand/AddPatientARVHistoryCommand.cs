using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using IQCare.Library;

namespace IQCare.Common.BusinessProcess.Commands.PersonCommand
{
   public  class AddPatientARVHistoryCommand :IRequest<Result<AddPatientARVHistoryResponse>>
    {
        public int PatientId { get; set; }


       public int ServiceId { get; set; }
        public string TreatmentType { get; set; }
        public string Purpose { get; set; }
        public string Regimen { get; set; }
        

       public Boolean DeleteFlag { get; set; }

        public int CreatedBy { get; set; }

      

        public int? Weeks { get; set; }

        public int? Months { get; set; }

        public DateTime? InitiationDate { get; set; }

        public int? RegimenUse { get; set; }

    }

    public class AddPatientARVHistoryResponse
    {
        public int ARVHistoryId { get; set; }
        
    }
}
