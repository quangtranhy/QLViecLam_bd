using Microsoft.AspNetCore.Mvc;
using QLViecLam.Data;

namespace QLViecLam.Controllers.Admin.Systems.DanhMuc.DanToc
{
    public class DanTocController : Controller
    {
        private readonly ApplicationDbContext _db;

        public DanTocController(ApplicationDbContext db)
        {
            _db = db;
        }

        [Route("DanToc")]
        [HttpGet]
        public IActionResult Index()
        {
            var model = _db.DanToc;
            var lastRecord = _db.DanToc.OrderByDescending(e => e.MaDanToc).FirstOrDefault();
            ViewData["stt"] = lastRecord != null ? lastRecord.MaDanToc : 1;

            ViewData["MenuLv1"] = "danhmuc";
            ViewData["MenuLv2"] = "danhmuc_dantoc";
            return View("Views/Admin/Systems/Danhmuc/DanToc/Index.cshtml", model);
        }
        [Route("DanToc/Store")]
        [HttpPost]
        public IActionResult Store(string TenDanToc_create, int MaDanToc_create, string TrangThai_create)
        {
            var model = new Models.Admin.Systems.DanhMuc.DanToc
            {
                TenDanToc = TenDanToc_create,
                MaDanToc = MaDanToc_create,
                TrangThai = TrangThai_create,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now,
            };
            _db.DanToc.Add(model);
            _db.SaveChanges();
            return RedirectToAction("Index", "DanToc");
        }
        [Route("DanToc/Edit")]
        [HttpPost]
        public JsonResult Edit(int id)
        {
            var model = _db.DanToc.FirstOrDefault(p => p.Id == id);

            if (model != null)
            {
                string result = "<div class='modal-body' id='edit_thongtin'>";

                result += "<div class='row text-left'>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Mã danh mục</b></label>";
                result += "<input type='number' id='MaDanToc_edit' name='MaDanToc_edit' value='" + model.MaDanToc + "' class='form-control'/>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Tên danh mục</b></label>";
                result += "<input type='text' id='TenDanToc_edit' name='TenDanToc_edit' value='" + model.TenDanToc + "' class='form-control'/>";
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
        [Route("DanToc/Update")]
        [HttpPost]
        public IActionResult Update(string TenDanToc_edit, int MaDanToc_edit, int Id_edit, string TrangThai_edit)
        {
            var model = _db.DanToc.FirstOrDefault(t => t.Id == Id_edit);
            if (model != null)
            {
                model.MaDanToc = MaDanToc_edit;
                model.TenDanToc = TenDanToc_edit;
                model.TrangThai = TrangThai_edit;
                model.Updated_at = DateTime.Now;
                _db.DanToc.Update(model);
                _db.SaveChanges();
            }

            return RedirectToAction("Index", "DanToc");
        }
        [Route("DanToc/Delete")]
        [HttpPost]
        public IActionResult Delete(int Id_delete)
        {
            var model = _db.DanToc.FirstOrDefault(t => t.Id == Id_delete);
            if (model != null)
            {
                _db.DanToc.Remove(model);
                _db.SaveChanges();
            }
            return RedirectToAction("Index", "DanToc");
        }
    }
}
