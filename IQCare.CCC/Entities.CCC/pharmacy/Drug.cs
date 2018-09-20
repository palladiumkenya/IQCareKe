using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.CCC.pharmacy
{
    [Serializable]
    [Table("Mst_Drug")]
    public class Drug
    {
        public Drug()
        {
            DrugID = "";
            ItemInstructions = "";
            Abbreviation = "";
        }
        public int Drug_pk { get; set; }
        public string DrugID { get; set; }
        public int ItemTypeID { get; set; }
        public string DrugName { get; set; }
        public bool? DeleteFlag { get; set; }
        public int UserID { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public decimal? DispensingMargin { get; set; }
        public decimal? DispensingUnitPrice { get; set; }
        public string FDACode { get; set; }
        public int? Manufacturer { get; set; }
        public int? MaxStock { get; set; }
        public int? MinStock { get; set; }
        public decimal? PurchaseUnitPrice { get; set; }
        public int? QtyPerPurchaseUnit { get; set; }
        public decimal SellingUnitPrice { get; set; }
        public int? DispensingUnit { get; set; }
        public int? PurchaseUnit { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public int Sequence { get; set; }
        public string ItemInstructions { get; set; }
        public string Abbreviation { get; set; }
    }

}
