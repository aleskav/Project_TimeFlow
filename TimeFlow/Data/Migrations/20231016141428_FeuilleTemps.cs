using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeFlow.Data.Migrations
{
    /// <inheritdoc />
    public partial class FeuilleTemps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Annee",
                table: "Taches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Semaine",
                table: "Taches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "FeuilleTemps",
                columns: table => new
                {
                    Annee = table.Column<int>(type: "int", nullable: false),
                    Semaine = table.Column<int>(type: "int", nullable: false),
                    EmployeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProjetId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EstSoumis = table.Column<bool>(type: "bit", nullable: false),
                    EstConfirme = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeuilleTemps", x => new { x.Annee, x.Semaine, x.EmployeId, x.ProjetId });
                    table.ForeignKey(
                        name: "FK_FeuilleTemps_AspNetUsers_EmployeId",
                        column: x => x.EmployeId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FeuilleTemps_Projets_ProjetId",
                        column: x => x.ProjetId,
                        principalTable: "Projets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FeuilleJours",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    JourNom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HoursTravaille = table.Column<int>(type: "int", nullable: false),
                    FeuilleTempsAnnee = table.Column<int>(type: "int", nullable: true),
                    FeuilleTempsEmployeId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    FeuilleTempsProjetId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    FeuilleTempsSemaine = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeuilleJours", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeuilleJours_FeuilleTemps_FeuilleTempsAnnee_FeuilleTempsSemaine_FeuilleTempsEmployeId_FeuilleTempsProjetId",
                        columns: x => new { x.FeuilleTempsAnnee, x.FeuilleTempsSemaine, x.FeuilleTempsEmployeId, x.FeuilleTempsProjetId },
                        principalTable: "FeuilleTemps",
                        principalColumns: new[] { "Annee", "Semaine", "EmployeId", "ProjetId" });
                });

            migrationBuilder.CreateIndex(
                name: "IX_Taches_Annee_Semaine_MembreChargeId_ProjetId",
                table: "Taches",
                columns: new[] { "Annee", "Semaine", "MembreChargeId", "ProjetId" });

            migrationBuilder.CreateIndex(
                name: "IX_FeuilleJours_FeuilleTempsAnnee_FeuilleTempsSemaine_FeuilleTempsEmployeId_FeuilleTempsProjetId",
                table: "FeuilleJours",
                columns: new[] { "FeuilleTempsAnnee", "FeuilleTempsSemaine", "FeuilleTempsEmployeId", "FeuilleTempsProjetId" });

            migrationBuilder.CreateIndex(
                name: "IX_FeuilleTemps_EmployeId",
                table: "FeuilleTemps",
                column: "EmployeId");

            migrationBuilder.CreateIndex(
                name: "IX_FeuilleTemps_ProjetId",
                table: "FeuilleTemps",
                column: "ProjetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Taches_FeuilleTemps_Annee_Semaine_MembreChargeId_ProjetId",
                table: "Taches",
                columns: new[] { "Annee", "Semaine", "MembreChargeId", "ProjetId" },
                principalTable: "FeuilleTemps",
                principalColumns: new[] { "Annee", "Semaine", "EmployeId", "ProjetId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Taches_FeuilleTemps_Annee_Semaine_MembreChargeId_ProjetId",
                table: "Taches");

            migrationBuilder.DropTable(
                name: "FeuilleJours");

            migrationBuilder.DropTable(
                name: "FeuilleTemps");

            migrationBuilder.DropIndex(
                name: "IX_Taches_Annee_Semaine_MembreChargeId_ProjetId",
                table: "Taches");

            migrationBuilder.DropColumn(
                name: "Annee",
                table: "Taches");

            migrationBuilder.DropColumn(
                name: "Semaine",
                table: "Taches");
        }
    }
}
