using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NftApi.Migrations
{
    /// <inheritdoc />
    public partial class RenameNftTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Nfts_AspNetUsers_UserId",
                table: "Nfts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Nfts",
                table: "Nfts");

            migrationBuilder.RenameTable(
                name: "Nfts",
                newName: "Nft");

            migrationBuilder.RenameIndex(
                name: "IX_Nfts_UserId",
                table: "Nft",
                newName: "IX_Nft_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Nft",
                table: "Nft",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Nft_AspNetUsers_UserId",
                table: "Nft",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Nft_AspNetUsers_UserId",
                table: "Nft");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Nft",
                table: "Nft");

            migrationBuilder.RenameTable(
                name: "Nft",
                newName: "Nfts");

            migrationBuilder.RenameIndex(
                name: "IX_Nft_UserId",
                table: "Nfts",
                newName: "IX_Nfts_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Nfts",
                table: "Nfts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Nfts_AspNetUsers_UserId",
                table: "Nfts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
