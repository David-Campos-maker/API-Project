using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs.Game
{
    public class UpdateGameRequestDto
    {
        [Required]
        [MaxLength(100 , ErrorMessage = "Name can not be over 100 characters")]
        public string Name { get; set; } = string.Empty;
        public string? CoverPhoto { get; set; } = string.Empty;
        public DateOnly Published { get; set; }

        [Required]
        [MaxLength(100 , ErrorMessage = "Platformcan not be over 100 characters")]
        public string Platform { get; set; } = string.Empty;

        [Required]
        [MaxLength(100 , ErrorMessage = "Gender can not be over 100 characters")]
        public string Gender { get; set; } = string.Empty;
    }
}