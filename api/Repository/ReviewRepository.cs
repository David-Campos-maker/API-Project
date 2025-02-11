using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
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
    }
}