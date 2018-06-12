using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.Documents.Client;

namespace DutchAzureMeetup
{
    public static class GetMovieList
    {
        [FunctionName("GetMovieList")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "movies")]HttpRequest req, TraceWriter log)
        {
            using (DocumentClient client = CosmosDBHelper.CreateDocumentClient())
            {
                var collectionUri = await client.GetDocumentCollectionUri();

                var movies = client.CreateDocumentQuery<MovieSummary>(collectionUri)
                    .AsEnumerable()
                    .OrderByDescending(movie => movie.Title)
                    .ToList();

                return new JsonResult(movies);                
            }
        }
    }
}
