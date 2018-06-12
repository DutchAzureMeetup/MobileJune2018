using Newtonsoft.Json;

namespace DutchAzureMeetup
{
    public class MovieSummary
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }
    }
}