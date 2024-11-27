using Microsoft.AspNetCore.Mvc;

namespace QLViecLam.Controllers.Admin.Error
{
    public class ErrorController : Controller
    {
        public IActionResult Page()
        {
            ViewData["Messages"] = "Bạn không có quyền truy cập vào chức năng này!";
            return View("Views/Admin/Error/Page.cshtml");
        }

        public IActionResult SessionOut()
        {
            return View("Views/Admin/Error/SessionOut.cshtml");
        }

        public IActionResult SupPort()
        {
            return View("Views/Admin/Error/SupPort.cshtml");
        }


    }
}
