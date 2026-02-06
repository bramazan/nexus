using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nexus.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePullRequestReviewSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "body",
                table: "pull_request_reviews",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "external_id",
                table: "pull_request_reviews",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "body",
                table: "pull_request_reviews");

            migrationBuilder.DropColumn(
                name: "external_id",
                table: "pull_request_reviews");
        }
    }
}
