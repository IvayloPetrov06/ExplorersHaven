using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Explorers_Haven.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class setup3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 1,
                column: "Rating",
                value: 3m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 1,
                column: "Rating",
                value: null);
        }
    }
}
