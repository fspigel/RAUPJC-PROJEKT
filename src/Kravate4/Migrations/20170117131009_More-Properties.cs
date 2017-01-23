using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kravate4.Migrations
{
    public partial class MoreProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PartyID",
                table: "Politician",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<float>(
                name: "Popularity",
                table: "Politician",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Score",
                table: "Politician",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "Politician",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VoteCount",
                table: "Politician",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PartyID",
                table: "Politician");

            migrationBuilder.DropColumn(
                name: "Popularity",
                table: "Politician");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "Politician");

            migrationBuilder.DropColumn(
                name: "Surname",
                table: "Politician");

            migrationBuilder.DropColumn(
                name: "VoteCount",
                table: "Politician");
        }
    }
}
