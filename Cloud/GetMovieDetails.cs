using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.Documents.Client;
using System.Linq;
using System.Threading.Tasks;

namespace DutchAzureMeetup
{
    public static class GetMovieDetails
    {
        [FunctionName("GetMovieDetails")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "movies/{id}")]HttpRequest req, string id, TraceWriter log)
        {
            using (DocumentClient client = CosmosDBHelper.CreateDocumentClient())
            {
                var collectionUri = await client.GetDocumentCollectionUri();

                var movie = client.CreateDocumentQuery<MovieDetails>(collectionUri)
                    .Where(m => m.Id == id)
                    .AsEnumerable()
                    .FirstOrDefault();

                if (movie != null)
                {
                    return new JsonResult(movie);
                }

                return new NotFoundResult();
            }
        }
    }
}
