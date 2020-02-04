﻿// <auto-generated />
using System;
using Isitar.PlantLogBook.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Isitar.PlantLogBook.Core.Data.Migrations
{
    [DbContext(typeof(PlantLogBookContext))]
    [Migration("20200204203119_PlantData")]
    partial class PlantData
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Isitar.PlantLogBook.Core.Data.DAO.Plant", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<Guid>("PlantSpeciesId")
                        .HasColumnType("uuid");

                    b.Property<int>("PlantState")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PlantSpeciesId");

                    b.ToTable("Plants");
                });

            modelBuilder.Entity("Isitar.PlantLogBook.Core.Data.DAO.PlantLog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Log")
                        .HasColumnType("text");

                    b.Property<Guid>("PlantId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("PlantLogTypeId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("PlantId");

                    b.HasIndex("PlantLogTypeId");

                    b.ToTable("PlantLogs");
                });

            modelBuilder.Entity("Isitar.PlantLogBook.Core.Data.DAO.PlantLogType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("PlantLogTypes");
                });

            modelBuilder.Entity("Isitar.PlantLogBook.Core.Data.DAO.PlantSpecies", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("PlantSpecies");
                });

            modelBuilder.Entity("Isitar.PlantLogBook.Core.Data.DAO.Plant", b =>
                {
                    b.HasOne("Isitar.PlantLogBook.Core.Data.DAO.PlantSpecies", "PlantSpecies")
                        .WithMany()
                        .HasForeignKey("PlantSpeciesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Isitar.PlantLogBook.Core.Data.DAO.PlantLog", b =>
                {
                    b.HasOne("Isitar.PlantLogBook.Core.Data.DAO.Plant", "Plant")
                        .WithMany()
                        .HasForeignKey("PlantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Isitar.PlantLogBook.Core.Data.DAO.PlantLogType", "LogType")
                        .WithMany()
                        .HasForeignKey("PlantLogTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
