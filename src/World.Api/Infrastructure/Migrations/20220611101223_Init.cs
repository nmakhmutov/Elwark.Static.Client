using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace World.Api.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "countries",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    alpha2 = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false),
                    alpha3 = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    Flag = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_countries", x => x.id);
                    table.UniqueConstraint("AK_countries_alpha2", x => x.alpha2);
                    table.UniqueConstraint("AK_countries_alpha3", x => x.alpha3);
                });

            migrationBuilder.CreateTable(
                name: "country_translations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    country_id = table.Column<int>(type: "integer", nullable: false),
                    language = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false),
                    official = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    common = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_country_translations", x => x.id);
                    table.UniqueConstraint("AK_country_translations_country_id_language", x => new { x.country_id, x.language });
                    table.ForeignKey(
                        name: "FK_country_translations_countries_country_id",
                        column: x => x.country_id,
                        principalTable: "countries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "country_translations");

            migrationBuilder.DropTable(
                name: "countries");
        }
    }
}
