using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NftApi.Migrations
{
    /// <inheritdoc />
    public partial class MadeAuctionIdOptionalForNft : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Nft_Auction_AuctionId",
                table: "Nft");

            migrationBuilder.AlterColumn<int>(
                name: "AuctionId",
                table: "Nft",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Nft_Auction_AuctionId",
                table: "Nft",
                column: "AuctionId",
                principalTable: "Auction",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Nft_Auction_AuctionId",
                table: "Nft");

            migrationBuilder.AlterColumn<int>(
                name: "AuctionId",
                table: "Nft",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Nft_Auction_AuctionId",
                table: "Nft",
                column: "AuctionId",
                principalTable: "Auction",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
