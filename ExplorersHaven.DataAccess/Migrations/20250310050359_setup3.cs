using System;
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
                table: "Travels",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateFinish", "DateStart" },
                values: new object[] { new DateTime(2025, 3, 10, 8, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 9, 6, 30, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Travels",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateFinish", "DateStart" },
                values: new object[] { new DateTime(2025, 3, 16, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 15, 12, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Travels",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateFinish", "DateStart" },
                values: new object[] { new DateTime(2025, 3, 9, 6, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 10, 8, 30, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Travels",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateFinish", "DateStart" },
                values: new object[] { new DateTime(2025, 3, 15, 12, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 16, 14, 0, 0, 0, DateTimeKind.Unspecified) });
        }
    }
}
