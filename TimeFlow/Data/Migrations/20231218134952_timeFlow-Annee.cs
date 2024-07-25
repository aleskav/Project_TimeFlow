using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeFlow.Data.Migrations
{
    /// <inheritdoc />
    public partial class timeFlowAnnee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Annee",
                table: "FeuilleTemps",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Annee",
                table: "FeuilleTemps");
        }
    }
}
