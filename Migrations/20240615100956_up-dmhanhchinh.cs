using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLViecLam.Migrations
{
    /// <inheritdoc />
    public partial class updmhanhchinh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "DmHanhChinh");

            migrationBuilder.DropColumn(
                name: "Kv",
                table: "DmHanhChinh");

            migrationBuilder.DropColumn(
                name: "MaDvql",
                table: "DmHanhChinh");

            migrationBuilder.DropColumn(
                name: "MaQuocGia",
                table: "DmHanhChinh");

            migrationBuilder.DropColumn(
                name: "Public",
                table: "DmHanhChinh");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "DmHanhChinh",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "DmHanhChinh",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "DmHanhChinh",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Kv",
                table: "DmHanhChinh",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaDvql",
                table: "DmHanhChinh",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaQuocGia",
                table: "DmHanhChinh",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Public",
                table: "DmHanhChinh",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
