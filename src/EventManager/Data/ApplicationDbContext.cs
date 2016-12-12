using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EventManager.Models;

namespace EventManager.Data
{
    public class Attendance                         
    {
        public string UserID { get; set; }
        public ApplicationUser User { get; set; }
        public int EventID { get; set; }
        public Event Event { get; set; }
    }
    public class Following
    {
        public string FollowerID { get; set; }
        public ApplicationUser Follower { get; set; }
        public string ArtistID { get; set; }
        public ApplicationUser Artist { get; set; }
    }
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<Attendance> Attendance { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Following> Following { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Attendance>()
               .HasKey(p => new { p.UserID, p.EventID });

            builder.Entity<Attendance>()
                .HasOne(p => p.User)
                .WithMany(p => p.Events)
                .HasForeignKey(p => p.UserID);

            builder.Entity<Attendance>()
                .HasOne(p => p.Event)
                .WithMany(p => p.Users)
                .HasForeignKey(p => p.EventID);


            builder.Entity<Following>()
              .HasKey(p => new { p.FollowerID, p.ArtistID });

            builder.Entity<Following>()
                .HasOne(p => p.Follower)
                .WithMany(p => p.Artists)
                .HasForeignKey(p => p.FollowerID);

            builder.Entity<Following>()
                .HasOne(p => p.Artist)
                .WithMany(p => p.Followers)
                .HasForeignKey(p => p.ArtistID);
        }
    }
}
