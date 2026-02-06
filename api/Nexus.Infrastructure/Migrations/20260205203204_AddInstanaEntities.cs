using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nexus.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddInstanaEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "external_id",
                table: "incidents",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "service_metrics",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    service_id = table.Column<Guid>(type: "uuid", nullable: false),
                    time_stamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    latency = table.Column<double>(type: "double precision", nullable: true),
                    error_rate = table.Column<double>(type: "double precision", nullable: true),
                    throughput = table.Column<double>(type: "double precision", nullable: true),
                    cpu_usage = table.Column<double>(type: "double precision", nullable: true),
                    memory_usage = table.Column<double>(type: "double precision", nullable: true),
                    metric_type = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_service_metrics", x => x.id);
                    table.ForeignKey(
                        name: "fk_service_metrics_services_service_id",
                        column: x => x.service_id,
                        principalTable: "services",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_service_metrics_service_id",
                table: "service_metrics",
                column: "service_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "service_metrics");

            migrationBuilder.DropColumn(
                name: "external_id",
                table: "incidents");
        }
    }
}
