using Microsoft.AspNetCore.Mvc;
using QLViecLam.Data;

namespace QLViecLam.Controllers.Admin.Systems.DanhMuc.QuocGia
{
    public class QuocGiaController : Controller
    {
        private readonly ApplicationDbContext _db;

        public QuocGiaController(ApplicationDbContext db)
        {
            _db = db;
        }

        [Route("QuocGia")]
        [HttpGet]
        public IActionResult Index()
        {
            var model = _db.QuocGia;
            //var lastRecord = _db.QuocGia.OrderByDescending(e => e.MaQuocGia).FirstOrDefault();
            //ViewData["stt"] = lastRecord != null ? lastRecord.MaQuocGia : 1;

            ViewData["MenuLv1"] = "danhmuc";
            ViewData["MenuLv2"] = "danhmuc_quocgia";
            return View("Views/Admin/Systems/Danhmuc/QuocGia/Index.cshtml", model);
        }
        [Route("QuocGia/Store")]
        [HttpPost]
        public IActionResult Store(string TenQuocGia_create, string MaQuocGia_create, string TrangThai_create)
        {
            var model = new Models.Admin.Systems.DanhMuc.QuocGia
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
        [Route("QuocGia/Edit")]
        [HttpPost]
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
                result += "<option value='kh'>Kích hoạt</option>";
                result += "<option value='kkh'>Không kích hoạt</option>";
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
        [Route("QuocGia/Update")]
        [HttpPost]
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
        [Route("QuocGia/Delete")]
        [HttpPost]
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
