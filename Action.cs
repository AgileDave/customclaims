using Newtonsoft.Json;

namespace Customers.Publix
{
    public class Action
    {
        [JsonProperty("@odata.type")]
        public string odatatype { get; set; }
        public Claims claims { get; set; }
        public Action()
        {
            odatatype = "microsoft.graph.provideClaimsForToken";
            claims = new Claims();
        }
    }
}