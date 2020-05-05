using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class remove_Name_from_comments_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Comments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Comments",
                maxLength: 30,
                nullable: false,
                defaultValue: "");
        }
    }
}
