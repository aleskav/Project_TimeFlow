using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeFlow.Data.Migrations
{
    /// <inheritdoc />
    public partial class joursTachedbset : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JourTache_FeuilleJours_FeuilleJourId",
                table: "JourTache");

            migrationBuilder.DropForeignKey(
                name: "FK_JourTache_Taches_TacheId",
                table: "JourTache");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JourTache",
                table: "JourTache");

            migrationBuilder.RenameTable(
                name: "JourTache",
                newName: "JourTaches");

            migrationBuilder.RenameIndex(
                name: "IX_JourTache_TacheId",
                table: "JourTaches",
                newName: "IX_JourTaches_TacheId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JourTaches",
                table: "JourTaches",
                columns: new[] { "FeuilleJourId", "TacheId" });

            migrationBuilder.AddForeignKey(
                name: "FK_JourTaches_FeuilleJours_FeuilleJourId",
                table: "JourTaches",
                column: "FeuilleJourId",
                principalTable: "FeuilleJours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JourTaches_Taches_TacheId",
                table: "JourTaches",
                column: "TacheId",
                principalTable: "Taches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JourTaches_FeuilleJours_FeuilleJourId",
                table: "JourTaches");

            migrationBuilder.DropForeignKey(
                name: "FK_JourTaches_Taches_TacheId",
                table: "JourTaches");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JourTaches",
                table: "JourTaches");

            migrationBuilder.RenameTable(
                name: "JourTaches",
                newName: "JourTache");

            migrationBuilder.RenameIndex(
                name: "IX_JourTaches_TacheId",
                table: "JourTache",
                newName: "IX_JourTache_TacheId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JourTache",
                table: "JourTache",
                columns: new[] { "FeuilleJourId", "TacheId" });

            migrationBuilder.AddForeignKey(
                name: "FK_JourTache_FeuilleJours_FeuilleJourId",
                table: "JourTache",
                column: "FeuilleJourId",
                principalTable: "FeuilleJours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JourTache_Taches_TacheId",
                table: "JourTache",
                column: "TacheId",
                principalTable: "Taches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
