using Microsoft.AspNetCore.Mvc;
using QLViecLam.Data;
using QLViecLam.Models.Admin.Manages.ThongTinThiTruongLD;

namespace QLViecLam.Controllers.Admin.Manages.TongHop_PhanTich_DuDoan.ThongTinCung_Cau.NguoiLaoDong
{
    public class NguoiLaoDongController : Controller
    {
        private readonly ApplicationDbContext _db;

        public NguoiLaoDongController(ApplicationDbContext db)
        {
            _db = db;
        }

        [Route("NguoiLaoDong")]
        [HttpGet]
        public IActionResult Index()
        {
   
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                bool check_per = true;
                if (check_per)
                {
  
                    var nguoilaodong = _db.NguoiLaoDong.Where(x => x.Nation == "VN");
                    var company = _db.Company;

                    var model = (from m in nguoilaodong
                                 join c in company on m.Company equals c.Id into details from c_de in details.DefaultIfEmpty()  
                                 select new QLViecLam.Models.Admin.Manages.ThongTinThiTruongLD.NguoiLaoDong
                                 {
                                     Id = m.Id,
                                     HoVaTen = m.HoVaTen,
                                     TenDn = c_de.Name,
                                     NgayThangNamSinh = m.NgayThangNamSinh,
                                     Tinh = m.Tinh,
                                     SoCCCD = m.SoCCCD, 
                                 });
           
                    ViewData["MenuLv1"] = "menu_capnhatcungcau";
                    ViewData["MenuLv2"] = "menu_capnhatcungcau_nguoilaodong";
                    return View("Views/Admin/Manages/TongHop_PhanTich_DuDoan/ThongTinCung_Cau/NguoiLaoDong/Index.cshtml",model);
                }
                else
                {
                    ViewData["Messages"] = "Bạn không có quyền truy cập vào chức năng này!";
                    return View("Views/Admin/Error/Page.cshtml");
                }
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }
        }
    }
}
