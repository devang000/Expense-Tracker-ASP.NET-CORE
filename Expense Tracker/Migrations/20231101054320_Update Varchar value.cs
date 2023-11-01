using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Expense_Tracker.Migrations
{
    public partial class UpdateVarcharvalue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_userAccount",
                table: "userAccount");

            migrationBuilder.RenameTable(
                name: "userAccount",
                newName: "UserAccount");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "UserAccount",
                type: "nvarchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)");

            migrationBuilder.AlterColumn<string>(
                name: "ConfirmPasssword",
                table: "UserAccount",
                type: "nvarchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAccount",
                table: "UserAccount",
                column: "idUser");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAccount",
                table: "UserAccount");

            migrationBuilder.RenameTable(
                name: "UserAccount",
                newName: "userAccount");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "userAccount",
                type: "nvarchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)");

            migrationBuilder.AlterColumn<string>(
                name: "ConfirmPasssword",
                table: "userAccount",
                type: "nvarchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_userAccount",
                table: "userAccount",
                column: "idUser");
        }
    }
}
