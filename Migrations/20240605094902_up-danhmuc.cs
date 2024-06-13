using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLViecLam.Migrations
{
    /// <inheritdoc />
    public partial class updanhmuc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DmDanToc");

            migrationBuilder.DropTable(
                name: "DmNghanhHoc");

            migrationBuilder.DropTable(
                name: "DmQuocGia");

            migrationBuilder.DropTable(
                name: "DmTinhTrangThamGiaHdkt");

            migrationBuilder.DropTable(
                name: "DmTinhTrangThamGiaHdktCt");

            migrationBuilder.DropTable(
                name: "DmTinhTrangThamGiaHdktCt2");

            migrationBuilder.DropTable(
                name: "DmTinhTrangViecLam");

            migrationBuilder.AddColumn<string>(
                name: "TrangThai",
                table: "TrinhDoHV",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TrangThai",
                table: "TrinhDoCMKT",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DanToc",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaDanToc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenDanToc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrangThai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DanToc", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NganhHoc",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaNganhHoc = table.Column<int>(type: "int", nullable: true),
                    TenNganhHoc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrangThai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NganhHoc", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuocGia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaQuocGia = table.Column<int>(type: "int", nullable: true),
                    TenQuocGia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrangThai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuocGia", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TinhTrangVL",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaTinhTrangVL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenTinhTrangVL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrangThai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TinhTrangVL", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DanToc");

            migrationBuilder.DropTable(
                name: "NganhHoc");

            migrationBuilder.DropTable(
                name: "QuocGia");

            migrationBuilder.DropTable(
                name: "TinhTrangVL");

            migrationBuilder.DropColumn(
                name: "TrangThai",
                table: "TrinhDoHV");

            migrationBuilder.DropColumn(
                name: "TrangThai",
                table: "TrinhDoCMKT");

            migrationBuilder.CreateTable(
                name: "DmDanToc",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaDanToc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenDanToc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DmDanToc", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DmNghanhHoc",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaNghanhHoc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenNghanhHoc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DmNghanhHoc", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DmQuocGia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaQuocGia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenQuocGia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DmQuocGia", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DmTinhTrangThamGiaHdkt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaDmTgkt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stt = table.Column<int>(type: "int", nullable: false),
                    TenTgkt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DmTinhTrangThamGiaHdkt", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DmTinhTrangThamGiaHdktCt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaDmTgktCt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaNhom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stt = table.Column<int>(type: "int", nullable: false),
                    TenTgktCt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DmTinhTrangThamGiaHdktCt", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DmTinhTrangThamGiaHdktCt2",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaDmTgktCt2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaNhom2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stt = table.Column<int>(type: "int", nullable: false),
                    TenTgktCt2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DmTinhTrangThamGiaHdktCt2", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DmTinhTrangViecLam",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaTinhTrangViecLam = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenTinhTrangViecLam = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DmTinhTrangViecLam", x => x.Id);
                });
        }
    }
}
