using Common.Contracts;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppFlix.Services
{
    [Headers("Accept: application/json")]
    public interface IMovieApi
    {
        [Get("/movies")]
        Task<IEnumerable<MovieSummary>> GetMovies();

        [Get("/movies/{id}")]
        Task<MovieDetails> GetMovieDetails(string id);
    }
}
