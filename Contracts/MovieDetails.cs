using Newtonsoft.Json;

namespace DutchAzureMeetup
{
    public class MovieDetails : MovieSummary
    {
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("rating")]
        public decimal Rating { get; set; }
    }
}