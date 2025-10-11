using GameOria.Application.Stores.DTOs;
using GameOria.Domains.Entities.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOria.Application.Stores.Service
{
    public interface IStoreReviewRepository
    {
        //Task<List<StoreReviewDto>> GetAllAsync(Guid storeId);
        //Task<StoreReviewDto?> GetByIdAsync(Guid id);
        Task<StoreReview> CreateAsync(StoreReview review);
        Task UpdateAsync(StoreReview review);
        Task DeleteAsync(Guid id);
        Task ApproveReviewAsync(Guid id, string approvedBy);
    }
}
