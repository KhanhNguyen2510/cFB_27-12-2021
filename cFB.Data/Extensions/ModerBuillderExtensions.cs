﻿using cFB.Data.Entites;
using cFB.Utilities.Constants;
using Microsoft.EntityFrameworkCore;

namespace cFB.Data.Extensions
{
    public static class ModerBuillderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<FaceBookType>().HasData(
                new FaceBookType { FaceBookTypeId = "CGR", FaceBookTypeName = "Nhóm Kín", Description = "Group kín, nơi mọi người có thể xem tên nhóm và thành viên, nhưng không xem được nội dung của nhóm." },
                new FaceBookType { FaceBookTypeId = "GR", FaceBookTypeName = "Nhóm Công Khai", Description = "Group công khai, nơi mọi người có thể xem tất cả nội dung và các thành viên." },
                new FaceBookType { FaceBookTypeId = "PAGE", FaceBookTypeName = "Trang", Description = "Trang công khai" },
                new FaceBookType { FaceBookTypeId = "USER", FaceBookTypeName = "Cá nhân", Description = "Tài khoản của người dùng thông thường" }
                );
            modelBuilder.Entity<NewsLabel>().HasData(
                new NewsLabel { NewsLabelId = "AN", NewsLabelName = "Âm nhạc" },
                 new NewsLabel { NewsLabelId = "ANQG", NewsLabelName = "An ninh quốc gia" },
                 new NewsLabel { NewsLabelId = "ANTT", NewsLabelName = "An ninh trật tự" },
                 new NewsLabel { NewsLabelId = "AT", NewsLabelName = "Ẩm thực" },
                 new NewsLabel { NewsLabelId = "CN", NewsLabelName = "Công nghệ" },
                 new NewsLabel { NewsLabelId = "DL", NewsLabelName = "Du lịch" },
                 new NewsLabel { NewsLabelId = "GD", NewsLabelName = "Giáo dục" },
                 new NewsLabel { NewsLabelId = "GT", NewsLabelName = "Giải trí" },
                 new NewsLabel { NewsLabelId = "KD", NewsLabelName = "Kinh Doanh" },
                  new NewsLabel { NewsLabelId = "PA", NewsLabelName = "Phim ảnh" },
                  new NewsLabel { NewsLabelId = "PL", NewsLabelName = "Pháp luật" },
                  new NewsLabel { NewsLabelId = "SK", NewsLabelName = "Sức khỏe" },
                  new NewsLabel { NewsLabelId = "ST", NewsLabelName = "Sống trẻ" },
                  new NewsLabel { NewsLabelId = "TG", NewsLabelName = "Thế giới" },
                  new NewsLabel { NewsLabelId = "TS", NewsLabelName = "Thời sự" },
                  new NewsLabel { NewsLabelId = "TTH", NewsLabelName = "Thể thao" },
                  new NewsLabel { NewsLabelId = "TTR", NewsLabelName = "Thời trang" },
                  new NewsLabel { NewsLabelId = "XB", NewsLabelName = "Xuất bản" },
                  new NewsLabel { NewsLabelId = "XE", NewsLabelName = "Xe" }
                );
            modelBuilder.Entity<SentimentLabel>().HasData(
                new SentimentLabel { SentimentLabelId = "NEG", SentimentLabelName = "Tiêu cực" },
                new SentimentLabel { SentimentLabelId = "NEU", SentimentLabelName = "Bình thường" },
                new SentimentLabel { SentimentLabelId = "POS", SentimentLabelName = "Tích cực" }
                );

            modelBuilder.Entity<RoleManager>().HasData(
                new RoleManager { ManagerId = "Admin", ManagerName = "Nơi quản lý cao nhất" },
                new RoleManager { ManagerId = "User", ManagerName = "Các địa phương sử dụng" }
                );
           var pass =  ShareContants.MD5("1");
            modelBuilder.Entity<AdministrativeDivision>().HasData(
                new AdministrativeDivision { AdministrativeDivisionId = "86C1", AdministrativeDivisionName = "Thành phố Phan Thiết", NumberPhone = "02523822694", Addrees = "158 Đặng Văn Lãnh, TP Phan Thiết", Password = "1", ManagerId = "Admin" },
                new AdministrativeDivision { AdministrativeDivisionId = "86B1", AdministrativeDivisionName = "Huyện Tuy Phong", NumberPhone = "02523850163", Addrees = "Đường 17/4, thị trấn Liên Hương, huyện Tuy Phong", Password = "1", ManagerId = "User" },
                new AdministrativeDivision { AdministrativeDivisionId = "86B2", AdministrativeDivisionName = "Huyện Bắc Bình", NumberPhone = "02523860133", Addrees = "17 Lê Hồng Phong, khu phố Xuân An 2, thị trấn Chợ Lầu, huyện Bắc Bình", Password = "1", ManagerId = "User" },
                new AdministrativeDivision { AdministrativeDivisionId = "86B3", AdministrativeDivisionName = "Huyện Hàm Thuận Bắc", NumberPhone = "02523865113", Addrees = "Khu phố Lâm Hòa, thị trấn Ma Lâm, huyện Hàm Thuận Bắc", Password = "1", ManagerId = "User" },
                new AdministrativeDivision { AdministrativeDivisionId = "86B4", AdministrativeDivisionName = "Huyện Hàm Thuận Nam", NumberPhone = "02523867113", Addrees = "14 Trần phú, thị trấn Thuận Nam, huyện Hàm Thuận Nam", Password = "1", ManagerId = "User" },
                new AdministrativeDivision { AdministrativeDivisionId = "86B5", AdministrativeDivisionName = "Huyện Hàm Tân", NumberPhone = "02523876100", Addrees = "Khu phố 2, Thị trấn Tân Nghĩa, huyện Hàm Tân", Password = "1", ManagerId = "User" },
                new AdministrativeDivision { AdministrativeDivisionId = "86B6", AdministrativeDivisionName = "Thị xã La Gi", NumberPhone = "02523871988", Addrees = "02 Ngô Gia Tự, phường Tân An, thị xã La Gi", Password = "1", ManagerId = "User" },
                new AdministrativeDivision { AdministrativeDivisionId = "86B7", AdministrativeDivisionName = "Huyện Tánh Linh", NumberPhone = "0794201396", Addrees = "449 Trần Hưng Đạo, khu phố Lạc Hóa 1, thị trấn Lạc Tánh, huyện Tánh Linh", Password = "1", ManagerId = "User" },
                new AdministrativeDivision { AdministrativeDivisionId = "86B8", AdministrativeDivisionName = "Huyện Đức Linh", NumberPhone = "02523882113", Addrees = "Thôn 2, xã Nam Chính, huyện Đức Linh", Password = "1", ManagerId = "User" },
                new AdministrativeDivision { AdministrativeDivisionId = "86B9", AdministrativeDivisionName = "Huyện Phú Quý", NumberPhone = "02523767434", Addrees = "239 Hùng Vương, thôn Quý Thanh, xã Ngũ Phụng, huyện Phú Quý", Password = "1", ManagerId = "User" },
                 new AdministrativeDivision { AdministrativeDivisionId = "Admin", AdministrativeDivisionName = "Admin", NumberPhone = "02523767434", Addrees = "239 Hùng Vương, thôn Quý Thanh, xã Ngũ Phụng, huyện Phú Quý", Password = "1", ManagerId = "Admin" }
                 );
            modelBuilder.Entity<WatchList>().HasData(
                new WatchList { FaceBookId = "1648199831900386", FaceBookName = "Triệt Hạ Bò Đỏ", FaceBookUrl = "https://www.facebook.com/groups/1648199831900386", AdministrativeDivisionId = "Admin", FaceBookTypeId = "GR" },
                new WatchList { FaceBookId = "1752456318197543", FaceBookName = "Xây Dựng Đảng", FaceBookUrl = "https://www.facebook.com/groups/1752456318197543", AdministrativeDivisionId = "Admin", FaceBookTypeId = "GR" },
                new WatchList { FaceBookId = "187530275233978", FaceBookName = "BÀN LUẬN về KINH TẾ - CHÍNH TRỊ 2", FaceBookUrl = "https://www.facebook.com/groups/187530275233978", AdministrativeDivisionId = "Admin", FaceBookTypeId = "GR" },
                new WatchList { FaceBookId = "90trieunguoi", FaceBookName = "VIỆT NAM DÂN CHỦ", FaceBookUrl = "https://www.facebook.com/90trieunguoi", AdministrativeDivisionId = "Admin", FaceBookTypeId = "PAGE" },
                new WatchList { FaceBookId = "bao.luong.5011516", FaceBookName = "Luong Quang Bao", FaceBookUrl = "https://www.facebook.com/bao.luong.5011516", AdministrativeDivisionId = "Admin", FaceBookTypeId = "USER" },
                new WatchList { FaceBookId = "BinhLuanVeDangCongSan", FaceBookName = "Bình Luận Về Đảng Cộng Sản", FaceBookUrl = "https://www.facebook.com/BinhLuanVeDangCongSan", AdministrativeDivisionId = "Admin", FaceBookTypeId = "PAGE" },
                new WatchList { FaceBookId = "DuaLeo.Stand.up.comedian", FaceBookName = "Dưa Leo - Stand up comedian", FaceBookUrl = "https://www.facebook.com/DuaLeo.Stand.up.comedian", AdministrativeDivisionId = "Admin", FaceBookTypeId = "PAGE" },
                new WatchList { FaceBookId = "HuyFreedomSaigon", FaceBookName = "Huỳnh Quốc Huy (John Whale)", FaceBookUrl = "https://www.facebook.com/HuyFreedomSaigon", AdministrativeDivisionId = "Admin", FaceBookTypeId = "USER" },
                new WatchList { FaceBookId = "kinhtechinhtrixahoivn", FaceBookName = "Bàn Luận về Kinh Tế - Chính Trị", FaceBookUrl = "https://www.facebook.com/groups/kinhtechinhtrixahoivn", AdministrativeDivisionId = "Admin", FaceBookTypeId = "GR" },
                new WatchList { FaceBookId = "lukhach", FaceBookName = "Nguyen Huy Vu", FaceBookUrl = "https://www.facebook.com/lukhach", AdministrativeDivisionId = "Admin", FaceBookTypeId = "USER" },
                new WatchList { FaceBookId = "mothermushroom", FaceBookName = "Mẹ Nấm", FaceBookUrl = "https://www.facebook.com/mothermushroom", AdministrativeDivisionId = "Admin", FaceBookTypeId = "USER" },
                new WatchList { FaceBookId = "nhabaocongdan", FaceBookName = "Góc nhìn báo chí - Công dân", FaceBookUrl = "https://www.facebook.com/groups/nhabaocongdan", AdministrativeDivisionId = "Admin", FaceBookTypeId = "GR" },
                new WatchList { FaceBookId = "nhatkyyeunuoc1", FaceBookName = "Nhật Ký Yêu Nước", FaceBookUrl = "https://www.facebook.com/nhatkyyeunuoc1", AdministrativeDivisionId = "Admin", FaceBookTypeId = "PAGE" },
                new WatchList { FaceBookId = "phapluatvacuocsong.vn", FaceBookName = "Pháp luật & Cuộc sống", FaceBookUrl = "https://www.facebook.com/phapluatvacuocsong.vn", AdministrativeDivisionId = "Admin", FaceBookTypeId = "PAGE" },
                new WatchList { FaceBookId = "TamConXuyenDiep", FaceBookName = "Tam Côn Xuyên Diệp", FaceBookUrl = "https://www.facebook.com/groups/TamConXuyenDiep", AdministrativeDivisionId = "Admin", FaceBookTypeId = "GR" },
                new WatchList { FaceBookId = "thong.luan.1", FaceBookName = "Tập Hợp Dân Chủ Đa Nguyên", FaceBookUrl = "https://www.facebook.com/thong.luan.1", AdministrativeDivisionId = "Admin", FaceBookTypeId = "PAGE" },
                new WatchList { FaceBookId = "thuc.tranhuynhduy", FaceBookName = "Trần Huỳnh Duy Thức", FaceBookUrl = "https://www.facebook.com/thuc.tranhuynhduy", AdministrativeDivisionId = "Admin", FaceBookTypeId = "USER" },
                new WatchList { FaceBookId = "vietnamconghoa123", FaceBookName = "Việt Nam Cộng Hòa", FaceBookUrl = "https://www.facebook.com/vietnamconghoa123", AdministrativeDivisionId = "Admin", FaceBookTypeId = "PAGE" },
                new WatchList { FaceBookId = "viettan", FaceBookName = "Việt Tân", FaceBookUrl = "https://www.facebook.com/viettan", AdministrativeDivisionId = "Admin", FaceBookTypeId = "PAGE" }
                );
        }
    }
}
