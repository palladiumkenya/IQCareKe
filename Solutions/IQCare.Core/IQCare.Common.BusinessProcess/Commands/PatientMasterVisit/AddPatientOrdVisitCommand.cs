using System;
using IQCare.Common.Core.Models;
using IQCare.Library;
using MediatR;

namespace IQCare.Common.BusinessProcess.Commands.PatientMasterVisit
{
    public class AddPatientOrdVisitCommand : IRequest<Result<OrdVisit>>
    {
        public int Ptn_Pk { get; set; }
        public int LocationID { get; set; }
        public DateTime VisitDate { get; set; }
        public int UserID { get; set; }
    }
}