using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Student.Persistence.Migrations
{
    public partial class third : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdmissionNumber",
                table: "Courses");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdmissionNumber",
                table: "Courses",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
