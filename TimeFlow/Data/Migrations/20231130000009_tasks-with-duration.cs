using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeFlow.Data.Migrations
{
    /// <inheritdoc />
    public partial class taskswithduration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeuilleJours_FeuilleTemps_FeuilleTempsId",
                table: "FeuilleJours");

            migrationBuilder.DropForeignKey(
                name: "FK_Taches_FeuilleTemps_FeuilleTempsId",
                table: "Taches");

            migrationBuilder.DropIndex(
                name: "IX_Taches_FeuilleTempsId",
                table: "Taches");

            migrationBuilder.DropColumn(
                name: "FeuilleTempsId",
                table: "Taches");

            migrationBuilder.AlterColumn<string>(
                name: "FeuilleTempsId",
                table: "FeuilleJours",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "JourTache",
                columns: table => new
                {
                    TacheId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FeuilleJourId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    HeureDebut = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HeureFin = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JourTache", x => new { x.FeuilleJourId, x.TacheId });
                    table.ForeignKey(
                        name: "FK_JourTache_FeuilleJours_FeuilleJourId",
                        column: x => x.FeuilleJourId,
                        principalTable: "FeuilleJours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JourTache_Taches_TacheId",
                        column: x => x.TacheId,
                        principalTable: "Taches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JourTache_TacheId",
                table: "JourTache",
                column: "TacheId");

            migrationBuilder.AddForeignKey(
                name: "FK_FeuilleJours_FeuilleTemps_FeuilleTempsId",
                table: "FeuilleJours",
                column: "FeuilleTempsId",
                principalTable: "FeuilleTemps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeuilleJours_FeuilleTemps_FeuilleTempsId",
                table: "FeuilleJours");

            migrationBuilder.DropTable(
                name: "JourTache");

            migrationBuilder.AddColumn<string>(
                name: "FeuilleTempsId",
                table: "Taches",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FeuilleTempsId",
                table: "FeuilleJours",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_Taches_FeuilleTempsId",
                table: "Taches",
                column: "FeuilleTempsId");

            migrationBuilder.AddForeignKey(
                name: "FK_FeuilleJours_FeuilleTemps_FeuilleTempsId",
                table: "FeuilleJours",
                column: "FeuilleTempsId",
                principalTable: "FeuilleTemps",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Taches_FeuilleTemps_FeuilleTempsId",
                table: "Taches",
                column: "FeuilleTempsId",
                principalTable: "FeuilleTemps",
                principalColumn: "Id");
        }
    }
}
