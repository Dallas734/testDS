﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using testDS.Data;

#nullable disable

namespace testDS.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("testDS.Models.WeatherModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AtmosphericPressure")
                        .HasColumnType("int");

                    b.Property<int?>("Cloudiness")
                        .HasColumnType("int");

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date");

                    b.Property<int>("H")
                        .HasColumnType("int");

                    b.Property<double>("Humadity")
                        .HasColumnType("float");

                    b.Property<double>("Td")
                        .HasColumnType("float");

                    b.Property<double>("Temp")
                        .HasColumnType("float");

                    b.Property<TimeOnly>("Time")
                        .HasColumnType("time");

                    b.Property<int?>("VV")
                        .HasColumnType("int");

                    b.Property<string>("WeatherPhenomena")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WindDirection")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WindSpeed")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Weathers");
                });
#pragma warning restore 612, 618
        }
    }
}
