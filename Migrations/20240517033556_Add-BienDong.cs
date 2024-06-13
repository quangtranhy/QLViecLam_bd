using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLViecLam.Migrations
{
    /// <inheritdoc />
    public partial class AddBienDong : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BienDong",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KyDieuTra = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    User = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhanLoai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoaiBang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoLuong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThoiDiem = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BienDong", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BienDong");
        }
    }
}
