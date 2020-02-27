using Microsoft.EntityFrameworkCore.Migrations;

namespace LarpQuestSystem.Data.Migrations.Quest
{
    public partial class MaterialsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RequestsPayed",
                table: "Locations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Materials",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 250, nullable: true),
                    UnitOfMeasure = table.Column<int>(nullable: false),
                    Price = table.Column<int>(nullable: false),
                    AmountPerPayedRequestInLocation = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materials", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MaterialsRequests",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Customer = table.Column<string>(maxLength: 250, nullable: true),
                    IsPayed = table.Column<bool>(nullable: false),
                    LocationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialsRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaterialsRequests_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SingleMaterialRequests",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuantityOrdered = table.Column<int>(nullable: false),
                    QuantityServed = table.Column<int>(nullable: false),
                    MaterialId = table.Column<int>(nullable: false),
                    MaterialsRequestId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SingleMaterialRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SingleMaterialRequests_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SingleMaterialRequests_MaterialsRequests_MaterialsRequestId",
                        column: x => x.MaterialsRequestId,
                        principalTable: "MaterialsRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Materials_Name",
                table: "Materials",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialsRequests_LocationId",
                table: "MaterialsRequests",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_SingleMaterialRequests_MaterialId",
                table: "SingleMaterialRequests",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_SingleMaterialRequests_MaterialsRequestId",
                table: "SingleMaterialRequests",
                column: "MaterialsRequestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SingleMaterialRequests");

            migrationBuilder.DropTable(
                name: "Materials");

            migrationBuilder.DropTable(
                name: "MaterialsRequests");

            migrationBuilder.DropColumn(
                name: "RequestsPayed",
                table: "Locations");
        }
    }
}
