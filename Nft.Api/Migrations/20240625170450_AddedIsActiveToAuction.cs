using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NftApi.Migrations
{
    /// <inheritdoc />
    public partial class AddedIsActiveToAuction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Auction_AuctionId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Nft_Auction_AuctionId",
                table: "Nft");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Auction",
                table: "Auction");

            migrationBuilder.RenameTable(
                name: "Auction",
                newName: "Auctions");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Auctions",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Auctions",
                table: "Auctions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Auctions_AuctionId",
                table: "AspNetUsers",
                column: "AuctionId",
                principalTable: "Auctions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Nft_Auctions_AuctionId",
                table: "Nft",
                column: "AuctionId",
                principalTable: "Auctions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Auctions_AuctionId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Nft_Auctions_AuctionId",
                table: "Nft");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Auctions",
                table: "Auctions");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Auctions");

            migrationBuilder.RenameTable(
                name: "Auctions",
                newName: "Auction");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Auction",
                table: "Auction",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Auction_AuctionId",
                table: "AspNetUsers",
                column: "AuctionId",
                principalTable: "Auction",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Nft_Auction_AuctionId",
                table: "Nft",
                column: "AuctionId",
                principalTable: "Auction",
                principalColumn: "Id");
        }
    }
}
