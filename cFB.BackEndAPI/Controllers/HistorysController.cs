using cFB.Applications.Catalog.Historys;
using cFB.Data.EFs;
using cFB.ViewModels.Catalog.Historys;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using cFB.Data.Entites;

namespace cFB.BackEndAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistorysController : Controller
    {
        private readonly IHistorySevice _historySevice;
        private readonly IHttpContextAccessor _accessor;
        private readonly cFBDbContext _context;
        public HistorysController(IHistorySevice historySevice, IHttpContextAccessor accessor, cFBDbContext context)
        {
            _historySevice = historySevice;
            _accessor = accessor;
            _context = context;
        }

        [HttpGet("GetAllHistory")]
        public async Task<JsonResult> GetAllHistory([FromQuery] GetManagerHistoryRequest request)
        {
            var history = await _historySevice.GetAllHistory(request);
            
            return Json(history);
        }

        [HttpGet("GetAllHistoryClient")]
        public async Task<JsonResult> GetAllHistoryClient([FromQuery] GetManagerHistoryClientRequest request)
        {
            var history = await _historySevice.GetAllHistoryClient(request);

            return Json(history);
        }

        [HttpPost]
        public async Task<JsonResult> CreateHistoryClient(string AdministrativeDivisionID)
        {
            var ipAdress = _accessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
            var userAgent = Request?.Headers?["User-Agent"];

            var data = new HistoryClient()
            {
                AdministrativeDivisionID = AdministrativeDivisionID,
                IPAddress = ipAdress,
                NameMachine = userAgent,
                Time = DateTime.Now
            };
          
            _context.HistoryClients.Add(data);

            await _context.SaveChangesAsync();

            return Json(true);
        }
    }

}
