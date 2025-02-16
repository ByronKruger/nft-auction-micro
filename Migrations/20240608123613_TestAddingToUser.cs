using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NftApi.Migrations
{
    /// <inheritdoc />
    public partial class TestAddingToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PasswordSalt",
                table: "AspNetUsers",
                newName: "MyNftTestProp");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MyNftTestProp",
                table: "AspNetUsers",
                newName: "PasswordSalt");
        }
    }
}
