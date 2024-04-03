using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingApplication.Migrations
{
    /// <inheritdoc />
    public partial class inital4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookReservation_BookReservation_BookReservationId",
                table: "BookReservation");

            migrationBuilder.DropIndex(
                name: "IX_BookReservation_BookReservationId",
                table: "BookReservation");

            migrationBuilder.DropColumn(
                name: "BookReservationId",
                table: "BookReservation");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BookReservationId",
                table: "BookReservation",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookReservation_BookReservationId",
                table: "BookReservation",
                column: "BookReservationId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookReservation_BookReservation_BookReservationId",
                table: "BookReservation",
                column: "BookReservationId",
                principalTable: "BookReservation",
                principalColumn: "Id");
        }
    }
}
