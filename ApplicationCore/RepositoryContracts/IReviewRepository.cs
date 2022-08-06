using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.RepositoryContracts
{
    public interface IReviewRepository
    {
        Task<List<Review>> GetAllReviews(int id);
        Task<Review> AddReview(Review review);
        Task<Review> UpdateReview(Review review);
        Task<bool> DeleteReview(int UserId, int MovieId);
        Task<bool> DoesReviewExist(int UserId, int MovieId);

    }
}
