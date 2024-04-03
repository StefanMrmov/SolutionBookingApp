using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingApplication.Migrations
{
    /// <inheritdoc />
    public partial class inital2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "bookingListId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BookingLists",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingLists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BookReservation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    reservationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    bookingListId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number_of_nights = table.Column<int>(type: "int", nullable: false),
                    BookReservationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookReservation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookReservation_BookReservation_BookReservationId",
                        column: x => x.BookReservationId,
                        principalTable: "BookReservation",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BookReservation_BookingLists_bookingListId",
                        column: x => x.bookingListId,
                        principalTable: "BookingLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookReservation_Reservations_reservationId",
                        column: x => x.reservationId,
                        principalTable: "Reservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_bookingListId",
                table: "AspNetUsers",
                column: "bookingListId");

            migrationBuilder.CreateIndex(
                name: "IX_BookReservation_bookingListId",
                table: "BookReservation",
                column: "bookingListId");

            migrationBuilder.CreateIndex(
                name: "IX_BookReservation_BookReservationId",
                table: "BookReservation",
                column: "BookReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_BookReservation_reservationId",
                table: "BookReservation",
                column: "reservationId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_BookingLists_bookingListId",
                table: "AspNetUsers",
                column: "bookingListId",
                principalTable: "BookingLists",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_BookingLists_bookingListId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "BookReservation");

            migrationBuilder.DropTable(
                name: "BookingLists");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_bookingListId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "bookingListId",
                table: "AspNetUsers");
        }
    }
}
