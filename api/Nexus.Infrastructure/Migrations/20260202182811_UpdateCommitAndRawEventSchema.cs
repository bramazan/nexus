using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nexus.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCommitAndRawEventSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "error_message",
                table: "raw_events",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "processed_at",
                table: "raw_events",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "status",
                table: "raw_events",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "author_name",
                table: "commits",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "authored_date",
                table: "commits",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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

            migrationBuilder.AddColumn<Guid>(
                name: "user_id",
                table: "commits",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "web_url",
                table: "commits",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_commits_user_id",
                table: "commits",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "fk_commits_users_user_id",
                table: "commits",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_commits_users_user_id",
                table: "commits");

            migrationBuilder.DropIndex(
                name: "ix_commits_user_id",
                table: "commits");

            migrationBuilder.DropColumn(
                name: "error_message",
                table: "raw_events");

            migrationBuilder.DropColumn(
                name: "processed_at",
                table: "raw_events");

            migrationBuilder.DropColumn(
                name: "status",
                table: "raw_events");

            migrationBuilder.DropColumn(
                name: "author_name",
                table: "commits");

            migrationBuilder.DropColumn(
                name: "authored_date",
                table: "commits");

            migrationBuilder.DropColumn(
                name: "committer_email",
                table: "commits");

            migrationBuilder.DropColumn(
                name: "committer_name",
                table: "commits");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "commits");

            migrationBuilder.DropColumn(
                name: "web_url",
                table: "commits");
        }
    }
}
