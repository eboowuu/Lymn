using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace LymcWeb.Data.Migrations
{
    public partial class AddedUserRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LenghtInFeet",
                table: "Boats",
                newName: "LengthInFeet");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LengthInFeet",
                table: "Boats",
                newName: "LenghtInFeet");
        }
    }
}
