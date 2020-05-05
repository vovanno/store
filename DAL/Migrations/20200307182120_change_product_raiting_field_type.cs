using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class change_product_raiting_field_type : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Rating",
                table: "Products",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Rating",
                table: "Products",
                nullable: false,
                oldClrType: typeof(double));
        }
    }
}
