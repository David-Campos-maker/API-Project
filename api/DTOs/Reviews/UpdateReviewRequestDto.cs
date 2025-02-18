using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs.Reviews
{
    public class UpdateReviewRequestDto
    {
        [Required]
        [Range(1 , 5)]
        public int Rate { get; set; }

        [Required]
        [MaxLength(600 , ErrorMessage = "A review can not be over 600 characters")]
        public string Content { get; set; } = string.Empty;
    }
}