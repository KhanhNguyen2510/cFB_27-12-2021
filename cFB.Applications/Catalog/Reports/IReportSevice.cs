using cFB.ViewModels.Catalog.Reports;
using cFB.ViewModels.Common;
using System.Threading.Tasks;

namespace cFB.Applications.Catalog.Reports
{
    public interface IReportSevice
    {
        //Create
        Task CreateReport(GetManageReportRequest request);
        //List
        Task<PagedResult<GetReportViewModel>> GetAllReport(GetReportRequest request);
    }
}
