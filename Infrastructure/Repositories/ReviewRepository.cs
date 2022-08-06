using ApplicationCore.Entities;
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
    public class ReviewRepository: IReviewRepository
    {
        private readonly MovieShopDbContext _movieShopDbContext;

        public ReviewRepository(MovieShopDbContext movieShopDbContext)
        {
            _movieShopDbContext = movieShopDbContext;
        }

        public async Task<Review> AddReview(Review review)
        {
            _movieShopDbContext.Reviews.Add(review);
            await _movieShopDbContext.SaveChangesAsync();
            return review;
        }

        public async Task<bool> DeleteReview(int UserId, int MovieId)
        {
            var review = _movieShopDbContext.Reviews.FirstOrDefault(x => x.UserId == UserId && x.MovieId == MovieId);
            _movieShopDbContext.Reviews.Remove(review);
            await _movieShopDbContext.SaveChangesAsync();
            return true;
        }

		public async Task<bool> DoesReviewExist(int UserId, int MovieId)
		{
            var reviewMovie = await _movieShopDbContext.Reviews.FirstOrDefaultAsync(x => x.UserId == UserId && x.MovieId == MovieId);
            if (reviewMovie != null)
            {
                return true;
            }
            return false;
        }

		public async Task<List<Review>> GetAllReviews(int id)
        {
            var reviews = await _movieShopDbContext.Reviews.Where(r => r.UserId == id).ToListAsync();
            return reviews;
        }

        public async Task<Review> UpdateReview(Review review)
        {
            _movieShopDbContext.Reviews.Update(review);
            await _movieShopDbContext.SaveChangesAsync();
            return review;
        }
    }
}
