using Common.Contracts;
using Refit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppFlix.Services
{
    public class MovieService : IMovieService
    {
        public MovieService()
        {

        }

        public Task<MovieDetails> GetMovieDetails(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<MovieSummary>> GetMovies()
        {
            throw new NotImplementedException();
        }
    }
}