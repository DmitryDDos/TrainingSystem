using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace trSys.Migrations
{
    /// <inheritdoc />
    public partial class AddLessonFiles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LessonFiles_Lessons_LessonId",
                table: "LessonFiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LessonFiles",
                table: "LessonFiles");

            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "LessonFiles");

            migrationBuilder.DropColumn(
                name: "FileSize",
                table: "LessonFiles");

            migrationBuilder.RenameTable(
                name: "LessonFiles",
                newName: "LessonFile");

            migrationBuilder.RenameColumn(
                name: "FilePath",
                table: "LessonFile",
                newName: "FileType");

            migrationBuilder.RenameIndex(
                name: "IX_LessonFiles_LessonId",
                table: "LessonFile",
                newName: "IX_LessonFile_LessonId");

            migrationBuilder.AddColumn<byte[]>(
                name: "Content",
                table: "LessonFile",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddPrimaryKey(
                name: "PK_LessonFile",
                table: "LessonFile",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LessonFile_Lessons_LessonId",
                table: "LessonFile",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LessonFile_Lessons_LessonId",
                table: "LessonFile");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LessonFile",
                table: "LessonFile");

            migrationBuilder.DropColumn(
                name: "Content",
                table: "LessonFile");

            migrationBuilder.RenameTable(
                name: "LessonFile",
                newName: "LessonFiles");

            migrationBuilder.RenameColumn(
                name: "FileType",
                table: "LessonFiles",
                newName: "FilePath");

            migrationBuilder.RenameIndex(
                name: "IX_LessonFile_LessonId",
                table: "LessonFiles",
                newName: "IX_LessonFiles_LessonId");

            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "LessonFiles",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "FileSize",
                table: "LessonFiles",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddPrimaryKey(
                name: "PK_LessonFiles",
                table: "LessonFiles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LessonFiles_Lessons_LessonId",
                table: "LessonFiles",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
