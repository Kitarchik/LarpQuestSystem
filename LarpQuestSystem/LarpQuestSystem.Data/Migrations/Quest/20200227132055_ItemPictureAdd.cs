using Microsoft.EntityFrameworkCore.Migrations;

namespace LarpQuestSystem.Data.Migrations.Quest
{
    public partial class ItemPictureAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PictureLink",
                table: "Items",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PictureLink",
                table: "Items");
        }
    }
}
