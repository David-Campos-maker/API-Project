using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Reviews;
using api.Models;

namespace api.Mappers
{
    public static class ReviewMappers
    {
        public static ReviewDto ToReviewDto(this Review reviewModel) 
        {
            return new ReviewDto {
                Id = reviewModel.Id,
                GameId = reviewModel.GameId,
                Content = reviewModel.Content,
                Rate = reviewModel.Rate,
                WritedAt = reviewModel.WritedAt
            };
        }

        public static Review ToReviewFromCreateReviewDto(this CreateReviewRequestDto reviewDto)
        {
            return new Review {
                GameId = reviewDto.GameId,
                Rate = reviewDto.Rate,
                Content = reviewDto.Content,
                WritedAt = DateTime.Now
            };
        }
    }
}