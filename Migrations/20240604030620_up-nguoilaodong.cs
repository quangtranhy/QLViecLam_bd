using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLViecLam.Migrations
{
    /// <inheritdoc />
    public partial class upnguoilaodong : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "NguoiLaoDong");

            migrationBuilder.DropColumn(
                name: "Ccnd",
                table: "NguoiLaoDong");

            migrationBuilder.DropColumn(
                name: "NgaySinh",
                table: "NguoiLaoDong");

            migrationBuilder.RenameColumn(
                name: "TrinhDoCmkt",
                table: "NguoiLaoDong",
                newName: "TrinhDoCMKT");

            migrationBuilder.RenameColumn(
                name: "GioiTinh",
                table: "NguoiLaoDong",
                newName: "Gioitinh");

            migrationBuilder.RenameColumn(
                name: "TrinhDoGiaoDuc",
                table: "NguoiLaoDong",
                newName: "HoVaTen");

            migrationBuilder.RenameColumn(
                name: "HoTen",
                table: "NguoiLaoDong",
                newName: "DiaChiCuThe");

            migrationBuilder.AlterColumn<int>(
                name: "TrinhDoCMKT",
                table: "NguoiLaoDong",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SoBaoHiem",
                table: "NguoiLaoDong",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PcThamNienNghe",
                table: "NguoiLaoDong",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PcThamNien",
                table: "NguoiLaoDong",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PcChucVu",
                table: "NguoiLaoDong",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Luong",
                table: "NguoiLaoDong",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Gioitinh",
                table: "NguoiLaoDong",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DanToc",
                table: "NguoiLaoDong",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayThangNamSinh",
                table: "NguoiLaoDong",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SoCCCD",
                table: "NguoiLaoDong",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TrinhDoHV",
                table: "NguoiLaoDong",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NgayThangNamSinh",
                table: "NguoiLaoDong");

            migrationBuilder.DropColumn(
                name: "SoCCCD",
                table: "NguoiLaoDong");

            migrationBuilder.DropColumn(
                name: "TrinhDoHV",
                table: "NguoiLaoDong");

            migrationBuilder.RenameColumn(
                name: "TrinhDoCMKT",
                table: "NguoiLaoDong",
                newName: "TrinhDoCmkt");

            migrationBuilder.RenameColumn(
                name: "Gioitinh",
                table: "NguoiLaoDong",
                newName: "GioiTinh");

            migrationBuilder.RenameColumn(
                name: "HoVaTen",
                table: "NguoiLaoDong",
                newName: "TrinhDoGiaoDuc");

            migrationBuilder.RenameColumn(
                name: "DiaChiCuThe",
                table: "NguoiLaoDong",
                newName: "HoTen");

            migrationBuilder.AlterColumn<string>(
                name: "TrinhDoCmkt",
                table: "NguoiLaoDong",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SoBaoHiem",
                table: "NguoiLaoDong",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PcThamNienNghe",
                table: "NguoiLaoDong",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PcThamNien",
                table: "NguoiLaoDong",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PcChucVu",
                table: "NguoiLaoDong",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Luong",
                table: "NguoiLaoDong",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "GioiTinh",
                table: "NguoiLaoDong",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DanToc",
                table: "NguoiLaoDong",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "NguoiLaoDong",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ccnd",
                table: "NguoiLaoDong",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgaySinh",
                table: "NguoiLaoDong",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
