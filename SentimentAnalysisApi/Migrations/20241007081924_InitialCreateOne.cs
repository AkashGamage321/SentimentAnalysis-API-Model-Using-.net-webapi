using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SentimentAnalysisApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateOne : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "SentimentAnalyses");

            migrationBuilder.DropColumn(
                name: "PredictionScore",
                table: "SentimentAnalyses");

            migrationBuilder.RenameColumn(
                name: "SentimentLabel",
                table: "SentimentAnalyses",
                newName: "Sentiment");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Sentiment",
                table: "SentimentAnalyses",
                newName: "SentimentLabel");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "SentimentAnalyses",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "PredictionScore",
                table: "SentimentAnalyses",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
