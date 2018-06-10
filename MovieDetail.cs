
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;

namespace DutchAzureMeetup
{
    public static class MovieDetail
    {
        [FunctionName("MovieDetails")]
        public static IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "movies/{id:int?}")]HttpRequest req, int id, TraceWriter log)
        {
            return new OkObjectResult("Details for movie: " + id);
        }
    }
}
