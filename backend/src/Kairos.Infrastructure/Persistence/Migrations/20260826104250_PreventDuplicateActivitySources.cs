using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kairos.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class PreventDuplicateActivitySources : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_activities_owner_subject_content_hash_sha256",
                table: "activities",
                columns: new[] { "owner_subject", "content_hash_sha256" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_activities_owner_subject_content_hash_sha256",
                table: "activities");
        }
    }
}
