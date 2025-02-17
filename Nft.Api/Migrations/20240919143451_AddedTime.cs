using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NftApi.Migrations
{
    /// <inheritdoc />
    public partial class AddedTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "image",
                table: "Nft",
                newName: "Image");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Nft",
                newName: "Description");

            migrationBuilder.AlterColumn<int>(
                name: "Amount",
                table: "Bids",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "Time",
                table: "Bids",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Time",
                table: "Bids");

            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Nft",
                newName: "image");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Nft",
                newName: "description");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Bids",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");
        }
    }
}
