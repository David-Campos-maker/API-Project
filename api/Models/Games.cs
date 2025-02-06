using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models {
    public class Games {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? CoverPhoto { get; set; } = string.Empty;
        public DateOnly Published { get; set; }
        public string Platform { get; set; } = string.Empty;
        public List<string> Genders { get; set; } = new List<string>();
        public List<Review> Reviews { get; set; } = new List<Review>();
    }
}