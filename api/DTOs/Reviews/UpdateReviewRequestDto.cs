using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs.Reviews
{
    public class UpdateReviewRequestDto
    {
        public int Rate { get; set; }
        public string Content { get; set; } = string.Empty;
    }
}