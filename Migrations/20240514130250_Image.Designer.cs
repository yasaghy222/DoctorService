﻿// <auto-generated />
using System;
using DoctorService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DoctorService.Migrations
{
    [DbContext(typeof(DoctorServiceContext))]
    [Migration("20240514130250_Image")]
    partial class Image
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DoctorService.Entities.Clinic", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("LocationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ModifyAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.ToTable("Clinics");
                });

            modelBuilder.Entity("DoctorService.Entities.Doctor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("AcceptVisit")
                        .HasColumnType("bit");

                    b.Property<Guid?>("ClinicId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EmailValidate")
                        .HasColumnType("bit");

                    b.Property<string>("Family")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte>("Gender")
                        .HasColumnType("tinyint");

                    b.Property<bool>("HasTelCounseling")
                        .HasColumnType("bit");

                    b.Property<bool>("HasTextCounseling")
                        .HasColumnType("bit");

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte>("LineStatus")
                        .HasColumnType("tinyint");

                    b.Property<int>("MedicalSysCode")
                        .HasColumnType("int");

                    b.Property<DateTime>("ModifyAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneValidate")
                        .HasColumnType("bit");

                    b.Property<float>("Rate")
                        .HasColumnType("real");

                    b.Property<int>("RaterCount")
                        .HasColumnType("int");

                    b.Property<Guid>("SpecialtyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte>("Status")
                        .HasColumnType("tinyint");

                    b.Property<string>("StatusDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SuccessConsolationCount")
                        .HasColumnType("int");

                    b.Property<int>("SuccessReservationCount")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClinicId");

                    b.HasIndex("SpecialtyId");

                    b.ToTable("Doctors");
                });

            modelBuilder.Entity("DoctorService.Entities.Location", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("datetime2");

                    b.Property<float>("Latitudes")
                        .HasColumnType("real");

                    b.Property<float>("Longitudes")
                        .HasColumnType("real");

                    b.Property<DateTime>("ModifyAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("ParentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("DoctorService.Entities.OnlinePlan", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("datetime2");

                    b.Property<byte>("DayOfWeek")
                        .HasColumnType("tinyint");

                    b.Property<Guid>("DoctorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<TimeOnly>("EndTime")
                        .HasColumnType("time");

                    b.Property<DateTime>("ModifyAt")
                        .HasColumnType("datetime2");

                    b.Property<TimeOnly>("StartTime")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.HasIndex("DoctorId");

                    b.ToTable("OnlinePlans");
                });

            modelBuilder.Entity("DoctorService.Entities.Specialty", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifyAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Specialties");
                });

            modelBuilder.Entity("DoctorService.Entities.VisitPlan", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("datetime2");

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date");

                    b.Property<Guid>("DoctorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<TimeOnly>("EndTime")
                        .HasColumnType("time");

                    b.Property<DateTime>("ModifyAt")
                        .HasColumnType("datetime2");

                    b.Property<TimeOnly>("StartTime")
                        .HasColumnType("time");

                    b.Property<byte>("Status")
                        .HasColumnType("tinyint");

                    b.HasKey("Id");

                    b.HasIndex("DoctorId");

                    b.ToTable("VisitPlans");
                });

            modelBuilder.Entity("DoctorService.Entities.Clinic", b =>
                {
                    b.HasOne("DoctorService.Entities.Location", "Location")
                        .WithMany("Clinics")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Location");
                });

            modelBuilder.Entity("DoctorService.Entities.Doctor", b =>
                {
                    b.HasOne("DoctorService.Entities.Clinic", "Clinic")
                        .WithMany("Doctors")
                        .HasForeignKey("ClinicId");

                    b.HasOne("DoctorService.Entities.Specialty", "Specialty")
                        .WithMany("Doctors")
                        .HasForeignKey("SpecialtyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Clinic");

                    b.Navigation("Specialty");
                });

            modelBuilder.Entity("DoctorService.Entities.OnlinePlan", b =>
                {
                    b.HasOne("DoctorService.Entities.Doctor", "Doctor")
                        .WithMany("OnlinePlans")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctor");
                });

            modelBuilder.Entity("DoctorService.Entities.VisitPlan", b =>
                {
                    b.HasOne("DoctorService.Entities.Doctor", "Doctor")
                        .WithMany("VisitPlans")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctor");
                });

            modelBuilder.Entity("DoctorService.Entities.Clinic", b =>
                {
                    b.Navigation("Doctors");
                });

            modelBuilder.Entity("DoctorService.Entities.Doctor", b =>
                {
                    b.Navigation("OnlinePlans");

                    b.Navigation("VisitPlans");
                });

            modelBuilder.Entity("DoctorService.Entities.Location", b =>
                {
                    b.Navigation("Clinics");
                });

            modelBuilder.Entity("DoctorService.Entities.Specialty", b =>
                {
                    b.Navigation("Doctors");
                });
#pragma warning restore 612, 618
        }
    }
}
