using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NftApi.Migrations
{
    /// <inheritdoc />
    public partial class RemoveRequiredForeignForAuctionInUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Auction_AuctionId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Auction_AuctionId1",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_AuctionId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_AuctionId1",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AuctionId1",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "CharityId",
                table: "Auction",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AuctionId",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.CreateIndex(
                name: "IX_Auction_CharityId",
                table: "Auction",
                column: "CharityId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AuctionId",
                table: "AspNetUsers",
                column: "AuctionId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Auction_AuctionId",
                table: "AspNetUsers",
                column: "AuctionId",
                principalTable: "Auction",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Auction_AspNetUsers_CharityId",
                table: "Auction",
                column: "CharityId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Auction_AuctionId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Auction_AspNetUsers_CharityId",
                table: "Auction");

            migrationBuilder.DropIndex(
                name: "IX_Auction_CharityId",
                table: "Auction");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_AuctionId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CharityId",
                table: "Auction");

            migrationBuilder.AlterColumn<int>(
                name: "AuctionId",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AuctionId1",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: true);

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
                name: "FK_AspNetUsers_Auction_AuctionId",
                table: "AspNetUsers",
                column: "AuctionId",
                principalTable: "Auction",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Auction_AuctionId1",
                table: "AspNetUsers",
                column: "AuctionId1",
                principalTable: "Auction",
                principalColumn: "Id");
        }
    }
}
