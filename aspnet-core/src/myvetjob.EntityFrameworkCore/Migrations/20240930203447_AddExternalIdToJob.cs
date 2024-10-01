using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace myvetjob.Migrations
{
    /// <inheritdoc />
    public partial class AddExternalIdToJob : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExternalId",
                table: "Jobs",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExternalId",
                table: "Jobs");
        }
    }
}
