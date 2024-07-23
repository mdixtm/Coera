﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TestProject;

#nullable disable

namespace TestProject.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240723173234_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.7");

            modelBuilder.Entity("TestProject.Models.Candidate", b =>
                {
                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<TimeSpan?>("CallTimeEnd")
                        .HasColumnType("TEXT");

                    b.Property<TimeSpan?>("CallTimeStart")
                        .HasColumnType("TEXT");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("GitHubUrl")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LinkedinUrl")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Email");

                    b.ToTable("Candidates");

                    b.HasData(
                        new
                        {
                            Email = "john.doe@example.com",
                            CallTimeEnd = new TimeSpan(0, 17, 0, 0, 0),
                            CallTimeStart = new TimeSpan(0, 9, 0, 0, 0),
                            Comment = "Looking forward to the opportunity.",
                            FirstName = "John",
                            GitHubUrl = "https://github.com/johndoe",
                            LastName = "Doe",
                            LinkedinUrl = "https://linkedin.com/in/johndoe",
                            PhoneNumber = "123-456-7890"
                        },
                        new
                        {
                            Email = "jane.smith@example.com",
                            CallTimeEnd = new TimeSpan(0, 18, 0, 0, 0),
                            CallTimeStart = new TimeSpan(0, 10, 0, 0, 0),
                            Comment = "Excited to apply!",
                            FirstName = "Jane",
                            GitHubUrl = "https://github.com/janesmith",
                            LastName = "Smith",
                            LinkedinUrl = "https://linkedin.com/in/janesmith",
                            PhoneNumber = "987-654-3210"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}