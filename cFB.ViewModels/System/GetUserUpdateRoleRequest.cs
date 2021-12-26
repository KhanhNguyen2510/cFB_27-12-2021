using System.ComponentModel.DataAnnotations;

namespace cFB.ViewModels.System
{
    public class GetUserUpdateRoleRequest
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bạn cần nhập tên tài khoản người thêm")]
        public string AdministrativeDivision_Admin { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bạn cần nhập tên tài khoản muốn thay đổi")]
        public string AdministrativeDivisionID { get; set; }
    }
}
