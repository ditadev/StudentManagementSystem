#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Student.Persistence.Migrations;

public partial class initial : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            "Administrators");

        migrationBuilder.DropTable(
            "Admins");

        migrationBuilder.DropTable(
            "CourseStudent");

        migrationBuilder.DropTable(
            "Students");

        migrationBuilder.DropPrimaryKey(
            "PK_Users",
            "Users");

        migrationBuilder.RenameColumn(
            "AdmissionNumber",
            "Users",
            "PhoneNumber");

        migrationBuilder.RenameColumn(
            "Name",
            "Departments",
            "DepartmentName");

        migrationBuilder.AlterColumn<string>(
            "PasswordHash",
            "Users",
            "text",
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "text",
            oldNullable: true);

        migrationBuilder.AddColumn<long>(
                "UserId",
                "Users",
                "bigint",
                nullable: false,
                defaultValue: 0L)
            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

        migrationBuilder.AddColumn<string>(
            "DepartmentId",
            "Users",
            "text",
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<string>(
            "EmailAddress",
            "Users",
            "text",
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<string>(
            "FirstName",
            "Users",
            "text",
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<string>(
            "IdentificationNumber",
            "Users",
            "text",
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<string>(
            "LastName",
            "Users",
            "text",
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<int[]>(
            "Roles",
            "Users",
            "integer[]",
            nullable: false,
            defaultValue: new int[0]);

        migrationBuilder.AddColumn<string>(
            "AdmissionNumber",
            "Courses",
            "text",
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddPrimaryKey(
            "PK_Users",
            "Users",
            "UserId");

        migrationBuilder.CreateTable(
            "CourseUser",
            table => new
            {
                CoursesCourseCode = table.Column<string>("text", nullable: false),
                StudentsUserId = table.Column<long>("bigint", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_CourseUser", x => new { x.CoursesCourseCode, x.StudentsUserId });
                table.ForeignKey(
                    "FK_CourseUser_Courses_CoursesCourseCode",
                    x => x.CoursesCourseCode,
                    "Courses",
                    "CourseCode",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    "FK_CourseUser_Users_StudentsUserId",
                    x => x.StudentsUserId,
                    "Users",
                    "UserId",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            "IX_Users_DepartmentId",
            "Users",
            "DepartmentId");

        migrationBuilder.CreateIndex(
            "IX_Users_EmailAddress",
            "Users",
            "EmailAddress",
            unique: true);

        migrationBuilder.CreateIndex(
            "IX_Users_IdentificationNumber",
            "Users",
            "IdentificationNumber",
            unique: true);

        migrationBuilder.CreateIndex(
            "IX_Departments_DepartmentName",
            "Departments",
            "DepartmentName",
            unique: true);

        migrationBuilder.CreateIndex(
            "IX_CourseUser_StudentsUserId",
            "CourseUser",
            "StudentsUserId");

        migrationBuilder.AddForeignKey(
            "FK_Users_Departments_DepartmentId",
            "Users",
            "DepartmentId",
            "Departments",
            principalColumn: "DepartmentId",
            onDelete: ReferentialAction.Cascade);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            "FK_Users_Departments_DepartmentId",
            "Users");

        migrationBuilder.DropTable(
            "CourseUser");

        migrationBuilder.DropPrimaryKey(
            "PK_Users",
            "Users");

        migrationBuilder.DropIndex(
            "IX_Users_DepartmentId",
            "Users");

        migrationBuilder.DropIndex(
            "IX_Users_EmailAddress",
            "Users");

        migrationBuilder.DropIndex(
            "IX_Users_IdentificationNumber",
            "Users");

        migrationBuilder.DropIndex(
            "IX_Departments_DepartmentName",
            "Departments");

        migrationBuilder.DropColumn(
            "UserId",
            "Users");

        migrationBuilder.DropColumn(
            "DepartmentId",
            "Users");

        migrationBuilder.DropColumn(
            "EmailAddress",
            "Users");

        migrationBuilder.DropColumn(
            "FirstName",
            "Users");

        migrationBuilder.DropColumn(
            "IdentificationNumber",
            "Users");

        migrationBuilder.DropColumn(
            "LastName",
            "Users");

        migrationBuilder.DropColumn(
            "Roles",
            "Users");

        migrationBuilder.DropColumn(
            "AdmissionNumber",
            "Courses");

        migrationBuilder.RenameColumn(
            "PhoneNumber",
            "Users",
            "AdmissionNumber");

        migrationBuilder.RenameColumn(
            "DepartmentName",
            "Departments",
            "Name");

        migrationBuilder.AlterColumn<string>(
            "PasswordHash",
            "Users",
            "text",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "text");

        migrationBuilder.AddPrimaryKey(
            "PK_Users",
            "Users",
            "AdmissionNumber");

        migrationBuilder.CreateTable(
            "Administrators",
            table => new
            {
                AdminId = table.Column<string>("text", nullable: false),
                DepartmentId = table.Column<string>("text", nullable: false),
                Firstname = table.Column<string>("text", nullable: false),
                Lastname = table.Column<string>("text", nullable: false),
                Position = table.Column<string>("text", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Administrators", x => x.AdminId);
                table.ForeignKey(
                    "FK_Administrators_Departments_DepartmentId",
                    x => x.DepartmentId,
                    "Departments",
                    "DepartmentId",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "Admins",
            table => new
            {
                AdminId = table.Column<string>("text", nullable: false),
                PasswordHash = table.Column<string>("text", nullable: true)
            },
            constraints: table => { table.PrimaryKey("PK_Admins", x => x.AdminId); });

        migrationBuilder.CreateTable(
            "Students",
            table => new
            {
                AdmissionNumber = table.Column<string>("text", nullable: false),
                DepartmentId = table.Column<string>("text", nullable: false),
                Firstname = table.Column<string>("text", nullable: false),
                Lastname = table.Column<string>("text", nullable: false)
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
            "IX_Administrators_DepartmentId",
            "Administrators",
            "DepartmentId");

        migrationBuilder.CreateIndex(
            "IX_CourseStudent_StudentsAdmissionNumber",
            "CourseStudent",
            "StudentsAdmissionNumber");

        migrationBuilder.CreateIndex(
            "IX_Students_DepartmentId",
            "Students",
            "DepartmentId");
    }
}