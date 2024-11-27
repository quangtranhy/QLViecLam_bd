using Microsoft.AspNetCore.Mvc;
using QLViecLam.Data;
using QLViecLam.Models.Admin.Manages;
using QLViecLam.Helper;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace QLViecLam.Controllers.Admin.Manages.DoanhNghiep
{
    public class NhuCauTuyenDungController : Controller
    {
        private readonly ApplicationDbContext _db;

        public NhuCauTuyenDungController(ApplicationDbContext db)
        {
            _db = db;
        }
        [Route("NhuCauTuyenDung")]
        [HttpGet]
        public IActionResult Index(DateTime tungay, DateTime denngay ,string user)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (!Helpers.CheckChucNang(HttpContext.Session, "NhuCauTuyenDung", "DanhSach"))
                {
                    ViewData["Messages"] = "Bạn không có quyền truy cập vào chức năng này!";
                    return View("Views/Admin/Error/Page.cshtml");
                }
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }


            var TuyenDung = _db.TuyenDung.AsQueryable();
            ViewData["tungay"] = tungay;
            ViewData["denngay"] = denngay;

            ViewData["Title"] = "Kế hoạch thu thập thông tin lao động";
            ViewData["MenuLv1"] = "doanhnghiep";
            ViewData["MenuLv2"] = "doanhnghiep_nhucautuyendung";

            if (Helpers.ConvertDateToStr(tungay) != "")
            {
                TuyenDung = TuyenDung.Where(x => x.Created_at >= tungay);
            }
            if (Helpers.ConvertDateToStr(denngay) != "")
            {
                TuyenDung = TuyenDung.Where(x => x.ThoiHan <= denngay);
            }
            var Company = _db.Company.AsQueryable();
            if (user != null)
            {
                int u = int.Parse(user);
                Company = Company.Where(x => x.User == u);

                var model = from td in TuyenDung
                            join comp in Company on td.User equals comp.User 
                            select new TuyenDung
                            {
                                Id = td.Id,
                                NoiDung = td.NoiDung,
                                TenCongTy = comp.Name,

                            };
                return View("Views/Admin/Manages/DoanhNghiep/NhuCauTuyenDung/Index.cshtml", model);
            }
            else
            {
                var model = from td in TuyenDung
                            join comp in Company on td.User equals comp.User into details
                            from comp_de in details.DefaultIfEmpty()
                            select new TuyenDung
                            {
                                Id = td.Id,
                                NoiDung = td.NoiDung,
                                TenCongTy = comp_de != null ? comp_de.Name : "",

                            };
                return View("Views/Admin/Manages/DoanhNghiep/NhuCauTuyenDung/Index.cshtml", model);
            }

        
        }

        [Route("NhuCauTuyenDung/Detail")]
        [HttpGet]
        public IActionResult Detail(int Id)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (!Helpers.CheckChucNang(HttpContext.Session, "NhuCauTuyenDung", "DanhSach"))
                {
                    ViewData["Messages"] = "Bạn không có quyền truy cập vào chức năng này!";
                    return View("Views/Admin/Error/Page.cshtml");
                }
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }


            var TuyenDung = _db.TuyenDung.Where(x => x.Id == Id).FirstOrDefault();
            var ViTriTuyenDung = _db.ViTriTuyenDung.Where(x => x.IdTuyenDung == Id);

            ViewData["TuyenDung"] = TuyenDung;
            ViewData["ViTriTuyenDung"] = ViTriTuyenDung;
            ViewData["Title"] = "Kế hoạch thu thập thông tin lao động";
            ViewData["MenuLv1"] = "doanhnghiep";
            ViewData["MenuLv2"] = "doanhnghiep_nhucautuyendung";
            return View("Views/Admin/Manages/DoanhNghiep/NhuCauTuyenDung/Detail.cshtml", TuyenDung);
        }

        [Route("NhuCauTuyenDung/BcMau01")]
        [HttpGet]
        public IActionResult BcMau01(string id)
        {
    
            ViewData["HinhThucDoanhNghiep"] = _db.HinhThucDoanhNghiep;
            return View("Views/Admin/Manages/DoanhNghiep/NhuCauTuyenDung/BcMau01.cshtml");

        }


        [Route("NhuCauTuyenDung/BcMau02")]
        [HttpGet]
        public IActionResult BcMau02()
        {

            return View("Views/Admin/Manages/DoanhNghiep/NhuCauTuyenDung/BcMau02.cshtml");
        }


        [Route("NhuCauTuyenDung/BcMau03")]
        [HttpGet]
        public IActionResult BcMau03()
        {

            return View("Views/Admin/Manages/DoanhNghiep/NhuCauTuyenDung/BcMau03.cshtml");
        }

        [Route("NhuCauTuyenDung/BcMau03a")]
        [HttpGet]
        public IActionResult BcMau03a()
        {

            return View("Views/Admin/Manages/DoanhNghiep/NhuCauTuyenDung/BcMau03a.cshtml");
        }
    }
}
