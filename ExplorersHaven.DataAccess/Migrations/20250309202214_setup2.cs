using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Explorers_Haven.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class setup2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateStart",
                table: "Travels",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateFinish",
                table: "Travels",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Disc",
                table: "Stays",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Disc",
                table: "Offers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 1,
                column: "Disc",
                value: "");

            migrationBuilder.UpdateData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 2,
                column: "Disc",
                value: null);

            migrationBuilder.UpdateData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 3,
                column: "Disc",
                value: null);

            migrationBuilder.UpdateData(
                table: "Stays",
                keyColumn: "Id",
                keyValue: 1,
                column: "Disc",
                value: "This Luxurious Premium Ultra all-inclusive resort in Hurghada offers only suites and villas with beachfront accommodation with total landscape area of 255.000 m2. It features 1km private sandy beach, 30 Swimming pools (9 types), 1 main buffet restaurant, 7 a-la-carte restaurants, 14 bars and free Wi-Fi in the entire property. This 5-star hotel offers private beach and pool cabanas upon request.");

            migrationBuilder.UpdateData(
                table: "Stays",
                keyColumn: "Id",
                keyValue: 2,
                column: "Disc",
                value: null);

            migrationBuilder.UpdateData(
                table: "Stays",
                keyColumn: "Id",
                keyValue: 3,
                column: "Disc",
                value: null);

            migrationBuilder.UpdateData(
                table: "Travels",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateFinish", "DateStart" },
                values: new object[] { new DateTime(2025, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 16, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Travels",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateFinish", "DateStart" },
                values: new object[] { new DateTime(2025, 3, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Travels",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateFinish", "DateStart" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Travels",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DateFinish", "DateStart" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Travels",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DateFinish", "DateStart" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Travels",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "DateFinish", "DateStart" },
                values: new object[] { null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Disc",
                table: "Stays");

            migrationBuilder.DropColumn(
                name: "Disc",
                table: "Offers");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "DateStart",
                table: "Travels",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "DateFinish",
                table: "Travels",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Travels",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateFinish", "DateStart" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Travels",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateFinish", "DateStart" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Travels",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateFinish", "DateStart" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Travels",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DateFinish", "DateStart" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Travels",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DateFinish", "DateStart" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Travels",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "DateFinish", "DateStart" },
                values: new object[] { null, null });
        }
    }
}
