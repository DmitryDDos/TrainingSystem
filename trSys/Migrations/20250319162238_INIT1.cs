using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace trSys.Migrations
{
    /// <inheritdoc />
    public partial class INIT1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Questions_QuestionId",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_CourseRegistrations_CourseRegistrationId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_CourseRegistrations_CourseRegistrationId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_CourseRegistrationId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Courses_CourseRegistrationId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Answers_QuestionId",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "CourseRegistrationId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CourseRegistrationId",
                table: "Courses");

            migrationBuilder.CreateTable(
                name: "AnswerQuestion",
                columns: table => new
                {
                    AnswersId = table.Column<int>(type: "integer", nullable: false),
                    QuestionsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswerQuestion", x => new { x.AnswersId, x.QuestionsId });
                    table.ForeignKey(
                        name: "FK_AnswerQuestion_Answers_AnswersId",
                        column: x => x.AnswersId,
                        principalTable: "Answers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnswerQuestion_Questions_QuestionsId",
                        column: x => x.QuestionsId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseRegistrations_CourseId",
                table: "CourseRegistrations",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseRegistrations_UserId",
                table: "CourseRegistrations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AnswerQuestion_QuestionsId",
                table: "AnswerQuestion",
                column: "QuestionsId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseRegistrations_Courses_CourseId",
                table: "CourseRegistrations",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseRegistrations_Users_UserId",
                table: "CourseRegistrations",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseRegistrations_Courses_CourseId",
                table: "CourseRegistrations");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseRegistrations_Users_UserId",
                table: "CourseRegistrations");

            migrationBuilder.DropTable(
                name: "AnswerQuestion");

            migrationBuilder.DropIndex(
                name: "IX_CourseRegistrations_CourseId",
                table: "CourseRegistrations");

            migrationBuilder.DropIndex(
                name: "IX_CourseRegistrations_UserId",
                table: "CourseRegistrations");

            migrationBuilder.AddColumn<int>(
                name: "CourseRegistrationId",
                table: "Users",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CourseRegistrationId",
                table: "Courses",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_CourseRegistrationId",
                table: "Users",
                column: "CourseRegistrationId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CourseRegistrationId",
                table: "Courses",
                column: "CourseRegistrationId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuestionId",
                table: "Answers",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Questions_QuestionId",
                table: "Answers",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_CourseRegistrations_CourseRegistrationId",
                table: "Courses",
                column: "CourseRegistrationId",
                principalTable: "CourseRegistrations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_CourseRegistrations_CourseRegistrationId",
                table: "Users",
                column: "CourseRegistrationId",
                principalTable: "CourseRegistrations",
                principalColumn: "Id");
        }
    }
}
