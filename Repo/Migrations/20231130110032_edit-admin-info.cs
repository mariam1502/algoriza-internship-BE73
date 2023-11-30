using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repo.Migrations
{
    /// <inheritdoc />
    public partial class editadmininfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "SecurityStamp" },
                values: new object[] { "4007ed77-a84c-4f48-b68a-1f5dff767c4d", "Admin@gmail.com", "32d3090c-3303-4248-ba2b-06e1915411a6" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "SecurityStamp" },
                values: new object[] { "0aa5411d-2a7c-49f3-b572-9a423369f8ea", null, "43be3272-9ae4-43fc-8108-211fb2ec4371" });
        }
    }
}
