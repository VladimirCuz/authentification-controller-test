using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace authentification_controller_test.Migrations
{
    /// <inheritdoc />
    public partial class CommentsFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "Comments",
                newName: "CommentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CommentId",
                table: "Comments",
                newName: "id");
        }
    }
}
