using ApplicationCore.ServiesContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        [Route("top-grossing")]
        //Attribute Routing
        public async Task<IActionResult> GetTopGrossingMovies() 
        {
            //call my service
            var movies = await _movieService.GetTopRevenueMovies();
            //return movies info in json format
            //asp.net core automatically serializes c# objects to json objects
            //system.text.json
            //return data(json format) along with it return the http status code
            if (movies == null || !movies.Any())
            {
                //404
                return NotFound(new { errorMessage = "No Movies Found"});
            }

            return Ok(movies);

        }
        [HttpGet]
        [Route("{movieId:int}")]
        public async Task<IActionResult> getMovie(int movieId) 
        {
            var movie = await _movieService.GetMovieDetails(movieId);
            if (movie == null) 
            {
                return NotFound(new { errorMessage = $"No Movie Found For {movieId}" });
            }
            return Ok(movie);

        }
        [HttpGet]
        [Route("genre/{genreId}")]
        public async Task<ActionResult> GetGenreMovies(int genreid, int pageSize = 30, int page = 1) 
        {
            var pagedMovies = await _movieService.GetMoviesByPagination(genreid, pageSize, page);
            if (pagedMovies == null) 
            {
                return NotFound(new { errorMessage = $"No Movie Found For {genreid}" });
            }
            return Ok();
        }
    }
    
}
