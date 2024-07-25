using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeFlow.Data.Migrations
{
    /// <inheritdoc />
    public partial class updatetasks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Taches_FeuilleTemps_FeuilleTempsId",
                table: "Taches");

            migrationBuilder.DropColumn(
                name: "Annee",
                table: "Taches");

            migrationBuilder.DropColumn(
                name: "Semaine",
                table: "Taches");

            migrationBuilder.AlterColumn<string>(
                name: "FeuilleTempsId",
                table: "Taches",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Taches",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_Taches_FeuilleTemps_FeuilleTempsId",
                table: "Taches",
                column: "FeuilleTempsId",
                principalTable: "FeuilleTemps",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Taches_FeuilleTemps_FeuilleTempsId",
                table: "Taches");

            migrationBuilder.AlterColumn<string>(
                name: "FeuilleTempsId",
                table: "Taches",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Taches",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Taches_FeuilleTemps_FeuilleTempsId",
                table: "Taches",
                column: "FeuilleTempsId",
                principalTable: "FeuilleTemps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
