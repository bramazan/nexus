using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nexus.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateReleaseEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "author_id",
                table: "releases",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "commit_sha",
                table: "releases",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "tag_name",
                table: "releases",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_releases_author_id",
                table: "releases",
                column: "author_id");

            migrationBuilder.AddForeignKey(
                name: "fk_releases_users_author_id",
                table: "releases",
                column: "author_id",
                principalTable: "users",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_releases_users_author_id",
                table: "releases");

            migrationBuilder.DropIndex(
                name: "ix_releases_author_id",
                table: "releases");

            migrationBuilder.DropColumn(
                name: "author_id",
                table: "releases");

            migrationBuilder.DropColumn(
                name: "commit_sha",
                table: "releases");

            migrationBuilder.DropColumn(
                name: "tag_name",
                table: "releases");
        }
    }
}
