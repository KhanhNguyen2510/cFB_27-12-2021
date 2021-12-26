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
        [RegularExpression("/^(0?)(3[2-9]|5[6|8|9]|7[0|6-9]|8[0-6|8|9]|9[0-4|6-9])[0-9]{7}$/", ErrorMessage = "Số điện thoại này không tồn tại")]
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
