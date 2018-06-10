
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;

namespace DutchAzureMeetup
{
    public static class MovieList
    {
        [FunctionName("MovieList")]
        public static IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "movies")]HttpRequest req, TraceWriter log)
        {
            return new OkObjectResult("Deadpool 2;Infinity War");
        }
    }
}
