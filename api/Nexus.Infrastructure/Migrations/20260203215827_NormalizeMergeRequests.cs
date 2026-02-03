using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nexus.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NormalizeMergeRequests : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "pull_requests",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "first_review_at",
                table: "pull_requests",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "source_branch",
                table: "pull_requests",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "target_branch",
                table: "pull_requests",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "description",
                table: "pull_requests");

            migrationBuilder.DropColumn(
                name: "first_review_at",
                table: "pull_requests");

            migrationBuilder.DropColumn(
                name: "source_branch",
                table: "pull_requests");

            migrationBuilder.DropColumn(
                name: "target_branch",
                table: "pull_requests");
        }
    }
}
