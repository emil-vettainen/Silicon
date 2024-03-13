using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class OptionalAddressEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OptionalAddress",
                table: "UserAddresses");

            migrationBuilder.AddColumn<int>(
                name: "OptionalAddressId",
                table: "UserAddresses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OptionalAddresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OptionalAddress = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OptionalAddresses", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserAddresses_OptionalAddressId",
                table: "UserAddresses",
                column: "OptionalAddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAddresses_OptionalAddresses_OptionalAddressId",
                table: "UserAddresses",
                column: "OptionalAddressId",
                principalTable: "OptionalAddresses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAddresses_OptionalAddresses_OptionalAddressId",
                table: "UserAddresses");

            migrationBuilder.DropTable(
                name: "OptionalAddresses");

            migrationBuilder.DropIndex(
                name: "IX_UserAddresses_OptionalAddressId",
                table: "UserAddresses");

            migrationBuilder.DropColumn(
                name: "OptionalAddressId",
                table: "UserAddresses");

            migrationBuilder.AddColumn<string>(
                name: "OptionalAddress",
                table: "UserAddresses",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
