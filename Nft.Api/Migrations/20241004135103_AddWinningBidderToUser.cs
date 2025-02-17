using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NftApi.Migrations
{
    /// <inheritdoc />
    public partial class AddWinningBidderToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AuctionId1",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AuctionId1",
                table: "AspNetUsers",
                column: "AuctionId1",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Auctions_AuctionId1",
                table: "AspNetUsers",
                column: "AuctionId1",
                principalTable: "Auctions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Auctions_AuctionId1",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_AuctionId1",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AuctionId1",
                table: "AspNetUsers");
        }
    }
}
