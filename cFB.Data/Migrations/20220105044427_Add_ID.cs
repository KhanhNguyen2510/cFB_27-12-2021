using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace cFB.Data.Migrations
{
    public partial class Add_ID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistoryClient_AdministrativeDivision_AdministrativeDivisionID",
                table: "HistoryClient");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HistoryClient",
                table: "HistoryClient");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateReport",
                table: "Report",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 5, 11, 44, 27, 323, DateTimeKind.Local).AddTicks(3892),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 1, 3, 17, 4, 32, 925, DateTimeKind.Local).AddTicks(5215));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UploadTime",
                table: "Post",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 5, 11, 44, 27, 313, DateTimeKind.Local).AddTicks(2344),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2022, 1, 3, 17, 4, 32, 915, DateTimeKind.Local).AddTicks(6467));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CrawledTime",
                table: "Post",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 5, 11, 44, 27, 313, DateTimeKind.Local).AddTicks(2779),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2022, 1, 3, 17, 4, 32, 915, DateTimeKind.Local).AddTicks(6823));

            migrationBuilder.AlterColumn<string>(
                name: "AdministrativeDivisionID",
                table: "HistoryClient",
                type: "varchar(10)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(10)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "HistoryClient",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 5, 11, 44, 27, 325, DateTimeKind.Local).AddTicks(4015),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 1, 3, 17, 4, 32, 926, DateTimeKind.Local).AddTicks(9539));

            migrationBuilder.AlterColumn<string>(
                name: "IPAddress",
                table: "HistoryClient",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "HistoryClient",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeOnline",
                table: "AdministrativeDivision",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 5, 11, 44, 27, 302, DateTimeKind.Local).AddTicks(6155),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2022, 1, 3, 17, 4, 32, 905, DateTimeKind.Local).AddTicks(4067));

            migrationBuilder.AddPrimaryKey(
                name: "PK_HistoryClient",
                table: "HistoryClient",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_HistoryClient_AdministrativeDivision_AdministrativeDivisionID",
                table: "HistoryClient",
                column: "AdministrativeDivisionID",
                principalTable: "AdministrativeDivision",
                principalColumn: "AdministrativeDivisionId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistoryClient_AdministrativeDivision_AdministrativeDivisionID",
                table: "HistoryClient");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HistoryClient",
                table: "HistoryClient");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "HistoryClient");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateReport",
                table: "Report",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 3, 17, 4, 32, 925, DateTimeKind.Local).AddTicks(5215),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 1, 5, 11, 44, 27, 323, DateTimeKind.Local).AddTicks(3892));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UploadTime",
                table: "Post",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 3, 17, 4, 32, 915, DateTimeKind.Local).AddTicks(6467),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2022, 1, 5, 11, 44, 27, 313, DateTimeKind.Local).AddTicks(2344));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CrawledTime",
                table: "Post",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 3, 17, 4, 32, 915, DateTimeKind.Local).AddTicks(6823),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2022, 1, 5, 11, 44, 27, 313, DateTimeKind.Local).AddTicks(2779));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "HistoryClient",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 3, 17, 4, 32, 926, DateTimeKind.Local).AddTicks(9539),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 1, 5, 11, 44, 27, 325, DateTimeKind.Local).AddTicks(4015));

            migrationBuilder.AlterColumn<string>(
                name: "IPAddress",
                table: "HistoryClient",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AdministrativeDivisionID",
                table: "HistoryClient",
                type: "varchar(10)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeOnline",
                table: "AdministrativeDivision",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 3, 17, 4, 32, 905, DateTimeKind.Local).AddTicks(4067),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2022, 1, 5, 11, 44, 27, 302, DateTimeKind.Local).AddTicks(6155));

            migrationBuilder.AddPrimaryKey(
                name: "PK_HistoryClient",
                table: "HistoryClient",
                columns: new[] { "IPAddress", "Time", "AdministrativeDivisionID" });

            migrationBuilder.AddForeignKey(
                name: "FK_HistoryClient_AdministrativeDivision_AdministrativeDivisionID",
                table: "HistoryClient",
                column: "AdministrativeDivisionID",
                principalTable: "AdministrativeDivision",
                principalColumn: "AdministrativeDivisionId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
