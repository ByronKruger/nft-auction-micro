using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NftApi.Migrations
{
    /// <inheritdoc />
    public partial class AddHasNftsToUser1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "hasNfts",
                table: "Auctions");

            migrationBuilder.AddColumn<bool>(
                name: "HasNfts",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasNfts",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<bool>(
                name: "hasNfts",
                table: "Auctions",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }
    }
}
