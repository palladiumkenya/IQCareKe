using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Library;
using MediatR;

namespace IQCare.Common.BusinessProcess.Commands.PersonCommand
{
   public  class AddPatientTransferInCommand : IRequest<Result<AddPatientTransferInResponse>>
    {
        public int PatientId { get; set; }

      

        public int ServiceId { get; set; }


        public DateTime TransferInDate { get; set; }


        public DateTime TreatmentStartDate { get; set; }
        public string CurrentTreatment { get; set; }
        public string FacilityFrom { get; set; }
        public int MflCode { get; set; }
        public string CountyFrom { get; set; }
        public string TransferInNotes { get; set; }

        public int CreatedBy { get; set; }

        public bool DeleteFlag { get; set; }


    }

    public class AddPatientTransferInResponse
    {
        public int TransferInId { get; set; }
    }
}
