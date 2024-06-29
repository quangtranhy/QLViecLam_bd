using Microsoft.AspNetCore.Mvc;

namespace QLViecLam.Controllers.Admin.Error
{
    public class PageController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Messages"] = "Bạn không có quyền truy cập vào chức năng này!";
            return View("Views/Admin/Error/Page.cshtml");
        }
    }
}
