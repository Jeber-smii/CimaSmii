using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gestion_de_films.Migrations
{
    /// <inheritdoc />
    public partial class addMovieDurationAndPlacesDispo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeOnly>(
                name: "MovieDuration",
                table: "Movies",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.AddColumn<int>(
                name: "NbPlacesDispo",
                table: "Movies",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MovieDuration",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "NbPlacesDispo",
                table: "Movies");
        }
    }
}
