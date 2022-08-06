using ApplicationCore.Entities;
using ApplicationCore.Models;
using ApplicationCore.RepositoryContracts;
using ApplicationCore.ServiesContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieShopMVC.Infra;

namespace MovieShopMVC.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Purchases(int id)
        {
            var purchasedMovies = await _userService.GetAllPurchasesForUser(id);
            return View(purchasedMovies);
        }
        [HttpGet]
        public async Task<IActionResult> Favorites(int id)
        {
            var favoriteMovies = await _userService.GetAllFavoritesForUser(id);
            return View(favoriteMovies);
        }

        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(UserEditModel model)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> BuyMovie(int movieId, decimal price, int userId)
        {
            PurchaseRequestModel purchase = new PurchaseRequestModel
            {
                MovieId = movieId,
                Price = price,
            };
            var buy = await _userService.PurchaseMovie(purchase, userId);
            return LocalRedirect("~/");
        }

        [HttpPost]
        public async Task<IActionResult> FavoriteMovie(int movieId, int userId)
        {
            FavoriteRequestModel favorite = new FavoriteRequestModel
            {
                MovieId=movieId,
                UserId = userId
            };
            var fav = await _userService.AddFavorite(favorite);
            return LocalRedirect("~/");
        }
        [HttpPost]
        public async Task<IActionResult> RemoveFavoriteMovie(int movieId, int userId)
        {
            FavoriteRequestModel favorite = new FavoriteRequestModel
            {
                MovieId = movieId,
                UserId = userId
            };
            var fav = await _userService.RemoveFavorite(favorite);
            return LocalRedirect("~/");
        }

        [HttpPost]
        public async Task<IActionResult> ReviewMovie(int movieId, int userId, decimal rating, string reviewText) 
        {
            ReviewRequestModel newReview = new ReviewRequestModel 
            {
                MovieId = movieId,
                UserId = userId,
                Rating = rating,
                ReviewText = reviewText
            };
            var review = await _userService.AddMovieReview(newReview);
            return View();
        }
        [HttpPut]
        public async Task<IActionResult> UpdateReviewMovie(int movieId, int userId, decimal rating, string reviewText) 
        {
            ReviewRequestModel newReview = new ReviewRequestModel
            {
                MovieId = movieId,
                UserId = userId,
                Rating = rating,
                ReviewText = reviewText
            };
            var review = await _userService.UpdateMovieReview(newReview);
            return View();
        }


    }
}
