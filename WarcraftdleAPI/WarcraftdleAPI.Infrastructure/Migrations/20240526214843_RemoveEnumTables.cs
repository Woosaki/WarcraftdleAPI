using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WarcraftdleAPI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveEnumTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AffiliationWowCharacter");

            migrationBuilder.DropTable(
                name: "ExpansionWowCharacter");

            migrationBuilder.DropTable(
                name: "WowCharacterZone");

            migrationBuilder.DropTable(
                name: "Expansion");

            migrationBuilder.DropTable(
                name: "WowCharacter");

            migrationBuilder.DropTable(
                name: "Class");

            migrationBuilder.DropTable(
                name: "Gender");

            migrationBuilder.DropTable(
                name: "Race");

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
                name: "Character");

            migrationBuilder.CreateTable(
                name: "Class",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Class", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Expansion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Abbreviation = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expansion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Gender",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gender", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Race",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Race", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WowCharacter",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClassId = table.Column<int>(type: "integer", nullable: true),
                    GenderId = table.Column<int>(type: "integer", nullable: false),
                    RaceId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Photo = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WowCharacter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WowCharacter_Class_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Class",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WowCharacter_Gender_GenderId",
                        column: x => x.GenderId,
                        principalTable: "Gender",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WowCharacter_Race_RaceId",
                        column: x => x.RaceId,
                        principalTable: "Race",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AffiliationWowCharacter",
                columns: table => new
                {
                    AffiliationsId = table.Column<int>(type: "integer", nullable: false),
                    WowCharactersId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AffiliationWowCharacter", x => new { x.AffiliationsId, x.WowCharactersId });
                    table.ForeignKey(
                        name: "FK_AffiliationWowCharacter_Affiliation_AffiliationsId",
                        column: x => x.AffiliationsId,
                        principalTable: "Affiliation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AffiliationWowCharacter_WowCharacter_WowCharactersId",
                        column: x => x.WowCharactersId,
                        principalTable: "WowCharacter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExpansionWowCharacter",
                columns: table => new
                {
                    ExpansionsId = table.Column<int>(type: "integer", nullable: false),
                    WowCharactersId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpansionWowCharacter", x => new { x.ExpansionsId, x.WowCharactersId });
                    table.ForeignKey(
                        name: "FK_ExpansionWowCharacter_Expansion_ExpansionsId",
                        column: x => x.ExpansionsId,
                        principalTable: "Expansion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExpansionWowCharacter_WowCharacter_WowCharactersId",
                        column: x => x.WowCharactersId,
                        principalTable: "WowCharacter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WowCharacterZone",
                columns: table => new
                {
                    WowCharactersId = table.Column<int>(type: "integer", nullable: false),
                    ZonesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WowCharacterZone", x => new { x.WowCharactersId, x.ZonesId });
                    table.ForeignKey(
                        name: "FK_WowCharacterZone_WowCharacter_WowCharactersId",
                        column: x => x.WowCharactersId,
                        principalTable: "WowCharacter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WowCharacterZone_Zone_ZonesId",
                        column: x => x.ZonesId,
                        principalTable: "Zone",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Class",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Warrior" },
                    { 2, "Paladin" },
                    { 3, "Hunter" },
                    { 4, "Rogue" },
                    { 5, "Priest" },
                    { 6, "Shaman" },
                    { 7, "Mage" },
                    { 8, "Warlock" },
                    { 9, "Monk" },
                    { 10, "Druid" },
                    { 11, "Demon Hunter" },
                    { 12, "Death Knight" },
                    { 13, "Evoker" }
                });

            migrationBuilder.InsertData(
                table: "Expansion",
                columns: new[] { "Id", "Abbreviation", "Name" },
                values: new object[,]
                {
                    { 1, null, "Classic" },
                    { 2, "TBC", "The Burning Crusade" },
                    { 3, "WotLK", "Wrath of the Lich King" },
                    { 4, "Cata", "Cataclysm" },
                    { 5, "MOP", "Mists of Pandaria" },
                    { 6, "WOD", "Warlords of Draenor" },
                    { 7, null, "Legion" },
                    { 8, "BFA", "Battle for Azeroth" },
                    { 9, "SL", "Shadowlands" },
                    { 10, "DF", "Dragonflight" }
                });

            migrationBuilder.InsertData(
                table: "Gender",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Male" },
                    { 2, "Female" }
                });

            migrationBuilder.InsertData(
                table: "Race",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Human" },
                    { 2, "Dwarf" },
                    { 3, "Night Elf" },
                    { 4, "Gnome" },
                    { 5, "Draenei" },
                    { 6, "Worgen" },
                    { 7, "Pandaren" },
                    { 8, "Orc" },
                    { 9, "Undead" },
                    { 10, "Tauren" },
                    { 11, "Troll" },
                    { 12, "Blood Elf" },
                    { 13, "Goblin" },
                    { 14, "Dragon" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AffiliationWowCharacter_WowCharactersId",
                table: "AffiliationWowCharacter",
                column: "WowCharactersId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpansionWowCharacter_WowCharactersId",
                table: "ExpansionWowCharacter",
                column: "WowCharactersId");

            migrationBuilder.CreateIndex(
                name: "IX_WowCharacter_ClassId",
                table: "WowCharacter",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_WowCharacter_GenderId",
                table: "WowCharacter",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_WowCharacter_RaceId",
                table: "WowCharacter",
                column: "RaceId");

            migrationBuilder.CreateIndex(
                name: "IX_WowCharacterZone_ZonesId",
                table: "WowCharacterZone",
                column: "ZonesId");
        }
    }
}
