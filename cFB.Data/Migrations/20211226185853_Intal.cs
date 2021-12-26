using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace cFB.Data.Migrations
{
    public partial class Intal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FaceBookType",
                columns: table => new
                {
                    FaceBookTypeId = table.Column<string>(type: "varchar(8)", nullable: false),
                    FaceBookTypeName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FaceBookType", x => x.FaceBookTypeId);
                });

            migrationBuilder.CreateTable(
                name: "NewsLabel",
                columns: table => new
                {
                    NewsLabelId = table.Column<string>(type: "varchar(8)", nullable: false),
                    NewsLabelName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsLabel", x => x.NewsLabelId);
                });

            migrationBuilder.CreateTable(
                name: "RoleManager",
                columns: table => new
                {
                    ManagerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ManagerName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleManager", x => x.ManagerId);
                });

            migrationBuilder.CreateTable(
                name: "SentimentLabel",
                columns: table => new
                {
                    SentimentLabelId = table.Column<string>(type: "varchar(8)", nullable: false),
                    SentimentLabelName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SentimentLabel", x => x.SentimentLabelId);
                });

            migrationBuilder.CreateTable(
                name: "AdministrativeDivision",
                columns: table => new
                {
                    AdministrativeDivisionId = table.Column<string>(type: "varchar(10)", nullable: false),
                    AdministrativeDivisionName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    NumberPhone = table.Column<string>(type: "varchar(13)", maxLength: 13, nullable: true),
                    Addrees = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    TimeOnline = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(2021, 12, 27, 1, 58, 53, 76, DateTimeKind.Local).AddTicks(6288)),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ManagerId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CheckPass = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdministrativeDivision", x => x.AdministrativeDivisionId);
                    table.ForeignKey(
                        name: "FK_AdministrativeDivision_RoleManager_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "RoleManager",
                        principalColumn: "ManagerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "History",
                columns: table => new
                {
                    AdministrativeDivisionId = table.Column<string>(type: "varchar(10)", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Event = table.Column<int>(type: "int", nullable: false),
                    StatusHistory = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_History", x => new { x.AdministrativeDivisionId, x.Time });
                    table.ForeignKey(
                        name: "FK_History_AdministrativeDivision_AdministrativeDivisionId",
                        column: x => x.AdministrativeDivisionId,
                        principalTable: "AdministrativeDivision",
                        principalColumn: "AdministrativeDivisionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WatchList",
                columns: table => new
                {
                    FaceBookId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FaceBookName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FaceBookUrl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    AdministrativeDivisionId = table.Column<string>(type: "varchar(10)", nullable: true),
                    FaceBookTypeId = table.Column<string>(type: "varchar(8)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WatchList", x => x.FaceBookId);
                    table.ForeignKey(
                        name: "FK_WatchList_AdministrativeDivision_AdministrativeDivisionId",
                        column: x => x.AdministrativeDivisionId,
                        principalTable: "AdministrativeDivision",
                        principalColumn: "AdministrativeDivisionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WatchList_FaceBookType_FaceBookTypeId",
                        column: x => x.FaceBookTypeId,
                        principalTable: "FaceBookType",
                        principalColumn: "FaceBookTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Post",
                columns: table => new
                {
                    PostId = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    UserUrl = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    PostUrl = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    PostContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UploadTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(2021, 12, 27, 1, 58, 53, 87, DateTimeKind.Local).AddTicks(4443)),
                    CrawledTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(2021, 12, 27, 1, 58, 53, 87, DateTimeKind.Local).AddTicks(4847)),
                    TotalLikes = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    TotalComment = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    TotalShare = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    NewsLabelId = table.Column<string>(type: "varchar(8)", nullable: true),
                    FaceBookId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SentimentLabelId = table.Column<string>(type: "varchar(8)", nullable: true),
                    AdministrativeDivisionId = table.Column<string>(type: "varchar(10)", nullable: true),
                    Report = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    FilePDF = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, defaultValue: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post", x => x.PostId);
                    table.ForeignKey(
                        name: "FK_Post_AdministrativeDivision_AdministrativeDivisionId",
                        column: x => x.AdministrativeDivisionId,
                        principalTable: "AdministrativeDivision",
                        principalColumn: "AdministrativeDivisionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Post_NewsLabel_NewsLabelId",
                        column: x => x.NewsLabelId,
                        principalTable: "NewsLabel",
                        principalColumn: "NewsLabelId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Post_SentimentLabel_SentimentLabelId",
                        column: x => x.SentimentLabelId,
                        principalTable: "SentimentLabel",
                        principalColumn: "SentimentLabelId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Post_WatchList_FaceBookId",
                        column: x => x.FaceBookId,
                        principalTable: "WatchList",
                        principalColumn: "FaceBookId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Report",
                columns: table => new
                {
                    ReportId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FileReport = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, defaultValue: ""),
                    DateReport = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2021, 12, 27, 1, 58, 53, 97, DateTimeKind.Local).AddTicks(5573)),
                    AdministrativeDivisionId = table.Column<string>(type: "varchar(10)", nullable: true),
                    PostId = table.Column<string>(type: "nvarchar(500)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report", x => x.ReportId);
                    table.ForeignKey(
                        name: "FK_Report_AdministrativeDivision_AdministrativeDivisionId",
                        column: x => x.AdministrativeDivisionId,
                        principalTable: "AdministrativeDivision",
                        principalColumn: "AdministrativeDivisionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Report_Post_PostId",
                        column: x => x.PostId,
                        principalTable: "Post",
                        principalColumn: "PostId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "FaceBookType",
                columns: new[] { "FaceBookTypeId", "Description", "FaceBookTypeName" },
                values: new object[,]
                {
                    { "CGR", "Group kín, nơi mọi người có thể xem tên nhóm và thành viên, nhưng không xem được nội dung của nhóm.", "Nhóm Kín" },
                    { "GR", "Group công khai, nơi mọi người có thể xem tất cả nội dung và các thành viên.", "Nhóm Công Khai" },
                    { "PAGE", "Trang công khai", "Trang" },
                    { "USER", "Tài khoản của người dùng thông thường", "Cá nhân" }
                });

            migrationBuilder.InsertData(
                table: "NewsLabel",
                columns: new[] { "NewsLabelId", "NewsLabelName" },
                values: new object[,]
                {
                    { "XE", "Xe" },
                    { "XB", "Xuất bản" },
                    { "TTR", "Thời trang" },
                    { "TTH", "Thể thao" },
                    { "TS", "Thời sự" },
                    { "TG", "Thế giới" },
                    { "ST", "Sống trẻ" },
                    { "SK", "Sức khỏe" },
                    { "PL", "Pháp luật" },
                    { "PA", "Phim ảnh" },
                    { "KD", "Kinh Doanh" },
                    { "GT", "Giải trí" },
                    { "GD", "Giáo dục" },
                    { "DL", "Du lịch" },
                    { "CN", "Công nghệ" },
                    { "AT", "Ẩm thực" },
                    { "ANTT", "An ninh trật tự" },
                    { "ANQG", "An ninh quốc gia" },
                    { "AN", "Âm nhạc" }
                });

            migrationBuilder.InsertData(
                table: "RoleManager",
                columns: new[] { "ManagerId", "ManagerName" },
                values: new object[,]
                {
                    { "Admin", "Nơi quản lý cao nhất" },
                    { "User", "Các địa phương sử dụng" }
                });

            migrationBuilder.InsertData(
                table: "SentimentLabel",
                columns: new[] { "SentimentLabelId", "SentimentLabelName" },
                values: new object[,]
                {
                    { "NEG", "Tiêu cực" },
                    { "NEU", "Bình thường" },
                    { "POS", "Tích cực" }
                });

            migrationBuilder.InsertData(
                table: "AdministrativeDivision",
                columns: new[] { "AdministrativeDivisionId", "Addrees", "AdministrativeDivisionName", "CheckPass", "ManagerId", "NumberPhone", "Password" },
                values: new object[,]
                {
                    { "86C1", "158 Đặng Văn Lãnh, TP Phan Thiết", "Thành phố Phan Thiết", false, "Admin", "02523822694", "1" },
                    { "Admin", "239 Hùng Vương, thôn Quý Thanh, xã Ngũ Phụng, huyện Phú Quý", "Admin", false, "Admin", "02523767434", "1" },
                    { "86B1", "Đường 17/4, thị trấn Liên Hương, huyện Tuy Phong", "Huyện Tuy Phong", false, "User", "02523850163", "1" },
                    { "86B2", "17 Lê Hồng Phong, khu phố Xuân An 2, thị trấn Chợ Lầu, huyện Bắc Bình", "Huyện Bắc Bình", false, "User", "02523860133", "1" },
                    { "86B3", "Khu phố Lâm Hòa, thị trấn Ma Lâm, huyện Hàm Thuận Bắc", "Huyện Hàm Thuận Bắc", false, "User", "02523865113", "1" },
                    { "86B4", "14 Trần phú, thị trấn Thuận Nam, huyện Hàm Thuận Nam", "Huyện Hàm Thuận Nam", false, "User", "02523867113", "1" },
                    { "86B5", "Khu phố 2, Thị trấn Tân Nghĩa, huyện Hàm Tân", "Huyện Hàm Tân", false, "User", "02523876100", "1" },
                    { "86B6", "02 Ngô Gia Tự, phường Tân An, thị xã La Gi", "Thị xã La Gi", false, "User", "02523871988", "1" },
                    { "86B7", "449 Trần Hưng Đạo, khu phố Lạc Hóa 1, thị trấn Lạc Tánh, huyện Tánh Linh", "Huyện Tánh Linh", false, "User", "0794201396", "1" },
                    { "86B8", "Thôn 2, xã Nam Chính, huyện Đức Linh", "Huyện Đức Linh", false, "User", "02523882113", "1" },
                    { "86B9", "239 Hùng Vương, thôn Quý Thanh, xã Ngũ Phụng, huyện Phú Quý", "Huyện Phú Quý", false, "User", "02523767434", "1" }
                });

            migrationBuilder.InsertData(
                table: "WatchList",
                columns: new[] { "FaceBookId", "AdministrativeDivisionId", "FaceBookName", "FaceBookTypeId", "FaceBookUrl" },
                values: new object[,]
                {
                    { "1648199831900386", "Admin", "Triệt Hạ Bò Đỏ", "GR", "https://www.facebook.com/groups/1648199831900386" },
                    { "thuc.tranhuynhduy", "Admin", "Trần Huỳnh Duy Thức", "USER", "https://www.facebook.com/thuc.tranhuynhduy" },
                    { "thong.luan.1", "Admin", "Tập Hợp Dân Chủ Đa Nguyên", "PAGE", "https://www.facebook.com/thong.luan.1" },
                    { "TamConXuyenDiep", "Admin", "Tam Côn Xuyên Diệp", "GR", "https://www.facebook.com/groups/TamConXuyenDiep" },
                    { "phapluatvacuocsong.vn", "Admin", "Pháp luật & Cuộc sống", "PAGE", "https://www.facebook.com/phapluatvacuocsong.vn" },
                    { "nhatkyyeunuoc1", "Admin", "Nhật Ký Yêu Nước", "PAGE", "https://www.facebook.com/nhatkyyeunuoc1" },
                    { "nhabaocongdan", "Admin", "Góc nhìn báo chí - Công dân", "GR", "https://www.facebook.com/groups/nhabaocongdan" },
                    { "mothermushroom", "Admin", "Mẹ Nấm", "USER", "https://www.facebook.com/mothermushroom" },
                    { "vietnamconghoa123", "Admin", "Việt Nam Cộng Hòa", "PAGE", "https://www.facebook.com/vietnamconghoa123" },
                    { "lukhach", "Admin", "Nguyen Huy Vu", "USER", "https://www.facebook.com/lukhach" },
                    { "HuyFreedomSaigon", "Admin", "Huỳnh Quốc Huy (John Whale)", "USER", "https://www.facebook.com/HuyFreedomSaigon" },
                    { "DuaLeo.Stand.up.comedian", "Admin", "Dưa Leo - Stand up comedian", "PAGE", "https://www.facebook.com/DuaLeo.Stand.up.comedian" },
                    { "BinhLuanVeDangCongSan", "Admin", "Bình Luận Về Đảng Cộng Sản", "PAGE", "https://www.facebook.com/BinhLuanVeDangCongSan" },
                    { "bao.luong.5011516", "Admin", "Luong Quang Bao", "USER", "https://www.facebook.com/bao.luong.5011516" },
                    { "90trieunguoi", "Admin", "VIỆT NAM DÂN CHỦ", "PAGE", "https://www.facebook.com/90trieunguoi" },
                    { "187530275233978", "Admin", "BÀN LUẬN về KINH TẾ - CHÍNH TRỊ 2", "GR", "https://www.facebook.com/groups/187530275233978" },
                    { "1752456318197543", "Admin", "Xây Dựng Đảng", "GR", "https://www.facebook.com/groups/1752456318197543" },
                    { "kinhtechinhtrixahoivn", "Admin", "Bàn Luận về Kinh Tế - Chính Trị", "GR", "https://www.facebook.com/groups/kinhtechinhtrixahoivn" },
                    { "viettan", "Admin", "Việt Tân", "PAGE", "https://www.facebook.com/viettan" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdministrativeDivision_ManagerId",
                table: "AdministrativeDivision",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Post_AdministrativeDivisionId",
                table: "Post",
                column: "AdministrativeDivisionId");

            migrationBuilder.CreateIndex(
                name: "IX_Post_FaceBookId",
                table: "Post",
                column: "FaceBookId");

            migrationBuilder.CreateIndex(
                name: "IX_Post_NewsLabelId",
                table: "Post",
                column: "NewsLabelId");

            migrationBuilder.CreateIndex(
                name: "IX_Post_SentimentLabelId",
                table: "Post",
                column: "SentimentLabelId");

            migrationBuilder.CreateIndex(
                name: "IX_Report_AdministrativeDivisionId",
                table: "Report",
                column: "AdministrativeDivisionId");

            migrationBuilder.CreateIndex(
                name: "IX_Report_PostId",
                table: "Report",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_WatchList_AdministrativeDivisionId",
                table: "WatchList",
                column: "AdministrativeDivisionId");

            migrationBuilder.CreateIndex(
                name: "IX_WatchList_FaceBookTypeId",
                table: "WatchList",
                column: "FaceBookTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "History");

            migrationBuilder.DropTable(
                name: "Report");

            migrationBuilder.DropTable(
                name: "Post");

            migrationBuilder.DropTable(
                name: "NewsLabel");

            migrationBuilder.DropTable(
                name: "SentimentLabel");

            migrationBuilder.DropTable(
                name: "WatchList");

            migrationBuilder.DropTable(
                name: "AdministrativeDivision");

            migrationBuilder.DropTable(
                name: "FaceBookType");

            migrationBuilder.DropTable(
                name: "RoleManager");
        }
    }
}
