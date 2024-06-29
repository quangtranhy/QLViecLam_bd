using Microsoft.AspNetCore.Mvc;

namespace QLViecLam.Controllers.Admin.Error
{
    public class SessionOutController : Controller
    {
        public IActionResult Index()
        {
 
            return View("Views/Admin/Error/SessionOut.cshtml");
        }
    }
}
