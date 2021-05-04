using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AppAttendance.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppConfigs",
                columns: table => new
                {
                    Key = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateUpdate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppConfigs", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "AppRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRoleClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    NormalizedName = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Description = table.Column<string>(maxLength: 200, nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateUpdate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUserLogins",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: true),
                    ProviderKey = table.Column<string>(nullable: true),
                    ProviderDisplayName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserLogins", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "AppUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserRoles", x => new { x.UserId, x.RoleId });
                });

            migrationBuilder.CreateTable(
                name: "AppUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    NormalizedUserName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    NormalizedEmail = table.Column<string>(nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FullName = table.Column<string>(nullable: true),
                    UrlImg = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateUpdate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserTokens", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Class",
                columns: table => new
                {
                    Id_Class = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateUpdate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Class", x => x.Id_Class);
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FullName = table.Column<string>(nullable: true),
                    UrlImg = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateUpdate = table.Column<DateTime>(nullable: false),
                    StatusEquipment = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subject",
                columns: table => new
                {
                    Id_Subject = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    NumberOfCredits = table.Column<int>(nullable: false),
                    Lesson = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateUpdate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subject", x => x.Id_Subject);
                });

            migrationBuilder.CreateTable(
                name: "Teacher",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FullName = table.Column<string>(nullable: true),
                    UrlImg = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateUpdate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teacher", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Equipment",
                columns: table => new
                {
                    Id_BLE = table.Column<string>(nullable: false),
                    Id_Student = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Id_Equipment = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateUpdate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipment", x => x.Id_BLE);
                    table.ForeignKey(
                        name: "FK_Equipment_Student_Id_Student",
                        column: x => x.Id_Student,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reason = table.Column<string>(nullable: true),
                    Id_BLE = table.Column<string>(nullable: true),
                    Id_User = table.Column<Guid>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateUpdate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notification_Student_Id_User",
                        column: x => x.Id_User,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    Id_Course = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Id_Teacher = table.Column<Guid>(nullable: false),
                    Id_Subject = table.Column<int>(nullable: false),
                    DateBegin = table.Column<DateTime>(nullable: false),
                    DateEnd = table.Column<DateTime>(nullable: false),
                    SchoolYear = table.Column<string>(nullable: true),
                    Semester = table.Column<int>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateUpdate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course", x => x.Id_Course);
                    table.ForeignKey(
                        name: "FK_Course_Subject_Id_Subject",
                        column: x => x.Id_Subject,
                        principalTable: "Subject",
                        principalColumn: "Id_Subject",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Course_Teacher_Id_Teacher",
                        column: x => x.Id_Teacher,
                        principalTable: "Teacher",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HistoryAttendance",
                columns: table => new
                {
                    Id_HistoryAttendace = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_Course = table.Column<int>(nullable: false),
                    Id_Schedule = table.Column<int>(nullable: false),
                    Id_EquipmentTeacher = table.Column<string>(nullable: true),
                    DateAttendance = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryAttendance", x => x.Id_HistoryAttendace);
                    table.ForeignKey(
                        name: "FK_HistoryAttendance_Course_Id_Course",
                        column: x => x.Id_Course,
                        principalTable: "Course",
                        principalColumn: "Id_Course",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RegisterCourse",
                columns: table => new
                {
                    Id_RegisterCourse = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_Course = table.Column<int>(nullable: false),
                    Id_Student = table.Column<Guid>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateUpdate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegisterCourse", x => x.Id_RegisterCourse);
                    table.ForeignKey(
                        name: "FK_RegisterCourse_Course_Id_Course",
                        column: x => x.Id_Course,
                        principalTable: "Course",
                        principalColumn: "Id_Course",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RegisterCourse_Student_Id_Student",
                        column: x => x.Id_Student,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Schedule",
                columns: table => new
                {
                    Id_Schedule = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_Course = table.Column<int>(nullable: false),
                    Id_Class = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    TimeBegin = table.Column<int>(nullable: false),
                    TimeEnd = table.Column<int>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateUpdate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedule", x => x.Id_Schedule);
                    table.ForeignKey(
                        name: "FK_Schedule_Class_Id_Class",
                        column: x => x.Id_Class,
                        principalTable: "Class",
                        principalColumn: "Id_Class",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Schedule_Course_Id_Course",
                        column: x => x.Id_Course,
                        principalTable: "Course",
                        principalColumn: "Id_Course",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetailHA",
                columns: table => new
                {
                    Id_DetailHA = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_Student = table.Column<Guid>(nullable: false),
                    Id_HistoryAttendance = table.Column<int>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetailHA", x => x.Id_DetailHA);
                    table.ForeignKey(
                        name: "FK_DetailHA_HistoryAttendance_Id_HistoryAttendance",
                        column: x => x.Id_HistoryAttendance,
                        principalTable: "HistoryAttendance",
                        principalColumn: "Id_HistoryAttendace",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetailHA_Student_Id_Student",
                        column: x => x.Id_Student,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AppRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "DateCreate", "DateUpdate", "Description", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"), "cec96616-85f1-4645-89f0-310332ba3a03", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Administrator role", "admin", "admin" },
                    { new Guid("7bbc55c5-6ea6-43e1-7581-08d8ed00ec81"), "b7fd934b-52f1-477f-8a69-4a0ca28af696", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Student Role", "student", "student" },
                    { new Guid("8125c97d-6048-4d57-7582-08d8ed00ec81"), "35458ff7-73ac-44b6-be76-7bb09307897f", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Teacher Role", "teacher", "teacher" }
                });

            migrationBuilder.InsertData(
                table: "AppUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"), new Guid("8d04dce2-969a-435d-bba4-df3f325983dc") });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateCreate", "DateUpdate", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "Type", "UrlImg", "UserName" },
                values: new object[] { new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"), 0, "1232ffe3-071b-4bec-9322-6318a0029a2d", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com", true, "Administrator", false, null, "admin@gmail.com", "admin", "AQAAAAEAACcQAAAAEDjo7qYgRQ3IrLAbL3v371uHzA5q6Tuxy1of1Vy5fNfosHx0Aw71FVrP/zkYWfo0Vg==", null, false, "", false, "admin", null, "admin" });

            migrationBuilder.InsertData(
                table: "Class",
                columns: new[] { "Id_Class", "DateCreate", "DateUpdate", "Description", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2021, 4, 4, 13, 35, 31, 521, DateTimeKind.Utc).AddTicks(9440), new DateTime(2021, 4, 4, 13, 35, 31, 522, DateTimeKind.Utc).AddTicks(717), "Phòng tòa nhà A", "A101" },
                    { 2, new DateTime(2021, 4, 4, 13, 35, 31, 522, DateTimeKind.Utc).AddTicks(1754), new DateTime(2021, 4, 4, 13, 35, 31, 522, DateTimeKind.Utc).AddTicks(1775), "Phòng tòa nhà A", "A102" },
                    { 3, new DateTime(2021, 4, 4, 13, 35, 31, 522, DateTimeKind.Utc).AddTicks(1791), new DateTime(2021, 4, 4, 13, 35, 31, 522, DateTimeKind.Utc).AddTicks(1792), "Phòng tòa nhà B", "B101" },
                    { 4, new DateTime(2021, 4, 4, 13, 35, 31, 522, DateTimeKind.Utc).AddTicks(1795), new DateTime(2021, 4, 4, 13, 35, 31, 522, DateTimeKind.Utc).AddTicks(1796), "Phòng tòa nhà B", "B102" },
                    { 5, new DateTime(2021, 4, 4, 13, 35, 31, 522, DateTimeKind.Utc).AddTicks(1798), new DateTime(2021, 4, 4, 13, 35, 31, 522, DateTimeKind.Utc).AddTicks(1800), "Phòng tòa nhà C", "C101" },
                    { 6, new DateTime(2021, 4, 4, 13, 35, 31, 522, DateTimeKind.Utc).AddTicks(1802), new DateTime(2021, 4, 4, 13, 35, 31, 522, DateTimeKind.Utc).AddTicks(1804), "Phòng tòa nhà C", "C101" }
                });

            migrationBuilder.InsertData(
                table: "Subject",
                columns: new[] { "Id_Subject", "DateCreate", "DateUpdate", "Description", "Lesson", "Name", "NumberOfCredits" },
                values: new object[,]
                {
                    { 1, new DateTime(2021, 4, 4, 13, 35, 31, 524, DateTimeKind.Utc).AddTicks(9612), new DateTime(2021, 4, 4, 13, 35, 31, 525, DateTimeKind.Utc).AddTicks(310), "Cơ sở dữ liệu", 12, "IT004", 3 },
                    { 2, new DateTime(2021, 4, 4, 13, 35, 31, 525, DateTimeKind.Utc).AddTicks(1088), new DateTime(2021, 4, 4, 13, 35, 31, 525, DateTimeKind.Utc).AddTicks(1110), "Công nghệ Java", 12, "IE303", 3 },
                    { 3, new DateTime(2021, 4, 4, 13, 35, 31, 525, DateTimeKind.Utc).AddTicks(1120), new DateTime(2021, 4, 4, 13, 35, 31, 525, DateTimeKind.Utc).AddTicks(1122), "Nhập môn lập trình", 12, "IT001", 3 },
                    { 4, new DateTime(2021, 4, 4, 13, 35, 31, 525, DateTimeKind.Utc).AddTicks(1123), new DateTime(2021, 4, 4, 13, 35, 31, 525, DateTimeKind.Utc).AddTicks(1125), "Đại số tuyến tính", 12, "MA003", 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Course_Id_Subject",
                table: "Course",
                column: "Id_Subject");

            migrationBuilder.CreateIndex(
                name: "IX_Course_Id_Teacher",
                table: "Course",
                column: "Id_Teacher");

            migrationBuilder.CreateIndex(
                name: "IX_DetailHA_Id_HistoryAttendance",
                table: "DetailHA",
                column: "Id_HistoryAttendance");

            migrationBuilder.CreateIndex(
                name: "IX_DetailHA_Id_Student",
                table: "DetailHA",
                column: "Id_Student");

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_Id_Student",
                table: "Equipment",
                column: "Id_Student");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryAttendance_Id_Course",
                table: "HistoryAttendance",
                column: "Id_Course");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_Id_User",
                table: "Notification",
                column: "Id_User");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterCourse_Id_Course",
                table: "RegisterCourse",
                column: "Id_Course");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterCourse_Id_Student",
                table: "RegisterCourse",
                column: "Id_Student");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_Id_Class",
                table: "Schedule",
                column: "Id_Class");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_Id_Course",
                table: "Schedule",
                column: "Id_Course");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppConfigs");

            migrationBuilder.DropTable(
                name: "AppRoleClaims");

            migrationBuilder.DropTable(
                name: "AppRoles");

            migrationBuilder.DropTable(
                name: "AppUserClaims");

            migrationBuilder.DropTable(
                name: "AppUserLogins");

            migrationBuilder.DropTable(
                name: "AppUserRoles");

            migrationBuilder.DropTable(
                name: "AppUsers");

            migrationBuilder.DropTable(
                name: "AppUserTokens");

            migrationBuilder.DropTable(
                name: "DetailHA");

            migrationBuilder.DropTable(
                name: "Equipment");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "RegisterCourse");

            migrationBuilder.DropTable(
                name: "Schedule");

            migrationBuilder.DropTable(
                name: "HistoryAttendance");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "Class");

            migrationBuilder.DropTable(
                name: "Course");

            migrationBuilder.DropTable(
                name: "Subject");

            migrationBuilder.DropTable(
                name: "Teacher");
        }
    }
}
