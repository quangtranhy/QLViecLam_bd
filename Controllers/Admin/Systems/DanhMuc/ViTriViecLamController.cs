using Microsoft.AspNetCore.Mvc;
using QLViecLam.Data;
using QLViecLam.Models.Admin.Systems.DanhMuc;

namespace QLViecLam.Controllers.Admin.Systems.DanhMuc
{
    public class ViTriViecLamController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ViTriViecLamController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet("ViTriViecLam")]
        public IActionResult Index()
        {
            var model = _db.ViTriViecLam;
            var stt = model.AsEnumerable().Select(e => int.Parse(e.MaViTriViecLam!)).DefaultIfEmpty(0).Max();

            ViewData["stt"] = stt != 0 ? stt + 1 : 1;
            ViewData["Title"] = "Dân tộc";
            ViewData["MenuLv1"] = "hethong";
            ViewData["MenuLv2"] = "hethong_danhmuc";
            ViewData["MenuLv3"] = "hethong_danhmuc_vitrivieclam";
            return View("Views/Admin/Systems/Danhmuc/ViTriViecLam/Index.cshtml", model);
        }

        [HttpPost("ViTriViecLam/Store")]
        public IActionResult Store(string TenViTriViecLam_create, string MaViTriViecLam_create, string TrangThai_create)
        {
            var model = new ViTriViecLam
            {
                TenViTriViecLam = TenViTriViecLam_create,
                MaViTriViecLam = MaViTriViecLam_create,
                TrangThai = TrangThai_create,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now,
            };
            _db.ViTriViecLam.Add(model);
            _db.SaveChanges();
            return RedirectToAction("Index", "ViTriViecLam");
        }

        [HttpPost("ViTriViecLam/Edit")]
        public JsonResult Edit(int id)
        {
            var model = _db.ViTriViecLam.FirstOrDefault(p => p.Id == id);

            if (model != null)
            {
                string result = "<div class='modal-body' id='edit_thongtin'>";

                result += "<div class='row text-left'>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Mã danh mục</b></label>";
                result += "<input type='number' id='MaViTriViecLam_edit' name='MaViTriViecLam_edit' value='" + model.MaViTriViecLam + "' class='form-control'/>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Tên danh mục</b></label>";
                result += "<input type='text' id='TenViTriViecLam_edit' name='TenViTriViecLam_edit' value='" + model.TenViTriViecLam + "' class='form-control'/>";
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

        [HttpPost("ViTriViecLam/Update")]
        public IActionResult Update(string TenViTriViecLam_edit, string MaViTriViecLam_edit, int Id_edit, string TrangThai_edit)
        {
            var model = _db.ViTriViecLam.FirstOrDefault(t => t.Id == Id_edit);
            if (model != null)
            {
                model.MaViTriViecLam = MaViTriViecLam_edit;
                model.TenViTriViecLam = TenViTriViecLam_edit;
                model.TrangThai = TrangThai_edit;
                model.Updated_at = DateTime.Now;
                _db.ViTriViecLam.Update(model);
                _db.SaveChanges();
            }

            return RedirectToAction("Index", "ViTriViecLam");
        }

        [HttpPost("ViTriViecLam/Delete")]
        public IActionResult Delete(int Id_delete)
        {
            var model = _db.ViTriViecLam.FirstOrDefault(t => t.Id == Id_delete);
            if (model != null)
            {
                _db.ViTriViecLam.Remove(model);
                _db.SaveChanges();
            }
            return RedirectToAction("Index", "ViTriViecLam");
        }
    }
}
