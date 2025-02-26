using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace testDS.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Weathers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Time = table.Column<TimeOnly>(type: "time", nullable: false),
                    Temp = table.Column<double>(type: "float", nullable: false),
                    Humadity = table.Column<double>(type: "float", nullable: false),
                    Td = table.Column<double>(type: "float", nullable: false),
                    AtmosphericPressure = table.Column<int>(type: "int", nullable: false),
                    WindDirection = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WindSpeed = table.Column<int>(type: "int", nullable: false),
                    Cloudiness = table.Column<int>(type: "int", nullable: true),
                    H = table.Column<int>(type: "int", nullable: false),
                    VV = table.Column<int>(type: "int", nullable: true),
                    WeatherPhenomena = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weathers", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Weathers");
        }
    }
}
