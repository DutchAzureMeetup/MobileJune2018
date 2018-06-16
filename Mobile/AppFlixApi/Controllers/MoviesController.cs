using System.Linq;
using System.Threading.Tasks;
using Common.Contracts;
using Common.Utils;
using Microsoft.AspNetCore.Mvc;

namespace AppFlixApi.Controllers
{
    [Route("api/[controller]")]
    public class MoviesController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            using (var client = CosmosDBHelper.CreateDocumentClient())
            {
                var collectionUri = await client.GetDocumentCollectionUri();

                var movies = client.CreateDocumentQuery<MovieSummary>(collectionUri)
                    .AsEnumerable()
                    .OrderByDescending(movie => movie.Title)
                    .ToList();

                return new JsonResult(movies);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            using (var client = CosmosDBHelper.CreateDocumentClient())
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
