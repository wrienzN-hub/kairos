using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kairos.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddActivityListMetadata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "analysis_status",
                table: "activities",
                type: "character varying(32)",
                maxLength: 32,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "distance_meters",
                table: "activities",
                type: "numeric(14,3)",
                precision: 14,
                scale: 3,
                nullable: true);

            migrationBuilder.Sql(
                """
                UPDATE activities
                SET analysis_status = CASE
                    WHEN EXISTS (
                        SELECT 1
                        FROM jsonb_array_elements(document -> 'qualityFindings') finding
                        WHERE finding ->> 'severity' = 'error'
                    ) THEN 'blocked'
                    WHEN EXISTS (
                        SELECT 1
                        FROM jsonb_array_elements(document -> 'qualityFindings') finding
                        WHERE finding ->> 'severity' = 'warning'
                    ) THEN 'limited'
                    ELSE 'eligible'
                END,
                distance_meters = (
                    SELECT (metric ->> 'value')::numeric
                    FROM jsonb_array_elements(document -> 'summary') metric
                    WHERE metric ->> 'code' = 'distance'
                    LIMIT 1
                );
                """
            );

            migrationBuilder.AlterColumn<string>(
                name: "analysis_status",
                table: "activities",
                type: "character varying(32)",
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(32)",
                oldMaxLength: 32,
                oldNullable: true
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "analysis_status",
                table: "activities");

            migrationBuilder.DropColumn(
                name: "distance_meters",
                table: "activities");
        }
    }
}
