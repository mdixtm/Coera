using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TestProject.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Candidates",
                columns: table => new
                {
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    Comment = table.Column<string>(type: "TEXT", nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: false),
                    LinkedinUrl = table.Column<string>(type: "TEXT", nullable: false),
                    GitHubUrl = table.Column<string>(type: "TEXT", nullable: false),
                    CallTimeStart = table.Column<TimeSpan>(type: "TEXT", nullable: true),
                    CallTimeEnd = table.Column<TimeSpan>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidates", x => x.Email);
                });

            migrationBuilder.InsertData(
                table: "Candidates",
                columns: new[] { "Email", "CallTimeEnd", "CallTimeStart", "Comment", "FirstName", "GitHubUrl", "LastName", "LinkedinUrl", "PhoneNumber" },
                values: new object[,]
                {
                    { "jane.smith@example.com", new TimeSpan(0, 18, 0, 0, 0), new TimeSpan(0, 10, 0, 0, 0), "Excited to apply!", "Jane", "https://github.com/janesmith", "Smith", "https://linkedin.com/in/janesmith", "987-654-3210" },
                    { "john.doe@example.com", new TimeSpan(0, 17, 0, 0, 0), new TimeSpan(0, 9, 0, 0, 0), "Looking forward to the opportunity.", "John", "https://github.com/johndoe", "Doe", "https://linkedin.com/in/johndoe", "123-456-7890" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Candidates");
        }
    }
}
