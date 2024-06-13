using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLViecLam.Migrations
{
    /// <inheritdoc />
    public partial class upnhankhau : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bhxh",
                table: "NhanKhau");

            migrationBuilder.DropColumn(
                name: "Cccd",
                table: "NhanKhau");

            migrationBuilder.DropColumn(
                name: "ChuyenMonKyThuat",
                table: "NhanKhau");

            migrationBuilder.DropColumn(
                name: "DiaChi",
                table: "NhanKhau");

            migrationBuilder.DropColumn(
                name: "Ho",
                table: "NhanKhau");

            migrationBuilder.DropColumn(
                name: "HoTen",
                table: "NhanKhau");

            migrationBuilder.DropColumn(
                name: "Mqh",
                table: "NhanKhau");

            migrationBuilder.DropColumn(
                name: "NgaySinh",
                table: "NhanKhau");

            migrationBuilder.DropColumn(
                name: "TinhTrangHdkt",
                table: "NhanKhau");

            migrationBuilder.RenameColumn(
                name: "ThamGiaBhxh",
                table: "NhanKhau",
                newName: "ThamGiaBHXH");

            migrationBuilder.RenameColumn(
                name: "GioiTinh",
                table: "NhanKhau",
                newName: "Gioitinh");

            migrationBuilder.RenameColumn(
                name: "TrinhDoGiaoDuc",
                table: "NhanKhau",
                newName: "HoVaTen");

            migrationBuilder.AlterColumn<int>(
                name: "ThuongTru",
                table: "NhanKhau",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ThamGiaBHXH",
                table: "NhanKhau",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Gioitinh",
                table: "NhanKhau",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DanToc",
                table: "NhanKhau",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaBHXH",
                table: "NhanKhau",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayThangNamSinh",
                table: "NhanKhau",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NoiOHienTai",
                table: "NhanKhau",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PhanLoaiHo",
                table: "NhanKhau",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "QuanHe",
                table: "NhanKhau",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SoCCCD",
                table: "NhanKhau",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SoDinhDanhHoGD",
                table: "NhanKhau",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TinhTrangVL",
                table: "NhanKhau",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TrinhDoCMKT",
                table: "NhanKhau",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TrinhDoHV",
                table: "NhanKhau",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaBHXH",
                table: "NhanKhau");

            migrationBuilder.DropColumn(
                name: "NgayThangNamSinh",
                table: "NhanKhau");

            migrationBuilder.DropColumn(
                name: "NoiOHienTai",
                table: "NhanKhau");

            migrationBuilder.DropColumn(
                name: "PhanLoaiHo",
                table: "NhanKhau");

            migrationBuilder.DropColumn(
                name: "QuanHe",
                table: "NhanKhau");

            migrationBuilder.DropColumn(
                name: "SoCCCD",
                table: "NhanKhau");

            migrationBuilder.DropColumn(
                name: "SoDinhDanhHoGD",
                table: "NhanKhau");

            migrationBuilder.DropColumn(
                name: "TinhTrangVL",
                table: "NhanKhau");

            migrationBuilder.DropColumn(
                name: "TrinhDoCMKT",
                table: "NhanKhau");

            migrationBuilder.DropColumn(
                name: "TrinhDoHV",
                table: "NhanKhau");

            migrationBuilder.RenameColumn(
                name: "ThamGiaBHXH",
                table: "NhanKhau",
                newName: "ThamGiaBhxh");

            migrationBuilder.RenameColumn(
                name: "Gioitinh",
                table: "NhanKhau",
                newName: "GioiTinh");

            migrationBuilder.RenameColumn(
                name: "HoVaTen",
                table: "NhanKhau",
                newName: "TrinhDoGiaoDuc");

            migrationBuilder.AlterColumn<string>(
                name: "ThuongTru",
                table: "NhanKhau",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ThamGiaBhxh",
                table: "NhanKhau",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "GioiTinh",
                table: "NhanKhau",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DanToc",
                table: "NhanKhau",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Bhxh",
                table: "NhanKhau",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cccd",
                table: "NhanKhau",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ChuyenMonKyThuat",
                table: "NhanKhau",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DiaChi",
                table: "NhanKhau",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ho",
                table: "NhanKhau",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HoTen",
                table: "NhanKhau",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Mqh",
                table: "NhanKhau",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NgaySinh",
                table: "NhanKhau",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TinhTrangHdkt",
                table: "NhanKhau",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
