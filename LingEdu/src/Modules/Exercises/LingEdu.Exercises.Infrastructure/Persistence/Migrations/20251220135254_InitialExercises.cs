using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LingEdu.Exercises.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialExercises : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Exercises",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    IsPremium = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercises", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserProgress",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExerciseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Points = table.Column<int>(type: "int", nullable: false),
                    IsCorrect = table.Column<bool>(type: "bit", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProgress", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuizQuestions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExerciseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Prompt = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuizQuestions_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuizOptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsCorrect = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuizOptions_QuizQuestions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "QuizQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Exercises",
                columns: new[] { "Id", "IsPremium", "Level", "Title", "Type" },
                values: new object[,]
                {
                    { new Guid("33333333-3333-3333-3333-333333333333"), false, 1, "Present Simple Basics", 1 },
                    { new Guid("44444444-4444-4444-4444-444444444444"), true, 2, "Past Simple Challenge", 1 }
                });

            migrationBuilder.InsertData(
                table: "QuizQuestions",
                columns: new[] { "Id", "ExerciseId", "Prompt" },
                values: new object[,]
                {
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa1"), new Guid("33333333-3333-3333-3333-333333333333"), "Choose the correct form: She ___ to school every day." },
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa2"), new Guid("33333333-3333-3333-3333-333333333333"), "I ___ coffee in the morning." },
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb1"), new Guid("44444444-4444-4444-4444-444444444444"), "Choose the correct form: Yesterday, I ___ a movie." },
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb2"), new Guid("44444444-4444-4444-4444-444444444444"), "He ___ to work last Monday." }
                });

            migrationBuilder.InsertData(
                table: "QuizOptions",
                columns: new[] { "Id", "IsCorrect", "QuestionId", "Text" },
                values: new object[,]
                {
                    { new Guid("c1111111-1111-1111-1111-111111111111"), true, new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa1"), "goes" },
                    { new Guid("c1111111-1111-1111-1111-111111111112"), false, new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa1"), "go" },
                    { new Guid("c1111111-1111-1111-1111-111111111113"), false, new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa1"), "going" },
                    { new Guid("c1111111-1111-1111-1111-111111111114"), false, new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa1"), "gone" },
                    { new Guid("c2222222-2222-2222-2222-222222222221"), true, new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa2"), "drink" },
                    { new Guid("c2222222-2222-2222-2222-222222222222"), false, new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa2"), "drinks" },
                    { new Guid("c2222222-2222-2222-2222-222222222223"), false, new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa2"), "drank" },
                    { new Guid("c2222222-2222-2222-2222-222222222224"), false, new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa2"), "drinking" },
                    { new Guid("d1111111-1111-1111-1111-111111111111"), true, new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb1"), "watched" },
                    { new Guid("d1111111-1111-1111-1111-111111111112"), false, new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb1"), "watch" },
                    { new Guid("d1111111-1111-1111-1111-111111111113"), false, new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb1"), "watches" },
                    { new Guid("d1111111-1111-1111-1111-111111111114"), false, new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb1"), "watching" },
                    { new Guid("d2222222-2222-2222-2222-222222222221"), true, new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb2"), "went" },
                    { new Guid("d2222222-2222-2222-2222-222222222222"), false, new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb2"), "go" },
                    { new Guid("d2222222-2222-2222-2222-222222222223"), false, new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb2"), "goes" },
                    { new Guid("d2222222-2222-2222-2222-222222222224"), false, new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb2"), "going" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuizOptions_QuestionId",
                table: "QuizOptions",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizQuestions_ExerciseId",
                table: "QuizQuestions",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProgress_UserId_CompletedAt",
                table: "UserProgress",
                columns: new[] { "UserId", "CompletedAt" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuizOptions");

            migrationBuilder.DropTable(
                name: "UserProgress");

            migrationBuilder.DropTable(
                name: "QuizQuestions");

            migrationBuilder.DropTable(
                name: "Exercises");
        }
    }
}
