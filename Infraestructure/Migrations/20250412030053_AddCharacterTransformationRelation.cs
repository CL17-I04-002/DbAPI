using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCharacterTransformationRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CharacterId",
                table: "Transformation",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transformation_CharacterId",
                table: "Transformation",
                column: "CharacterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transformation_Character_CharacterId",
                table: "Transformation",
                column: "CharacterId",
                principalTable: "Character",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transformation_Character_CharacterId",
                table: "Transformation");

            migrationBuilder.DropIndex(
                name: "IX_Transformation_CharacterId",
                table: "Transformation");

            migrationBuilder.DropColumn(
                name: "CharacterId",
                table: "Transformation");
        }
    }
}
