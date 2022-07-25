using ApplicationCore.Entities;
using ApplicationCore.RepositoryContracts;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class MovieRepository : IMovieRepository
    {

        private readonly MovieShopDbContext _movieShopDbContext;

        public MovieRepository(MovieShopDbContext movieShopDbContext)
        {
            _movieShopDbContext = movieShopDbContext;
        }

        public async Task<Movie> GetById(int id)
        {
            // select * from movie where id =1 join genre, cast, moviegenre, moviecast
            var movieDetails = await _movieShopDbContext.Movies
                .Include(m => m.GenresOfMovie).ThenInclude(m => m.Genre)
                .Include(m => m.CastsOfMovie).ThenInclude(m => m.Cast)
                .Include(m => m.Trailers)
                .FirstOrDefaultAsync(m => m.Id == id);
            return movieDetails;
        }

        public async Task<List<Movie>> GetTop30HighestRevenueMovies()
        {
            //call the database with EF core and get the data
            //use MovieShopDbContext and Movies DbSet
            //select * from movies Order by  Revenue
            //write corresponding LINQ query

            var movies = await _movieShopDbContext.Movies.OrderByDescending(m => m.Revenue).Take(30).ToListAsync();
            return movies;
        }

        public Task<List<Movie>> GetTop30RatedMovies()
        {
            throw new NotImplementedException();
        }
    }
}
