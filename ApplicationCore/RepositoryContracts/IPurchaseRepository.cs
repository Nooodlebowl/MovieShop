using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.RepositoryContracts
{
    public interface IPurchaseRepository
    {
        Task<List<Purchase>> GetAllPurchases(int id);
        Task<Purchase> AddPurchase(Purchase purchase);
        Task<Purchase> GetPurchaseDetails(int userId, int movieId);
        Task<bool> IsMoviePurchased(int userId, int movieId);
    }
}
