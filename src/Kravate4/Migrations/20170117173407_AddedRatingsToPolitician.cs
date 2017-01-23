using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kravate4.Migrations
{
    public partial class AddedRatingsToPolitician : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Rating_PoliticianID",
                table: "Rating",
                column: "PoliticianID");

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_Politician_PoliticianID",
                table: "Rating",
                column: "PoliticianID",
                principalTable: "Politician",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rating_Politician_PoliticianID",
                table: "Rating");

            migrationBuilder.DropIndex(
                name: "IX_Rating_PoliticianID",
                table: "Rating");
        }
    }
}
