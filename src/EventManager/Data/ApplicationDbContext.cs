﻿using System;
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
        public string Id { get; set; }
        public ApplicationUser User { get; set; }
        public int EventID { get; set; }
        public Event Event { get; set; }
    }
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<Attendance> Attendance { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Attendance>()
               .HasKey(p => new { p.Id, p.EventID });

            builder.Entity<Attendance>()
                .HasOne(p => p.User)
                .WithMany(p => p.Events)
                .HasForeignKey(p => p.Id);

            builder.Entity<Attendance>()
                .HasOne(p => p.Event)
                .WithMany(p => p.Users)
                .HasForeignKey(p => p.EventID);
        }
    }
}
