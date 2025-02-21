using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data {
    // Extends IdentityDbContext with the AppUser Model
    public class ApplicationDBContext : IdentityDbContext<AppUser> 
    {
        public ApplicationDBContext(DbContextOptions dbOptions) 
        : base(dbOptions) 
        {
            // Initialize the database options
        }

        public DbSet<Games> Games { get; set; }
        public DbSet<Review> Reviews { get; set; }
    }
}