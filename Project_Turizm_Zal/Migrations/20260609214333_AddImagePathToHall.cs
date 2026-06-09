using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Turizm_Zal.Migrations
{
    /// <inheritdoc />
    public partial class AddImagePathToHall : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Halls",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Halls");
        }
    }
}
