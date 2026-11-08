using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Salon.Infrastructure.EntityFrameworkCore.Migrations;

/// <inheritdoc />
public partial class InitialCreate : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterDatabase()
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(
            name: "Clients",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                Phone = table.Column<string>(type: "longtext", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                FullName = table.Column<string>(type: "longtext", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                Gender = table.Column<int>(type: "int", nullable: false),
                BirthDate = table.Column<DateOnly>(type: "date", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Clients", x => x.Id);
            })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(
            name: "Masters",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                PassportNumber = table.Column<string>(type: "longtext", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                Specialization = table.Column<string>(type: "longtext", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                ExperienceYears = table.Column<int>(type: "int", nullable: false),
                FullName = table.Column<string>(type: "longtext", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                Gender = table.Column<int>(type: "int", nullable: false),
                BirthDate = table.Column<DateOnly>(type: "date", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Masters", x => x.Id);
            })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(
            name: "Services",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                Name = table.Column<string>(type: "longtext", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                Category = table.Column<int>(type: "int", nullable: false),
                Price = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                Duration = table.Column<TimeSpan>(type: "time(6)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Services", x => x.Id);
            })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(
            name: "Appointments",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                StartTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                ClientId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                MasterId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                ServiceId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                IsRegular = table.Column<bool>(type: "tinyint(1)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Appointments", x => x.Id);
                table.ForeignKey(
                    name: "FK_Appointments_Clients_ClientId",
                    column: x => x.ClientId,
                    principalTable: "Clients",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_Appointments_Masters_MasterId",
                    column: x => x.MasterId,
                    principalTable: "Masters",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_Appointments_Services_ServiceId",
                    column: x => x.ServiceId,
                    principalTable: "Services",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateIndex(
            name: "IX_Appointments_ClientId",
            table: "Appointments",
            column: "ClientId");

        migrationBuilder.CreateIndex(
            name: "IX_Appointments_MasterId",
            table: "Appointments",
            column: "MasterId");

        migrationBuilder.CreateIndex(
            name: "IX_Appointments_ServiceId",
            table: "Appointments",
            column: "ServiceId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Appointments");

        migrationBuilder.DropTable(
            name: "Clients");

        migrationBuilder.DropTable(
            name: "Masters");

        migrationBuilder.DropTable(
            name: "Services");
    }
}
