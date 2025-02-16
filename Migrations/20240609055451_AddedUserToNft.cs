using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NftApi.Migrations
{
    /// <inheritdoc />
    public partial class AddedUserToNft : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Nft_AspNetUsers_UserId",
                table: "Nft");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Nft",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

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

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Nft",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Nft_AspNetUsers_UserId",
                table: "Nft",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
