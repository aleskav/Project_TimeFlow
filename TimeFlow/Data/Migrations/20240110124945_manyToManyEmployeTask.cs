using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeFlow.Data.Migrations
{
    /// <inheritdoc />
    public partial class manyToManyEmployeTask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Taches_AspNetUsers_MembreChargeId",
                table: "Taches");

            migrationBuilder.DropIndex(
                name: "IX_Taches_MembreChargeId",
                table: "Taches");

            migrationBuilder.DropColumn(
                name: "MembreChargeId",
                table: "Taches");

            migrationBuilder.CreateTable(
                name: "EmployeTache",
                columns: table => new
                {
                    MembresChargeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TachesId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeTache", x => new { x.MembresChargeId, x.TachesId });
                    table.ForeignKey(
                        name: "FK_EmployeTache_AspNetUsers_MembresChargeId",
                        column: x => x.MembresChargeId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeTache_Taches_TachesId",
                        column: x => x.TachesId,
                        principalTable: "Taches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeTache_TachesId",
                table: "EmployeTache",
                column: "TachesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeTache");

            migrationBuilder.AddColumn<string>(
                name: "MembreChargeId",
                table: "Taches",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Taches_MembreChargeId",
                table: "Taches",
                column: "MembreChargeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Taches_AspNetUsers_MembreChargeId",
                table: "Taches",
                column: "MembreChargeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
