using Newtonsoft.Json;

namespace IQCare.HTS.Core.ViewModel
{
    public class EncounterViewModel
    {
        [JsonProperty(PropertyName = "everTested")]
        public int EverTested { get; set; }
        [JsonProperty(PropertyName = "noofmonthsretest")]
        public int NoOfMonthsReTest { get; set; }
        [JsonProperty(PropertyName = "serviceEntryPoint")]
        public string ServiceEntryPoint { get; set; }
        [JsonProperty(PropertyName = "hasDisability")]
        public string HasDisability { get; set; }
        [JsonProperty(PropertyName = "SelfTestLastTwelveMonths")]
        public string SelfTestLastTwelveMonths { get; set; }
        [JsonProperty(PropertyName = "disability")]
        public string Disabilities { get; set; }
        [JsonProperty(PropertyName = "consent")]
        public string Consent { get; set; }
        [JsonProperty(PropertyName = "clientTestedAs")]
        public string ClientTested { get; set; }
        [JsonProperty(PropertyName = "strategy")]
        public string Strategy { get; set; }
        [JsonProperty(PropertyName = "remarks")]
        public string Remarks { get; set; }
        [JsonProperty(PropertyName = "tbScreening")]
        public string TbScreening { get; set; }
    }
}