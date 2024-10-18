﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PROG6212_PART2_ST10396724.Data;

#nullable disable

namespace PROG6212_PART2_ST10396724.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241017150050_CreateProgTableFinalA")]
    partial class CreateProgTableFinalA
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PROG6212_PART2_ST10396724.Models.AcademicManager", b =>
                {
                    b.Property<int>("AcademicManagerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AcademicManagerID"));

                    b.Property<string>("AcademicManagerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("acaEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AcademicManagerID");

                    b.ToTable("academicManagers");
                });

            modelBuilder.Entity("PROG6212_PART2_ST10396724.Models.Claim", b =>
                {
                    b.Property<int>("ClaimID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClaimID"));

                    b.Property<int>("LecturerID")
                        .HasColumnType("int");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("hourlyPay")
                        .HasColumnType("int");

                    b.Property<int>("hoursWorked")
                        .HasColumnType("int");

                    b.HasKey("ClaimID");

                    b.HasIndex("LecturerID");

                    b.ToTable("claim");
                });

            modelBuilder.Entity("PROG6212_PART2_ST10396724.Models.Lecturer", b =>
                {
                    b.Property<int>("LecturerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LecturerID"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LecturerName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("LecturerID");

                    b.ToTable("lecturer");
                });

            modelBuilder.Entity("PROG6212_PART2_ST10396724.Models.ProgCoOrdinator", b =>
                {
                    b.Property<int>("ProgCoOrdinatorID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProgCoOrdinatorID"));

                    b.Property<string>("ProgCoOrdinatorName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("progEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProgCoOrdinatorID");

                    b.ToTable("progs");
                });

            modelBuilder.Entity("PROG6212_PART2_ST10396724.Models.test", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("test");
                });

            modelBuilder.Entity("PROG6212_PART2_ST10396724.Models.Claim", b =>
                {
                    b.HasOne("PROG6212_PART2_ST10396724.Models.Lecturer", "lecturer")
                        .WithMany("claims")
                        .HasForeignKey("LecturerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("lecturer");
                });

            modelBuilder.Entity("PROG6212_PART2_ST10396724.Models.Lecturer", b =>
                {
                    b.Navigation("claims");
                });
#pragma warning restore 612, 618
        }
    }
}
