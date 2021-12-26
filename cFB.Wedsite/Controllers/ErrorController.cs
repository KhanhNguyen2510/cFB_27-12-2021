using Microsoft.AspNetCore.Mvc;

namespace cFB.Wedsite.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatuscodehanler(int statusCode)
        {
            switch (statusCode)
            {

                case 404:
                    {
                        ViewBag.ErrorMessage = "Thông tin bị lỗi";
                        break;
                    }
                case 500:
                    {
                        ViewBag.ErrorMessage = "Thông tin bị lỗi";
                        break;
                    }
            }
            return View("NotFound");
        }
    }
}
