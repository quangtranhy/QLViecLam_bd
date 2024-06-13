using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLViecLam.Migrations
{
    /// <inheritdoc />
    public partial class upnhankhau4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Hdld",
                table: "NhanKhau");

            migrationBuilder.DropColumn(
                name: "KhongThamGiaHdkt",
                table: "NhanKhau");

            migrationBuilder.DropColumn(
                name: "LoaiHinhNoiLamViec",
                table: "NhanKhau");

            migrationBuilder.DropColumn(
                name: "NguoiCoVieclam",
                table: "NhanKhau");

            migrationBuilder.DropColumn(
                name: "ThatNghiep",
                table: "NhanKhau");

            migrationBuilder.AlterColumn<int>(
                name: "UuTien",
                table: "NhanKhau",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ThuongTru",
                table: "NhanKhau",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ThoiGianThatNghiep",
                table: "NhanKhau",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LyDoKhongThamGia",
                table: "NhanKhau",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LyDoKhongThamGia",
                table: "NhanKhau");

            migrationBuilder.AlterColumn<string>(
                name: "UuTien",
                table: "NhanKhau",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ThuongTru",
                table: "NhanKhau",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ThoiGianThatNghiep",
                table: "NhanKhau",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Hdld",
                table: "NhanKhau",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KhongThamGiaHdkt",
                table: "NhanKhau",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LoaiHinhNoiLamViec",
                table: "NhanKhau",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NguoiCoVieclam",
                table: "NhanKhau",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ThatNghiep",
                table: "NhanKhau",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
