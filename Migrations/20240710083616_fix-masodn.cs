using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLViecLam.Migrations
{
    /// <inheritdoc />
    public partial class fixmasodn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Company",
                table: "NguoiLaoDong");

            migrationBuilder.DropColumn(
                name: "Dkkd",
                table: "Company");

            migrationBuilder.AddColumn<string>(
                name: "MaDn",
                table: "NguoiLaoDong",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaDn",
                table: "NguoiLaoDong");

            migrationBuilder.AddColumn<int>(
                name: "Company",
                table: "NguoiLaoDong",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Dkkd",
                table: "Company",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
