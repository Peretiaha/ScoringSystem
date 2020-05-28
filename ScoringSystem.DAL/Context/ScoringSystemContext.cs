using Microsoft.EntityFrameworkCore;
using ScoringSystem.DAL.Configurations;
using ScoringSystem.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScoringSystem.DAL.Context
{
    public class ScoringSystemContext : DbContext
    {
        public ScoringSystemContext(DbContextOptions<ScoringSystemContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Health> Healths { get; set; }

        public DbSet<BankAccount> BankAccounts { get; set; }

        public DbSet<Bank> Banks { get; set; }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<UsersRoles> UsersRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new UsersRolesConfiguration());
            modelBuilder.ApplyConfiguration(new UsersHealthConfiguration());
        }
    }
}
