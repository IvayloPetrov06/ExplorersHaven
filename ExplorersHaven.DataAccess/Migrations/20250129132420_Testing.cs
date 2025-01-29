using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Explorers_Haven.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Testing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Travelogues",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Egipet Patepis" });

            migrationBuilder.InsertData(
                table: "Trips",
                columns: new[] { "Id", "Name", "TravelogueId" },
                values: new object[,]
                {
                    { 1, "KazanlakPlovdiv", 1 },
                    { 2, "PlovdivKairo", 1 },
                    { 3, "KairoKazanluk", 1 }
                });

            migrationBuilder.InsertData(
                table: "Activites",
                columns: new[] { "Id", "Name", "TripId" },
                values: new object[] { 1, "Qzdene na kamili", 2 });

            migrationBuilder.InsertData(
                table: "Stays",
                columns: new[] { "Id", "Name", "TripId" },
                values: new object[] { 1, "ZlatniPqsuci", 2 });

            migrationBuilder.InsertData(
                table: "Travels",
                columns: new[] { "Id", "Finish", "Start", "Transport", "TripId" },
                values: new object[,]
                {
                    { 1, "Plovdiv", "Kazanlak", "Car", 1 },
                    { 2, "Kairo", "Plovdiv", "Plane", 2 },
                    { 3, "Kazanlak", "Kairo", "Plane", 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Activites",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Stays",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Travels",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Travels",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Travels",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Travelogues",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
