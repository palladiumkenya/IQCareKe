using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Library;
using MediatR;
namespace IQCare.Prep.BusinessProcess.Commands
{
   public  class AddMonthlyRefillCommand : IRequest<Result<MonthlyRefillResponse>>
    {
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }

        public int CreatedBy { get; set; }

        public int ServiceAreaId { get; set; }

        public DateTime VisitDate { get; set; }

        public  List<PatientAdherenceOutCome> Adherence { get; set; }

        public List<PatientScreeningDetail> screeningdetail { get; set; }

        public  List<Remarklist> clinicalNotes { get; set; }
    }

    public class PatientAdherenceOutCome
    {
        public int AdherenceType { get; set; }

        public int Score { get; set; }
    }

    public class Remarklist
    {
        

        public string remark { get; set; }

     

     


    }
    public class PatientScreeningDetail
    {
        public int ScreeningTypeId { get; set; }
        public int ScreeningValueId { get; set; }

        public string Comment { get; set; }

       
    }

    public class MonthlyRefillResponse
    {
        public int Id { get; set; }
        public string Message { get; set; }

    }
}
