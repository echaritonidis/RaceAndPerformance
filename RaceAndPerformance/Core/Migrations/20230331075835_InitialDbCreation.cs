using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RaceAndPerformance.Core.Migrations
{
    public partial class InitialDbCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Match",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    MatchDate = table.Column<DateTime>(nullable: false),
                    MatchTime = table.Column<DateTime>(nullable: false),
                    TeamA = table.Column<string>(nullable: true),
                    TeamB = table.Column<string>(nullable: true),
                    Sport = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Match", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MatchOdds",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Specifier = table.Column<string>(nullable: true),
                    Odd = table.Column<double>(nullable: false),
                    MatchId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchOdds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MatchOdds_Match_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Match",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Match_MatchDate_TeamA",
                table: "Match",
                columns: new[] { "MatchDate", "TeamA" },
                unique: true,
                filter: "[TeamA] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Match_MatchDate_TeamB",
                table: "Match",
                columns: new[] { "MatchDate", "TeamB" },
                unique: true,
                filter: "[TeamB] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Match_MatchDate_TeamA_TeamB",
                table: "Match",
                columns: new[] { "MatchDate", "TeamA", "TeamB" },
                unique: true,
                filter: "[TeamA] IS NOT NULL AND [TeamB] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_MatchOdds_MatchId",
                table: "MatchOdds",
                column: "MatchId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MatchOdds");

            migrationBuilder.DropTable(
                name: "Match");
        }
    }
}
