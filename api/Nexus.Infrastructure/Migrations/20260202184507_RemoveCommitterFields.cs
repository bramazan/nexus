using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nexus.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCommitterFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "committer_email",
                table: "commits");

            migrationBuilder.DropColumn(
                name: "committer_name",
                table: "commits");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "committer_email",
                table: "commits",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "committer_name",
                table: "commits",
                type: "text",
                nullable: true);
        }
    }
}
