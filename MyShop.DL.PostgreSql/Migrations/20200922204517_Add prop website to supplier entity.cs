using Microsoft.EntityFrameworkCore.Migrations;

namespace MyShop.DL.PostgreSql.Migrations
{
    public partial class Addpropwebsitetosupplierentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Website",
                table: "Suppliers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Website",
                table: "Suppliers");
        }
    }
}
