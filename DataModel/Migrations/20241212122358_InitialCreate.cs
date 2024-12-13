using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataModel.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Genera",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    scientificName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    colloquialName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genera", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Species",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    scientificName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    colloquialName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    genusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Species", x => x.id);
                    table.ForeignKey(
                        name: "FK_Species_Genus",
                        column: x => x.genusId,
                        principalTable: "Genera",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Species_genusId",
                table: "Species",
                column: "genusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Species");

            migrationBuilder.DropTable(
                name: "Genera");
        }
    }
}
