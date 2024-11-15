using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BongOliver.Migrations
{
    /// <inheritdoc />
    public partial class UpdateService : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_Bookings_BookingId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Services_BookingId",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "BookingId",
                table: "Services");

            migrationBuilder.CreateTable(
                name: "BookingService",
                columns: table => new
                {
                    BookingsId = table.Column<int>(type: "int", nullable: false),
                    ServicesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingService", x => new { x.BookingsId, x.ServicesId });
                    table.ForeignKey(
                        name: "FK_BookingService_Bookings_BookingsId",
                        column: x => x.BookingsId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookingService_Services_ServicesId",
                        column: x => x.ServicesId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookingService_ServicesId",
                table: "BookingService",
                column: "ServicesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingService");

            migrationBuilder.AddColumn<int>(
                name: "BookingId",
                table: "Services",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Services_BookingId",
                table: "Services",
                column: "BookingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Bookings_BookingId",
                table: "Services",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id");
        }
    }
}
