using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class addorderGamedbset : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderGame_Games_GameId",
                table: "OrderGame");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderGame_Orders_OrderId",
                table: "OrderGame");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderGame",
                table: "OrderGame");

            migrationBuilder.RenameTable(
                name: "OrderGame",
                newName: "OrderGames");

            migrationBuilder.RenameIndex(
                name: "IX_OrderGame_GameId",
                table: "OrderGames",
                newName: "IX_OrderGames_GameId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderGames",
                table: "OrderGames",
                columns: new[] { "OrderId", "GameId" });

            migrationBuilder.AddForeignKey(
                name: "FK_OrderGames_Games_GameId",
                table: "OrderGames",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "GameId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderGames_Orders_OrderId",
                table: "OrderGames",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderGames_Games_GameId",
                table: "OrderGames");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderGames_Orders_OrderId",
                table: "OrderGames");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderGames",
                table: "OrderGames");

            migrationBuilder.RenameTable(
                name: "OrderGames",
                newName: "OrderGame");

            migrationBuilder.RenameIndex(
                name: "IX_OrderGames_GameId",
                table: "OrderGame",
                newName: "IX_OrderGame_GameId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderGame",
                table: "OrderGame",
                columns: new[] { "OrderId", "GameId" });

            migrationBuilder.AddForeignKey(
                name: "FK_OrderGame_Games_GameId",
                table: "OrderGame",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "GameId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderGame_Orders_OrderId",
                table: "OrderGame",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
