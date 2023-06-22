using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class removeitemidintemplateitem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TemplateItem_Items_ItemId",
                table: "TemplateItem");

            migrationBuilder.DropIndex(
                name: "IX_TemplateItem_ItemId",
                table: "TemplateItem");

            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "TemplateItem");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ItemId",
                table: "TemplateItem",
                type: "character varying(36)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TemplateItem_ItemId",
                table: "TemplateItem",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_TemplateItem_Items_ItemId",
                table: "TemplateItem",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id");
        }
    }
}
