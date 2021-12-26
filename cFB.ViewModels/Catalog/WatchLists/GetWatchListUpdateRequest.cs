using cFB.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace cFB.ViewModels.Catalog.WatchLists
{
    public class GetWatchListUpdateRequest
    {
        [Display(Name = "Mã danh sách theo dõi")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Cần nhập địa chỉ Id")]
        public string FaceBookID { get; set; }
        [Display(Name = "Tên danh sách theo doi")]
        public string FaceBookName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Cần xác định kiễu dữ liệu")]
        [StringLength(maximumLength: 10, MinimumLength = 4, ErrorMessage = "Chỉ nhập cái thuộc tính từ 4 đến 10")]
        public string FaceBookTypeID { get; set; }
        public Status? Status { get; set; }
    }
}
