using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NftApi.Migrations
{
    /// <inheritdoc />
    public partial class AddedExplicitCharityToAuction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Nft_AuctionId",
                table: "Nft");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_AuctionId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "MyNftTestProp",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "AuctionId1",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Nft_AuctionId",
                table: "Nft",
                column: "AuctionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AuctionId",
                table: "AspNetUsers",
                column: "AuctionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AuctionId1",
                table: "AspNetUsers",
                column: "AuctionId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Auction_AuctionId1",
                table: "AspNetUsers",
                column: "AuctionId1",
                principalTable: "Auction",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Auction_AuctionId1",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_Nft_AuctionId",
                table: "Nft");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_AuctionId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_AuctionId1",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AuctionId1",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<byte[]>(
                name: "MyNftTestProp",
                table: "AspNetUsers",
                type: "BLOB",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Nft_AuctionId",
                table: "Nft",
                column: "AuctionId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AuctionId",
                table: "AspNetUsers",
                column: "AuctionId");
        }
    }
}
