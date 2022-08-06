using ApplicationCore.Entities;
using ApplicationCore.Models;
using ApplicationCore.RepositoryContracts;
using ApplicationCore.ServiesContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{       
    public class UserService : IUserService
    {
        private readonly IFavoriteRepository _favoriteRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IPurchaseRepository _purchaseRepository;

        public UserService(IPurchaseRepository purchaseRepository, IReviewRepository reviewRepository, IFavoriteRepository favoriteRepository)
        {
            _purchaseRepository = purchaseRepository;
            _reviewRepository = reviewRepository;
            _favoriteRepository = favoriteRepository;
        }

        public async Task<bool> AddFavorite(FavoriteRequestModel favoriteRequest)
        {
            var FavoriteMovie = new Favorite
            {
                MovieId = favoriteRequest.MovieId,
                UserId = favoriteRequest.UserId
            };
            await _favoriteRepository.AddFavorite(FavoriteMovie);
            return true;
        }

        public async Task<bool> AddMovieReview(ReviewRequestModel reviewRequest)
        {
            var newReview = new Review
            {
                MovieId = reviewRequest.MovieId,
                UserId = reviewRequest.UserId,
                Rating = reviewRequest.Rating,
                ReviewText = reviewRequest.ReviewText
            };
            await _reviewRepository.AddReview(newReview);
            return true;
        }

        public async Task<bool> DeleteMovieReview(int userId, int movieId)
        {
            await _reviewRepository.DeleteReview(userId, movieId);
            return true;
        }

		public async Task<bool> DoesReviewExist(int UserId, int MovieId)
		{
            var doesExist = await _reviewRepository.DoesReviewExist(UserId, MovieId);
            return doesExist;
		}

		public async Task<bool> FavoriteExists(int id, int movieId)
        {
            var doesExist = await _favoriteRepository.DoesFavoriteExist(id, movieId);
            return doesExist;
        }

        public async Task<List<FavoriteRequestModel>> GetAllFavoritesForUser(int id)
        {
            var favorites = await _favoriteRepository.GetAllFavorites(id);
            var FavoriteModel = new List<FavoriteRequestModel>();
            foreach (var favorite in favorites) 
            {
                FavoriteModel.Add(new FavoriteRequestModel { MovieId = favorite.MovieId, UserId = favorite.UserId, movie = favorite.Movie });
            }
            return FavoriteModel;
        }

        public async Task<List<PurchaseRequestModel>> GetAllPurchasesForUser(int id)
        {
            var purchases = await _purchaseRepository.GetAllPurchases(id);
            var purchasesModel = new List<PurchaseRequestModel>();
            foreach (var purchase in purchases) 
            {
                purchasesModel.Add(new PurchaseRequestModel { MovieId = purchase.MovieId, UserId = purchase.UserId, Movie = purchase.Movie});
            }
            return purchasesModel;
        }

        public async Task<List<ReviewRequestModel>> GetAllReviewsByUser(int id)
        {
            var reviews = await _reviewRepository.GetAllReviews(id);
            var ReviewModel = new List<ReviewRequestModel>();
            foreach (var review in reviews) 
            {
                ReviewModel.Add(new ReviewRequestModel { MovieId = review.MovieId, Rating = review.Rating, ReviewText = review.ReviewText, UserId = review.UserId });
            }
            return ReviewModel;
        }

        public async Task<PurchaseRequestModel> GetPurchasesDetails(int userId, int movieId)
        {
            var purchaseDetails = await _purchaseRepository.GetPurchaseDetails(userId, movieId);
            return new PurchaseRequestModel { UserId = purchaseDetails.UserId, Movie = purchaseDetails.Movie, MovieId = purchaseDetails.MovieId };
        }

        public async Task<bool> IsMoviePurchased(PurchaseRequestModel purchaseRequest, int userId)
        {
            var isPurchased = await _purchaseRepository.IsMoviePurchased(userId, purchaseRequest.MovieId);
            return isPurchased;
        }

        public async Task<bool> PurchaseMovie(PurchaseRequestModel purchaseRequest, int userId)
        {
            var purchase = new Purchase
            {
                UserId = userId,
                MovieId = purchaseRequest.MovieId,
                PurchaseNumber = purchaseRequest.PurchaseNumber.ToString(),
                TotalPrice = purchaseRequest.Price,
                PurchaseDateTime = purchaseRequest.PurchaseDateTime
            };
            await _purchaseRepository.AddPurchase(purchase);
            return true;
        }

        public async Task<bool> RemoveFavorite(FavoriteRequestModel favoriteRequest)
        {
            await _favoriteRepository.removeFavorite(favoriteRequest);
            return true;

        }

        public async Task<bool> UpdateMovieReview(ReviewRequestModel reviewRequest)
        {
            var newReview = new Review
            {
                MovieId = reviewRequest.MovieId,
                UserId = reviewRequest.UserId,
                CreatedDate = reviewRequest.CreatedDate,
                Rating = reviewRequest.Rating,
                ReviewText = reviewRequest.ReviewText
            };
            await _reviewRepository.UpdateReview(newReview);
            return true;
        }
    }
}
