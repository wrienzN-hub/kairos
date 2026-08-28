using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kairos.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddActivityAuditEvents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "activity_audit_events",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    owner_subject = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    activity_id = table.Column<Guid>(type: "uuid", nullable: false),
                    action = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    occurred_at_utc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    details = table.Column<string>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_activity_audit_events", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_activity_audit_events_activity_id_occurred_at_utc",
                table: "activity_audit_events",
                columns: new[] { "activity_id", "occurred_at_utc" });

            migrationBuilder.CreateIndex(
                name: "IX_activity_audit_events_owner_subject_occurred_at_utc",
                table: "activity_audit_events",
                columns: new[] { "owner_subject", "occurred_at_utc" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "activity_audit_events");
        }
    }
}
