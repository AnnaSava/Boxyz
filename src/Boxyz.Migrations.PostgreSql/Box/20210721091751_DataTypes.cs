using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Boxyz.Migrations.PostgreSql.Box
{
    public partial class DataTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "order_number",
                table: "shape_sides",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "box_side_floats",
                columns: table => new
                {
                    box_side_id = table.Column<long>(type: "bigint", nullable: false),
                    value = table.Column<double>(type: "double precision", nullable: false),
                    measure = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_box_side_floats", x => x.box_side_id);
                    table.ForeignKey(
                        name: "fk_box_side_floats_box_sides_box_side_id",
                        column: x => x.box_side_id,
                        principalTable: "box_sides",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "box_side_integers",
                columns: table => new
                {
                    box_side_id = table.Column<long>(type: "bigint", nullable: false),
                    value = table.Column<long>(type: "bigint", nullable: false),
                    measure = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_box_side_integers", x => x.box_side_id);
                    table.ForeignKey(
                        name: "fk_box_side_integers_box_sides_box_side_id",
                        column: x => x.box_side_id,
                        principalTable: "box_sides",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "box_side_links",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    box_side_id = table.Column<long>(type: "bigint", nullable: false),
                    linked_box_version_id = table.Column<long>(type: "bigint", nullable: false),
                    order_number = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_box_side_links", x => x.id);
                    table.ForeignKey(
                        name: "fk_box_side_links_box_sides_box_side_id",
                        column: x => x.box_side_id,
                        principalTable: "box_sides",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_box_side_links_box_versions_linked_box_version_id",
                        column: x => x.linked_box_version_id,
                        principalTable: "box_versions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "box_side_moneys",
                columns: table => new
                {
                    box_side_id = table.Column<long>(type: "bigint", nullable: false),
                    value = table.Column<decimal>(type: "numeric", nullable: false),
                    currency = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_box_side_moneys", x => x.box_side_id);
                    table.ForeignKey(
                        name: "fk_box_side_moneys_box_sides_box_side_id",
                        column: x => x.box_side_id,
                        principalTable: "box_sides",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "box_side_points",
                columns: table => new
                {
                    box_side_id = table.Column<long>(type: "bigint", nullable: false),
                    x = table.Column<double>(type: "double precision", nullable: false),
                    y = table.Column<double>(type: "double precision", nullable: false),
                    z = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_box_side_points", x => x.box_side_id);
                    table.ForeignKey(
                        name: "fk_box_side_points_box_sides_box_side_id",
                        column: x => x.box_side_id,
                        principalTable: "box_sides",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_box_side_links_box_side_id",
                table: "box_side_links",
                column: "box_side_id");

            migrationBuilder.CreateIndex(
                name: "ix_box_side_links_linked_box_version_id",
                table: "box_side_links",
                column: "linked_box_version_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "box_side_floats");

            migrationBuilder.DropTable(
                name: "box_side_integers");

            migrationBuilder.DropTable(
                name: "box_side_links");

            migrationBuilder.DropTable(
                name: "box_side_moneys");

            migrationBuilder.DropTable(
                name: "box_side_points");

            migrationBuilder.DropColumn(
                name: "order_number",
                table: "shape_sides");
        }
    }
}
