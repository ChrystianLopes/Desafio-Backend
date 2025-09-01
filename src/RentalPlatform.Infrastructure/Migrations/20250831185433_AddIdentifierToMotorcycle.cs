using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentalPlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIdentifierToMotorcycle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Identifier",
                table: "Motorcycles",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Motorcycles_Identifier",
                table: "Motorcycles",
                column: "Identifier",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Motorcycles_Identifier",
                table: "Motorcycles");

            migrationBuilder.DropColumn(
                name: "Identifier",
                table: "Motorcycles");
        }
    }
}
