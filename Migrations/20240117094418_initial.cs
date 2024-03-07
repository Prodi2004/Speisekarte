using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Speisekarte.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Speisen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Titel = table.Column<string>(type: "TEXT", nullable: false),
                    Notizen = table.Column<string>(type: "TEXT", nullable: true),
                    Sterne = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Speisen", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Zutaten",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Beschreibung = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Einheit = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Menge = table.Column<decimal>(type: "TEXT", nullable: false),
                    SpeiseId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zutaten", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Zutaten_Speisen_SpeiseId",
                        column: x => x.SpeiseId,
                        principalTable: "Speisen",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Zutaten_SpeiseId",
                table: "Zutaten",
                column: "SpeiseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Zutaten");

            migrationBuilder.DropTable(
                name: "Speisen");
        }
    }
}
