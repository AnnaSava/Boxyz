using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Boxyz.Migrations.PostgreSql.Box
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "shape_boards",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true),
                    parent_board_id = table.Column<long>(type: "bigint", nullable: true),
                    level = table.Column<int>(type: "integer", nullable: false),
                    path = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_shape_boards", x => x.id);
                    table.ForeignKey(
                        name: "fk_shape_boards_shape_boards_parent_board_id",
                        column: x => x.parent_board_id,
                        principalTable: "shape_boards",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "shape_board_cultures",
                columns: table => new
                {
                    content_id = table.Column<long>(type: "bigint", nullable: false),
                    culture = table.Column<string>(type: "text", nullable: false),
                    title = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_shape_board_cultures", x => new { x.culture, x.content_id });
                    table.ForeignKey(
                        name: "fk_shape_board_cultures_shape_boards_content_id",
                        column: x => x.content_id,
                        principalTable: "shape_boards",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "shapes",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    const_name = table.Column<string>(type: "text", nullable: true),
                    board_id = table.Column<long>(type: "bigint", nullable: false),
                    last_updated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_shapes", x => x.id);
                    table.ForeignKey(
                        name: "fk_shapes_shape_boards_board_id",
                        column: x => x.board_id,
                        principalTable: "shape_boards",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "boxes",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    shape_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_boxes", x => x.id);
                    table.ForeignKey(
                        name: "fk_boxes_shapes_shape_id",
                        column: x => x.shape_id,
                        principalTable: "shapes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "shape_versions",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    content_id = table.Column<long>(type: "bigint", nullable: false),
                    is_approved = table.Column<bool>(type: "boolean", nullable: false),
                    created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_shape_versions", x => x.id);
                    table.ForeignKey(
                        name: "fk_shape_versions_shapes_content_id",
                        column: x => x.content_id,
                        principalTable: "shapes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "box_versions",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    content_id = table.Column<long>(type: "bigint", nullable: false),
                    shape_version_id = table.Column<long>(type: "bigint", nullable: false),
                    created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    is_approved = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_box_versions", x => x.id);
                    table.ForeignKey(
                        name: "fk_box_versions_boxes_content_id",
                        column: x => x.content_id,
                        principalTable: "boxes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_box_versions_shape_versions_shape_version_id",
                        column: x => x.shape_version_id,
                        principalTable: "shape_versions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "shape_sides",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    const_name = table.Column<string>(type: "text", nullable: true),
                    shape_version_id = table.Column<long>(type: "bigint", nullable: false),
                    data_type = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_shape_sides", x => x.id);
                    table.ForeignKey(
                        name: "fk_shape_sides_shape_versions_shape_version_id",
                        column: x => x.shape_version_id,
                        principalTable: "shape_versions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "shape_version_cultures",
                columns: table => new
                {
                    content_id = table.Column<long>(type: "bigint", nullable: false),
                    culture = table.Column<string>(type: "text", nullable: false),
                    title = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_shape_version_cultures", x => new { x.culture, x.content_id });
                    table.ForeignKey(
                        name: "fk_shape_version_cultures_shape_versions_content_id",
                        column: x => x.content_id,
                        principalTable: "shape_versions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "box_sides",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    box_version_id = table.Column<long>(type: "bigint", nullable: false),
                    shape_side_id = table.Column<long>(type: "bigint", nullable: false),
                    universal_value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_box_sides", x => x.id);
                    table.ForeignKey(
                        name: "fk_box_sides_box_versions_box_version_id",
                        column: x => x.box_version_id,
                        principalTable: "box_versions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_box_sides_shape_sides_shape_side_id",
                        column: x => x.shape_side_id,
                        principalTable: "shape_sides",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "shape_side_cultures",
                columns: table => new
                {
                    content_id = table.Column<long>(type: "bigint", nullable: false),
                    culture = table.Column<string>(type: "text", nullable: false),
                    title = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_shape_side_cultures", x => new { x.culture, x.content_id });
                    table.ForeignKey(
                        name: "fk_shape_side_cultures_shape_sides_content_id",
                        column: x => x.content_id,
                        principalTable: "shape_sides",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "box_side_cultures",
                columns: table => new
                {
                    content_id = table.Column<long>(type: "bigint", nullable: false),
                    culture = table.Column<string>(type: "text", nullable: false),
                    value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_box_side_cultures", x => new { x.culture, x.content_id });
                    table.ForeignKey(
                        name: "fk_box_side_cultures_box_sides_content_id",
                        column: x => x.content_id,
                        principalTable: "box_sides",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_box_side_cultures_content_id",
                table: "box_side_cultures",
                column: "content_id");

            migrationBuilder.CreateIndex(
                name: "ix_box_sides_box_version_id",
                table: "box_sides",
                column: "box_version_id");

            migrationBuilder.CreateIndex(
                name: "ix_box_sides_shape_side_id",
                table: "box_sides",
                column: "shape_side_id");

            migrationBuilder.CreateIndex(
                name: "ix_box_versions_content_id",
                table: "box_versions",
                column: "content_id");

            migrationBuilder.CreateIndex(
                name: "ix_box_versions_shape_version_id",
                table: "box_versions",
                column: "shape_version_id");

            migrationBuilder.CreateIndex(
                name: "ix_boxes_shape_id",
                table: "boxes",
                column: "shape_id");

            migrationBuilder.CreateIndex(
                name: "ix_shape_board_cultures_content_id",
                table: "shape_board_cultures",
                column: "content_id");

            migrationBuilder.CreateIndex(
                name: "ix_shape_boards_parent_board_id",
                table: "shape_boards",
                column: "parent_board_id");

            migrationBuilder.CreateIndex(
                name: "ix_shape_side_cultures_content_id",
                table: "shape_side_cultures",
                column: "content_id");

            migrationBuilder.CreateIndex(
                name: "ix_shape_sides_shape_version_id",
                table: "shape_sides",
                column: "shape_version_id");

            migrationBuilder.CreateIndex(
                name: "ix_shape_version_cultures_content_id",
                table: "shape_version_cultures",
                column: "content_id");

            migrationBuilder.CreateIndex(
                name: "ix_shape_versions_content_id",
                table: "shape_versions",
                column: "content_id");

            migrationBuilder.CreateIndex(
                name: "ix_shapes_board_id",
                table: "shapes",
                column: "board_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "box_side_cultures");

            migrationBuilder.DropTable(
                name: "shape_board_cultures");

            migrationBuilder.DropTable(
                name: "shape_side_cultures");

            migrationBuilder.DropTable(
                name: "shape_version_cultures");

            migrationBuilder.DropTable(
                name: "box_sides");

            migrationBuilder.DropTable(
                name: "box_versions");

            migrationBuilder.DropTable(
                name: "shape_sides");

            migrationBuilder.DropTable(
                name: "boxes");

            migrationBuilder.DropTable(
                name: "shape_versions");

            migrationBuilder.DropTable(
                name: "shapes");

            migrationBuilder.DropTable(
                name: "shape_boards");
        }
    }
}
