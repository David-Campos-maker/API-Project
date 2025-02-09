using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs.Review
{
    public class ReviewDto
    {
        
        public int Id { get; set; }
        // Reference to a Game 
        public int? GameId { get; set; }     
        public int Rate { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime WritedAt { get; set; } = DateTime.Now;
    }
}