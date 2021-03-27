using JudgeSystem.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace JudgeSystem.Entities
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Team> Teams { get; set; }
        public DbSet<Problem> Problems { get; set; }
        public DbSet<Solution> Solutions { get; set; }
        public DbSet<CodeFile> CodeFiles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var appUser = new IdentityUser
            {
                UserName = "Admin",
                EmailConfirmed = true,
                NormalizedUserName = "ADMIN"
            };

            PasswordHasher<IdentityUser> hasher = new PasswordHasher<IdentityUser>();
            appUser.PasswordHash = hasher.HashPassword(appUser, "Admin");

            builder.Entity<IdentityUser>().HasData(appUser);
        }
    }
}
