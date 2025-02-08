using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Routing;

namespace api.Models {
    public class Review {
        public int Id { get; set; }
        // Reference to a Game 
        public int? GameId { get; set; }
        //Navigation Property
        public Games? Game { get; set; }
        // Reference to a User         
        public int Rate { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime WritedAt { get; set; } = DateTime.Now;
    }
}