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
    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly MovieShopDbContext _movieShopDbContext;

        public PurchaseRepository(MovieShopDbContext movieShopDbContext)
        {
            _movieShopDbContext = movieShopDbContext;
        }

        public async Task<Purchase> AddPurchase(Purchase purchase)
        {
            _movieShopDbContext.Purchases.Add(purchase);
            await _movieShopDbContext.SaveChangesAsync();
            return purchase;
        }

        public async Task<List<Purchase>> GetAllPurchases(int id)
        {
            var purchases = await _movieShopDbContext.Purchases.Include(m => m.Movie).Where(p => p.UserId == id).ToListAsync();
            return purchases;
        }

        public async Task<Purchase> GetPurchaseDetails(int userId, int movieId)
        {
            var purchaseDetails = await _movieShopDbContext.Purchases.Include(m => m.Movie).Where(p => p.UserId == userId && p.MovieId == movieId).FirstOrDefaultAsync();
            return purchaseDetails;
        }

        public async Task<bool> IsMoviePurchased(int userId, int movieId)
        {
            var purchase = await _movieShopDbContext.Purchases.Where(p => p.UserId == userId && p.MovieId == movieId).FirstOrDefaultAsync();
            if (purchase != null) 
            {
                return true;
            }
            return false;
        }
    }
}
