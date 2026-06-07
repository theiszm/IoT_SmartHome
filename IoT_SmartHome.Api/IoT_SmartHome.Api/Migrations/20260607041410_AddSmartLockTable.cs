using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IoT_SmartHome.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddSmartLockTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SmartLocks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IsLocked = table.Column<bool>(type: "INTEGER", nullable: false),
                    BatteryPercentage = table.Column<int>(type: "INTEGER", nullable: false),
                    LastUnlockedTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Room = table.Column<string>(type: "TEXT", nullable: false),
                    IsOnline = table.Column<bool>(type: "INTEGER", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartLocks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SmartSecurityCameras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IsRecording = table.Column<bool>(type: "INTEGER", nullable: false),
                    StorageUsagePercentage = table.Column<int>(type: "INTEGER", nullable: false),
                    MotionDetected = table.Column<bool>(type: "INTEGER", nullable: false),
                    Room = table.Column<string>(type: "TEXT", nullable: false),
                    IsOnline = table.Column<bool>(type: "INTEGER", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartSecurityCameras", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SmartSpeakers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Volume = table.Column<int>(type: "INTEGER", nullable: false),
                    CurrentTrack = table.Column<string>(type: "TEXT", nullable: false),
                    IsMuted = table.Column<bool>(type: "INTEGER", nullable: false),
                    Room = table.Column<string>(type: "TEXT", nullable: false),
                    IsOnline = table.Column<bool>(type: "INTEGER", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartSpeakers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SmartThermostats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CurrentTemperature = table.Column<double>(type: "REAL", nullable: false),
                    TargetTemperature = table.Column<double>(type: "REAL", nullable: false),
                    SystemMode = table.Column<string>(type: "TEXT", nullable: false),
                    Room = table.Column<string>(type: "TEXT", nullable: false),
                    IsOnline = table.Column<bool>(type: "INTEGER", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartThermostats", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SmartLocks");

            migrationBuilder.DropTable(
                name: "SmartSecurityCameras");

            migrationBuilder.DropTable(
                name: "SmartSpeakers");

            migrationBuilder.DropTable(
                name: "SmartThermostats");
        }
    }
}
