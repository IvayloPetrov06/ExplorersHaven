using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Explorers_Haven.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class updateValues1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Activites",
                keyColumn: "Id",
                keyValue: 1,
                column: "CoverImage",
                value: "https://res.cloudinary.com/dkoshuv9z/image/upload/v1741243536/Egypt_geyymk.jpg");

            migrationBuilder.UpdateData(
                table: "Activites",
                keyColumn: "Id",
                keyValue: 2,
                column: "CoverImage",
                value: "https://res.cloudinary.com/dkoshuv9z/image/upload/v1741243536/Egypt_geyymk.jpg");

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Custom");

            migrationBuilder.UpdateData(
                table: "Travels",
                keyColumn: "Id",
                keyValue: 1,
                column: "TransportId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Travels",
                keyColumn: "Id",
                keyValue: 2,
                column: "TransportId",
                value: 4);

            migrationBuilder.InsertData(
                table: "Travels",
                columns: new[] { "Id", "DateFinish", "DateStart", "Finish", "OfferId", "Start", "TransportId" },
                values: new object[] { 7, new DateTime(2025, 3, 10, 8, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 9, 6, 30, 0, 0, DateTimeKind.Unspecified), "Cairo", 1, "Sofia", 2 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Travels",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.UpdateData(
                table: "Activites",
                keyColumn: "Id",
                keyValue: 1,
                column: "CoverImage",
                value: null);

            migrationBuilder.UpdateData(
                table: "Activites",
                keyColumn: "Id",
                keyValue: 2,
                column: "CoverImage",
                value: null);

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Plane");

            migrationBuilder.UpdateData(
                table: "Travels",
                keyColumn: "Id",
                keyValue: 1,
                column: "TransportId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Travels",
                keyColumn: "Id",
                keyValue: 2,
                column: "TransportId",
                value: 1);
        }
    }
}
