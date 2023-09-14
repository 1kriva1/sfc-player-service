using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SFC.Players.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Players");

            migrationBuilder.CreateTable(
                name: "Players",
                schema: "Players",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Availability",
                schema: "Players",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    From = table.Column<TimeSpan>(type: "time", nullable: true),
                    To = table.Column<TimeSpan>(type: "time", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Availability", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Availability_Players_Id",
                        column: x => x.Id,
                        principalSchema: "Players",
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FootballProfiles",
                schema: "Players",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Height = table.Column<short>(type: "smallint", nullable: true),
                    Weight = table.Column<short>(type: "smallint", nullable: true),
                    Position = table.Column<byte>(type: "tinyint", nullable: true),
                    AdditionalPosition = table.Column<byte>(type: "tinyint", nullable: true),
                    WorkingFoot = table.Column<byte>(type: "tinyint", nullable: true),
                    Number = table.Column<short>(type: "smallint", nullable: true),
                    GameStyle = table.Column<byte>(type: "tinyint", nullable: true),
                    Skill = table.Column<byte>(type: "tinyint", nullable: true),
                    WeakFoot = table.Column<byte>(type: "tinyint", nullable: true),
                    PhysicalCondition = table.Column<byte>(type: "tinyint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FootballProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FootballProfiles_Players_Id",
                        column: x => x.Id,
                        principalSchema: "Players",
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GeneralProfiles",
                schema: "Players",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Biography = table.Column<string>(type: "nvarchar(1050)", maxLength: 1050, nullable: true),
                    Birthday = table.Column<DateTime>(type: "date", nullable: true),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FreePlay = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneralProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GeneralProfiles_Players_Id",
                        column: x => x.Id,
                        principalSchema: "Players",
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Photos",
                schema: "Players",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Source = table.Column<byte[]>(type: "image", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Size = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Photos_Players_Id",
                        column: x => x.Id,
                        principalSchema: "Players",
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Points",
                schema: "Players",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Available = table.Column<short>(type: "smallint", nullable: false, defaultValue: (short)0),
                    Used = table.Column<short>(type: "smallint", nullable: false, defaultValue: (short)0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Points", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Points_Players_Id",
                        column: x => x.Id,
                        principalSchema: "Players",
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stats",
                schema: "Players",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<byte>(type: "tinyint", nullable: false),
                    Category = table.Column<byte>(type: "tinyint", nullable: false),
                    Value = table.Column<byte>(type: "tinyint", nullable: false),
                    PlayerId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stats_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalSchema: "Players",
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                schema: "Players",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PlayerId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tags_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalSchema: "Players",
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "Players",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Players_Id",
                        column: x => x.Id,
                        principalSchema: "Players",
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AvailableDays",
                schema: "Players",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AvailabilityId = table.Column<long>(type: "bigint", nullable: false),
                    Day = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvailableDays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AvailableDays_Availability_AvailabilityId",
                        column: x => x.AvailabilityId,
                        principalSchema: "Players",
                        principalTable: "Availability",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IdentityUsers",
                schema: "Players",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUsers", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_IdentityUsers_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Players",
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AvailableDays_AvailabilityId",
                schema: "Players",
                table: "AvailableDays",
                column: "AvailabilityId");

            migrationBuilder.CreateIndex(
                name: "IX_Stats_PlayerId",
                schema: "Players",
                table: "Stats",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_PlayerId",
                schema: "Players",
                table: "Tags",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Id",
                schema: "Players",
                table: "Users",
                column: "Id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AvailableDays",
                schema: "Players");

            migrationBuilder.DropTable(
                name: "FootballProfiles",
                schema: "Players");

            migrationBuilder.DropTable(
                name: "GeneralProfiles",
                schema: "Players");

            migrationBuilder.DropTable(
                name: "IdentityUsers",
                schema: "Players");

            migrationBuilder.DropTable(
                name: "Photos",
                schema: "Players");

            migrationBuilder.DropTable(
                name: "Points",
                schema: "Players");

            migrationBuilder.DropTable(
                name: "Stats",
                schema: "Players");

            migrationBuilder.DropTable(
                name: "Tags",
                schema: "Players");

            migrationBuilder.DropTable(
                name: "Availability",
                schema: "Players");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "Players");

            migrationBuilder.DropTable(
                name: "Players",
                schema: "Players");
        }
    }
}
