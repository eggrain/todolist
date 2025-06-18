using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace todolist.Migrations
{
    /// <inheritdoc />
    public partial class RemoveGoalsRelationshipToTodo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GoalTodo");

            migrationBuilder.AddColumn<TimeOnly>(
                name: "AtTime",
                table: "Todos",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Completed",
                table: "Todos",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AtTime",
                table: "Todos");

            migrationBuilder.DropColumn(
                name: "Completed",
                table: "Todos");

            migrationBuilder.CreateTable(
                name: "GoalTodo",
                columns: table => new
                {
                    GoalsId = table.Column<string>(type: "TEXT", nullable: false),
                    TodosId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoalTodo", x => new { x.GoalsId, x.TodosId });
                    table.ForeignKey(
                        name: "FK_GoalTodo_Goals_GoalsId",
                        column: x => x.GoalsId,
                        principalTable: "Goals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GoalTodo_Todos_TodosId",
                        column: x => x.TodosId,
                        principalTable: "Todos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GoalTodo_TodosId",
                table: "GoalTodo",
                column: "TodosId");
        }
    }
}
