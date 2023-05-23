using Newtonsoft.Json;

namespace Customers.Publix
{
    public class ResponseContent
    {
        [JsonProperty("data")]
        public Data data { get; set; }
        public ResponseContent()
        {
            data = new Data();
        }
    }
}