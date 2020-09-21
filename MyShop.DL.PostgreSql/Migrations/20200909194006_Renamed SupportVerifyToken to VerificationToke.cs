using Microsoft.EntityFrameworkCore.Migrations;

namespace MyShop.DL.PostgreSql.Migrations
{
    public partial class RenamedSupportVerifyTokentoVerificationToke : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SupportVerifyTokens",
                table: "SupportVerifyTokens");

            migrationBuilder.RenameTable(
                name: "SupportVerifyTokens",
                newName: "VerificationTokens");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VerificationTokens",
                table: "VerificationTokens",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_VerificationTokens",
                table: "VerificationTokens");

            migrationBuilder.RenameTable(
                name: "VerificationTokens",
                newName: "SupportVerifyTokens");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SupportVerifyTokens",
                table: "SupportVerifyTokens",
                column: "Id");
        }
    }
}
