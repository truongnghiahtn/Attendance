using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AppAttendance.Data.Migrations
{
    public partial class statusRegister : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "RegisterCourse",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("7bbc55c5-6ea6-43e1-7581-08d8ed00ec81"),
                column: "ConcurrencyStamp",
                value: "6319d46e-2949-4328-b985-c4b8a480508d");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8125c97d-6048-4d57-7582-08d8ed00ec81"),
                column: "ConcurrencyStamp",
                value: "dc5d7cde-2280-47b4-ba19-e37b43d6c623");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "f786f8ab-af5c-417f-a6e7-f9c8e56f8e35");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "0c367cae-d0ff-42ab-a668-1b9b2303164c", "AQAAAAEAACcQAAAAEFNvQliUbeW6kuu/jqQ4GMFde34p3Pf6n6wRmteSTXFTuvdFR0G2hEORsv3h3Mjh6g==" });

            migrationBuilder.UpdateData(
                table: "Class",
                keyColumn: "Id_Class",
                keyValue: 1,
                columns: new[] { "DateCreate", "DateUpdate" },
                values: new object[] { new DateTime(2021, 4, 15, 13, 52, 3, 357, DateTimeKind.Utc).AddTicks(7491), new DateTime(2021, 4, 15, 13, 52, 3, 357, DateTimeKind.Utc).AddTicks(8585) });

            migrationBuilder.UpdateData(
                table: "Class",
                keyColumn: "Id_Class",
                keyValue: 2,
                columns: new[] { "DateCreate", "DateUpdate" },
                values: new object[] { new DateTime(2021, 4, 15, 13, 52, 3, 357, DateTimeKind.Utc).AddTicks(9488), new DateTime(2021, 4, 15, 13, 52, 3, 357, DateTimeKind.Utc).AddTicks(9507) });

            migrationBuilder.UpdateData(
                table: "Class",
                keyColumn: "Id_Class",
                keyValue: 3,
                columns: new[] { "DateCreate", "DateUpdate" },
                values: new object[] { new DateTime(2021, 4, 15, 13, 52, 3, 357, DateTimeKind.Utc).AddTicks(9523), new DateTime(2021, 4, 15, 13, 52, 3, 357, DateTimeKind.Utc).AddTicks(9525) });

            migrationBuilder.UpdateData(
                table: "Class",
                keyColumn: "Id_Class",
                keyValue: 4,
                columns: new[] { "DateCreate", "DateUpdate" },
                values: new object[] { new DateTime(2021, 4, 15, 13, 52, 3, 357, DateTimeKind.Utc).AddTicks(9527), new DateTime(2021, 4, 15, 13, 52, 3, 357, DateTimeKind.Utc).AddTicks(9529) });

            migrationBuilder.UpdateData(
                table: "Class",
                keyColumn: "Id_Class",
                keyValue: 5,
                columns: new[] { "DateCreate", "DateUpdate" },
                values: new object[] { new DateTime(2021, 4, 15, 13, 52, 3, 357, DateTimeKind.Utc).AddTicks(9531), new DateTime(2021, 4, 15, 13, 52, 3, 357, DateTimeKind.Utc).AddTicks(9532) });

            migrationBuilder.UpdateData(
                table: "Class",
                keyColumn: "Id_Class",
                keyValue: 6,
                columns: new[] { "DateCreate", "DateUpdate" },
                values: new object[] { new DateTime(2021, 4, 15, 13, 52, 3, 357, DateTimeKind.Utc).AddTicks(9534), new DateTime(2021, 4, 15, 13, 52, 3, 357, DateTimeKind.Utc).AddTicks(9536) });

            migrationBuilder.UpdateData(
                table: "Subject",
                keyColumn: "Id_Subject",
                keyValue: 1,
                columns: new[] { "DateCreate", "DateUpdate" },
                values: new object[] { new DateTime(2021, 4, 15, 13, 52, 3, 360, DateTimeKind.Utc).AddTicks(6142), new DateTime(2021, 4, 15, 13, 52, 3, 360, DateTimeKind.Utc).AddTicks(7023) });

            migrationBuilder.UpdateData(
                table: "Subject",
                keyColumn: "Id_Subject",
                keyValue: 2,
                columns: new[] { "DateCreate", "DateUpdate" },
                values: new object[] { new DateTime(2021, 4, 15, 13, 52, 3, 360, DateTimeKind.Utc).AddTicks(7940), new DateTime(2021, 4, 15, 13, 52, 3, 360, DateTimeKind.Utc).AddTicks(7976) });

            migrationBuilder.UpdateData(
                table: "Subject",
                keyColumn: "Id_Subject",
                keyValue: 3,
                columns: new[] { "DateCreate", "DateUpdate" },
                values: new object[] { new DateTime(2021, 4, 15, 13, 52, 3, 360, DateTimeKind.Utc).AddTicks(8053), new DateTime(2021, 4, 15, 13, 52, 3, 360, DateTimeKind.Utc).AddTicks(8056) });

            migrationBuilder.UpdateData(
                table: "Subject",
                keyColumn: "Id_Subject",
                keyValue: 4,
                columns: new[] { "DateCreate", "DateUpdate" },
                values: new object[] { new DateTime(2021, 4, 15, 13, 52, 3, 360, DateTimeKind.Utc).AddTicks(8058), new DateTime(2021, 4, 15, 13, 52, 3, 360, DateTimeKind.Utc).AddTicks(8060) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "RegisterCourse");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("7bbc55c5-6ea6-43e1-7581-08d8ed00ec81"),
                column: "ConcurrencyStamp",
                value: "b7fd934b-52f1-477f-8a69-4a0ca28af696");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8125c97d-6048-4d57-7582-08d8ed00ec81"),
                column: "ConcurrencyStamp",
                value: "35458ff7-73ac-44b6-be76-7bb09307897f");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "cec96616-85f1-4645-89f0-310332ba3a03");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "1232ffe3-071b-4bec-9322-6318a0029a2d", "AQAAAAEAACcQAAAAEDjo7qYgRQ3IrLAbL3v371uHzA5q6Tuxy1of1Vy5fNfosHx0Aw71FVrP/zkYWfo0Vg==" });

            migrationBuilder.UpdateData(
                table: "Class",
                keyColumn: "Id_Class",
                keyValue: 1,
                columns: new[] { "DateCreate", "DateUpdate" },
                values: new object[] { new DateTime(2021, 4, 4, 13, 35, 31, 521, DateTimeKind.Utc).AddTicks(9440), new DateTime(2021, 4, 4, 13, 35, 31, 522, DateTimeKind.Utc).AddTicks(717) });

            migrationBuilder.UpdateData(
                table: "Class",
                keyColumn: "Id_Class",
                keyValue: 2,
                columns: new[] { "DateCreate", "DateUpdate" },
                values: new object[] { new DateTime(2021, 4, 4, 13, 35, 31, 522, DateTimeKind.Utc).AddTicks(1754), new DateTime(2021, 4, 4, 13, 35, 31, 522, DateTimeKind.Utc).AddTicks(1775) });

            migrationBuilder.UpdateData(
                table: "Class",
                keyColumn: "Id_Class",
                keyValue: 3,
                columns: new[] { "DateCreate", "DateUpdate" },
                values: new object[] { new DateTime(2021, 4, 4, 13, 35, 31, 522, DateTimeKind.Utc).AddTicks(1791), new DateTime(2021, 4, 4, 13, 35, 31, 522, DateTimeKind.Utc).AddTicks(1792) });

            migrationBuilder.UpdateData(
                table: "Class",
                keyColumn: "Id_Class",
                keyValue: 4,
                columns: new[] { "DateCreate", "DateUpdate" },
                values: new object[] { new DateTime(2021, 4, 4, 13, 35, 31, 522, DateTimeKind.Utc).AddTicks(1795), new DateTime(2021, 4, 4, 13, 35, 31, 522, DateTimeKind.Utc).AddTicks(1796) });

            migrationBuilder.UpdateData(
                table: "Class",
                keyColumn: "Id_Class",
                keyValue: 5,
                columns: new[] { "DateCreate", "DateUpdate" },
                values: new object[] { new DateTime(2021, 4, 4, 13, 35, 31, 522, DateTimeKind.Utc).AddTicks(1798), new DateTime(2021, 4, 4, 13, 35, 31, 522, DateTimeKind.Utc).AddTicks(1800) });

            migrationBuilder.UpdateData(
                table: "Class",
                keyColumn: "Id_Class",
                keyValue: 6,
                columns: new[] { "DateCreate", "DateUpdate" },
                values: new object[] { new DateTime(2021, 4, 4, 13, 35, 31, 522, DateTimeKind.Utc).AddTicks(1802), new DateTime(2021, 4, 4, 13, 35, 31, 522, DateTimeKind.Utc).AddTicks(1804) });

            migrationBuilder.UpdateData(
                table: "Subject",
                keyColumn: "Id_Subject",
                keyValue: 1,
                columns: new[] { "DateCreate", "DateUpdate" },
                values: new object[] { new DateTime(2021, 4, 4, 13, 35, 31, 524, DateTimeKind.Utc).AddTicks(9612), new DateTime(2021, 4, 4, 13, 35, 31, 525, DateTimeKind.Utc).AddTicks(310) });

            migrationBuilder.UpdateData(
                table: "Subject",
                keyColumn: "Id_Subject",
                keyValue: 2,
                columns: new[] { "DateCreate", "DateUpdate" },
                values: new object[] { new DateTime(2021, 4, 4, 13, 35, 31, 525, DateTimeKind.Utc).AddTicks(1088), new DateTime(2021, 4, 4, 13, 35, 31, 525, DateTimeKind.Utc).AddTicks(1110) });

            migrationBuilder.UpdateData(
                table: "Subject",
                keyColumn: "Id_Subject",
                keyValue: 3,
                columns: new[] { "DateCreate", "DateUpdate" },
                values: new object[] { new DateTime(2021, 4, 4, 13, 35, 31, 525, DateTimeKind.Utc).AddTicks(1120), new DateTime(2021, 4, 4, 13, 35, 31, 525, DateTimeKind.Utc).AddTicks(1122) });

            migrationBuilder.UpdateData(
                table: "Subject",
                keyColumn: "Id_Subject",
                keyValue: 4,
                columns: new[] { "DateCreate", "DateUpdate" },
                values: new object[] { new DateTime(2021, 4, 4, 13, 35, 31, 525, DateTimeKind.Utc).AddTicks(1123), new DateTime(2021, 4, 4, 13, 35, 31, 525, DateTimeKind.Utc).AddTicks(1125) });
        }
    }
}
