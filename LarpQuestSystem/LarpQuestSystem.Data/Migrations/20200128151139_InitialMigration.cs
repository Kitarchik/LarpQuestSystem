using Microsoft.EntityFrameworkCore.Migrations;

namespace LarpQuestSystem.Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Player_Locations_LocationId",
            //    table: "Player");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_QuestPlayer_Player_PlayerId",
            //    table: "QuestPlayer");

            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_Player",
            //    table: "Player");

            //migrationBuilder.RenameTable(
            //    name: "Player",
            //    newName: "Players");

            //migrationBuilder.RenameIndex(
            //    name: "IX_Player_Name",
            //    table: "Players",
            //    newName: "IX_Players_Name");

            //migrationBuilder.RenameIndex(
            //    name: "IX_Player_LocationId",
            //    table: "Players",
            //    newName: "IX_Players_LocationId");

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_Players",
            //    table: "Players",
            //    column: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Players_Locations_LocationId",
            //    table: "Players",
            //    column: "LocationId",
            //    principalTable: "Locations",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_QuestPlayer_Players_PlayerId",
            //    table: "QuestPlayer",
            //    column: "PlayerId",
            //    principalTable: "Players",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Players_Locations_LocationId",
            //    table: "Players");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_QuestPlayer_Players_PlayerId",
            //    table: "QuestPlayer");

            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_Players",
            //    table: "Players");

            //migrationBuilder.RenameTable(
            //    name: "Players",
            //    newName: "Player");

            //migrationBuilder.RenameIndex(
            //    name: "IX_Players_Name",
            //    table: "Player",
            //    newName: "IX_Player_Name");

            //migrationBuilder.RenameIndex(
            //    name: "IX_Players_LocationId",
            //    table: "Player",
            //    newName: "IX_Player_LocationId");

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_Player",
            //    table: "Player",
            //    column: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Player_Locations_LocationId",
            //    table: "Player",
            //    column: "LocationId",
            //    principalTable: "Locations",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_QuestPlayer_Player_PlayerId",
            //    table: "QuestPlayer",
            //    column: "PlayerId",
            //    principalTable: "Player",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }
    }
}
