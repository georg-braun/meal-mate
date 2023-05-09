using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class templates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TemplateItem",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(36)", nullable: false),
                    ItemId = table.Column<string>(type: "character varying(36)", nullable: true),
                    Amount = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemplateItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TemplateItem_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Templates",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(36)", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Instructions = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Templates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TemplateTemplateItem",
                columns: table => new
                {
                    TemplateId = table.Column<string>(type: "character varying(36)", nullable: false),
                    TemplateItemsId = table.Column<string>(type: "character varying(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemplateTemplateItem", x => new { x.TemplateId, x.TemplateItemsId });
                    table.ForeignKey(
                        name: "FK_TemplateTemplateItem_TemplateItem_TemplateItemsId",
                        column: x => x.TemplateItemsId,
                        principalTable: "TemplateItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TemplateTemplateItem_Templates_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "Templates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TemplateItem_ItemId",
                table: "TemplateItem",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_TemplateTemplateItem_TemplateItemsId",
                table: "TemplateTemplateItem",
                column: "TemplateItemsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TemplateTemplateItem");

            migrationBuilder.DropTable(
                name: "TemplateItem");

            migrationBuilder.DropTable(
                name: "Templates");
        }
    }
}
