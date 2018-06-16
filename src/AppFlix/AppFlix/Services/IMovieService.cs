using Common.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppFlix.Services
{
    public interface IMovieService
    {
        Task<MovieDetails> GetMovieDetails(string id);
        Task<IEnumerable<MovieSummary>> GetMovies();
    }
}
