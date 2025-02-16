using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NftApi.Migrations
{
    /// <inheritdoc />
    public partial class AddedAuction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AuctionId",
                table: "Nft",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AuctionId",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Auction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auction", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Nft_AuctionId",
                table: "Nft",
                column: "AuctionId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AuctionId",
                table: "AspNetUsers",
                column: "AuctionId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Auction_AuctionId",
                table: "AspNetUsers",
                column: "AuctionId",
                principalTable: "Auction",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Nft_Auction_AuctionId",
                table: "Nft",
                column: "AuctionId",
                principalTable: "Auction",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Auction_AuctionId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Nft_Auction_AuctionId",
                table: "Nft");

            migrationBuilder.DropTable(
                name: "Auction");

            migrationBuilder.DropIndex(
                name: "IX_Nft_AuctionId",
                table: "Nft");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_AuctionId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AuctionId",
                table: "Nft");

            migrationBuilder.DropColumn(
                name: "AuctionId",
                table: "AspNetUsers");
        }
    }
}
