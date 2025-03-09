using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Explorers_Haven.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class setup4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 1,
                column: "Disc",
                value: "Travel across Egypt and cruise down the Nile River, tour the pyramids of Giza.");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 1,
                column: "Disc",
                value: "");
        }
    }
}
