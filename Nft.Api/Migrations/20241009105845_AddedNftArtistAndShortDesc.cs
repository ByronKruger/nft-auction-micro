using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NftApi.Migrations
{
    /// <inheritdoc />
    public partial class AddedNftArtistAndShortDesc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ArtistName",
                table: "Nft",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "shortDescription",
                table: "Nft",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArtistName",
                table: "Nft");

            migrationBuilder.DropColumn(
                name: "shortDescription",
                table: "Nft");
        }
    }
}
