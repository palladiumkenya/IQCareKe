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
        public int ServiceEntryPoint { get; set; }
        [JsonProperty(PropertyName = "hasDisability")]
        public int HasDisability { get; set; }
        [JsonProperty(PropertyName = "selfTest")]
        public int SelfTestLastTwelveMonths { get; set; }
        [JsonProperty(PropertyName = "disability")]
        public int[] Disabilities { get; set; }
        [JsonProperty(PropertyName = "consent")]
        public int Consent { get; set; }
        [JsonProperty(PropertyName = "clientTestedAs")]
        public int ClientTested { get; set; }
        [JsonProperty(PropertyName = "strategy")]
        public int Strategy { get; set; }
        [JsonProperty(PropertyName = "remarks")]
        public string Remarks { get; set; }
        [JsonProperty(PropertyName = "tbScreening")]
        public int TbScreening { get; set; }
    }
}