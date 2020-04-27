﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SmartHome.Models;

namespace SmartHome.Migrations
{
    [DbContext(typeof(SmartHomeDbContext))]
    [Migration("20200425183120_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("SmartHome.Models.Device", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Model")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("RoomId")
                        .HasColumnType("int");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<double>("Value")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.HasIndex("RoomId");

                    b.ToTable("Devices");
                });

            modelBuilder.Entity("SmartHome.Models.House", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("OwnerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Houses");
                });

            modelBuilder.Entity("SmartHome.Models.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("HouseId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("HouseId");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("SmartHome.Models.Scenario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("DeviceId")
                        .HasColumnType("int");

                    b.Property<string>("EventURL")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTimeOffset>("TimeOfEvent")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("DeviceId");

                    b.ToTable("Scenarios");
                });

            modelBuilder.Entity("SmartHome.Models.UserDevice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("DeviceId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DeviceId");

                    b.HasIndex("UserId");

                    b.ToTable("UserDevices");
                });

            modelBuilder.Entity("SmartHome.Models.UserModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("ResidenceId")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("ResidenceId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SmartHome.Models.Device", b =>
                {
                    b.HasOne("SmartHome.Models.Room", "Room")
                        .WithMany("Devices")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SmartHome.Models.House", b =>
                {
                    b.HasOne("SmartHome.Models.UserModel", "Owner")
                        .WithMany("Houses")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SmartHome.Models.Room", b =>
                {
                    b.HasOne("SmartHome.Models.House", "House")
                        .WithMany("Rooms")
                        .HasForeignKey("HouseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SmartHome.Models.Scenario", b =>
                {
                    b.HasOne("SmartHome.Models.Device", "Device")
                        .WithMany("Scenarios")
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SmartHome.Models.UserDevice", b =>
                {
                    b.HasOne("SmartHome.Models.Device", "Device")
                        .WithMany("Users")
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SmartHome.Models.UserModel", "User")
                        .WithMany("Devices")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SmartHome.Models.UserModel", b =>
                {
                    b.HasOne("SmartHome.Models.House", "Residence")
                        .WithMany("Residents")
                        .HasForeignKey("ResidenceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
