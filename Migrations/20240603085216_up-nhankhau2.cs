using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLViecLam.Migrations
{
    /// <inheritdoc />
    public partial class upnhankhau2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhanLoaiHo",
                table: "NhanKhau",
                newName: "ThuongTru2");

            migrationBuilder.AlterColumn<string>(
                name: "ThuongTru",
                table: "NhanKhau",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ThuongTru2",
                table: "NhanKhau",
                newName: "PhanLoaiHo");

            migrationBuilder.AlterColumn<int>(
                name: "ThuongTru",
                table: "NhanKhau",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
