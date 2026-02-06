using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nexus.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SupportMergeRequestChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_code_changes_commits_commit_id",
                table: "code_changes");

            migrationBuilder.AlterColumn<Guid>(
                name: "commit_id",
                table: "code_changes",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<Guid>(
                name: "pull_request_id",
                table: "code_changes",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_code_changes_pull_request_id",
                table: "code_changes",
                column: "pull_request_id");

            migrationBuilder.AddForeignKey(
                name: "fk_code_changes_commits_commit_id",
                table: "code_changes",
                column: "commit_id",
                principalTable: "commits",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_code_changes_pull_requests_pull_request_id",
                table: "code_changes",
                column: "pull_request_id",
                principalTable: "pull_requests",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_code_changes_commits_commit_id",
                table: "code_changes");

            migrationBuilder.DropForeignKey(
                name: "fk_code_changes_pull_requests_pull_request_id",
                table: "code_changes");

            migrationBuilder.DropIndex(
                name: "ix_code_changes_pull_request_id",
                table: "code_changes");

            migrationBuilder.DropColumn(
                name: "pull_request_id",
                table: "code_changes");

            migrationBuilder.AlterColumn<Guid>(
                name: "commit_id",
                table: "code_changes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "fk_code_changes_commits_commit_id",
                table: "code_changes",
                column: "commit_id",
                principalTable: "commits",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
