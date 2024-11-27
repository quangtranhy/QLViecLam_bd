using Microsoft.AspNetCore.Mvc;
using QLViecLam.Data;
using QLViecLam.Models.Admin.Systems.DanhMuc;

namespace QLViecLam.Controllers.Admin.Systems.DanhMuc
{
    public class QuanHeController : Controller
    {
        private readonly ApplicationDbContext _db;

        public QuanHeController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet("QuanHe")]
        public IActionResult Index()
        {
            var model = _db.QuanHe;
            var stt = model.AsEnumerable().Select(e => int.Parse(e.MaQuanHe!)).DefaultIfEmpty(0).Max();

            ViewData["stt"] = stt != 0 ? stt + 1 : 1;
            ViewData["Title"] = "Mối quan hệ";
            ViewData["MenuLv1"] = "hethong";
            ViewData["MenuLv2"] = "hethong_danhmuc";
            ViewData["MenuLv3"] = "hethong_danhmuc_quanhe";
            return View("Views/Admin/Systems/Danhmuc/QuanHe/Index.cshtml", model);
        }

        [HttpPost("QuanHe/Store")]
        public IActionResult Store(string TenQuanHe_create, string MaQuanHe_create, string TrangThai_create)
        {
            var model = new QuanHe
            {
                TenQuanHe = TenQuanHe_create,
                MaQuanHe = MaQuanHe_create,
                TrangThai = TrangThai_create,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now,
            };
            _db.QuanHe.Add(model);
            _db.SaveChanges();
            return RedirectToAction("Index", "QuanHe");
        }

        [HttpPost("QuanHe/Edit")]
        public JsonResult Edit(int id)
        {
            var model = _db.QuanHe.FirstOrDefault(p => p.Id == id);

            if (model != null)
            {
                string result = "<div class='modal-body' id='edit_thongtin'>";

                result += "<div class='row text-left'>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Mã danh mục</b></label>";
                result += "<input type='number' id='MaQuanHe_edit' name='MaQuanHe_edit' value='" + model.MaQuanHe + "' class='form-control'/>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Tên danh mục</b></label>";
                result += "<input type='text' id='TenQuanHe_edit' name='TenQuanHe_edit' value='" + model.TenQuanHe + "' class='form-control'/>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Trạng thái</b></label>";
                result += "<select id='TrangThai_edit' name='TrangThai_edit' class='form-control'>";
                result += "<option value='1' " + (model.TrangThai == "1" ? "selected" : "") + " >Kích hoạt</option>";
                result += "<option value='0' " + (model.TrangThai == "0" ? "selected" : "") + " >Vô hiệu</option>";
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

        [HttpPost("QuanHe/Update")]
        public IActionResult Update(string TenQuanHe_edit, string MaQuanHe_edit, int Id_edit, string TrangThai_edit)
        {
            var model = _db.QuanHe.FirstOrDefault(t => t.Id == Id_edit);
            if (model != null)
            {
                model.MaQuanHe = MaQuanHe_edit;
                model.TenQuanHe = TenQuanHe_edit;
                model.TrangThai = TrangThai_edit;
                model.Updated_at = DateTime.Now;
                _db.QuanHe.Update(model);
                _db.SaveChanges();
            }

            return RedirectToAction("Index", "QuanHe");
        }

        [HttpPost("QuanHe/Delete")]
        public IActionResult Delete(int Id_delete)
        {
            var model = _db.QuanHe.FirstOrDefault(t => t.Id == Id_delete);
            if (model != null)
            {
                _db.QuanHe.Remove(model);
                _db.SaveChanges();
            }
            return RedirectToAction("Index", "QuanHe");
        }
    }
}
