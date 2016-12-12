using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EventManager.Migrations
{
    public partial class attendUserChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendance_AspNetUsers_Id",
                table: "Attendance");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Attendance",
                table: "Attendance");

            migrationBuilder.DropIndex(
                name: "IX_Attendance_Id",
                table: "Attendance");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Attendance");

            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "Attendance",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Attendance",
                table: "Attendance",
                columns: new[] { "UserID", "EventID" });

            migrationBuilder.CreateIndex(
                name: "IX_Attendance_UserID",
                table: "Attendance",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendance_AspNetUsers_UserID",
                table: "Attendance",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendance_AspNetUsers_UserID",
                table: "Attendance");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Attendance",
                table: "Attendance");

            migrationBuilder.DropIndex(
                name: "IX_Attendance_UserID",
                table: "Attendance");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Attendance");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "Attendance",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Attendance",
                table: "Attendance",
                columns: new[] { "Id", "EventID" });

            migrationBuilder.CreateIndex(
                name: "IX_Attendance_Id",
                table: "Attendance",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendance_AspNetUsers_Id",
                table: "Attendance",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
