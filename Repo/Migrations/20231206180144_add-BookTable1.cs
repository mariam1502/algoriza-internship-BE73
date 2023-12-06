using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repo.Migrations
{
    /// <inheritdoc />
    public partial class addBookTable1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Book_TimeId",
                table: "Book");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "19a3ef68-66b7-46c0-9e7b-73a6dcc2ad29", "d8640de6-1e6b-4304-8fe1-9ae2dc378f34" });

            migrationBuilder.CreateIndex(
                name: "IX_Book_TimeId",
                table: "Book",
                column: "TimeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Book_TimeId",
                table: "Book");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "8cd3eb92-043f-41e1-b7ae-3c35ac30d08c", "dc7a74b0-07f3-4f84-9133-83f90c19ffe2" });

            migrationBuilder.CreateIndex(
                name: "IX_Book_TimeId",
                table: "Book",
                column: "TimeId",
                unique: true);
        }
    }
}
