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
        [RegularExpression("/^(0?)(3[2-9]|5[6|8|9]|7[0|6-9]|8[0-6|8|9]|9[0-4|6-9])[0-9]{7}$/", ErrorMessage = "Số điện thoại này không tồn tại")]
        public string NumberPhone { get; set; }

        [Display(Name = "Địa chỉ")]
        public string Addrees { get; set; }

        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }
    }
}
