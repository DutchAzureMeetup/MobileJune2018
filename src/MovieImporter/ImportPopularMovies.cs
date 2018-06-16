using System;
using System.Threading.Tasks;
using Common.Contracts;
using Common.Utils;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using TMDbLib.Client;

namespace MovieImporter
{
    public static class ImportPopularMovies
    {
        [FunctionName("ImportPopularMovies")]
        public static async Task Run([TimerTrigger("0 */1 * * * *")]TimerInfo myTimer, TraceWriter log)
        {
            var tmdbClient = new TMDbClient("c6b31d1cdad6a56a23f0c913e2482a31");

            var movies = await tmdbClient.GetMoviePopularListAsync();

            using (var cosmosClient = CosmosDBHelper.CreateDocumentClient())
            {
                var collectionUri = await cosmosClient.GetDocumentCollectionUri();

                foreach (var movie in movies.Results)
                {
                    var movieDetails = new MovieDetails
                    {
                        Id = $"tmdb:{movie.Id}",
                        Title = movie.Title,
                        Description = movie.Overview,
                        Rating = (decimal)movie.VoteAverage
                    };

                    await cosmosClient.UpsertDocumentAsync(collectionUri, movieDetails);
                }
            }
        }
    }
}
