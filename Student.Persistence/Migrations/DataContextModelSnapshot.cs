﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Student.Persistence;

#nullable disable

namespace Student.Persistence.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CourseDepartment", b =>
                {
                    b.Property<string>("CoursesCourseCode")
                        .HasColumnType("text");

                    b.Property<string>("DepartmentsDepartmentId")
                        .HasColumnType("text");

                    b.HasKey("CoursesCourseCode", "DepartmentsDepartmentId");

                    b.HasIndex("DepartmentsDepartmentId");

                    b.ToTable("CourseDepartment");
                });

            modelBuilder.Entity("CourseUser", b =>
                {
                    b.Property<string>("CoursesCourseCode")
                        .HasColumnType("text");

                    b.Property<long>("StudentsUserId")
                        .HasColumnType("bigint");

                    b.HasKey("CoursesCourseCode", "StudentsUserId");

                    b.HasIndex("StudentsUserId");

                    b.ToTable("CourseUser");
                });

            modelBuilder.Entity("Student.Model.Course", b =>
                {
                    b.Property<string>("CourseCode")
                        .HasColumnType("text");

                    b.Property<string>("CourseTitle")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("CreditLoad")
                        .HasColumnType("integer");

                    b.Property<string>("DepartmentId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("CourseCode");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("Student.Model.Department", b =>
                {
                    b.Property<string>("DepartmentId")
                        .HasColumnType("text");

                    b.Property<string>("DepartmentName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("DepartmentId");

                    b.HasIndex("DepartmentName")
                        .IsUnique();

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("Student.Model.User", b =>
                {
                    b.Property<long>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("UserId"));

                    b.Property<string>("DepartmentId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("IdentificationNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int[]>("Roles")
                        .IsRequired()
                        .HasColumnType("integer[]");

                    b.HasKey("UserId");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("EmailAddress")
                        .IsUnique();

                    b.HasIndex("IdentificationNumber")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CourseDepartment", b =>
                {
                    b.HasOne("Student.Model.Course", null)
                        .WithMany()
                        .HasForeignKey("CoursesCourseCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Student.Model.Department", null)
                        .WithMany()
                        .HasForeignKey("DepartmentsDepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CourseUser", b =>
                {
                    b.HasOne("Student.Model.Course", null)
                        .WithMany()
                        .HasForeignKey("CoursesCourseCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Student.Model.User", null)
                        .WithMany()
                        .HasForeignKey("StudentsUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Student.Model.User", b =>
                {
                    b.HasOne("Student.Model.Department", "Department")
                        .WithMany("Users")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("Student.Model.Department", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
