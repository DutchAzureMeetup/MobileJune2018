using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using System.Threading.Tasks;
using Microsoft.Azure.Documents.Client;
using System;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace DutchAzureMeetup
{
    public static class SeedMovies
    {
        [FunctionName("SeedMovies")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "movies/seed")]HttpRequest req, TraceWriter log = null)
        {
            using (DocumentClient client = CosmosDBHelper.CreateDocumentClient())
            {
                var collectionUri = await client.GetDocumentCollectionUri();

                Console.WriteLine("Importing movies into database, this may take a minute...");

                // Load the movie data, each line is a separate JSON document which we'll store
                // in a JObject.
                var documents = File.ReadAllLines("movies.json")
                    .Select(line => JObject.Parse(line));

                foreach (var document in documents)
                {
                    await client.UpsertDocumentAsync(collectionUri, document);
                }
            }

            return new OkObjectResult("Database initialized.");
        }
    }
}
