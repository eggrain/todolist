using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace todolist.Migrations
{
    /// <inheritdoc />
    public partial class AddGoalManyTodos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GoalId",
                table: "Todos",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Todos_GoalId",
                table: "Todos",
                column: "GoalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Todos_Goals_GoalId",
                table: "Todos",
                column: "GoalId",
                principalTable: "Goals",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Todos_Goals_GoalId",
                table: "Todos");

            migrationBuilder.DropIndex(
                name: "IX_Todos_GoalId",
                table: "Todos");

            migrationBuilder.DropColumn(
                name: "GoalId",
                table: "Todos");
        }
    }
}
