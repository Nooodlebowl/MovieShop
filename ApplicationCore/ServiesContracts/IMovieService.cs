using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.ServiesContracts
{
    public interface IMovieService
    {
        //controllers will call Servies
        Task< List<MovieCardModel>> GetTopRevenueMovies();

        Task<MovieDetailsModel> GetMovieDetails(int movieId);
    }
}
