using ApplicationCore.Entities;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.RepositoryContracts
{
    public interface IFavoriteRepository
    {
        Task<List<Favorite>> GetAllFavorites(int id);
        Task<Favorite> AddFavorite(Favorite favorite);
        Task removeFavorite(FavoriteRequestModel favoriteRequest);
        Task<bool> DoesFavoriteExist(int userId, int movieId);
    }
}
