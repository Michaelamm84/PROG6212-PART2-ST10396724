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
    [Migration("20241018163047_fixfix")]
    partial class fixfix
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

                    b.Property<string>("ApprovalStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

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

            modelBuilder.Entity("PROG6212_PART2_ST10396724.Models.ClaimApproval", b =>
                {
                    b.Property<int>("ClaimApprovalID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClaimApprovalID"));

                    b.Property<int>("AcademicManagerID")
                        .HasColumnType("int");

                    b.Property<int>("ClaimID")
                        .HasColumnType("int");

                    b.Property<int>("LecturerID")
                        .HasColumnType("int");

                    b.Property<int>("ProgCoOrdinatorID")
                        .HasColumnType("int");

                    b.Property<string>("newApprovalStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ClaimApprovalID");

                    b.HasIndex("AcademicManagerID");

                    b.HasIndex("ClaimID");

                    b.HasIndex("LecturerID");

                    b.HasIndex("ProgCoOrdinatorID");

                    b.ToTable("claimApproval");
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

            modelBuilder.Entity("PROG6212_PART2_ST10396724.Models.ClaimApproval", b =>
                {
                    b.HasOne("PROG6212_PART2_ST10396724.Models.AcademicManager", "AcademicManagers")
                        .WithMany()
                        .HasForeignKey("AcademicManagerID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("PROG6212_PART2_ST10396724.Models.Claim", "claim")
                        .WithMany()
                        .HasForeignKey("ClaimID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("PROG6212_PART2_ST10396724.Models.Lecturer", "lecturer")
                        .WithMany()
                        .HasForeignKey("LecturerID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("PROG6212_PART2_ST10396724.Models.ProgCoOrdinator", "progs")
                        .WithMany()
                        .HasForeignKey("ProgCoOrdinatorID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("AcademicManagers");

                    b.Navigation("claim");

                    b.Navigation("lecturer");

                    b.Navigation("progs");
                });

            modelBuilder.Entity("PROG6212_PART2_ST10396724.Models.Lecturer", b =>
                {
                    b.Navigation("claims");
                });
#pragma warning restore 612, 618
        }
    }
}
