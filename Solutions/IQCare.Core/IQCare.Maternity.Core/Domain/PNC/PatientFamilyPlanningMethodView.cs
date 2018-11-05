using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Maternity.Core.Domain.PNC
{
    public class PatientFamilyPlanningMethodView
    {
        public PatientFamilyPlanningMethodView()
        {

        }
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int PatientFPId { get; set; }
        public int FPMethodId { get; set; }
        public bool Active { get; set; }
        public bool DeleteFlag { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }
        public string AuditData { get; set; }
    }
}
