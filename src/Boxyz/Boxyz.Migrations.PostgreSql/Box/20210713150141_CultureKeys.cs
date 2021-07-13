using Microsoft.EntityFrameworkCore.Migrations;

namespace Boxyz.Migrations.PostgreSql.Box
{
    public partial class CultureKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_box_side_cultures",
                table: "box_side_cultures");

            migrationBuilder.DropPrimaryKey(
                name: "pk_box_shape_version_cultures",
                table: "box_shape_version_cultures");

            migrationBuilder.DropPrimaryKey(
                name: "pk_box_shape_side_cultures",
                table: "box_shape_side_cultures");

            migrationBuilder.DropPrimaryKey(
                name: "pk_box_shape_board_cultures",
                table: "box_shape_board_cultures");

            migrationBuilder.AddPrimaryKey(
                name: "pk_box_side_cultures",
                table: "box_side_cultures",
                columns: new[] { "culture", "box_side_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_box_shape_version_cultures",
                table: "box_shape_version_cultures",
                columns: new[] { "culture", "shape_version_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_box_shape_side_cultures",
                table: "box_shape_side_cultures",
                columns: new[] { "culture", "shape_side_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_box_shape_board_cultures",
                table: "box_shape_board_cultures",
                columns: new[] { "culture", "board_id" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_box_side_cultures",
                table: "box_side_cultures");

            migrationBuilder.DropPrimaryKey(
                name: "pk_box_shape_version_cultures",
                table: "box_shape_version_cultures");

            migrationBuilder.DropPrimaryKey(
                name: "pk_box_shape_side_cultures",
                table: "box_shape_side_cultures");

            migrationBuilder.DropPrimaryKey(
                name: "pk_box_shape_board_cultures",
                table: "box_shape_board_cultures");

            migrationBuilder.AddPrimaryKey(
                name: "pk_box_side_cultures",
                table: "box_side_cultures",
                column: "culture");

            migrationBuilder.AddPrimaryKey(
                name: "pk_box_shape_version_cultures",
                table: "box_shape_version_cultures",
                column: "culture");

            migrationBuilder.AddPrimaryKey(
                name: "pk_box_shape_side_cultures",
                table: "box_shape_side_cultures",
                column: "culture");

            migrationBuilder.AddPrimaryKey(
                name: "pk_box_shape_board_cultures",
                table: "box_shape_board_cultures",
                column: "culture");
        }
    }
}
