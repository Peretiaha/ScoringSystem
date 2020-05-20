using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScoringSystem.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScoringSystem.DAL.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasData(new Role
            { RoleId = 1, Name = "Admin" });
            builder.HasData(new Role
            { RoleId = 2, Name = "Customer" });
            builder.HasData(new Role
            { RoleId = 3, Name = "Manager" });
            builder.HasData(new Role
            { RoleId = 4, Name = "Insurance representative" });
        }
    }
}
