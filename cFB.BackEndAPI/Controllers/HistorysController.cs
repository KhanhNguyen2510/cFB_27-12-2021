using cFB.Applications.Catalog.Historys;
using cFB.ViewModels.Catalog.Historys;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace cFB.BackEndAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistorysController : Controller
    {
        private readonly IHistorySevice _historySevice;

        public HistorysController(IHistorySevice historySevice)
        {
            _historySevice = historySevice;
        }

        [HttpGet("GetAllHistory")]
        public async Task<JsonResult> GetAllHistory([FromQuery] GetManagerHistoryRequest request)
        {
            var history = await _historySevice.GetAllHistory(request);
            return Json(history);
        }
    }

}
