using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kairos.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddActivities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "activities",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    owner_subject = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    source_upload_id = table.Column<Guid>(type: "uuid", nullable: false),
                    activity_type = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    start_utc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    end_utc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    source_kind = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    source_provider = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    original_file_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    content_hash_sha256 = table.Column<string>(type: "character(64)", fixedLength: true, maxLength: 64, nullable: true),
                    imported_at_utc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    document = table.Column<string>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_activities", x => x.id);
                    table.ForeignKey(
                        name: "FK_activities_fit_uploads_source_upload_id",
                        column: x => x.source_upload_id,
                        principalTable: "fit_uploads",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_activities_owner_subject_start_utc",
                table: "activities",
                columns: new[] { "owner_subject", "start_utc" });

            migrationBuilder.CreateIndex(
                name: "IX_activities_source_upload_id",
                table: "activities",
                column: "source_upload_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "activities");
        }
    }
}
