using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Student_Event_Finder.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEventModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EventDate",
                table: "Events",
                newName: "Date");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Organizer",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Time",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Organizer",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Events",
                newName: "EventDate");
        }
    }
}
