using ApplicationCore.Models;
using ApplicationCore.ServiesContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        

        [HttpPost]
        [Route("purchase-movie")]
        public async Task<IActionResult> PurchaseMovie(int movieId, decimal price, int userId)
        {
            PurchaseRequestModel purchase = new PurchaseRequestModel
            {
                MovieId = movieId,
                Price = price,
            };
            var buy = await _userService.PurchaseMovie(purchase, userId);
            return Ok(buy);
        }

        [HttpPost]
        [Route("favorite")]
        public async Task<IActionResult> FavoriteMovie(int movieId, int userId)
        {
            FavoriteRequestModel favorite = new FavoriteRequestModel
            {
                MovieId = movieId,
                UserId = userId
            };
            var fav = await _userService.AddFavorite(favorite);
            return Ok(fav);
        }

        [HttpPost]
        [Route("un-favorite")]
        public async Task<IActionResult> RemoveFavorite(int movieId, int userId)
        {
            FavoriteRequestModel favorite = new FavoriteRequestModel
            {
                MovieId = movieId,
                UserId = userId
            };
            var fav = await _userService.RemoveFavorite(favorite);
            return Ok(fav);
        }

        [HttpGet]
        [Route("check-movie-favorite/{movieId}")]
        public async Task<IActionResult> CheckFavorite(int id, int movieId)
        {
            var doesExist = await _userService.FavoriteExists(id, movieId);
            return Ok(doesExist);
        }

        [HttpPost]
        [Route("add-review")]
        public async Task<IActionResult> addReview(int movieId, int userId, decimal rating, string reviewText)
        {
            ReviewRequestModel newReview = new ReviewRequestModel
            {
                MovieId = movieId,
                UserId = userId,
                Rating = rating,
                ReviewText = reviewText
            };
            var review = await _userService.AddMovieReview(newReview);
            return Ok(review);
        }
        [HttpPut]
        [Route("edit-review")]
        public async Task<IActionResult> updateReview(int movieId, int userId, decimal rating, string reviewText)
        {
            ReviewRequestModel newReview = new ReviewRequestModel
            {
                MovieId = movieId,
                UserId = userId,
                Rating = rating,
                ReviewText = reviewText
            };
            var review = await _userService.UpdateMovieReview(newReview);
            return Ok(review);
        }
        [HttpDelete]
        [Route("delete-review/{movieId}")]
        public async Task<IActionResult> DeleteReview(int userId, int movieId)
        {
            var review = await _userService.DeleteMovieReview(userId, movieId);
            return Ok(review);
        }

        [HttpGet]
        [Route("purchases")]
        public async Task<IActionResult> GetAllUserPurchases(int userId)
        {
            var movies = await _userService.GetAllPurchasesForUser(userId);
            return Ok(movies);
        }
        [HttpGet]
        [Route("purchase-details/{movieId}")]
        public async Task<IActionResult> GetMovieDetails(int userId, int movieId)
        {
            var movieDetails = await _userService.GetPurchasesDetails(userId, movieId);
            return Ok(movieDetails);
        }
        [HttpGet]
        [Route("check-movie-purchased/{movieId}")]
        public async Task<IActionResult> CheckPurchased(int movieId, int userId)
        {
            PurchaseRequestModel purchaseRequest = new PurchaseRequestModel
            {
                MovieId = movieId
            };
            var isPurchased = await _userService.IsMoviePurchased(purchaseRequest, userId);
            return Ok(isPurchased);
        }
        [HttpGet]
        [Route("favorites")]
        public async Task<IActionResult> Favorites(int userId)
        {
            var favorites = await _userService.GetAllFavoritesForUser(userId);
            return Ok(favorites);
        }
        [HttpGet]
        [Route("movie-reviews")]
        public async Task<IActionResult> Reviews(int userId) 
        {
            var reviews = await _userService.GetAllReviewsByUser(userId);
            return Ok(reviews);
        }
    }
}
