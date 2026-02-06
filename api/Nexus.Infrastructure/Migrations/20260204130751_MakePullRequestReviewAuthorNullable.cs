using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nexus.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MakePullRequestReviewAuthorNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_pull_request_reviews_tool_accounts_author_tool_account_id",
                table: "pull_request_reviews");

            migrationBuilder.AlterColumn<Guid>(
                name: "author_tool_account_id",
                table: "pull_request_reviews",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "fk_pull_request_reviews_tool_accounts_author_tool_account_id",
                table: "pull_request_reviews",
                column: "author_tool_account_id",
                principalTable: "tool_accounts",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_pull_request_reviews_tool_accounts_author_tool_account_id",
                table: "pull_request_reviews");

            migrationBuilder.AlterColumn<Guid>(
                name: "author_tool_account_id",
                table: "pull_request_reviews",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "fk_pull_request_reviews_tool_accounts_author_tool_account_id",
                table: "pull_request_reviews",
                column: "author_tool_account_id",
                principalTable: "tool_accounts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
