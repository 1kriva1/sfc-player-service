﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SFC.Player.Infrastructure.Persistence.Contexts;

#nullable disable

namespace SFC.Player.Infrastructure.Persistence.Migrations.Player
{
    [DbContext(typeof(PlayerDbContext))]
    partial class PlayerDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("Player")
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SFC.Player.Domain.Entities.Data.FootballPosition", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2")
                        .HasColumnOrder(1);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.ToTable("FootballPositions", "Data");
                });

            modelBuilder.Entity("SFC.Player.Domain.Entities.Data.GameStyle", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2")
                        .HasColumnOrder(1);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.ToTable("GameStyles", "Data");
                });

            modelBuilder.Entity("SFC.Player.Domain.Entities.Data.StatCategory", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2")
                        .HasColumnOrder(1);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.ToTable("StatCategories", "Data");
                });

            modelBuilder.Entity("SFC.Player.Domain.Entities.Data.StatSkill", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2")
                        .HasColumnOrder(1);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.ToTable("StatSkills", "Data");
                });

            modelBuilder.Entity("SFC.Player.Domain.Entities.Data.StatType", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2")
                        .HasColumnOrder(1);

                    b.Property<int>("SkillId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("SkillId");

                    b.ToTable("StatTypes", "Data");
                });

            modelBuilder.Entity("SFC.Player.Domain.Entities.Data.WorkingFoot", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2")
                        .HasColumnOrder(1);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.ToTable("WorkingFoots", "Data");
                });

            modelBuilder.Entity("SFC.Player.Domain.Entities.Identity.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnOrder(0);

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("LastModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("LastModifiedBy");

                    b.ToTable("Users", "Identity");
                });

            modelBuilder.Entity("SFC.Player.Domain.Entities.Metadata.Metadata", b =>
                {
                    b.Property<int>("Service")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<int>("Domain")
                        .HasColumnType("int");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.HasKey("Service", "Type");

                    b.HasIndex("Domain");

                    b.HasIndex("State");

                    b.HasIndex("Type");

                    b.ToTable("Metadata", "Metadata");

                    b.HasData(
                        new
                        {
                            Service = 0,
                            Type = 0,
                            Domain = 0,
                            State = 1
                        },
                        new
                        {
                            Service = 1,
                            Type = 1,
                            Domain = 1,
                            State = 1
                        },
                        new
                        {
                            Service = 2,
                            Type = 1,
                            Domain = 2,
                            State = 1
                        });
                });

            modelBuilder.Entity("SFC.Player.Domain.Entities.Metadata.MetadataDomain", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Domains", "Metadata");

                    b.HasData(
                        new
                        {
                            Id = 0,
                            Title = "Data"
                        },
                        new
                        {
                            Id = 1,
                            Title = "User"
                        },
                        new
                        {
                            Id = 2,
                            Title = "Player"
                        });
                });

            modelBuilder.Entity("SFC.Player.Domain.Entities.Metadata.MetadataService", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Services", "Metadata");

                    b.HasData(
                        new
                        {
                            Id = 0,
                            Title = "Data"
                        },
                        new
                        {
                            Id = 1,
                            Title = "Identity"
                        },
                        new
                        {
                            Id = 2,
                            Title = "Player"
                        });
                });

            modelBuilder.Entity("SFC.Player.Domain.Entities.Metadata.MetadataState", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("States", "Metadata");

                    b.HasData(
                        new
                        {
                            Id = 0,
                            Title = "Not Required"
                        },
                        new
                        {
                            Id = 1,
                            Title = "Required"
                        },
                        new
                        {
                            Id = 2,
                            Title = "Done"
                        });
                });

            modelBuilder.Entity("SFC.Player.Domain.Entities.Metadata.MetadataType", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Types", "Metadata");

                    b.HasData(
                        new
                        {
                            Id = 0,
                            Title = "Initialization"
                        },
                        new
                        {
                            Id = 1,
                            Title = "Seed"
                        });
                });

            modelBuilder.Entity("SFC.Player.Domain.Entities.Player.Player", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnOrder(0);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("LastModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("LastModifiedBy");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Players", "Player");
                });

            modelBuilder.Entity("SFC.Player.Domain.Entities.Player.PlayerAvailability", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<TimeSpan?>("From")
                        .HasColumnType("time");

                    b.Property<TimeSpan?>("To")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.ToTable("Availabilities", "Player");
                });

            modelBuilder.Entity("SFC.Player.Domain.Entities.Player.PlayerAvailableDay", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("AvailabilityId")
                        .HasColumnType("bigint");

                    b.Property<byte>("Day")
                        .HasColumnType("tinyint");

                    b.HasKey("Id");

                    b.HasIndex("AvailabilityId");

                    b.ToTable("AvailableDays", "Player");
                });

            modelBuilder.Entity("SFC.Player.Domain.Entities.Player.PlayerFootballProfile", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<int?>("AdditionalPositionId")
                        .HasColumnType("int");

                    b.Property<int?>("GameStyleId")
                        .HasColumnType("int");

                    b.Property<short?>("Height")
                        .HasColumnType("smallint");

                    b.Property<short?>("Number")
                        .HasColumnType("smallint");

                    b.Property<byte?>("PhysicalCondition")
                        .HasColumnType("tinyint");

                    b.Property<int?>("PositionId")
                        .HasColumnType("int");

                    b.Property<byte?>("Skill")
                        .HasColumnType("tinyint");

                    b.Property<byte?>("WeakFoot")
                        .HasColumnType("tinyint");

                    b.Property<short?>("Weight")
                        .HasColumnType("smallint");

                    b.Property<int?>("WorkingFootId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AdditionalPositionId");

                    b.HasIndex("GameStyleId");

                    b.HasIndex("PositionId");

                    b.HasIndex("WorkingFootId");

                    b.ToTable("FootballProfiles", "Player");
                });

            modelBuilder.Entity("SFC.Player.Domain.Entities.Player.PlayerGeneralProfile", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<string>("Biography")
                        .HasMaxLength(1050)
                        .HasColumnType("nvarchar(1050)");

                    b.Property<DateTime?>("Birthday")
                        .HasColumnType("date");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<bool>("FreePlay")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.ToTable("GeneralProfiles", "Player");
                });

            modelBuilder.Entity("SFC.Player.Domain.Entities.Player.PlayerPhoto", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<string>("Extension")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<int>("Size")
                        .HasMaxLength(5242880)
                        .HasColumnType("int");

                    b.Property<byte[]>("Source")
                        .IsRequired()
                        .HasColumnType("image");

                    b.HasKey("Id");

                    b.ToTable("Photos", "Player");
                });

            modelBuilder.Entity("SFC.Player.Domain.Entities.Player.PlayerStat", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("PlayerId")
                        .HasColumnType("bigint");

                    b.Property<int>("TypeId")
                        .HasColumnType("int");

                    b.Property<byte>("Value")
                        .HasColumnType("tinyint");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.HasIndex("TypeId");

                    b.ToTable("Stats", "Player");
                });

            modelBuilder.Entity("SFC.Player.Domain.Entities.Player.PlayerStatPoints", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<short>("Available")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint")
                        .HasDefaultValue((short)0);

                    b.Property<short>("Used")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint")
                        .HasDefaultValue((short)0);

                    b.HasKey("Id");

                    b.ToTable("Points", "Player");
                });

            modelBuilder.Entity("SFC.Player.Domain.Entities.Player.PlayerTag", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long?>("PlayerId")
                        .HasColumnType("bigint");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.ToTable("Tags", "Player");
                });

            modelBuilder.Entity("SFC.Player.Domain.Entities.Data.StatType", b =>
                {
                    b.HasOne("SFC.Player.Domain.Entities.Data.StatCategory", null)
                        .WithMany("Types")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SFC.Player.Domain.Entities.Data.StatSkill", null)
                        .WithMany("Types")
                        .HasForeignKey("SkillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SFC.Player.Domain.Entities.Identity.User", b =>
                {
                    b.HasOne("SFC.Player.Domain.Entities.Identity.User", null)
                        .WithMany()
                        .HasForeignKey("CreatedBy")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("SFC.Player.Domain.Entities.Identity.User", null)
                        .WithMany()
                        .HasForeignKey("LastModifiedBy")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SFC.Player.Domain.Entities.Metadata.Metadata", b =>
                {
                    b.HasOne("SFC.Player.Domain.Entities.Metadata.MetadataDomain", null)
                        .WithMany()
                        .HasForeignKey("Domain")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SFC.Player.Domain.Entities.Metadata.MetadataService", null)
                        .WithMany()
                        .HasForeignKey("Service")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SFC.Player.Domain.Entities.Metadata.MetadataState", null)
                        .WithMany()
                        .HasForeignKey("State")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SFC.Player.Domain.Entities.Metadata.MetadataType", null)
                        .WithMany()
                        .HasForeignKey("Type")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SFC.Player.Domain.Entities.Player.Player", b =>
                {
                    b.HasOne("SFC.Player.Domain.Entities.Identity.User", null)
                        .WithMany()
                        .HasForeignKey("CreatedBy")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("SFC.Player.Domain.Entities.Identity.User", null)
                        .WithMany()
                        .HasForeignKey("LastModifiedBy")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("SFC.Player.Domain.Entities.Identity.User", null)
                        .WithOne()
                        .HasForeignKey("SFC.Player.Domain.Entities.Player.Player", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SFC.Player.Domain.Entities.Player.PlayerAvailability", b =>
                {
                    b.HasOne("SFC.Player.Domain.Entities.Player.Player", "Player")
                        .WithOne("Availability")
                        .HasForeignKey("SFC.Player.Domain.Entities.Player.PlayerAvailability", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Player");
                });

            modelBuilder.Entity("SFC.Player.Domain.Entities.Player.PlayerAvailableDay", b =>
                {
                    b.HasOne("SFC.Player.Domain.Entities.Player.PlayerAvailability", "Availability")
                        .WithMany("Days")
                        .HasForeignKey("AvailabilityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Availability");
                });

            modelBuilder.Entity("SFC.Player.Domain.Entities.Player.PlayerFootballProfile", b =>
                {
                    b.HasOne("SFC.Player.Domain.Entities.Data.FootballPosition", null)
                        .WithMany()
                        .HasForeignKey("AdditionalPositionId");

                    b.HasOne("SFC.Player.Domain.Entities.Data.GameStyle", null)
                        .WithMany()
                        .HasForeignKey("GameStyleId");

                    b.HasOne("SFC.Player.Domain.Entities.Player.Player", "Player")
                        .WithOne("FootballProfile")
                        .HasForeignKey("SFC.Player.Domain.Entities.Player.PlayerFootballProfile", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SFC.Player.Domain.Entities.Data.FootballPosition", null)
                        .WithMany()
                        .HasForeignKey("PositionId");

                    b.HasOne("SFC.Player.Domain.Entities.Data.WorkingFoot", null)
                        .WithMany()
                        .HasForeignKey("WorkingFootId");

                    b.Navigation("Player");
                });

            modelBuilder.Entity("SFC.Player.Domain.Entities.Player.PlayerGeneralProfile", b =>
                {
                    b.HasOne("SFC.Player.Domain.Entities.Player.Player", "Player")
                        .WithOne("GeneralProfile")
                        .HasForeignKey("SFC.Player.Domain.Entities.Player.PlayerGeneralProfile", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Player");
                });

            modelBuilder.Entity("SFC.Player.Domain.Entities.Player.PlayerPhoto", b =>
                {
                    b.HasOne("SFC.Player.Domain.Entities.Player.Player", "Player")
                        .WithOne("Photo")
                        .HasForeignKey("SFC.Player.Domain.Entities.Player.PlayerPhoto", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Player");
                });

            modelBuilder.Entity("SFC.Player.Domain.Entities.Player.PlayerStat", b =>
                {
                    b.HasOne("SFC.Player.Domain.Entities.Player.Player", "Player")
                        .WithMany("Stats")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SFC.Player.Domain.Entities.Data.StatType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Player");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("SFC.Player.Domain.Entities.Player.PlayerStatPoints", b =>
                {
                    b.HasOne("SFC.Player.Domain.Entities.Player.Player", "Player")
                        .WithOne("Points")
                        .HasForeignKey("SFC.Player.Domain.Entities.Player.PlayerStatPoints", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Player");
                });

            modelBuilder.Entity("SFC.Player.Domain.Entities.Player.PlayerTag", b =>
                {
                    b.HasOne("SFC.Player.Domain.Entities.Player.Player", "Player")
                        .WithMany("Tags")
                        .HasForeignKey("PlayerId");

                    b.Navigation("Player");
                });

            modelBuilder.Entity("SFC.Player.Domain.Entities.Data.StatCategory", b =>
                {
                    b.Navigation("Types");
                });

            modelBuilder.Entity("SFC.Player.Domain.Entities.Data.StatSkill", b =>
                {
                    b.Navigation("Types");
                });

            modelBuilder.Entity("SFC.Player.Domain.Entities.Player.Player", b =>
                {
                    b.Navigation("Availability")
                        .IsRequired();

                    b.Navigation("FootballProfile")
                        .IsRequired();

                    b.Navigation("GeneralProfile")
                        .IsRequired();

                    b.Navigation("Photo")
                        .IsRequired();

                    b.Navigation("Points")
                        .IsRequired();

                    b.Navigation("Stats");

                    b.Navigation("Tags");
                });

            modelBuilder.Entity("SFC.Player.Domain.Entities.Player.PlayerAvailability", b =>
                {
                    b.Navigation("Days");
                });
#pragma warning restore 612, 618
        }
    }
}
