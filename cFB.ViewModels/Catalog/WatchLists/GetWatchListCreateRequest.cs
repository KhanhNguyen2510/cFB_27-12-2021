using cFB.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace cFB.ViewModels.Catalog.WatchLists
{
    public class GetWatchListCreateRequest
    {
        [Display(Name = "Bạn cần nhập thông tin này")]
        public string FaceBookID { get; set; }
        [Display(Name = "Bạn cần phải nhập tên đường link muốn theo dõi")]
        public string FaceBookName { get; set; }
        [Url(ErrorMessage = "Bạn cần phải chọn đúng đường link")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Cần xác định kiễu đường link")]
        public string FaceBookUrl { get; set; }
        public Status? Status { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Cần xác định người đăng")]
        [StringLength(maximumLength: 10, MinimumLength = 1, ErrorMessage = "Chỉ nhập cái thuộc tính từ 1 đến 10")]
        public string AdministrativeDivisionID { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Cần xác định kiễu dữ liệu")]
        [StringLength(maximumLength: 10, MinimumLength = 1, ErrorMessage = "Chỉ nhập cái thuộc tính từ 1 đến 10")]
        public string FaceBookTypeID { get; set; }
    }
}
