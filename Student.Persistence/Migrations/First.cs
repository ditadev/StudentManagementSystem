#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Student.Persistence.Migrations;

public partial class First : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            "Courses",
            table => new
            {
                CourseCode = table.Column<string>("text", nullable: false),
                CourseTitle = table.Column<string>("text", nullable: false),
                CreditLoad = table.Column<int>("integer", nullable: false)
            },
            constraints: table => { table.PrimaryKey("PK_Courses", x => x.CourseCode); });

        migrationBuilder.CreateTable(
            "Departments",
            table => new
            {
                DepartmentId = table.Column<string>("text", nullable: false),
                Name = table.Column<string>("text", nullable: false)
            },
            constraints: table => { table.PrimaryKey("PK_Departments", x => x.DepartmentId); });

        migrationBuilder.CreateTable(
            "Users",
            table => new
            {
                AdmissionNumber = table.Column<string>("text", nullable: false),
                PasswordHash = table.Column<string>("text", nullable: true)
            },
            constraints: table => { table.PrimaryKey("PK_Users", x => x.AdmissionNumber); });

        migrationBuilder.CreateTable(
            "CourseDepartment",
            table => new
            {
                CoursesCourseCode = table.Column<string>("text", nullable: false),
                DepartmentsDepartmentId = table.Column<string>("text", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_CourseDepartment", x => new { x.CoursesCourseCode, x.DepartmentsDepartmentId });
                table.ForeignKey(
                    "FK_CourseDepartment_Courses_CoursesCourseCode",
                    x => x.CoursesCourseCode,
                    "Courses",
                    "CourseCode",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    "FK_CourseDepartment_Departments_DepartmentsDepartmentId",
                    x => x.DepartmentsDepartmentId,
                    "Departments",
                    "DepartmentId",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "Students",
            table => new
            {
                AdmissionNumber = table.Column<string>("text", nullable: false),
                Firstname = table.Column<string>("text", nullable: false),
                Lastname = table.Column<string>("text", nullable: false),
                DepartmentId = table.Column<string>("text", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Students", x => x.AdmissionNumber);
                table.ForeignKey(
                    "FK_Students_Departments_DepartmentId",
                    x => x.DepartmentId,
                    "Departments",
                    "DepartmentId",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "CourseStudent",
            table => new
            {
                CoursesCourseCode = table.Column<string>("text", nullable: false),
                StudentsAdmissionNumber = table.Column<string>("text", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_CourseStudent", x => new { x.CoursesCourseCode, x.StudentsAdmissionNumber });
                table.ForeignKey(
                    "FK_CourseStudent_Courses_CoursesCourseCode",
                    x => x.CoursesCourseCode,
                    "Courses",
                    "CourseCode",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    "FK_CourseStudent_Students_StudentsAdmissionNumber",
                    x => x.StudentsAdmissionNumber,
                    "Students",
                    "AdmissionNumber",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            "IX_CourseDepartment_DepartmentsDepartmentId",
            "CourseDepartment",
            "DepartmentsDepartmentId");

        migrationBuilder.CreateIndex(
            "IX_CourseStudent_StudentsAdmissionNumber",
            "CourseStudent",
            "StudentsAdmissionNumber");

        migrationBuilder.CreateIndex(
            "IX_Students_DepartmentId",
            "Students",
            "DepartmentId");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            "CourseDepartment");

        migrationBuilder.DropTable(
            "CourseStudent");

        migrationBuilder.DropTable(
            "Users");

        migrationBuilder.DropTable(
            "Courses");

        migrationBuilder.DropTable(
            "Students");

        migrationBuilder.DropTable(
            "Departments");
    }
}