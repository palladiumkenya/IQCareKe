using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using IQCare.Library;
using MediatR;


namespace IQCare.PMTCT.BusinessProcess.Queries
{
    public class GetPatientVitalQueryByVisitDate : IRequest<Result<List<PatientVitalViewModel>>>
    {
        public int PatientId { get; set; }

        public DateTime VisitDate { get; set; }

    }

  
}

