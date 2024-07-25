using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeFlow.Data.Migrations
{
    /// <inheritdoc />
    public partial class addIDtoFT : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeuilleJours_FeuilleTemps_FeuilleTempsAnnee_FeuilleTempsSemaine_FeuilleTempsEmployeId_FeuilleTempsProjetId",
                table: "FeuilleJours");

            migrationBuilder.DropForeignKey(
                name: "FK_FeuilleTemps_Projets_ProjetId",
                table: "FeuilleTemps");

            migrationBuilder.DropForeignKey(
                name: "FK_Taches_FeuilleTemps_Annee_Semaine_MembreChargeId_ProjetId",
                table: "Taches");

            migrationBuilder.DropIndex(
                name: "IX_Taches_Annee_Semaine_MembreChargeId_ProjetId",
                table: "Taches");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FeuilleTemps",
                table: "FeuilleTemps");

            migrationBuilder.DropIndex(
                name: "IX_FeuilleTemps_ProjetId",
                table: "FeuilleTemps");

            migrationBuilder.DropIndex(
                name: "IX_FeuilleJours_FeuilleTempsAnnee_FeuilleTempsSemaine_FeuilleTempsEmployeId_FeuilleTempsProjetId",
                table: "FeuilleJours");

            migrationBuilder.DropColumn(
                name: "Annee",
                table: "FeuilleTemps");

            migrationBuilder.DropColumn(
                name: "FeuilleTempsAnnee",
                table: "FeuilleJours");

            migrationBuilder.DropColumn(
                name: "FeuilleTempsEmployeId",
                table: "FeuilleJours");

            migrationBuilder.DropColumn(
                name: "FeuilleTempsSemaine",
                table: "FeuilleJours");

            migrationBuilder.RenameColumn(
                name: "ProjetId",
                table: "FeuilleTemps",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "FeuilleTempsProjetId",
                table: "FeuilleJours",
                newName: "FeuilleTempsId");

            migrationBuilder.AddColumn<string>(
                name: "FeuilleTempsId",
                table: "Taches",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Prenom",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NomFamille",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateEmbauche",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FeuilleTemps",
                table: "FeuilleTemps",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Taches_FeuilleTempsId",
                table: "Taches",
                column: "FeuilleTempsId");

            migrationBuilder.CreateIndex(
                name: "IX_FeuilleJours_FeuilleTempsId",
                table: "FeuilleJours",
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
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropPrimaryKey(
                name: "PK_FeuilleTemps",
                table: "FeuilleTemps");

            migrationBuilder.DropIndex(
                name: "IX_FeuilleJours_FeuilleTempsId",
                table: "FeuilleJours");

            migrationBuilder.DropColumn(
                name: "FeuilleTempsId",
                table: "Taches");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "FeuilleTemps",
                newName: "ProjetId");

            migrationBuilder.RenameColumn(
                name: "FeuilleTempsId",
                table: "FeuilleJours",
                newName: "FeuilleTempsProjetId");

            migrationBuilder.AddColumn<int>(
                name: "Annee",
                table: "FeuilleTemps",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FeuilleTempsAnnee",
                table: "FeuilleJours",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FeuilleTempsEmployeId",
                table: "FeuilleJours",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FeuilleTempsSemaine",
                table: "FeuilleJours",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Prenom",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "NomFamille",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateEmbauche",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FeuilleTemps",
                table: "FeuilleTemps",
                columns: new[] { "Annee", "Semaine", "EmployeId", "ProjetId" });

            migrationBuilder.CreateIndex(
                name: "IX_Taches_Annee_Semaine_MembreChargeId_ProjetId",
                table: "Taches",
                columns: new[] { "Annee", "Semaine", "MembreChargeId", "ProjetId" });

            migrationBuilder.CreateIndex(
                name: "IX_FeuilleTemps_ProjetId",
                table: "FeuilleTemps",
                column: "ProjetId");

            migrationBuilder.CreateIndex(
                name: "IX_FeuilleJours_FeuilleTempsAnnee_FeuilleTempsSemaine_FeuilleTempsEmployeId_FeuilleTempsProjetId",
                table: "FeuilleJours",
                columns: new[] { "FeuilleTempsAnnee", "FeuilleTempsSemaine", "FeuilleTempsEmployeId", "FeuilleTempsProjetId" });

            migrationBuilder.AddForeignKey(
                name: "FK_FeuilleJours_FeuilleTemps_FeuilleTempsAnnee_FeuilleTempsSemaine_FeuilleTempsEmployeId_FeuilleTempsProjetId",
                table: "FeuilleJours",
                columns: new[] { "FeuilleTempsAnnee", "FeuilleTempsSemaine", "FeuilleTempsEmployeId", "FeuilleTempsProjetId" },
                principalTable: "FeuilleTemps",
                principalColumns: new[] { "Annee", "Semaine", "EmployeId", "ProjetId" });

            migrationBuilder.AddForeignKey(
                name: "FK_FeuilleTemps_Projets_ProjetId",
                table: "FeuilleTemps",
                column: "ProjetId",
                principalTable: "Projets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Taches_FeuilleTemps_Annee_Semaine_MembreChargeId_ProjetId",
                table: "Taches",
                columns: new[] { "Annee", "Semaine", "MembreChargeId", "ProjetId" },
                principalTable: "FeuilleTemps",
                principalColumns: new[] { "Annee", "Semaine", "EmployeId", "ProjetId" });
        }
    }
}
