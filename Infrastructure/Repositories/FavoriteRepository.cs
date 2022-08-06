using ApplicationCore.Entities;
using ApplicationCore.Models;
using ApplicationCore.RepositoryContracts;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class FavoriteRepository :IFavoriteRepository
    {
        private readonly MovieShopDbContext _movieShopDbContext;

        public FavoriteRepository(MovieShopDbContext movieShopDbContext)
        {
            _movieShopDbContext = movieShopDbContext;
        }

        public async Task<Favorite> AddFavorite(Favorite favorite)
        {
            _movieShopDbContext.Favorites.Add(favorite);
            await _movieShopDbContext.SaveChangesAsync();
            return favorite;
        }

        public async Task<bool> DoesFavoriteExist(int userId, int movieId)
        {
            var favoriteMovie = await _movieShopDbContext.Favorites.FirstOrDefaultAsync(x => x.UserId == userId && x.MovieId == movieId);
            if (favoriteMovie != null) 
            {
                return true;
            }
            return false;
        }

        public async Task<List<Favorite>> GetAllFavorites(int id)
        {
            var favorites = await _movieShopDbContext.Favorites.Include(f => f.Movie).Where(f => f.UserId == id).ToListAsync();
            return favorites;
        }

        public async Task removeFavorite(FavoriteRequestModel favoriteRequest)
        {
            var favorite = _movieShopDbContext.Favorites.FirstOrDefault(x => x.MovieId == favoriteRequest.MovieId && x.UserId == favoriteRequest.UserId);
            _movieShopDbContext.Favorites.Remove(favorite);
            await _movieShopDbContext.SaveChangesAsync();
        }
    }
}
