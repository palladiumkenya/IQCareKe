using Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Entities.CCC.ClinicalSummary
{
        [Serializable]
        [Table("ClinicalSummary")]
        public class ClinicalSummary : BaseEntity
        {
        [Key]
        //public List<Anthropoemetric> AllAnthropoemetric;
        //    public List<ARTRegimensLaboratory> AllARTRegimensLaboratory;
        //    public List<ChronicConditions> AllChronicConditions;
        //    public List<OIS> AllOIS;
        //    public List<RegimenChanges> AllRegimenChanges;

        //    public ClinicalSummary();
                 
            public string ClinicalEvaluation { get; set; }
            public int ClinicalSummary_id { get; set; }
            public string CliniciansName { get; set; }
            public string CommentOnTreatment { get; set; }
            public decimal CurrentHeight { get; set; }
            public decimal CurrentWeight { get; set; }
            public string DOB { get; set; }
            public string Email { get; set; }
            public string EnrollmentDate { get; set; }
            public string EvaluationForOtherText { get; set; }
            public string EvaluationForOtherVal { get; set; }
            public string EvidenceOfAdherence { get; set; }
            public string Form_filling_date { get; set; }
            public string HasDrugResistance { get; set; }
            public string HasFacilityMultidisciplinary { get; set; }
            public decimal location_id { get; set; }
            public string MDTMembers { get; set; }
            public string MFLName { get; set; }
            public int NumberOfAdherence { get; set; }
            public int NumberOfDOTS { get; set; }
            public int NumberOfHomeVisit { get; set; }
            public string PatientEnrollmentID { get; set; }
            public string PoorAdherence { get; set; }
            public string PrimaryReason { get; set; }
            public decimal Ptn_pk { get; set; }
            public string Sex { get; set; }
            public string SupportStructures { get; set; }
            public string Telephone { get; set; }
            public decimal Visit_Id { get; set; }
        }
    }
