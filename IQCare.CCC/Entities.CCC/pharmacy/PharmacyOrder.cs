using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.CCC.pharmacy
{
    [Serializable]
    [Table("ord_PatientPharmacyOrder")]
    public class PharmacyOrder
    {
        [Key]
        public int ptn_pharmacy_pk { get; set; }
        [Column("VisitID")]
        public int VisitId { get; set; }
        [Column("LocationID")]
        public int LocationId { get; set; }
        public int ? OrderedBy { get; set; }
        public DateTime ? OrderedByDate { get; set; }
        public int DispensedBy { get; set; }
        public DateTime DispensedByDate { get; set; }
        public decimal Height { get; set; }
        [Column("FDC")]
        public int ? Fdc { get; set; }
        [Column("ProgID")]
        public int ProgId { get; set; }
        public int Weight { get; set; }
        public int HoldMedicine { get; set; }
        public int Signature { get; set; }
        [Column("EmployeeID")]
        public long  EmployeeId { get; set; }
        [Column("ProviderID")]
        public int ProviderId { get; set; }
        public int DeleteFlag { get; set; }
        [Column("UserID")]
        public int UserId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int PharmacyPeriodTaken { get; set; }
        public int RegimenLine { get; set; }
        public string PharmacyNotes { get; set; }
        public int StoreId { get; set; }
        [Column("orderstatus")]
        public int OrderStatus { get; set; }
        [Column("ReportingID")]
        public string ReportingId { get; set; }
        public int PatientMasterVisitId { get; set; }
        public int PatientId { get; set; }
    }
}