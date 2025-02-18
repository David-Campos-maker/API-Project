using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Reviews;
using api.Models;

namespace api.Interfaces
{
    public interface IReviewRepository
    {
        Task<List<Review>> GetAllReviewsAsync();
        Task<Review?> GetReviewByIdAsync(int id);
        Task<Review?> DeleteReviewAsync(int id);
        Task<Review> AddReviewAsync(Review createReview);
        Task<Review?> UpdateReviewAsync(int id , UpdateReviewRequestDto updateReview);
    }
}