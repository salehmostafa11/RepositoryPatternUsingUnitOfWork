using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepositoryPatternUsingUOW.EF.Migrations
{
    /// <inheritdoc />
    public partial class BookEdited : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Books",
                newName: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Books",
                newName: "Title");
        }
    }
}
