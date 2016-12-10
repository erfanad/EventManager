using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EventManager.Migrations
{
    public partial class venue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "Events");

            migrationBuilder.AddColumn<string>(
                name: "Venue",
                table: "Events",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Venue",
                table: "Events");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Events",
                nullable: false,
                defaultValue: "");
        }
    }
}
