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
    public class CastRepository : ICastRepository
    {
        private readonly MovieShopDbContext _movieShopDbContext;

        public CastRepository(MovieShopDbContext movieShopDbContext)
        {
            _movieShopDbContext = movieShopDbContext;
        }

        public async Task<Cast> GetById(int id)
        {
            var movies = await _movieShopDbContext.Casts.
                Include(m => m.MoviesOfCast).ThenInclude(m => m.Movie).FirstOrDefaultAsync(m => m.Id == id);
            return movies;
        }
    }
}
