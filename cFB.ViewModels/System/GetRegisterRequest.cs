using System.ComponentModel.DataAnnotations;

namespace cFB.ViewModels.System
{
    public class GetRegisterRequest
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bạn cần phải thêm thuộc tính này ")]
        public string AdministrativeDivision_Admin { get; set; }
        [Display(Name = "Tên địa phương")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bạn cần nhập tên người dùng")]
        public string AdministrativeDivisionName { get; set; }
        [Display(Name = "Số điện thoại")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bạn cần nhập số diện thoại liên hệ")]
        [RegularExpression(@"^((09(\d){8})|(086(\d){7})|(088(\d){7})|(089(\d){7})|(01(\d){9})|(02(\d){9}))$", ErrorMessage = "Số điện thoại này không tồn tại")]
        public string NumberPhone { get; set; }
        [Display(Name = "Địa chỉ")]
        public string Addrees { get; set; }
        [Display(Name = "Mật khẩu")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Cần nhập mật khẩu")]
        public string Password { get; set; }
        [Display(Name = "Nhập lại mật khẩu")]
        [Compare(otherProperty: "Password", ErrorMessage = "Mật khẩu không khớp")]
        public string ConfirmPassword { get; set; }
    }
}
