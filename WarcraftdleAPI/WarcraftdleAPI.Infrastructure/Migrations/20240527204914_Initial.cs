using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WarcraftdleAPI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Affiliation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Affiliation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Character",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Gender = table.Column<int>(type: "integer", nullable: false),
                    Race = table.Column<int>(type: "integer", nullable: false),
                    Class = table.Column<int>(type: "integer", nullable: true),
                    Expansions = table.Column<int[]>(type: "integer[]", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Character", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Zone",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zone", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AffiliationCharacter",
                columns: table => new
                {
                    AffiliationsId = table.Column<int>(type: "integer", nullable: false),
                    CharactersId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AffiliationCharacter", x => new { x.AffiliationsId, x.CharactersId });
                    table.ForeignKey(
                        name: "FK_AffiliationCharacter_Affiliation_AffiliationsId",
                        column: x => x.AffiliationsId,
                        principalTable: "Affiliation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AffiliationCharacter_Character_CharactersId",
                        column: x => x.CharactersId,
                        principalTable: "Character",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CharacterZone",
                columns: table => new
                {
                    CharactersId = table.Column<int>(type: "integer", nullable: false),
                    ZonesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterZone", x => new { x.CharactersId, x.ZonesId });
                    table.ForeignKey(
                        name: "FK_CharacterZone_Character_CharactersId",
                        column: x => x.CharactersId,
                        principalTable: "Character",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterZone_Zone_ZonesId",
                        column: x => x.ZonesId,
                        principalTable: "Zone",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AffiliationCharacter_CharactersId",
                table: "AffiliationCharacter",
                column: "CharactersId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterZone_ZonesId",
                table: "CharacterZone",
                column: "ZonesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AffiliationCharacter");

            migrationBuilder.DropTable(
                name: "CharacterZone");

            migrationBuilder.DropTable(
                name: "Affiliation");

            migrationBuilder.DropTable(
                name: "Character");

            migrationBuilder.DropTable(
                name: "Zone");
        }
    }
}
