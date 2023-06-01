using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Repository.Migrations
{
    public partial class UserAnnualSalary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnnualSalary_AspNetUsers_UserId",
                table: "AnnualSalary");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AnnualSalary",
                table: "AnnualSalary");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AnnualSalary");

            migrationBuilder.RenameTable(
                name: "AnnualSalary",
                newName: "AnnualSalaryes");

            migrationBuilder.RenameIndex(
                name: "IX_AnnualSalary_UserId",
                table: "AnnualSalaryes",
                newName: "IX_AnnualSalaryes_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AnnualSalaryes",
                table: "AnnualSalaryes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AnnualSalaryes_AspNetUsers_UserId",
                table: "AnnualSalaryes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnnualSalaryes_AspNetUsers_UserId",
                table: "AnnualSalaryes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AnnualSalaryes",
                table: "AnnualSalaryes");

            migrationBuilder.RenameTable(
                name: "AnnualSalaryes",
                newName: "AnnualSalary");

            migrationBuilder.RenameIndex(
                name: "IX_AnnualSalaryes_UserId",
                table: "AnnualSalary",
                newName: "IX_AnnualSalary_UserId");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AnnualSalary",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AnnualSalary",
                table: "AnnualSalary",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AnnualSalary_AspNetUsers_UserId",
                table: "AnnualSalary",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
