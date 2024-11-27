using Microsoft.AspNetCore.Mvc;
using QLViecLam.Data;
using QLViecLam.Helper;
using QLViecLam.ViewModels.Admin.Manages.ThongTinThiTruongLD.ThuThapTT.HeThongTruyVanTT;
using QLViecLam.ViewModels.Admin.Manages.TongHop_PhanTich_DuDoan.ThongTinCung_Cau;

namespace QLViecLam.Controllers.Admin.Manages.DoanhNghiep
{
    public class DanhSachDoanhNghiepController : Controller
    {
        private readonly ApplicationDbContext _db;

        public DanhSachDoanhNghiepController(ApplicationDbContext db)
        {
            _db = db;
        }

        [Route("DanhSachDoanhNghiep")]
        [HttpGet]
        public IActionResult Index(string phanloai, DateTime tungay, DateTime denngay)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (!Helpers.CheckChucNang(HttpContext.Session, "DanhSachDoanhNghiep", "DanhSach"))
                {
                    ViewData["Messages"] = "Bạn không có quyền truy cập vào chức năng này!";
                    return View("Views/Admin/Error/Page.cshtml");
                }
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }

            var model = _db.Company;

            ViewData["MenuLv1"] = "doanhnghiep";
            ViewData["MenuLv2"] = "doanhnghiep_danhsachdoanhnghiep";
            return View("Views/Admin/Manages/DoanhNghiep/DanhSachDoanhNghiep/Index.cshtml", model);
        }
    }
}
