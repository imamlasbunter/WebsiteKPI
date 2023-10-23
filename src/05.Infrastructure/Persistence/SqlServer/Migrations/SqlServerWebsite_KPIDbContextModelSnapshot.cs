﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Pertamina.Website_KPI.Infrastructure.Persistence.SqlServer;

#nullable disable

namespace Pertamina.Website_KPI.Infrastructure.Persistence.SqlServer.Migrations;
[DbContext(typeof(SqlServerWebsite_KPIDbContext))]
partial class SqlServerWebsite_KPIDbContextModelSnapshot : ModelSnapshot
{
    protected override void BuildModel(ModelBuilder modelBuilder)
    {
#pragma warning disable 612, 618
        modelBuilder
            .HasAnnotation("ProductVersion", "6.0.8")
            .HasAnnotation("Relational:MaxIdentifierLength", 128);

        SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

        modelBuilder.Entity("Pertamina.Website_KPI.Domain.Entities.Audit", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("uniqueidentifier");

                b.Property<string>("ActionName")
                    .IsRequired()
                    .HasColumnType("nvarchar(100)");

                b.Property<string>("ActionType")
                    .IsRequired()
                    .HasColumnType("nvarchar(50)");

                b.Property<string>("ClientApplicationId")
                    .IsRequired()
                    .HasColumnType("nvarchar(50)");

                b.Property<DateTimeOffset>("Created")
                    .HasColumnType("datetimeoffset");

                b.Property<string>("CreatedBy")
                    .IsRequired()
                    .HasColumnType("nvarchar(100)");

                b.Property<Guid>("EntityId")
                    .HasColumnType("uniqueidentifier");

                b.Property<string>("EntityName")
                    .IsRequired()
                    .HasColumnType("nvarchar(50)");

                b.Property<string>("FromIpAddress")
                    .IsRequired()
                    .HasColumnType("nvarchar(20)");

                b.Property<string>("NewValues")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("OldValues")
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("TableName")
                    .IsRequired()
                    .HasColumnType("nvarchar(50)");

                b.HasKey("Id");

                b.ToTable("Audits", "Website_KPI");
            });

        modelBuilder.Entity("Pertamina.Website_KPI.Domain.Entities.Audit", b =>
            {
                b.OwnsOne("Pertamina.Website_KPI.Base.ValueObjects.Geolocation", "FromGeolocation", b1 =>
                    {
                        b1.Property<Guid>("AuditId")
                            .HasColumnType("uniqueidentifier");

                        b1.Property<double>("Accuracy")
                            .HasColumnType("float")
                            .HasColumnName("Accuracy");

                        b1.Property<double>("Latitude")
                            .HasColumnType("float")
                            .HasColumnName("Latitude");

                        b1.Property<double>("Longitude")
                            .HasColumnType("float")
                            .HasColumnName("Longitude");

                        b1.HasKey("AuditId");

                        b1.ToTable("Audits", "Website_KPI");

                        b1.WithOwner()
                            .HasForeignKey("AuditId");
                    });

                b.Navigation("FromGeolocation");
            });
#pragma warning restore 612, 618
    }
}
