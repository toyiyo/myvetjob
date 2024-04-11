using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace myvetjob.Migrations
{
    /// <inheritdoc />
    public partial class RemoveDescriptionfromindex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Jobs_CompanyName_Position_JobLocation_Description_OrderStat~",
                table: "Jobs");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_CompanyName_Position_JobLocation_OrderStatus",
                table: "Jobs",
                columns: new[] { "CompanyName", "Position", "JobLocation", "OrderStatus" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Jobs_CompanyName_Position_JobLocation_OrderStatus",
                table: "Jobs");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_CompanyName_Position_JobLocation_Description_OrderStat~",
                table: "Jobs",
                columns: new[] { "CompanyName", "Position", "JobLocation", "Description", "OrderStatus" });
        }
    }
}
