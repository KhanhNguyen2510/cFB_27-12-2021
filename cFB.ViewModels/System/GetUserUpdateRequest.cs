using System.ComponentModel.DataAnnotations;

namespace cFB.ViewModels.System
{
    public class GetUserUpdateRequest
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the name")]
        public string AdministrativeDivisionID { get; set; }

        [Display(Name = "Tên địa phương")]
        public string AdministrativeDivisionName { get; set; }

        [Display(Name = "Số điện thoại")]
        [RegularExpression(@"^((09(\d){8})|(086(\d){7})|(088(\d){7})|(089(\d){7})|(01(\d){9})|(02(\d){9}))$", ErrorMessage = "Số điện thoại này không tồn tại")]
        public string NumberPhone { get; set; }

        [Display(Name = "Địa chỉ")]
        public string Addrees { get; set; }

        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }
    }
}
