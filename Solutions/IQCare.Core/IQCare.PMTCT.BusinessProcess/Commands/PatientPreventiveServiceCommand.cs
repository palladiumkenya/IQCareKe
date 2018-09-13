using System;
using System.Collections.Generic;
using IQCare.Library;
using IQCare.PMTCT.Core.Models;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.Commands
{
    public  class PatientPreventiveServiceCommand:IRequest<Result<PatientPreventiveServiceResponse>>
   {
       public List<PreventiveService> PreventiveService;
       public int InsecticideTreatedNet { get; set; }
       public DateTime InsecticideGivenDate { get; set; }
       public int AntenatalExercise { get; set; }
        public int PartnerTestingVisit { get; set; }
        public int FinalHIVResult { get; set; }
       public int CreatedBy { get; set; }
    }

    public class PatientPreventiveServiceResponse{
        public int Id { get; set; }
    }
}
