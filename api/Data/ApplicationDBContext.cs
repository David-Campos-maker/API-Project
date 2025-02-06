using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Data {
    public class ApplicationDBContext : DbContext {
        public ApplicationDBContext(DbContextOptions dbOptions) 
        : base(dbOptions) {
            // Initialize the database options
        }

        public DbSet<Games> Games { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<User> Users { get; set; }
    }
}