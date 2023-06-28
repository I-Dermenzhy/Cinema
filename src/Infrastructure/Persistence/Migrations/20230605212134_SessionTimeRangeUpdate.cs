using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SessionTimeRangeUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "Sessions",
                newName: "Duration_Start");

            migrationBuilder.AddColumn<DateTime>(
                name: "Duration_End",
                table: "Sessions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duration_End",
                table: "Sessions");

            migrationBuilder.RenameColumn(
                name: "Duration_Start",
                table: "Sessions",
                newName: "DateTime");
        }
    }
}
