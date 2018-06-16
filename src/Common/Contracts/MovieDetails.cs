using Newtonsoft.Json;

namespace Common.Contracts
{
    public class MovieDetails : MovieSummary
    {
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("rating")]
        public decimal Rating { get; set; }
    }
}