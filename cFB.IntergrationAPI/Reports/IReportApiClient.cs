using cFB.ViewModels.Catalog.Reports;
using cFB.ViewModels.Common;
using System.Threading.Tasks;

namespace cFB.IntergrationAPI.Reports
{
    public interface IReportApiClient
    {
        Task<PagedResult<GetReportViewModel>> GetAllReport(GetReportRequest request);
    }
}
