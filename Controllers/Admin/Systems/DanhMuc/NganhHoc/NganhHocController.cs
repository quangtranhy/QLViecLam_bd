using Microsoft.AspNetCore.Mvc;
using QLViecLam.Data;

namespace QLViecLam.Controllers.Admin.Systems.DanhMuc.NganhHoc
{
    public class NganhHocController : Controller
    {
        private readonly ApplicationDbContext _db;

        public NganhHocController(ApplicationDbContext db)
        {
            _db = db;
        }

        [Route("NganhHoc")]
        [HttpGet]
        public IActionResult Index()
        {
            var model = _db.NganhHoc;
            //var lastRecord = _db.NganhHoc.OrderByDescending(e => e.MaNganhHoc).FirstOrDefault();
            //ViewData["stt"] = lastRecord != null ? lastRecord.MaNganhHoc : 1;

            ViewData["MenuLv1"] = "danhmuc";
            ViewData["MenuLv2"] = "danhmuc_nganhhoc";
            return View("Views/Admin/Systems/Danhmuc/NganhHoc/Index.cshtml", model);
        }
        [Route("NganhHoc/Store")]
        [HttpPost]
        public IActionResult Store(string TenNganhHoc_create, string MaNganhHoc_create, string TrangThai_create)
        {
            var model = new Models.Admin.Systems.DanhMuc.NganhHoc
            {
                TenNganhHoc = TenNganhHoc_create,
                MaNganhHoc = MaNganhHoc_create,
                TrangThai = TrangThai_create,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now,
            };
            _db.NganhHoc.Add(model);
            _db.SaveChanges();
            return RedirectToAction("Index", "NganhHoc");
        }
        [Route("NganhHoc/Edit")]
        [HttpPost]
        public JsonResult Edit(int id)
        {
            var model = _db.NganhHoc.FirstOrDefault(p => p.Id == id);

            if (model != null)
            {
                string result = "<div class='modal-body' id='edit_thongtin'>";

                result += "<div class='row text-left'>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Mã danh mục</b></label>";
                result += "<input type='text' id='MaNganhHoc_edit' name='MaNganhHoc_edit' value='" + model.MaNganhHoc + "' class='form-control'/>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Tên danh mục</b></label>";
                result += "<input type='text' id='TenNganhHoc_edit' name='TenNganhHoc_edit' value='" + model.TenNganhHoc + "' class='form-control'/>";
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
        [Route("NganhHoc/Update")]
        [HttpPost]
        public IActionResult Update(string TenNganhHoc_edit, string MaNganhHoc_edit, int Id_edit, string TrangThai_edit)
        {
            var model = _db.NganhHoc.FirstOrDefault(t => t.Id == Id_edit);
            if (model != null)
            {
                model.MaNganhHoc = MaNganhHoc_edit;
                model.TenNganhHoc = TenNganhHoc_edit;
                model.TrangThai = TrangThai_edit;
                model.Updated_at = DateTime.Now;
                _db.NganhHoc.Update(model);
                _db.SaveChanges();
            }

            return RedirectToAction("Index", "NganhHoc");
        }
        [Route("NganhHoc/Delete")]
        [HttpPost]
        public IActionResult Delete(int Id_delete)
        {
            var model = _db.NganhHoc.FirstOrDefault(t => t.Id == Id_delete);
            if (model != null)
            {
                _db.NganhHoc.Remove(model);
                _db.SaveChanges();
            }
            return RedirectToAction("Index", "NganhHoc");
        }
    }
}
