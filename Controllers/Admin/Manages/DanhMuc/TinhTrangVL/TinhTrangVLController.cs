using Microsoft.AspNetCore.Mvc;
using QLViecLam.Data;

namespace QLViecLam.Controllers.Admin.Manages.DanhMuc.TinhTrangVL
{
    public class TinhTrangVLController : Controller
    {
        private readonly ApplicationDbContext _db;

        public TinhTrangVLController(ApplicationDbContext db)
        {
            _db = db;
        }

        [Route("TinhTrangVL")]
        [HttpGet]
        public IActionResult Index()
        {
            var model = _db.TinhTrangVL;
            var lastRecord = _db.TinhTrangVL.OrderByDescending(e => e.MaTinhTrangVL).FirstOrDefault();
            ViewData["stt"] = lastRecord != null ? lastRecord.MaTinhTrangVL : 1;

            ViewData["MenuLv1"] = "menu_quanlydanhmucdulieu";
            ViewData["MenuLv2"] = "menu_quanlydanhmuc_tinhtrangvl";
            return View("Views/Admin/Manages/Danhmuc/TinhTrangVL/Index.cshtml", model);
        }
        [Route("TinhTrangVL/Store")]
        [HttpPost]
        public IActionResult Store(string TenTinhTrangVL_create, int MaTinhTrangVL_create, string TrangThai_create)
        {
            var model = new Models.Admin.Manages.DanhMuc.TinhTrangVL
            {
                TenTinhTrangVL = TenTinhTrangVL_create,
                MaTinhTrangVL = MaTinhTrangVL_create,
                TrangThai = TrangThai_create,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now,
            };
            _db.TinhTrangVL.Add(model);
            _db.SaveChanges();
            return RedirectToAction("Index", "TinhTrangVL");
        }
        [Route("TinhTrangVL/Edit")]
        [HttpPost]
        public JsonResult Edit(int id)
        {
            var model = _db.TinhTrangVL.FirstOrDefault(p => p.Id == id);

            if (model != null)
            {
                string result = "<div class='modal-body' id='edit_thongtin'>";

                result += "<div class='row text-left'>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Mã danh mục</b></label>";
                result += "<input type='number' id='MaTinhTrangVL_edit' name='MaTinhTrangVL_edit' value='" + model.MaTinhTrangVL + "' class='form-control'/>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Tên danh mục</b></label>";
                result += "<input type='text' id='TenTinhTrangVL_edit' name='TenTinhTrangVL_edit' value='" + model.TenTinhTrangVL + "' class='form-control'/>";
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
        [Route("TinhTrangVL/Update")]
        [HttpPost]
        public IActionResult Update(string TenTinhTrangVL_edit, int MaTinhTrangVL_edit, int Id_edit, string TrangThai_edit)
        {
            var model = _db.TinhTrangVL.FirstOrDefault(t => t.Id == Id_edit);
            if (model != null)
            {
                model.MaTinhTrangVL = MaTinhTrangVL_edit;
                model.TenTinhTrangVL = TenTinhTrangVL_edit;
                model.TrangThai = TrangThai_edit;
                model.Updated_at = DateTime.Now;
                _db.TinhTrangVL.Update(model);
                _db.SaveChanges();
            }

            return RedirectToAction("Index", "TinhTrangVL");
        }
        [Route("TinhTrangVL/Delete")]
        [HttpPost]
        public IActionResult Delete(int Id_delete)
        {
            var model = _db.TinhTrangVL.FirstOrDefault(t => t.Id == Id_delete);
            if (model != null)
            {
                _db.TinhTrangVL.Remove(model);
                _db.SaveChanges();
            }
            return RedirectToAction("Index", "TinhTrangVL");
        }
    }
}
