using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Explorers_Haven.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Setup2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Travelogues",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.UpdateData(
                table: "Activites",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Camel riding");

            migrationBuilder.UpdateData(
                table: "Stays",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Megawish Hotel");

            migrationBuilder.UpdateData(
                table: "Travelogues",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Egypt");

            migrationBuilder.InsertData(
                table: "Travelogues",
                columns: new[] { "Id", "Name", "Price" },
                values: new object[,]
                {
                    { 2, "Poland", 200 },
                    { 3, "Germany", 500 }
                });

            migrationBuilder.UpdateData(
                table: "Travels",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Finish", "Transport" },
                values: new object[] { "Sofia", "Bus" });

            migrationBuilder.UpdateData(
                table: "Travels",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Finish", "Start" },
                values: new object[] { "Cairo", "Sofia" });

            migrationBuilder.UpdateData(
                table: "Travels",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Finish", "Start" },
                values: new object[] { "Sofia", "Cairo" });

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "KazanlakSofia");

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "SofiaCairo");

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "CairoSofia");

            migrationBuilder.InsertData(
                table: "Trips",
                columns: new[] { "Id", "Name", "TravelogueId" },
                values: new object[,]
                {
                    { 4, "KazanlakSofia", 1 },
                    { 5, "SofiaWarsaw", 1 },
                    { 6, "WarsawSofia", 1 },
                    { 7, "KazanlakSofia", 1 },
                    { 8, "SofiaBerlin", 1 },
                    { 9, "BerlinSofia", 1 }
                });

            migrationBuilder.InsertData(
                table: "Activites",
                columns: new[] { "Id", "Name", "TripId" },
                values: new object[,]
                {
                    { 2, "Sightseeing", 5 },
                    { 3, "Sightseeing", 8 }
                });

            migrationBuilder.InsertData(
                table: "Stays",
                columns: new[] { "Id", "Name", "TripId" },
                values: new object[,]
                {
                    { 2, "InterContinental Warsaw Hotel", 5 },
                    { 3, "Mitte Hotel", 8 }
                });

            migrationBuilder.InsertData(
                table: "Travels",
                columns: new[] { "Id", "Finish", "Start", "Transport", "TripId" },
                values: new object[,]
                {
                    { 4, "Sofia", "Kazanlak", "Car", 4 },
                    { 5, "Warsaw", "Sofia", "Plane", 5 },
                    { 6, "Sofia", "Warsaw", "Plane", 6 },
                    { 7, "Sofia", "Kazanlak", "Train", 7 },
                    { 8, "Berlin", "Sofia", "Plane", 8 },
                    { 9, "Sofia", "Berlin", "Plane", 9 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Activites",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Activites",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Stays",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Stays",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Travelogues",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Travelogues",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Travels",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Travels",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Travels",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Travels",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Travels",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Travels",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.UpdateData(
                table: "Activites",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Qzdene na kamili");

            migrationBuilder.UpdateData(
                table: "Stays",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "ZlatniPqsuci");

            migrationBuilder.UpdateData(
                table: "Travelogues",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Egipet Patepis");

            migrationBuilder.InsertData(
                table: "Travelogues",
                columns: new[] { "Id", "Name", "Price" },
                values: new object[] { 8, "Poland", 200 });

            migrationBuilder.UpdateData(
                table: "Travels",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Finish", "Transport" },
                values: new object[] { "Plovdiv", "Car" });

            migrationBuilder.UpdateData(
                table: "Travels",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Finish", "Start" },
                values: new object[] { "Kairo", "Plovdiv" });

            migrationBuilder.UpdateData(
                table: "Travels",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Finish", "Start" },
                values: new object[] { "Kazanlak", "Kairo" });

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "KazanlakPlovdiv");

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "PlovdivKairo");

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "KairoKazanluk");
        }
    }
}
