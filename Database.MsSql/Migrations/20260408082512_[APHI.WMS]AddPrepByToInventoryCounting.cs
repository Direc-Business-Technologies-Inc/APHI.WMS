using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.MsSql.Migrations
{
    /// <inheritdoc />
    public partial class APHIWMSAddPrepByToInventoryCounting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PrepBy",
                table: "OICD",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrepBy",
                table: "OICD");
        }
    }
}
