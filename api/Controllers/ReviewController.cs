using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Reviews;
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

        [HttpPost]
        [Route("addReview")]
        public async Task<IActionResult> CreateReview([FromBody] CreateReviewRequestDto createReviewRequestDto) {
            var reviewModel = createReviewRequestDto.ToReviewFromCreateReviewDto();

            await _reviewRepository.AddReviewAsync(reviewModel);

            return CreatedAtAction(nameof(GetById) , new { id = reviewModel.Id } , reviewModel.ToReviewDto());
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateReview([FromRoute] int id , [FromBody] UpdateReviewRequestDto updateRequest) {
            var reviewModel = await _reviewRepository.UpdateReviewAsync(id , updateRequest);

            if (reviewModel == null) return NotFound();

            return Ok(reviewModel.ToReviewDto());
        }

        [HttpDelete] 
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteById([FromRoute] int id) {
            var reviewModel = await _reviewRepository.DeleteReviewAsync(id);

            if (reviewModel == null) return NotFound();

            return Ok(reviewModel.ToReviewDto());
        }
    }
}