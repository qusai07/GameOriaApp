using GameOria.Api.Repo.Interface;
using GameOria.Application.Stores.DTOs;
using GameOria.Domains.Entities.Stores;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOria.Application.Stores.Service
{
    public class StoreReviewRepository : IStoreReviewRepository
    {
        private readonly IDataService _dataService;

        public StoreReviewRepository(IDataService dataService)
        {
            _dataService = dataService;
        }

        //public async Task<List<StoreReviewDto>> GetAllAsync(Guid storeId)
        //{
        //    return await _dataService.Query<StoreReview>()
        //        .Include(r => r.Store)
        //        .Where(r => r.StoreId == storeId)
        //        .Select(r => new StoreReviewDto
        //        {
        //            Id = r.Id,
        //            StoreId = r.StoreId,
        //            StoreName = r.Store.Name,
        //            UserId = r.UserId,
        //            Rating = r.Rating,
        //            Title = r.Title,
        //            Content = r.Content,
        //            IsApproved = r.IsApproved,
        //            ApprovedAt = r.ApprovedAt,
        //            ApprovedBy = r.ApprovedBy
        //        }).ToListAsync();
        //}

        //public async Task<StoreReviewDto?> GetByIdAsync(Guid id)
        //{
        //    return await _dataService.Query<StoreReview>()
        //        .Include(r => r.Store)
        //        .Where(r => r.Id == id)
        //        .Select(r => new StoreReviewDto
        //        {
        //            Id = r.Id,
        //            StoreId = r.StoreId,
        //            StoreName = r.Store.Name,
        //            UserId = r.UserId,
        //            Rating = r.Rating,
        //            Title = r.Title,
        //            Content = r.Content,
        //            IsApproved = r.IsApproved,
        //            ApprovedAt = r.ApprovedAt,
        //            ApprovedBy = r.ApprovedBy
        //        }).FirstOrDefaultAsync();
        //}

        public async Task<StoreReview> CreateAsync(StoreReview review)
        {
            await _dataService.AddAsync(review);
            await _dataService.SaveAsync();
            return review;
        }

        public async Task UpdateAsync(StoreReview review)
        {
            _dataService.UpdateAsync(review);
            await _dataService.SaveAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var review = await _dataService.GetByIdAsync<StoreReview>(id);
            if (review != null)
            {
                _dataService.DeleteAsync<StoreReview>(review);
                await _dataService.SaveAsync();
            }
        }

        public async Task ApproveReviewAsync(Guid id, string approvedBy)
        {
            var review = await _dataService.GetByIdAsync<StoreReview>(id);
            if (review != null)
            {
                review.IsApproved = true;
                review.ApprovedAt = DateTime.UtcNow;
                review.ApprovedBy = approvedBy;
                _dataService.UpdateAsync(review);
                await _dataService.SaveAsync();
            }
        }
    }
}
