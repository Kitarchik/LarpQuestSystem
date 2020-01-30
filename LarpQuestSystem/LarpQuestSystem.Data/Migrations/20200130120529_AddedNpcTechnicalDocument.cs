using Microsoft.EntityFrameworkCore.Migrations;

namespace LarpQuestSystem.Data.Migrations
{
    public partial class AddedNpcTechnicalDocument : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsTechnicalDocumentReady",
                table: "QuestItems",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "TechnicalDocumentForNpc",
                table: "QuestItems",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsTechnicalDocumentReady",
                table: "QuestItems");

            migrationBuilder.DropColumn(
                name: "TechnicalDocumentForNpc",
                table: "QuestItems");
        }
    }
}
