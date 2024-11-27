using Microsoft.AspNetCore.Mvc;
using QLViecLam.Data;
using QLViecLam.Helper;
using QLViecLam.Models.Admin.Manages;
using QLViecLam.Models.Admin.Systems.HeThongChung;

namespace QLViecLam.Controllers.Admin.Manages.KeHoachThuThapTT
{
    public class KyDieuTraController : Controller
    {
        private readonly ApplicationDbContext _db;

        public KyDieuTraController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet("KyDieuTra")]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (!Helpers.CheckChucNang(HttpContext.Session, "BienDongCung", "DanhSach"))
                {
                    ViewData["Messages"] = "Bạn không có quyền truy cập vào chức năng này!";
                    return View("Views/Admin/Error/Page.cshtml");
                }
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }


            var model = _db.KyDieuTra;
            var nam = DateTime.Now.Year;
            var kdt = nam.ToString();
            var kdt_model = model.Where(x=>x.MaKyDieuTra == kdt);
            if (kdt_model != null)
            {
                ViewData["kydieutra"] = nam + 1;
            }
            else {
                ViewData["kydieutra"] = nam;
            }
           
            ViewData["Title"] = "Kỳ điều tra";
            ViewData["MenuLv1"] = "kehoach";
            ViewData["MenuLv2"] = "kehoach_kydieutra";
            return View("Views/Admin/Manages/KeHoachThuThapTT/KyDieuTra/Index.cshtml", model);
        }

        [HttpPost("KyDieuTra/Store")]
        public IActionResult Store(string MaKyDieuTra_create, string NoiDung_create, string TrangThai_create)
        {
            var model = new KyDieuTra
            {
                MaKyDieuTra = MaKyDieuTra_create,
                NoiDung= NoiDung_create,
                TrangThai = TrangThai_create,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now,
            };
            _db.KyDieuTra.Add(model);
            _db.SaveChanges();
            return RedirectToAction("Index", "KyDieuTra");
        }

        [HttpPost("KyDieuTra/Edit")]
        public JsonResult Edit(int id)
        {
            var model = _db.KyDieuTra.FirstOrDefault(p => p.Id == id);

            if (model != null)
            {
                string result = "<div class='modal-body' id='edit_thongtin'>";

                result += "<div class='row text-left'>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Mã danh mục</b></label>";
                result += "<input type='text' id='MaKyDieuTra_edit' name='MaKyDieuTra_edit' value='" + model.MaKyDieuTra + "' class='form-control' required />";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Tên danh mục</b></label>";
                result += "<input type='text' id='NoiDung_edit' name='NoiDung_edit' value='" + model.NoiDung + "' class='form-control'/>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Trạng thái</b></label>";
                result += "<select id='TrangThai_edit' name='TrangThai_edit' class='form-control'>";
                result += "<option value='1'>Kích hoạt</option>";
                result += "<option value='0'>kết thúc</option>";
                result += "</select>";
                result += "</div>";
                result += "</div>";
                result += "<input hidden type='text' id='Id_edit' name='Id_edit' value='" + model.Id + "'/>";
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

        [HttpPost("KyDieuTra/Update")]
        public IActionResult Update(string NoiDung_edit, string MaKyDieuTra_edit, int Id_edit, string TrangThai_edit)
        {
            var model = _db.KyDieuTra.FirstOrDefault(t => t.Id == Id_edit);
            if (model != null)
            {
                model.MaKyDieuTra = MaKyDieuTra_edit;
                model.NoiDung = NoiDung_edit;
                model.TrangThai = TrangThai_edit;
                model.Updated_at = DateTime.Now;
                _db.KyDieuTra.Update(model);
                _db.SaveChanges();
            }

            return RedirectToAction("Index", "KyDieuTra");
        }

        [HttpPost("KyDieuTra/Delete")]
        public IActionResult Delete(int Id_delete)
        {
            var model = _db.KyDieuTra.FirstOrDefault(t => t.Id == Id_delete);
            if (model != null)
            {
                _db.KyDieuTra.Remove(model);
                _db.SaveChanges();
            }
            return RedirectToAction("Index", "KyDieuTra");
        }


    }
}
