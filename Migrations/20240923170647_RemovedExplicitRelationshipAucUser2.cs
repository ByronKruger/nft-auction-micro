using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NftApi.Migrations
{
    /// <inheritdoc />
    public partial class RemovedExplicitRelationshipAucUser2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Auctions_AuctionId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "AuctionId",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Auctions_AuctionId",
                table: "AspNetUsers",
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

            migrationBuilder.AlterColumn<int>(
                name: "AuctionId",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Auctions_AuctionId",
                table: "AspNetUsers",
                column: "AuctionId",
                principalTable: "Auctions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
