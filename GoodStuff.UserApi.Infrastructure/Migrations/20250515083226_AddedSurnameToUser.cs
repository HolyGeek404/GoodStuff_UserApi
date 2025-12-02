using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoodStuff.UserApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedSurnameToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "User");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "User",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "User",
                type: "nvarchar(16)",
                maxLength: 16,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "User",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Surname",
                table: "User");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "User",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
