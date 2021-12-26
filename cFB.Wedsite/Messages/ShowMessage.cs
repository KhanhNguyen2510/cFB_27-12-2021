namespace cFB.Wedsite.Messages
{
    public class ShowMessage
    {
        public static string ConnectFailed()
        {
            return "Kết nối server Không thành công";
        }
        public static string AuthenticateFailed()
        {
            return "Tài khoản hoặc mật khẩu không chính xác";
        }
        public static string AddItemSuccessful()
        {
            return "Thêm thành công";
        }
        public static string CheckUser( string name)
        {
            return $"Tài khoản {name} không có quyền truy cập !";
        }
        public static string UpdateUserSuccessful()
        {
            return "Cập nhật người dùng thành công !";
        }
        public static string UpdateUserFaled()
        {
            return "Cập nhật người dùng thất bại !";
        }
        public static string AddUserSuccessful()
        {
            return "Thêm một người dùng thất bại !";
        }
        public static string AddUserFaled()
        {
            return "Thêm một người dùng thành công !";
        }
        public static string NumberPhone()
        {
            return "Số điện thoại đã tồn tại !";
        }
        public static string AddItemFaled()
        {
            return "Thêm thất bại, có lỗi xảy ra phía server";
        }
        public static string UpdateItemSuccessful()
        {
            return "Cập nhật thành công";
        }
        public static string UpdateItemFaled()
        {
            return "Cập nhật thất bại, có lỗi xảy ra phía server";
        }
        public static string Nulldata()
        {
            return "Chưa có dữ liệu";
        }
    }
}
