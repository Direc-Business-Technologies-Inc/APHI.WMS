using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.MsSql.Migrations
{
    /// <inheritdoc />
    public partial class AddMaxFailedLoginAttemptsSetting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "OSTN",
                columns: new[] { "Id", "Description", "Name", "Type", "Value" },
                values: new object[] { new Guid("b2c3d4e5-f6a7-8901-bcde-f12345678901"), "Number of consecutive failed login attempts before the user account is locked. Must be a positive integer. Defaults to 5 if not configured.", "Max Failed Login Attempts", "INT", "5" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OSTN",
                keyColumn: "Id",
                keyValue: new Guid("b2c3d4e5-f6a7-8901-bcde-f12345678901"));
        }
    }
}
