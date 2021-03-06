// <auto-generated />
using System;
using AppAttendance.Data.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AppAttendance.Data.Migrations
{
    [DbContext(typeof(AppAttendanceDbContext))]
    [Migration("20210404063532_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AppAttendance.Data.Entities.AppConfig", b =>
                {
                    b.Property<string>("Key")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("DateCreate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateUpdate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Key");

                    b.ToTable("AppConfigs");
                });

            modelBuilder.Entity("AppAttendance.Data.Entities.AppRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateCreate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateUpdate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AppRoles");

                    b.HasData(
                        new
                        {
                            Id = new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                            ConcurrencyStamp = "cec96616-85f1-4645-89f0-310332ba3a03",
                            DateCreate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateUpdate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Administrator role",
                            Name = "admin",
                            NormalizedName = "admin"
                        },
                        new
                        {
                            Id = new Guid("7bbc55c5-6ea6-43e1-7581-08d8ed00ec81"),
                            ConcurrencyStamp = "b7fd934b-52f1-477f-8a69-4a0ca28af696",
                            DateCreate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateUpdate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Student Role",
                            Name = "student",
                            NormalizedName = "student"
                        },
                        new
                        {
                            Id = new Guid("8125c97d-6048-4d57-7582-08d8ed00ec81"),
                            ConcurrencyStamp = "35458ff7-73ac-44b6-be76-7bb09307897f",
                            DateCreate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateUpdate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Teacher Role",
                            Name = "teacher",
                            NormalizedName = "teacher"
                        });
                });

            modelBuilder.Entity("AppAttendance.Data.Entities.AppUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateCreate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateUpdate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UrlImg")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AppUsers");

                    b.HasData(
                        new
                        {
                            Id = new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "1232ffe3-071b-4bec-9322-6318a0029a2d",
                            DateCreate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateUpdate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "admin@gmail.com",
                            EmailConfirmed = true,
                            FullName = "Administrator",
                            LockoutEnabled = false,
                            NormalizedEmail = "admin@gmail.com",
                            NormalizedUserName = "admin",
                            PasswordHash = "AQAAAAEAACcQAAAAEDjo7qYgRQ3IrLAbL3v371uHzA5q6Tuxy1of1Vy5fNfosHx0Aw71FVrP/zkYWfo0Vg==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "",
                            TwoFactorEnabled = false,
                            Type = "admin",
                            UserName = "admin"
                        });
                });

            modelBuilder.Entity("AppAttendance.Data.Entities.Class", b =>
                {
                    b.Property<int>("Id_Class")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateCreate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateUpdate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id_Class");

                    b.ToTable("Class");

                    b.HasData(
                        new
                        {
                            Id_Class = 1,
                            DateCreate = new DateTime(2021, 4, 4, 13, 35, 31, 521, DateTimeKind.Utc).AddTicks(9440),
                            DateUpdate = new DateTime(2021, 4, 4, 13, 35, 31, 522, DateTimeKind.Utc).AddTicks(717),
                            Description = "Phòng tòa nhà A",
                            Name = "A101"
                        },
                        new
                        {
                            Id_Class = 2,
                            DateCreate = new DateTime(2021, 4, 4, 13, 35, 31, 522, DateTimeKind.Utc).AddTicks(1754),
                            DateUpdate = new DateTime(2021, 4, 4, 13, 35, 31, 522, DateTimeKind.Utc).AddTicks(1775),
                            Description = "Phòng tòa nhà A",
                            Name = "A102"
                        },
                        new
                        {
                            Id_Class = 3,
                            DateCreate = new DateTime(2021, 4, 4, 13, 35, 31, 522, DateTimeKind.Utc).AddTicks(1791),
                            DateUpdate = new DateTime(2021, 4, 4, 13, 35, 31, 522, DateTimeKind.Utc).AddTicks(1792),
                            Description = "Phòng tòa nhà B",
                            Name = "B101"
                        },
                        new
                        {
                            Id_Class = 4,
                            DateCreate = new DateTime(2021, 4, 4, 13, 35, 31, 522, DateTimeKind.Utc).AddTicks(1795),
                            DateUpdate = new DateTime(2021, 4, 4, 13, 35, 31, 522, DateTimeKind.Utc).AddTicks(1796),
                            Description = "Phòng tòa nhà B",
                            Name = "B102"
                        },
                        new
                        {
                            Id_Class = 5,
                            DateCreate = new DateTime(2021, 4, 4, 13, 35, 31, 522, DateTimeKind.Utc).AddTicks(1798),
                            DateUpdate = new DateTime(2021, 4, 4, 13, 35, 31, 522, DateTimeKind.Utc).AddTicks(1800),
                            Description = "Phòng tòa nhà C",
                            Name = "C101"
                        },
                        new
                        {
                            Id_Class = 6,
                            DateCreate = new DateTime(2021, 4, 4, 13, 35, 31, 522, DateTimeKind.Utc).AddTicks(1802),
                            DateUpdate = new DateTime(2021, 4, 4, 13, 35, 31, 522, DateTimeKind.Utc).AddTicks(1804),
                            Description = "Phòng tòa nhà C",
                            Name = "C101"
                        });
                });

            modelBuilder.Entity("AppAttendance.Data.Entities.Course", b =>
                {
                    b.Property<int>("Id_Course")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateBegin")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateCreate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateEnd")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateUpdate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Id_Subject")
                        .HasColumnType("int");

                    b.Property<Guid>("Id_Teacher")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SchoolYear")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Semester")
                        .HasColumnType("int");

                    b.HasKey("Id_Course");

                    b.HasIndex("Id_Subject");

                    b.HasIndex("Id_Teacher");

                    b.ToTable("Course");
                });

            modelBuilder.Entity("AppAttendance.Data.Entities.DetailHA", b =>
                {
                    b.Property<int>("Id_DetailHA")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateCreate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Id_HistoryAttendance")
                        .HasColumnType("int");

                    b.Property<Guid>("Id_Student")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id_DetailHA");

                    b.HasIndex("Id_HistoryAttendance");

                    b.HasIndex("Id_Student");

                    b.ToTable("DetailHA");
                });

            modelBuilder.Entity("AppAttendance.Data.Entities.Equipment", b =>
                {
                    b.Property<string>("Id_BLE")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("DateCreate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateUpdate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Id_Equipment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("Id_Student")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("Id_BLE");

                    b.HasIndex("Id_Student");

                    b.ToTable("Equipment");
                });

            modelBuilder.Entity("AppAttendance.Data.Entities.HistoryAttendance", b =>
                {
                    b.Property<int>("Id_HistoryAttendace")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateAttendance")
                        .HasColumnType("datetime2");

                    b.Property<int>("Id_Course")
                        .HasColumnType("int");

                    b.Property<string>("Id_EquipmentTeacher")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Id_Schedule")
                        .HasColumnType("int");

                    b.HasKey("Id_HistoryAttendace");

                    b.HasIndex("Id_Course");

                    b.ToTable("HistoryAttendance");
                });

            modelBuilder.Entity("AppAttendance.Data.Entities.Notification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateCreate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateUpdate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Id_BLE")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("Id_User")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Reason")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("Id_User");

                    b.ToTable("Notification");
                });

            modelBuilder.Entity("AppAttendance.Data.Entities.RegisterCourse", b =>
                {
                    b.Property<int>("Id_RegisterCourse")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateCreate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateUpdate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Id_Course")
                        .HasColumnType("int");

                    b.Property<Guid>("Id_Student")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id_RegisterCourse");

                    b.HasIndex("Id_Course");

                    b.HasIndex("Id_Student");

                    b.ToTable("RegisterCourse");
                });

            modelBuilder.Entity("AppAttendance.Data.Entities.Schedule", b =>
                {
                    b.Property<int>("Id_Schedule")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateCreate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateUpdate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Id_Class")
                        .HasColumnType("int");

                    b.Property<int>("Id_Course")
                        .HasColumnType("int");

                    b.Property<int>("TimeBegin")
                        .HasColumnType("int");

                    b.Property<int>("TimeEnd")
                        .HasColumnType("int");

                    b.HasKey("Id_Schedule");

                    b.HasIndex("Id_Class");

                    b.HasIndex("Id_Course");

                    b.ToTable("Schedule");
                });

            modelBuilder.Entity("AppAttendance.Data.Entities.Student", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateCreate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateUpdate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("StatusEquipment")
                        .HasColumnType("bit");

                    b.Property<string>("UrlImg")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Student");
                });

            modelBuilder.Entity("AppAttendance.Data.Entities.Subject", b =>
                {
                    b.Property<int>("Id_Subject")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateCreate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateUpdate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Lesson")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfCredits")
                        .HasColumnType("int");

                    b.HasKey("Id_Subject");

                    b.ToTable("Subject");

                    b.HasData(
                        new
                        {
                            Id_Subject = 1,
                            DateCreate = new DateTime(2021, 4, 4, 13, 35, 31, 524, DateTimeKind.Utc).AddTicks(9612),
                            DateUpdate = new DateTime(2021, 4, 4, 13, 35, 31, 525, DateTimeKind.Utc).AddTicks(310),
                            Description = "Cơ sở dữ liệu",
                            Lesson = 12,
                            Name = "IT004",
                            NumberOfCredits = 3
                        },
                        new
                        {
                            Id_Subject = 2,
                            DateCreate = new DateTime(2021, 4, 4, 13, 35, 31, 525, DateTimeKind.Utc).AddTicks(1088),
                            DateUpdate = new DateTime(2021, 4, 4, 13, 35, 31, 525, DateTimeKind.Utc).AddTicks(1110),
                            Description = "Công nghệ Java",
                            Lesson = 12,
                            Name = "IE303",
                            NumberOfCredits = 3
                        },
                        new
                        {
                            Id_Subject = 3,
                            DateCreate = new DateTime(2021, 4, 4, 13, 35, 31, 525, DateTimeKind.Utc).AddTicks(1120),
                            DateUpdate = new DateTime(2021, 4, 4, 13, 35, 31, 525, DateTimeKind.Utc).AddTicks(1122),
                            Description = "Nhập môn lập trình",
                            Lesson = 12,
                            Name = "IT001",
                            NumberOfCredits = 3
                        },
                        new
                        {
                            Id_Subject = 4,
                            DateCreate = new DateTime(2021, 4, 4, 13, 35, 31, 525, DateTimeKind.Utc).AddTicks(1123),
                            DateUpdate = new DateTime(2021, 4, 4, 13, 35, 31, 525, DateTimeKind.Utc).AddTicks(1125),
                            Description = "Đại số tuyến tính",
                            Lesson = 12,
                            Name = "MA003",
                            NumberOfCredits = 3
                        });
                });

            modelBuilder.Entity("AppAttendance.Data.Entities.Teacher", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateCreate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateUpdate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UrlImg")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Teacher");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("AppRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("AppUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("AppUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.ToTable("AppUserRoles");

                    b.HasData(
                        new
                        {
                            UserId = new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                            RoleId = new Guid("8d04dce2-969a-435d-bba4-df3f325983dc")
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("AppUserTokens");
                });

            modelBuilder.Entity("AppAttendance.Data.Entities.Course", b =>
                {
                    b.HasOne("AppAttendance.Data.Entities.Subject", "Subject")
                        .WithMany("Courses")
                        .HasForeignKey("Id_Subject")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AppAttendance.Data.Entities.Teacher", "Teacher")
                        .WithMany("Courses")
                        .HasForeignKey("Id_Teacher")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AppAttendance.Data.Entities.DetailHA", b =>
                {
                    b.HasOne("AppAttendance.Data.Entities.HistoryAttendance", "HistoryAttendance")
                        .WithMany("DetailHAs")
                        .HasForeignKey("Id_HistoryAttendance")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AppAttendance.Data.Entities.Student", "Student")
                        .WithMany("DetailHAs")
                        .HasForeignKey("Id_Student")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AppAttendance.Data.Entities.Equipment", b =>
                {
                    b.HasOne("AppAttendance.Data.Entities.Student", "Student")
                        .WithMany("Equipment")
                        .HasForeignKey("Id_Student")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AppAttendance.Data.Entities.HistoryAttendance", b =>
                {
                    b.HasOne("AppAttendance.Data.Entities.Course", "Course")
                        .WithMany("HistoryAttendances")
                        .HasForeignKey("Id_Course")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AppAttendance.Data.Entities.Notification", b =>
                {
                    b.HasOne("AppAttendance.Data.Entities.Student", "Student")
                        .WithMany("Notifications")
                        .HasForeignKey("Id_User")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AppAttendance.Data.Entities.RegisterCourse", b =>
                {
                    b.HasOne("AppAttendance.Data.Entities.Course", "Course")
                        .WithMany("RegisterCourses")
                        .HasForeignKey("Id_Course")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AppAttendance.Data.Entities.Student", "Student")
                        .WithMany("RegisterCourses")
                        .HasForeignKey("Id_Student")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AppAttendance.Data.Entities.Schedule", b =>
                {
                    b.HasOne("AppAttendance.Data.Entities.Class", "Class")
                        .WithMany("Schedules")
                        .HasForeignKey("Id_Class")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AppAttendance.Data.Entities.Course", "Course")
                        .WithMany("Schedules")
                        .HasForeignKey("Id_Course")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
