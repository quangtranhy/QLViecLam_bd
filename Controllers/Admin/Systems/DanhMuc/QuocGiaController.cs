using Microsoft.AspNetCore.Mvc;
using QLViecLam.Data;
using QLViecLam.Models.Admin.Systems.DanhMuc;
namespace QLViecLam.Controllers.Admin.Systems.DanhMuc
{
    public class QuocGiaController : Controller
    {
        private readonly ApplicationDbContext _db;

        public QuocGiaController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet("QuocGia")]
        public IActionResult Index()
        {
            var model = _db.QuocGia;
            var stt = model.AsEnumerable().Select(e => int.Parse(e.MaQuocGia!)).DefaultIfEmpty(0).Max();

            ViewData["stt"] = stt != 0 ? stt + 1 : 1;
            ViewData["Title"] = "Quốc gia";
            ViewData["MenuLv1"] = "hethong";
            ViewData["MenuLv2"] = "hethong_danhmuc";
            ViewData["MenuLv3"] = "hethong_danhmuc_";
            return View("Views/Admin/Systems/Danhmuc/QuocGia/Index.cshtml", model);
        }

        [HttpPost("QuocGia/Store")]
        public IActionResult Store(string TenQuocGia_create, string MaQuocGia_create, string TrangThai_create)
        {
            var model = new QuocGia
            {
                TenQuocGia = TenQuocGia_create,
                MaQuocGia = MaQuocGia_create,
                TrangThai = TrangThai_create,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now,
            };
            _db.QuocGia.Add(model);
            _db.SaveChanges();
            return RedirectToAction("Index", "QuocGia");
        }

        [HttpPost("QuocGia/Edit")]
        public JsonResult Edit(int id)
        {
            var model = _db.QuocGia.FirstOrDefault(p => p.Id == id);

            if (model != null)
            {
                string result = "<div class='modal-body' id='edit_thongtin'>";

                result += "<div class='row text-left'>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Mã danh mục</b></label>";
                result += "<input type='text' id='MaQuocGia_edit' name='MaQuocGia_edit' value='" + model.MaQuocGia + "' class='form-control'/>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Tên danh mục</b></label>";
                result += "<input type='text' id='TenQuocGia_edit' name='TenQuocGia_edit' value='" + model.TenQuocGia + "' class='form-control'/>";
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

        [HttpPost("QuocGia/Update")]
        public IActionResult Update(string TenQuocGia_edit, string MaQuocGia_edit, int Id_edit, string TrangThai_edit)
        {
            var model = _db.QuocGia.FirstOrDefault(t => t.Id == Id_edit);
            if (model != null)
            {
                model.MaQuocGia = MaQuocGia_edit;
                model.TenQuocGia = TenQuocGia_edit;
                model.TrangThai = TrangThai_edit;
                model.Updated_at = DateTime.Now;
                _db.QuocGia.Update(model);
                _db.SaveChanges();
            }

            return RedirectToAction("Index", "QuocGia");
        }

        [HttpPost("QuocGia/Delete")]
        public IActionResult Delete(int Id_delete)
        {
            var model = _db.QuocGia.FirstOrDefault(t => t.Id == Id_delete);
            if (model != null)
            {
                _db.QuocGia.Remove(model);
                _db.SaveChanges();
            }
            return RedirectToAction("Index", "QuocGia");
        }
    }
}
