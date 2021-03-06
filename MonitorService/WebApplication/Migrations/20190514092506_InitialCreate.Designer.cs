﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApplication.Models;

namespace WebApplication.Migrations
{
    [DbContext(typeof(WallsMonitorDbContext))]
    [Migration("20190514092506_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebApplication.Models.Job", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("EndTime");

                    b.Property<int>("ExtensionServiceJobsId");

                    b.Property<string>("ExtensionServiceName");

                    b.Property<string>("ExtensionType");

                    b.Property<string>("FinalStatus");

                    b.Property<string>("JobState");

                    b.Property<string>("JobType");

                    b.Property<string>("LibraryName");

                    b.Property<string>("Messages");

                    b.Property<Guid>("OperationId");

                    b.Property<DateTime>("QueueTime");

                    b.Property<int>("Retries");

                    b.Property<DateTime>("StartTime");

                    b.Property<DateTime>("StateLastChangedTime");

                    b.HasKey("Id");

                    b.ToTable("Jobs");
                });

            modelBuilder.Entity("WebApplication.Models.Log", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("LogContent");

                    b.Property<string>("LogMessage");

                    b.Property<string>("ThreadId");

                    b.Property<DateTime>("TimeStamp");

                    b.HasKey("Id");

                    b.ToTable("Logs");
                });

            modelBuilder.Entity("WebApplication.Models.MetricsModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("MachineName");

                    b.Property<string>("Memory");

                    b.Property<string>("PhysicalDisk");

                    b.Property<string>("Processor");

                    b.Property<DateTime>("TimeStamp");

                    b.HasKey("Id");

                    b.ToTable("MetricsModels");
                });
#pragma warning restore 612, 618
        }
    }
}
