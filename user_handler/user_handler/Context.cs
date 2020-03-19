using System;
using Microsoft.EntityFrameworkCore;
using user_handler.Model;

namespace user_handler
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> opt) : base(opt) { }

        public DbSet<user_model> user { get; set; }

        
    }
}
