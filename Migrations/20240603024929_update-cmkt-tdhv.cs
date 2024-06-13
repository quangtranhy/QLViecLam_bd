using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLViecLam.Migrations
{
    /// <inheritdoc />
    public partial class updatecmkttdhv : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DmTrinhDoGdpt");

            migrationBuilder.DropTable(
                name: "DmTrinhDoHocVan");

            migrationBuilder.DropTable(
                name: "DmTrinhDoKyThuat");

            migrationBuilder.CreateTable(
                name: "TrinhDoCMKT",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaTrinhDoCMKT = table.Column<int>(type: "int", nullable: true),
                    TenTrinhDoCMKT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrinhDoCMKT", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrinhDoHV",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaTrinhDoHV = table.Column<int>(type: "int", nullable: true),
                    TenTrinhDoHV = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrinhDoHV", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrinhDoCMKT");

            migrationBuilder.DropTable(
                name: "TrinhDoHV");

            migrationBuilder.CreateTable(
                name: "DmTrinhDoGdpt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaDmGdpt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenGdpt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DmTrinhDoGdpt", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DmTrinhDoHocVan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaTrinhDoHocVan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenTrinhDoHocVan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DmTrinhDoHocVan", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DmTrinhDoKyThuat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaDmTdkt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stt = table.Column<int>(type: "int", nullable: false),
                    Tentdkt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DmTrinhDoKyThuat", x => x.Id);
                });
        }
    }
}
