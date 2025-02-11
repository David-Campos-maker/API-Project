using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace api.Controllers
{
    [Route("api/reviews")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewRepository _reviewRepository;
        public ReviewController(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReviews() {
            var reviews = await _reviewRepository.GetAllReviewsAsync();
            var reviewsDtos = reviews.Select(review => review.ToReviewDto());

            return Ok(reviewsDtos);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id) {
            var reviewModel = await _reviewRepository.GetReviewByIdAsync(id);

            if (reviewModel == null) return NotFound();

            return Ok(reviewModel.ToReviewDto());
        }
    }
}