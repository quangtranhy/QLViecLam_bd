using Microsoft.AspNetCore.Mvc;
using QLViecLam.Data;
using QLViecLam.Helper;
using QLViecLam.Models.Admin.Systems.HeThongChung;

namespace QLViecLam.Controllers.Admin.Systems.HeThongChung.NhomChucNang
{
    public class NhomChucNangController : Controller
    {
        private readonly ApplicationDbContext _db;

        public NhomChucNangController(ApplicationDbContext db)
        {
            _db = db;
        }

        [Route("NhomChucNang")]
        [HttpGet]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (!Helpers.CheckChucNang(HttpContext.Session, "NhomChucNang", "DanhSach"))
                {
                    ViewData["Messages"] = "Bạn không có quyền truy cập vào chức năng này!";
                    return View("Views/Admin/Error/Page.cshtml");
                }
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }

            var model = _db.DsNhomTaiKhoan;
            return View("Views/Admin/Systems/HeThongChung/NhomChucNang/Index.cshtml",model);
        }

        [Route("NhomChucNang/Detail")]
        [HttpGet]
        public IActionResult Detail(string manhom)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (!Helpers.CheckChucNang(HttpContext.Session, "NhomChucNang", "DanhSach"))
                {
                    ViewData["Messages"] = "Bạn không có quyền truy cập vào chức năng này!";
                    return View("Views/Admin/Error/Page.cshtml");
                }
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }

            var model = _db.Users.Where(x=>x.MaNhomChucNang == manhom);
            ViewData["TenNhom"] = _db.DsNhomTaiKhoan.FirstOrDefault(x=>x.MaNhomChucNang == manhom)!.TenNhomChucNang;
   
            return View("Views/Admin/Systems/HeThongChung/NhomChucNang/Detail.cshtml", model);
        }


        [Route("NhomChucNang/Store")]
        [HttpPost]
        public IActionResult Store(DsNhomTaiKhoan request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (!Helpers.CheckChucNang(HttpContext.Session, "NhomChucNang", "ThayDoi"))
                {
                    ViewData["Messages"] = "Bạn không có quyền truy cập vào chức năng này!";
                    return View("Views/Admin/Error/Page.cshtml");
                }
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }

            var manhom = DateTime.Now.ToString("yyyyMMddHHmmss");

            var model = new DsNhomTaiKhoan
            {
                MaNhomChucNang = manhom,
                TenNhomChucNang = request.TenNhomChucNang,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now,
            };
        
            _db.DsNhomTaiKhoan.Add(model);
            _db.SaveChanges();

            ViewData["MenuLv1"] = "hethong";
            ViewData["MenuLv2"] = "hethong_nhomchucnang";
            return RedirectToAction("Index", "NhomChucNang");
        }

        [Route("NhomChucNang/Edit")]
        [HttpPost]
        public JsonResult Edit(int id)
        {

            var DsNhomTaiKhoan = _db.DsNhomTaiKhoan;
            var model = DsNhomTaiKhoan.FirstOrDefault(p => p.Id == id);
            if (model != null)
            {
                string result = "<div class='modal-body' id='edit_thongtin'>";

                result += "<div class='row text-left'>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Tên nhóm chức năng</b><span class='require' >*</span></label>";
                result += "<input type='text' id='tennhomchucnang' name='tennhomchucnang' value='" + model.TenNhomChucNang + "' class='form-control' required />";
                result += "</div>";
                result += "</div>";
                result += "<input hidden type='text' id='Id' name='Id' value='" + model.Id + "'/>";
                result += "</div>";

                var data = new { status = "success", message = result };
                return Json(data);
            }
            else
            {
                var data = new { status = "error", message = "Không tìm thấy thông tin cần chỉnh sửa!!!" };
                return Json(data);
            }
        }

        [Route("NhomChucNang/Update")]
        [HttpPost]
        public IActionResult Update(DsNhomTaiKhoan request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (!Helpers.CheckChucNang(HttpContext.Session, "NhomChucNang", "ThayDoi"))
                {
                    ViewData["Messages"] = "Bạn không có quyền truy cập vào chức năng này!";
                    return View("Views/Admin/Error/Page.cshtml");
                }
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }

            var model = _db.DsNhomTaiKhoan.FirstOrDefault(t => t.Id == request.Id);
         
            if (model != null)
            {
                model.TenNhomChucNang = request.TenNhomChucNang;
                model.Updated_at = DateTime.Now;
                _db.DsNhomTaiKhoan.Update(model);
                _db.SaveChanges();
            }

            return RedirectToAction("Index", "NhomChucNang");
        }

        [Route("NhomChucNang/Delete")]
        [HttpPost]
        public IActionResult Delete(int Id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (!Helpers.CheckChucNang(HttpContext.Session, "NhomChucNang", "ThayDoi"))
                {
                    ViewData["Messages"] = "Bạn không có quyền truy cập vào chức năng này!";
                    return View("Views/Admin/Error/Page.cshtml");
                }
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }

            var model = _db.DsNhomTaiKhoan.FirstOrDefault(t => t.Id == Id_delete);
            if (model != null)
            {
                _db.DsNhomTaiKhoan.Remove(model);
                _db.SaveChanges();
            }
            return RedirectToAction("Index", "NhomChucNang");
        }
    }
}
