using System;
using Microsoft.EntityFrameworkCore;
using notification_handler.Model;

namespace notification_handler
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> opt) : base(opt) { }

        public DbSet<notif_model> notif { get; set; }

        public DbSet<notif_logs_model> notiflog { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<notif_logs_model>()
                .HasOne(X => X.notification)
                .WithMany()
                .HasForeignKey(X => X.notification_id);
        }

    }
}
