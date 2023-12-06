using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repo.Migrations
{
    /// <inheritdoc />
    public partial class addBookTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TimeId = table.Column<int>(type: "int", nullable: false),
                    CouponId = table.Column<int>(type: "int", nullable: true),
                    Request = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Book_AspNetUsers_PatientId",
                        column: x => x.PatientId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Book_Coupons_CouponId",
                        column: x => x.CouponId,
                        principalTable: "Coupons",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Book_Time_TimeId",
                        column: x => x.TimeId,
                        principalTable: "Time",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "8cd3eb92-043f-41e1-b7ae-3c35ac30d08c", "dc7a74b0-07f3-4f84-9133-83f90c19ffe2" });

            migrationBuilder.CreateIndex(
                name: "IX_Book_CouponId",
                table: "Book",
                column: "CouponId");

            migrationBuilder.CreateIndex(
                name: "IX_Book_PatientId",
                table: "Book",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Book_TimeId",
                table: "Book",
                column: "TimeId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Book");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "bbcedbae-4355-4eb8-b980-c475488ba4f5", "61b76443-9429-4ff0-8eec-4c6a96272224" });
        }
    }
}
