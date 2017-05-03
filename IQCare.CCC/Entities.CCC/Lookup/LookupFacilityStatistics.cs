using System;

namespace Entities.CCC.Lookup
{
    [Serializable]
    [System.ComponentModel.DataAnnotations.Schema.Table("facilityStatisticsView")]

    public class LookupFacilityStatistics
    {
        public int Id { get; set; }
       public int TotalCumulativePatients { get; set; }
       public int TotalActiveOnArt { get; set; }
       public int TotalTransferIn { get; set; }
       public int TotalTransit { get; set; }
       public int TotalOnCtxDapson { get; set; }
       public int TotalPatientsDead { get;set; }
       public int TotalPatientsTransferedOut { get;set; }
    }
}
