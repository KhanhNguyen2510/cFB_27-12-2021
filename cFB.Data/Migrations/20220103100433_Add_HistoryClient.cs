using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace cFB.Data.Migrations
{
    public partial class Add_HistoryClient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateReport",
                table: "Report",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 3, 17, 4, 32, 925, DateTimeKind.Local).AddTicks(5215),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 12, 27, 1, 58, 53, 97, DateTimeKind.Local).AddTicks(5573));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UploadTime",
                table: "Post",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 3, 17, 4, 32, 915, DateTimeKind.Local).AddTicks(6467),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2021, 12, 27, 1, 58, 53, 87, DateTimeKind.Local).AddTicks(4443));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CrawledTime",
                table: "Post",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 3, 17, 4, 32, 915, DateTimeKind.Local).AddTicks(6823),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2021, 12, 27, 1, 58, 53, 87, DateTimeKind.Local).AddTicks(4847));

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeOnline",
                table: "AdministrativeDivision",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 3, 17, 4, 32, 905, DateTimeKind.Local).AddTicks(4067),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2021, 12, 27, 1, 58, 53, 76, DateTimeKind.Local).AddTicks(6288));

            migrationBuilder.CreateTable(
                name: "HistoryClient",
                columns: table => new
                {
                    IPAddress = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AdministrativeDivisionID = table.Column<string>(type: "varchar(10)", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 1, 3, 17, 4, 32, 926, DateTimeKind.Local).AddTicks(9539)),
                    NameMachine = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryClient", x => new { x.IPAddress, x.Time, x.AdministrativeDivisionID });
                    table.ForeignKey(
                        name: "FK_HistoryClient_AdministrativeDivision_AdministrativeDivisionID",
                        column: x => x.AdministrativeDivisionID,
                        principalTable: "AdministrativeDivision",
                        principalColumn: "AdministrativeDivisionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HistoryClient_AdministrativeDivisionID",
                table: "HistoryClient",
                column: "AdministrativeDivisionID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistoryClient");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateReport",
                table: "Report",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 27, 1, 58, 53, 97, DateTimeKind.Local).AddTicks(5573),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 1, 3, 17, 4, 32, 925, DateTimeKind.Local).AddTicks(5215));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UploadTime",
                table: "Post",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 27, 1, 58, 53, 87, DateTimeKind.Local).AddTicks(4443),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2022, 1, 3, 17, 4, 32, 915, DateTimeKind.Local).AddTicks(6467));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CrawledTime",
                table: "Post",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 27, 1, 58, 53, 87, DateTimeKind.Local).AddTicks(4847),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2022, 1, 3, 17, 4, 32, 915, DateTimeKind.Local).AddTicks(6823));

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeOnline",
                table: "AdministrativeDivision",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 27, 1, 58, 53, 76, DateTimeKind.Local).AddTicks(6288),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2022, 1, 3, 17, 4, 32, 905, DateTimeKind.Local).AddTicks(4067));
        }
    }
}
