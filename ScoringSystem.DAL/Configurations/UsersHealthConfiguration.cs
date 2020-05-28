using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using ScoringSystem.Model.Entities;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ScoringSystem.DAL.Configurations
{
    public class UsersHealthConfiguration : IEntityTypeConfiguration<UsersHealth>
    {
        public void Configure(EntityTypeBuilder<UsersHealth> builder)
        {
            builder.HasOne(s => s.User).WithMany(sc => sc.UsersHealth)
               .HasForeignKey(s => s.UserId);
            builder.HasOne(s => s.Health).WithMany(sc => sc.UsersHealth)
                .HasForeignKey(s => s.HealthId);
            builder.HasKey(sc => new { sc.UserId, sc.HealthId });
        }
    }
}
