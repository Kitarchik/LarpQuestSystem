using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LarpQuestSystem.Data.Migrations.Quest
{
    public partial class Initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Chains",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 250, nullable: true),
                    Description = table.Column<string>(maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chains", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 250, nullable: true),
                    Description = table.Column<string>(maxLength: 2000, nullable: true),
                    ItemType = table.Column<int>(nullable: false),
                    AmountReady = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 250, nullable: true),
                    Description = table.Column<string>(maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Npcs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 250, nullable: true),
                    Description = table.Column<string>(maxLength: 2000, nullable: true),
                    LocationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Npcs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Npcs_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 250, nullable: true),
                    Description = table.Column<string>(maxLength: 2000, nullable: true),
                    NumberOfPayedAccounts = table.Column<int>(nullable: false),
                    LocationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Players_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Quests",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 250, nullable: true),
                    Description = table.Column<string>(maxLength: 2000, nullable: true),
                    ArtisticTextLink = table.Column<string>(maxLength: 2000, nullable: true),
                    IsArtisticTextReady = table.Column<bool>(nullable: false),
                    TechnicalDescriptionLink = table.Column<string>(maxLength: 2000, nullable: true),
                    IsTechnicalDescriptionReady = table.Column<bool>(nullable: false),
                    AmountToPrint = table.Column<int>(nullable: false),
                    IsPrinted = table.Column<bool>(nullable: false),
                    Complexity = table.Column<int>(nullable: false),
                    Grade = table.Column<int>(nullable: false),
                    IsCompleted = table.Column<bool>(nullable: false),
                    CompletionComment = table.Column<string>(maxLength: 2000, nullable: true),
                    CompletionDate = table.Column<DateTime>(nullable: true),
                    QuestGiverId = table.Column<int>(nullable: false),
                    QuestEndingId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Quests_Npcs_QuestEndingId",
                        column: x => x.QuestEndingId,
                        principalTable: "Npcs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Quests_Npcs_QuestGiverId",
                        column: x => x.QuestGiverId,
                        principalTable: "Npcs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "QuestChains",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StepNumber = table.Column<int>(nullable: false),
                    QuestId = table.Column<int>(nullable: false),
                    ChainId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestChains", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestChains_Chains_ChainId",
                        column: x => x.ChainId,
                        principalTable: "Chains",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestChains_Quests_QuestId",
                        column: x => x.QuestId,
                        principalTable: "Quests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AmountNeeded = table.Column<int>(nullable: false),
                    IsReady = table.Column<bool>(nullable: false),
                    TechnicalDocumentForNpc = table.Column<string>(maxLength: 2000, nullable: true),
                    IsTechnicalDocumentReady = table.Column<bool>(nullable: false),
                    QuestId = table.Column<int>(nullable: false),
                    ItemId = table.Column<int>(nullable: false),
                    StartingNpcId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestItems_Quests_QuestId",
                        column: x => x.QuestId,
                        principalTable: "Quests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestItems_Npcs_StartingNpcId",
                        column: x => x.StartingNpcId,
                        principalTable: "Npcs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestPlayers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerId = table.Column<int>(nullable: false),
                    QuestId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestPlayers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestPlayers_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestPlayers_Quests_QuestId",
                        column: x => x.QuestId,
                        principalTable: "Quests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Chains_Name",
                table: "Chains",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Items_Name",
                table: "Items",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_Name",
                table: "Locations",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Npcs_LocationId",
                table: "Npcs",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Npcs_Name",
                table: "Npcs",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Players_LocationId",
                table: "Players",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_Name",
                table: "Players",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_QuestChains_ChainId",
                table: "QuestChains",
                column: "ChainId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestChains_QuestId",
                table: "QuestChains",
                column: "QuestId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestItems_ItemId",
                table: "QuestItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestItems_QuestId",
                table: "QuestItems",
                column: "QuestId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestItems_StartingNpcId",
                table: "QuestItems",
                column: "StartingNpcId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestPlayers_PlayerId",
                table: "QuestPlayers",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestPlayers_QuestId",
                table: "QuestPlayers",
                column: "QuestId");

            migrationBuilder.CreateIndex(
                name: "IX_Quests_Name",
                table: "Quests",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Quests_QuestEndingId",
                table: "Quests",
                column: "QuestEndingId");

            migrationBuilder.CreateIndex(
                name: "IX_Quests_QuestGiverId",
                table: "Quests",
                column: "QuestGiverId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuestChains");

            migrationBuilder.DropTable(
                name: "QuestItems");

            migrationBuilder.DropTable(
                name: "QuestPlayers");

            migrationBuilder.DropTable(
                name: "Chains");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Quests");

            migrationBuilder.DropTable(
                name: "Npcs");

            migrationBuilder.DropTable(
                name: "Locations");
        }
    }
}
