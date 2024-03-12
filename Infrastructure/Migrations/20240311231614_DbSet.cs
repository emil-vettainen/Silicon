using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DbSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAddressEntity_AddressEntity_AddressId",
                table: "UserAddressEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAddressEntity_AspNetUsers_UserId",
                table: "UserAddressEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAddressEntity",
                table: "UserAddressEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AddressEntity",
                table: "AddressEntity");

            migrationBuilder.RenameTable(
                name: "UserAddressEntity",
                newName: "UserAddresses");

            migrationBuilder.RenameTable(
                name: "AddressEntity",
                newName: "Addresses");

            migrationBuilder.RenameIndex(
                name: "IX_UserAddressEntity_AddressId",
                table: "UserAddresses",
                newName: "IX_UserAddresses_AddressId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAddresses",
                table: "UserAddresses",
                columns: new[] { "UserId", "AddressId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Addresses",
                table: "Addresses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAddresses_Addresses_AddressId",
                table: "UserAddresses",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAddresses_AspNetUsers_UserId",
                table: "UserAddresses",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAddresses_Addresses_AddressId",
                table: "UserAddresses");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAddresses_AspNetUsers_UserId",
                table: "UserAddresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAddresses",
                table: "UserAddresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Addresses",
                table: "Addresses");

            migrationBuilder.RenameTable(
                name: "UserAddresses",
                newName: "UserAddressEntity");

            migrationBuilder.RenameTable(
                name: "Addresses",
                newName: "AddressEntity");

            migrationBuilder.RenameIndex(
                name: "IX_UserAddresses_AddressId",
                table: "UserAddressEntity",
                newName: "IX_UserAddressEntity_AddressId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAddressEntity",
                table: "UserAddressEntity",
                columns: new[] { "UserId", "AddressId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AddressEntity",
                table: "AddressEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAddressEntity_AddressEntity_AddressId",
                table: "UserAddressEntity",
                column: "AddressId",
                principalTable: "AddressEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAddressEntity_AspNetUsers_UserId",
                table: "UserAddressEntity",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
