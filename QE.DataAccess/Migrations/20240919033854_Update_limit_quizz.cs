using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QE.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Update_limit_quizz : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LimitTime",
                table: "Competitions");

            migrationBuilder.AddColumn<DateTime>(
                name: "LimitTime",
                table: "Quizzes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "CompetitionQuizzes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LimitTime",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "CompetitionQuizzes");

            migrationBuilder.AddColumn<DateTime>(
                name: "LimitTime",
                table: "Competitions",
                type: "datetime2",
                nullable: true);
        }
    }
}
