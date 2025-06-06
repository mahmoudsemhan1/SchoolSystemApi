using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Rename_Grade_To_StudentGrade_And_Add_SchoolGrade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SchoolGradeId",
                table: "Students",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SchoolGradeId",
                table: "Classes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SchoolGrades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolGrades", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_SchoolGradeId",
                table: "Students",
                column: "SchoolGradeId");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_SchoolGradeId",
                table: "Classes",
                column: "SchoolGradeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_SchoolGrades_SchoolGradeId",
                table: "Classes",
                column: "SchoolGradeId",
                principalTable: "SchoolGrades",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_SchoolGrades_SchoolGradeId",
                table: "Students",
                column: "SchoolGradeId",
                principalTable: "SchoolGrades",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_SchoolGrades_SchoolGradeId",
                table: "Classes");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_SchoolGrades_SchoolGradeId",
                table: "Students");

            migrationBuilder.DropTable(
                name: "SchoolGrades");

            migrationBuilder.DropIndex(
                name: "IX_Students_SchoolGradeId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Classes_SchoolGradeId",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "SchoolGradeId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "SchoolGradeId",
                table: "Classes");
        }
    }
}
