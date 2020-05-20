﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ScoringSystem.DAL.Context;

namespace ScoringSystem.DAL.Migrations
{
    [DbContext(typeof(ScoringSystemContext))]
    partial class ScoringSystemContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ScoringSystem.Model.Entities.Address", b =>
                {
                    b.Property<int>("AddressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AddressLine1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AddressLine2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StateOrProvince")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AddressId");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("ScoringSystem.Model.Entities.Bank", b =>
                {
                    b.Property<int>("BankId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("LinkToSite")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BankId");

                    b.ToTable("Banks");
                });

            modelBuilder.Entity("ScoringSystem.Model.Entities.BankAccount", b =>
                {
                    b.Property<int>("BankAccountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BankId")
                        .HasColumnType("int");

                    b.Property<string>("CardNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Credit")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Debt")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("BankAccountId");

                    b.HasIndex("BankId");

                    b.HasIndex("UserId");

                    b.ToTable("BankAccounts");
                });

            modelBuilder.Entity("ScoringSystem.Model.Entities.Health", b =>
                {
                    b.Property<int>("HealthId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ArterialPressure")
                        .HasColumnType("int");

                    b.Property<int>("Bilirubin")
                        .HasColumnType("int");

                    b.Property<int>("BloodSugar")
                        .HasColumnType("int");

                    b.Property<int>("BodyTemperature")
                        .HasColumnType("int");

                    b.Property<int>("HeartRate")
                        .HasColumnType("int");

                    b.Property<int>("Hemoglobin")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfRespiratoryMovements")
                        .HasColumnType("int");

                    b.Property<int>("Weight")
                        .HasColumnType("int");

                    b.Property<int>("WhiteBloodCells")
                        .HasColumnType("int");

                    b.HasKey("HealthId");

                    b.ToTable("Healths");
                });

            modelBuilder.Entity("ScoringSystem.Model.Entities.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            RoleId = 1,
                            Name = "Admin"
                        },
                        new
                        {
                            RoleId = 2,
                            Name = "Customer"
                        },
                        new
                        {
                            RoleId = 3,
                            Name = "Manager"
                        },
                        new
                        {
                            RoleId = 4,
                            Name = "Insurance representative"
                        });
                });

            modelBuilder.Entity("ScoringSystem.Model.Entities.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AddressId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("HealthId")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Photo")
                        .HasColumnType("varbinary(max)");

                    b.HasKey("UserId");

                    b.HasIndex("AddressId")
                        .IsUnique();

                    b.HasIndex("HealthId")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ScoringSystem.Model.Entities.UsersRoles", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UsersRoles");
                });

            modelBuilder.Entity("ScoringSystem.Model.Entities.BankAccount", b =>
                {
                    b.HasOne("ScoringSystem.Model.Entities.Bank", "Bank")
                        .WithMany("BankAccounts")
                        .HasForeignKey("BankId");

                    b.HasOne("ScoringSystem.Model.Entities.User", "User")
                        .WithMany("BankAccounts")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("ScoringSystem.Model.Entities.User", b =>
                {
                    b.HasOne("ScoringSystem.Model.Entities.Address", "Address")
                        .WithOne("User")
                        .HasForeignKey("ScoringSystem.Model.Entities.User", "AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ScoringSystem.Model.Entities.Health", "UserHealth")
                        .WithOne("User")
                        .HasForeignKey("ScoringSystem.Model.Entities.User", "HealthId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ScoringSystem.Model.Entities.UsersRoles", b =>
                {
                    b.HasOne("ScoringSystem.Model.Entities.Role", "Role")
                        .WithMany("UsersRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ScoringSystem.Model.Entities.User", "User")
                        .WithMany("UsersRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
