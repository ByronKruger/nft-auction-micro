using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NftApi.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCharityFromAuction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Auction_AspNetUsers_CharityId",
                table: "Auction");

            migrationBuilder.DropIndex(
                name: "IX_Auction_CharityId",
                table: "Auction");

            migrationBuilder.DropColumn(
                name: "CharityId",
                table: "Auction");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CharityId",
                table: "Auction",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Auction_CharityId",
                table: "Auction",
                column: "CharityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Auction_AspNetUsers_CharityId",
                table: "Auction",
                column: "CharityId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
