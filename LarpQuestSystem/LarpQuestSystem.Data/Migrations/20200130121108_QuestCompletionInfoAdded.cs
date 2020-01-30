using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LarpQuestSystem.Data.Migrations
{
    public partial class QuestCompletionInfoAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CompletionComment",
                table: "Quests",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CompletionDate",
                table: "Quests",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "Quests",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompletionComment",
                table: "Quests");

            migrationBuilder.DropColumn(
                name: "CompletionDate",
                table: "Quests");

            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "Quests");
        }
    }
}
