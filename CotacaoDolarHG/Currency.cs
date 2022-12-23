using Newtonsoft.Json;

namespace CotacaoDolarHG
{
    public class Currency
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "buy")]
        public decimal  Compra { get; set; }
        [JsonProperty(PropertyName = "sell")]
        public decimal Venda { get; set; }
        [JsonProperty(PropertyName = "variation")]
        public decimal Variacao { get; set; }
    }
}
