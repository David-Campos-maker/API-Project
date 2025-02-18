using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs.Reviews;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly ApplicationDBContext _context;
        public ReviewRepository(ApplicationDBContext context) 
        {
            _context = context;
        }

        public async Task<Review?> AddReviewAsync(Review createReview)
        {
            await _context.Reviews.AddAsync(createReview);
            await _context.SaveChangesAsync();

            return createReview;
        }

        public async Task<Review?> DeleteReviewAsync(int id)
        {
            var reviewModel = await _context.Reviews.FirstOrDefaultAsync(review => review.Id == id);

            if (reviewModel == null) return null;

            _context.Reviews.Remove(reviewModel);
            await _context.SaveChangesAsync();

            return reviewModel;
        }

        public async Task<List<Review>> GetAllReviewsAsync()
        {
            return await _context.Reviews.ToListAsync();
        }

        public async Task<Review?> GetReviewByIdAsync(int id)
        {
            var reviewModel = await _context.Reviews.FirstOrDefaultAsync(review => review.Id == id);

            if (reviewModel == null) return null;

            return reviewModel;
        }

        public async Task<Review?> UpdateReviewAsync(int id, UpdateReviewRequestDto updateReview)
        {
            var reviewModel = await _context.Reviews.FirstOrDefaultAsync(review => review.Id == id);

            if (reviewModel == null) return null;

            reviewModel.Content = updateReview.Content;
            reviewModel.Rate = updateReview.Rate;
            reviewModel.WritedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return reviewModel;
        }
    }
}