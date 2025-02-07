using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs.Game
{
    public class CreateGameRequestDto
    {
        public string Name { get; set; } = string.Empty;
        public string? CoverPhoto { get; set; } = string.Empty;
        public DateOnly Published { get; set; }
        public string Platform { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
    }
}