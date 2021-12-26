using cFB.Applications.Catalog.Reports;
using cFB.ViewModels.Catalog.Reports;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace cFB.BackEndAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : Controller
    {
        private readonly IReportSevice _reportSevice;

        public ReportsController(IReportSevice reportSevice)
        {
            _reportSevice = reportSevice;
        }

        [HttpGet("GetAllReport")]
        public async Task<JsonResult> GetAllReport([FromQuery] GetReportRequest request)
        {
            var report = await _reportSevice.GetAllReport(request);
            return Json(report);
        }
    }
}
