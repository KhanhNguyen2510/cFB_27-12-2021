using System.ComponentModel.DataAnnotations;

namespace cFB.ViewModels.System
{
    public class GetLoginRequest
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bạn cần nhập tên tài khoản")]
        public string UserName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bạn cần phải nhập mật khẩu")]
        public string Password { get; set; }
        public string ManagerID { get; set; }
    }
}
