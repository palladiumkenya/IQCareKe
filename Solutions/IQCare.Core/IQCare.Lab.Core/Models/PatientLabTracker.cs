using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IQCare.Lab.Core.Models
{
    public class PatientLabTracker
    {
        public PatientLabTracker()
        {

        }

        public PatientLabTracker(int patientId, string labName, int patientMasterVisitId,int labTestId,int labOrderId, int labOrderTestId,
            int facilityId, DateTime ? sampleDate, int createdBy, string orderReason, string auditData)
        {
            PatientId = patientId;
            LabName = labName;
            PatientMasterVisitId = patientMasterVisitId;
            LabTestId = labTestId;
            LabOrderId = labOrderId;
            LabOrderTestId = labOrderTestId;
            FacilityId = facilityId;
            CreatedBy = createdBy;
            CreateDate = DateTime.Now;
            Results = LabOrderStatus.Pending.ToString();
            SampleDate = sampleDate;
            Reasons = orderReason;
            AuditData = auditData;
        }

        public int Id { get; set; }
        public int PatientId { get; set; }
        public string LabName { get; set; }
        public int PatientMasterVisitId { get; set; }
        public DateTime? SampleDate { get; set; }
        public string Reasons { get; set; }
        public string Results { get; set; }
        public int LabOrderId { get; set; }
        public int LabTestId { get; set; }
        public int FacilityId { get; set; }
        public decimal? ResultValues { get; set; }
        public string ResultTexts { get; set; }
        public int LabOrderTestId { get; set; }
        public string ResultUnits { get; set; }
        public string ResultOptions { get; set; }
        public DateTime? ResultDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }
        public string AuditData { get; set; }

        public void UpdateResults(DateTime resultDate,string resultText,string resultUnit, decimal? resultValues, string resultOption)
        {
            ResultValues = resultValues;
            ResultDate = resultDate;
            ResultTexts = resultText;
            ResultUnits = resultUnit;
            ResultOptions = resultOption;
            Results = LabOrderStatus.Complete.ToString();
        }

        public void SetAsComplete()
        {
            Results = LabOrderStatus.Complete.ToString();
            ResultDate = DateTime.Now;
        }

    }
}
